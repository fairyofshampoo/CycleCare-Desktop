using Grpc.Net.Client;
using System;
using Google.Protobuf;
using System.Threading.Tasks;
using System.IO;
using static CycleCare.Protos.VideoService;
using CycleCare.Protos;
using Grpc.Core;
using CycleCare.Utilities;

namespace CycleCare.GrpcClients
{
    internal class GrpcVideoClient
    {
        private VideoServiceClient _client;
        private GrpcChannel _channel = GrpcChannel.ForAddress(Properties.Resources.GRPC_URL);

        public GrpcVideoClient()
        {
            _client = new VideoServiceClient(_channel);
        }

        public async Task DownloadVideo(string fileName)
        {
            try
            {
                using (var call = _client.streamVideo(new StreamVideoRequest
                {
                    Filename = fileName
                }))
                {
                    using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        await foreach (var chunk in call.ResponseStream.ReadAllAsync())
                        {
                            fileStream.Write(chunk.Data.ToByteArray(), 0, chunk.Data.Length);
                        }
                    }
                }
            }
            catch (RpcException e)
            {
                DialogManager.ShowErrorMessageBox("Error al descargar video");
                Console.WriteLine(e);
            }
            finally
            {
                await _channel.ShutdownAsync();
            }
        }

    }
}
