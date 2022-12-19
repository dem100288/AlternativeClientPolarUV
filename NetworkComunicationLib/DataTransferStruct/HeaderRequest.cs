using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
{
    public class HeaderRequest : HeaderBase
    {
        public TypeEnum Type;

        public HeaderRequest(byte[] bytes) : base(bytes) { }

        protected override void ReadStream(BinaryReader reader)
        {

            Type = (TypeEnum)reader.ReadInt32();
        }

        public HeaderRequest(TypeEnum type, int endpoint, int length) : base(endpoint, length)
        {
            Type = type;
        }

        protected override void WriteStream(BinaryWriter writer)
        {
            writer.Write((int)Type);
        }

    }
}
