using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SourceProtocol
{
    public class Request
    {
        
        public static byte[] A2S_INFO_REQUEST =  { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
        public static byte[] A2S_SERVERQUERY_GETCHALLENGE_REQUEST = { 0xFF, 0xFF, 0xFF, 0xFF, 0x55, 0xFF, 0xFF, 0xFF, 0xFF};
        public static byte[] A2S_PLAYER_HEADER_PART_REQUEST= { 0xFF, 0xFF, 0xFF, 0xFF, 0x55 };

        public static async System.Threading.Tasks.Task<byte[]> Send(string host, int port, byte[] request)
        {
            var listenPort = 56800;
            var receiver = new UdpSocketReceiver();

            TaskCompletionSource<byte[]> responseReceived = new TaskCompletionSource<byte[]>();

            receiver.MessageReceived += (sender, args) =>
            {
                responseReceived.SetResult(args.ByteData);
                receiver.StopListeningAsync();
            };

            //var client = new UdpSocketClient();            

            await receiver.StartListeningAsync(listenPort);            

            await receiver.SendToAsync(request, host, port);
            // listen for udp traffic on listenPort
            await responseReceived.Task;



            return responseReceived.Task.Result;
        }
    }
}
