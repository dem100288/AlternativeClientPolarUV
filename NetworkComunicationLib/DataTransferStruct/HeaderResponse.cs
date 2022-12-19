using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib.DataTransferStruct
{
    public class HeaderResponse : HeaderBase
    {


        public CodeEnum Code;

        public HeaderResponse(byte[] bytes) : base(bytes) { }

        protected override void ReadStream(BinaryReader reader)
        {

            Code = (CodeEnum)reader.ReadInt32();
        }

        public HeaderResponse(CodeEnum code, int endpoint, int length) : base(endpoint, length)
        {
            Code = code;
        }

        protected override void WriteStream(BinaryWriter writer)
        {
            writer.Write((int)Code);
        }

    }
}
