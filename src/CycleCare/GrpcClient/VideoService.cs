using Grpc.Net.Client;
using System;
using Google.Protobuf;
using System.Threading.Tasks;
using Grpc.Core;
using System.IO;

namespace CycleCare.GrpcClient
{
    public class VideoService
    {
        //private CycleService Client;
        //private GrpcChannel Channel = GrpcChannel.ForAddress(Properties.Resources.GRPC_URL);

        public VideoService()
        {
            //Client = new CycleService(Channel);
        }
    }
}