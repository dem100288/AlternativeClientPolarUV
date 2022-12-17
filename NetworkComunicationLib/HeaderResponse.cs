using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    [Serializable]
    public class HeaderResponse : ISerializable
    {
        [NonSerialized]
        public static readonly int HEADER_LENGTH = 12;

        public CodeEnum Code;

        public int EndpointId;

        public int Length;

        private HeaderResponse() { }
        public HeaderResponse(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            Code = (CodeEnum?)info.GetValue(nameof(Code), typeof(CodeEnum)) ?? CodeEnum.ConnectionError;
            EndpointId = (int?)info.GetValue(nameof(EndpointId), typeof(int)) ?? 0;
            Length = (int?)info.GetValue(nameof(Length), typeof(int)) ?? 0;
        }

        public HeaderResponse(CodeEnum code, int endpoint, int length) {
            Code = code;
            EndpointId = endpoint;
            Length = length;
            //TODO Нормальная сериализация заголовка
            List<byte> data = new List<byte>();
            data.AddRange(BitConverter.GetBytes((int)code));
            data.AddRange(BitConverter.GetBytes(EndpointId));
            data.AddRange(BitConverter.GetBytes(Length));
            _byte = data.ToArray();

        }

        public static HeaderResponse? FromBytes(byte[] bytes)
        {
            if (bytes.Length == HEADER_LENGTH)
            {
                HeaderResponse response = new HeaderResponse()
                {
                    Code = (CodeEnum)BitConverter.ToInt32(bytes, 0),
                    EndpointId = BitConverter.ToInt32(bytes, 4),
                    Length = BitConverter.ToInt32(bytes, 8),
                };
                return response;
            }
            return null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Code), Code);
            info.AddValue(nameof(EndpointId), EndpointId);
            info.AddValue(nameof(Length), Length);
        }

        [NonSerialized]
        private byte[] _byte = new byte[0];
        public byte[] Bytes => _byte;
        public int BytesLength => _byte.Length;

    }
}
