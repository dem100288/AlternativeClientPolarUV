using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkComunicationLib.DataTransferStruct;

namespace NetworkComunicationLib
{
    public class Response
    {
        public static explicit operator bool(Response response) => response.Header.Code == CodeEnum.Ok || response.Header.Code == CodeEnum.NoContent;

        private byte[] _bytes;
        private byte[] _bytes_decompresed;
        public byte[] DataBytes => _bytes;
        public byte[] DataDecompresedBytes => _bytes_decompresed;

        private HeaderResponse Header;
        public Response(CodeEnum code, int endpoint, byte[] data) : this(new HeaderResponse(code, endpoint, (int)data.Length), data) { }

        public Response(HeaderResponse header, byte[] data)
        {
            _bytes = data;
            _bytes_decompresed = LANController.Decompress(data);
            Header = header;
        }

        public Response(Response response) {
            _bytes = response._bytes;
            _bytes_decompresed = response._bytes_decompresed;
            Header = response.Header;
        }
    }
}
