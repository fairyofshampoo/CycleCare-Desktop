using CycleCare.Models;
using CycleCare.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CycleCare.Views;

namespace CycleCare.Service
{
    public class CycleService
    {
        private static readonly string TOKEN = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["TOKEN"].Value;
        private static readonly string URL = string.Concat(Properties.Resources.BASE_URL, "logs/");

        public static async Task<Response> GetCycleLogsByUser(int month, int year)
        {
            Response response = new Response();
            using (var httpClient = new HttpClient())
            {
                string urlWithParam = string.Concat(URL, "user-cycle-logs/", Uri.EscapeDataString(month.ToString()), "/", Uri.EscapeDataString(year.ToString()));
                try
                {
                    httpClient.DefaultRequestHeaders.Add("token", TOKEN);

                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(urlWithParam),
                    };

                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    if (httpResponseMessage != null)
                    {
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            string json = await httpResponseMessage.Content.ReadAsStringAsync();
                            response = JsonConvert.DeserializeObject<Response>(json);
                        }
                        response.Code = (int)httpResponseMessage.StatusCode;
                    }
                    else
                    {
                        response.Code = (int)HttpStatusCode.InternalServerError;
                        response.Details = "No se recibió respuesta del servidor.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error de red: {ex.Message}";
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (JsonException ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error al procesar la respuesta JSON: {ex.Message}";
                    Console.WriteLine(ex.Message);
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (Exception ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error inesperado: {ex.Message}";

                    Console.WriteLine(ex.Message);
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
            }
            return response;
        }

        public static async Task<Response> CreateCycleLog(CycleLog cycleLog)
        {
            Response response = new Response();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("token", TOKEN);

                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(URL),
                        Content = new StringContent(JsonConvert.SerializeObject(cycleLog), Encoding.UTF8, "application/json")
                    };

                    HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    if (httpResponseMessage != null)
                    {
                        if (httpResponseMessage.IsSuccessStatusCode)
                        {
                            string json = await httpResponseMessage.Content.ReadAsStringAsync();
                            response = JsonConvert.DeserializeObject<Response>(json);
                        }
                        response.Code = (int)httpResponseMessage.StatusCode;
                    }
                    else
                    {
                        response.Code = (int)HttpStatusCode.InternalServerError;
                        response.Details = "No se recibió respuesta del servidor.";
                    }
                }
                catch (HttpRequestException ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error de red: {ex.Message}";
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (JsonException ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error al procesar la respuesta JSON: {ex.Message}";
                    Console.WriteLine(ex.Message);
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (Exception ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error inesperado: {ex.Message}";

                    Console.WriteLine(ex.Message);
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
            }
            return response;
        }
    }
}
