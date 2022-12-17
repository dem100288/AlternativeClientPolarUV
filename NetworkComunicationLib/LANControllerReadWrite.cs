using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace NetworkComunicationLib
{
    public partial class LANController
    {
        public Response SendReadRequest(int endpoint)
        {
            return SendRequest(endpoint, TypeEnum.WR, "");
        }

        public Response SendWriteRequest(int endpoint, string data)
        {
            return SendRequest(endpoint, TypeEnum.W, data);
        }

        public Response SendReadWriteRequest(int endpoint, string data)
        {
            return SendRequest(endpoint, TypeEnum.WR, data);
        }

        private Response SendRequest(int endpoint, TypeEnum type, string data)
        {
            return SendRequest(new Request(data, type, endpoint));
        }

        private Response SendRequest(Request request)
        {
            return TransferData(request) ?? new Response("", CodeEnum.ConnectionError, request.Endpoint);
        }

        private Response? TransferData(Request request)
        {
            lock (_lockSocket)
            {
                _client.Client.Send(request.DataCompresedBytes, 0, request.DataCompresedBytes.Length, SocketFlags.None);
                byte[] res_data = new byte[256];
                var c_bytes = _client.Client.Receive(res_data, HeaderResponse.HEADER_LENGTH, SocketFlags.None);
            }
            return null;
        }
    }
}
