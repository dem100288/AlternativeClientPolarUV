using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkComunicationLib
{
    public class Request
    {
        private HeaderRequest Header;
        private string _data;
        private byte[] _bytes;
        private byte[] _bytes_compresed;
        public int Endpoint => Header.EndpointId;
        public string DataView => _data;
        public byte[] DataBytes => _bytes;
        public byte[] DataCompresedBytes => _bytes_compresed;
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
                _bytes_compresed = LANController.Compress(_bytes);
            }
        }

        public Request(string data, TypeEnum type, int endpoint)
        {
            Data = data;
            Header = new HeaderRequest(type, endpoint, type == TypeEnum.R ? 0 : (int)data.Length);
        }
    }
}
