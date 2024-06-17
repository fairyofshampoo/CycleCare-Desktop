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

        public async Task DownloadVideo(string fileName, Action<string> onStreamStarted = null)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                using (var call = _client.streamVideo(new StreamVideoRequest
                {
                    Filename = fileName
                }))
                {
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        onStreamStarted?.Invoke(tempFilePath);

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

        public async Task UploadVideo(string fileName, byte[] data)
        {
            try
            {
                using (var call = _client.uploadVideo())
                {
                    await call.RequestStream.WriteAsync(new VideoChunkResponse
                    {
                        Data = ByteString.CopyFrom(data),
                        Filename = fileName
                    });

                    await call.RequestStream.CompleteAsync();
                }
            }
            catch (RpcException e)
            {
                Console.WriteLine($"Error al subir el archivo: {e}");
            }
            finally
            {
                _channel.ShutdownAsync().Wait();
            }
        }

    }
}
