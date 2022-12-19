using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using NetworkComunicationLib.DataTransferStruct;

namespace NetworkComunicationLib
{
    public partial class LANController
    {
        private readonly int EndPointTelemetry = 1; 
        private readonly int EndPointCamera = 2;
        private readonly int EndPointControl = 3;
        public SensorData ReadTelemetry()
        {
            return SensorData.GetDataFromBytes(SendReadRequest(EndPointTelemetry).DataDecompresedBytes);
        }

        public void WriteCameraSettings(CameraSettings cam_settings)
        {
            var resp = SendWriteRequest(EndPointCamera, cam_settings);
        }

        public void WriteControl(DataControl control)
        {
            var resp = SendWriteRequest(EndPointControl, control);
        }

        public Response SendReadRequest(int endpoint)
        {
            return SendRequest(endpoint, TypeEnum.R, ISerilizeToByteArray.ZeroDataObject);
        }

        public Response SendWriteRequest(int endpoint, ISerilizeToByteArray data)
        {
            return SendRequest(endpoint, TypeEnum.W, data);
        }

        public Response SendReadWriteRequest(int endpoint, ISerilizeToByteArray data)
        {
            return SendRequest(endpoint, TypeEnum.WR, data);
        }

        private Response SendRequest(int endpoint, TypeEnum type, ISerilizeToByteArray data)
        {
            return SendRequest(new Request(data, type, endpoint));
        }

        private Response SendRequest(Request request)
        {
            return TransferData(request) ?? new Response(CodeEnum.ConnectionError, request.Endpoint, new byte[0]);
        }

        private Response? TransferData(Request request)
        {
            lock (_lockSocket)
            {
                var header_bytes = request.HeadBytes;
                _client.Client.Send(header_bytes, 0, header_bytes.Length, SocketFlags.None);
                _client.Client.Send(request.DataCompresedBytes, 0, request.DataCompresedBytes.Length, SocketFlags.None);
                var response = GetResponse(GetHeaderResponse());
                return response;
            }
        }

        private HeaderResponse GetHeaderResponse()
        {
            while (_client.Available < HeaderResponse.HEADER_LENGTH) ;
            byte[] res_data = new byte[HeaderResponse.HEADER_LENGTH];
            var c_bytes = _client.Client.Receive(res_data, HeaderResponse.HEADER_LENGTH, SocketFlags.None);
            return new HeaderResponse(res_data);
        }

        private Response GetResponse(HeaderResponse header)
        {
            while (_client.Available < header.Length) ;
            byte[] res_data = new byte[header.Length];
            var c_bytes = _client.Client.Receive(res_data, header.Length, SocketFlags.None);
            return new Response(header, res_data);
        }
    }
}
