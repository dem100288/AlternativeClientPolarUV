using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using ZstdNet;

namespace NetworkComunicationLib
{
    public partial class LANController
    {
        #region Static Compress Decompress

        private static readonly int COMPRESS_LVL = 22;

        public static byte[] Compress(byte[] data)
        {
            return new Compressor(new CompressionOptions(COMPRESS_LVL)).Wrap(data);
        }

        public static byte[] Decompress(byte[] data)
        {
            return new Decompressor().Unwrap(data);
        }

        #endregion

        private IPAddress? _ipAddress;
        private int _port = 0;
        public bool ParamValid => _ipAddress is not null && _ipAddress != IPAddress.None && _port != 0;
        private TcpClient? _client = null;
        private object _lockSocket = new object();
        public bool Connected => _client?.Connected ?? false;

        public LANController(IPAddress ip, int port = 2022)
        {
            _ipAddress = ip;
            _port = port;
        }

        public LANController(string ip = "192.168.1.25", int port = 2022)
        {
            IPAddress.TryParse(ip, out _ipAddress);
            _port = port;
        }

        public async Task<bool> Connect()
        {
            if (!Connected && ParamValid)
            {
                _client = new TcpClient();
                
                await _client.ConnectAsync(_ipAddress ?? IPAddress.None, _port);
                return _client.Connected;
            }
            return false;
        }
    }
}
