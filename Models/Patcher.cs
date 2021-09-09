using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
namespace IPSSharp.Models
{
    internal struct StandardRecord
    {
        public int offset;
        public ushort size;
        public byte[] data;
    }

    internal struct RLERecord
    {
        public int offset;
        public ushort times;
        public byte[] data;
    }
    
    public class Patcher
    {
        public string? romFile, ipsFile, outputFile;
        public bool overwrite;
        private readonly List<StandardRecord> standardRecords;
        private readonly List<RLERecord> rleRecords;

        public Patcher()
        {
            standardRecords = new List<StandardRecord>();
            rleRecords = new List<RLERecord>();
        }
        
        private static bool SetsContainSameElements<T>(IEnumerable<T> set1, IEnumerable<T> set2) {
            var setXOR = new HashSet<T>(set1);
            setXOR.SymmetricExceptWith(set2);
            return setXOR.Count == 0;
        }

        private void ReadRecords()
        {
            standardRecords.Clear();
            rleRecords.Clear();
            
            var binaryReader = new BinaryReader(File.Open(ipsFile ?? throw new InvalidOperationException(), FileMode.Open));
            
            byte[] header = binaryReader.ReadBytes(5);
            byte[] EOF = {
                0x45, 0x4f, 0x46
            };

            if (System.Text.Encoding.UTF8.GetString(header) != "PATCH")
            {
                throw new Exception("Input was not IPS file or IPS file was malformed");
            }
            
            for (;;)
            {
                byte[] offset = binaryReader.ReadBytes(3);
                
                if (SetsContainSameElements(offset, EOF))
                {
                    break;
                }
                
                byte[] size = binaryReader.ReadBytes(2);
                int offset_int = offset[0] << 16 | offset[1] << 8 | offset[2];
                ushort size_ushort = BinaryPrimitives.ReadUInt16BigEndian(size);
                
                if (size_ushort == 0)
                {
                    byte[] times = binaryReader.ReadBytes(2);
                    byte[] data = binaryReader.ReadBytes(1);
                    rleRecords.Add(new RLERecord
                    {
                        offset = offset_int,
                        times = BinaryPrimitives.ReadUInt16BigEndian(times),
                        data = data
                    });
                }
                else
                {
                    byte[] data = binaryReader.ReadBytes(BinaryPrimitives.ReadUInt16BigEndian(size));
                    standardRecords.Add(new StandardRecord
                    {
                        offset = offset_int,
                        size = size_ushort,
                        data = data
                    });
                }
                

            }
        }

        public void Patch()
        {
            ReadRecords();
            
            MemoryStream romMemCopy = new();
            using (FileStream fs = File.OpenRead(romFile ?? throw new InvalidOperationException()))
            {
                fs.CopyTo(romMemCopy);
            }

            foreach (RLERecord rleRecord in rleRecords)
            {
                for (var i = 0; i < rleRecord.times; i++)
                {
                    romMemCopy.Seek(rleRecord.offset + i, SeekOrigin.Begin);
                    romMemCopy.Write(rleRecord.data, 0, 1);
                }
            }
            
            foreach (StandardRecord record in standardRecords)
            {
                romMemCopy.Seek(record.offset, SeekOrigin.Begin);
                romMemCopy.Write(record.data, 0, record.size);
            }

            if (overwrite)
            {
                romMemCopy.WriteTo(File.Create(romFile ?? throw new InvalidOperationException()));
            }
            else
            {
                romMemCopy.WriteTo(File.Create(outputFile ?? throw new InvalidOperationException()));
            }
        }
    }
}
