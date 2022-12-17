using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    public struct HeaderRequest
    {
        public static readonly int HEADER_LENGTH = 12;
        public TypeEnum Type;
        public int EndpointId;
        public int Length;

        public HeaderRequest(TypeEnum type, int endpointId, int length)
        {
            Type = type;
            EndpointId = endpointId;
            Length = length;
            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes((int)type));
            data.AddRange(BitConverter.GetBytes(EndpointId));
            data.AddRange(BitConverter.GetBytes(Length));
            _byte = data.ToArray();
        }

        private byte[] _byte = new byte[0];

        public byte[] Bytes => _byte;
        public int BytesLength => _byte.Length;
    }
}
