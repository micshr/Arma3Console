using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceProtocol
{
    class SourceByteReader
    {
        byte[] _data = null;
        int pos = 0;
        public SourceByteReader(byte[] data)
        {
            _data = data;
        }
        public byte ReadByte()
        {
            return _data[pos++];
        }

        public byte[] ReadBytes(int count)
        {
            byte[] result = new byte[count];
            Array.Copy(_data, pos, result, 0, count);

            pos += count;

            return result;
        }

        public string ReadString()
        {
            List<byte> result = new List<byte>();

            while (_data[pos] != '\0')
            {
                result.Add(_data[pos++]);
            }

            pos++; // skip null termination

            return Encoding.UTF8.GetString(result.ToArray(), 0, result.Count);
        }

    }
}
