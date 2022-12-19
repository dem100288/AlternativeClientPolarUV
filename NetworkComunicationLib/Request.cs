using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkComunicationLib.DataTransferStruct;

namespace NetworkComunicationLib
{
    public class Request
    {
        private HeaderRequest Header;
        private ISerilizeToByteArray DataObject;
        private byte[] _bytes;
        private byte[] _bytes_compresed;
        public int Endpoint => Header.EndpointId;
        public byte[] DataBytes => _bytes;
        public byte[] DataCompresedBytes => _bytes_compresed;
        public byte[] HeadBytes => Header.ToArray();

        public Request(ISerilizeToByteArray data, TypeEnum type, int endpoint)
        {
            DataObject = data;
            _bytes = data.ToArray();
            _bytes_compresed = LANController.Compress(_bytes);
            Header = new HeaderRequest(type, endpoint, type == TypeEnum.R ? 0 : _bytes_compresed.Length);
        }
    }
}
