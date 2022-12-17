using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    public class Response
    {
        public static explicit operator bool(Response response) => response.Header.Code == CodeEnum.Ok || response.Header.Code == CodeEnum.NoContent;

        private string _data;
        private byte[] _bytes;
        private byte[] _bytes_decompresed;
        public string DataView => _data;
        public byte[] DataBytes => _bytes;
        public byte[] DataDecompresedBytes => _bytes_decompresed;
        private string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                _bytes = Encoding.UTF8.GetBytes(value);
                _bytes_decompresed = LANController.Decompress(_bytes);
            }
        }
        private HeaderResponse Header;
        public Response(string data, CodeEnum code, int endpoint) {
            Data = data;
            Header = new HeaderResponse(code, endpoint, (int)data.Length);
        }
        public Response(Response response) { 
            Data = response.Data;
            Header = response.Header;
        }
    }
}
