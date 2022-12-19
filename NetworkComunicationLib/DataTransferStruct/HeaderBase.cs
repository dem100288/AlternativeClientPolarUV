using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
{
    public class HeaderBase : ISerilizeToByteArray
    {
        public static readonly int HEADER_LENGTH = 12;
        public int EndpointId;
        public int Length;

        protected HeaderBase(byte[] bytes)
        {
            var reader = new BinaryReader(new MemoryStream(bytes));
            ReadStream(reader);
            EndpointId = reader.ReadInt32();
            Length = reader.ReadInt32();
        }

        public HeaderBase(int endpoint, int length)
        {
            EndpointId = endpoint;
            Length = length;
        }

        protected virtual void ReadStream(BinaryReader reader) { }

        public byte[] ToArray()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            WriteStream(writer);
            writer.Write(EndpointId);
            writer.Write(Length);

            return stream.ToArray();
        }

        protected virtual void WriteStream(BinaryWriter writer) { }
    }
}
