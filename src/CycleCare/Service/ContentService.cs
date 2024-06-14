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

namespace CycleCare.Service
{
    public class ContentService
    {
        private static readonly string TOKEN = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).AppSettings.Settings["TOKEN"].Value;
        private static readonly string URL = string.Concat(Properties.Resources.BASE_URL, "content/");


        public static async Task<Response> CreateReminder(Reminder reminder)
        {
            Response response = new Response();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("token", TOKEN);
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(reminder), Encoding.UTF8, "application/json"),
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(string.Concat(URL, "create-reminder"))
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
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (Exception ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error inesperado: {ex.Message}";
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
            }
            return response;
        }

        public static async Task<Response> GetCurrentInformativeContent()
        {
            Response response = new Response();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("token", TOKEN);

                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(string.Concat(URL, "obtain-informative-content"))
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
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (Exception ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error inesperado: {ex.Message}";
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
            }
            return response;
        }

        public static async Task<Response> RateInformativeContent(int informativeContentId, int rating)
        {
            Response response = new Response();
            using (var httpClient = new HttpClient())
            {
                try
                {
                    httpClient.DefaultRequestHeaders.Add("token", TOKEN);
                    string urlWithParam = string.Concat(URL, "create-rating/", Uri.EscapeDataString(informativeContentId.ToString()));
                    var ratingData = new { rating = rating };
                    var httpRequestMessage = new HttpRequestMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(ratingData), Encoding.UTF8, "application/json"),
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(urlWithParam)
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
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
                catch (Exception ex)
                {
                    response.Code = (int)HttpStatusCode.InternalServerError;
                    response.Details = $"Error inesperado: {ex.Message}";
                    DialogManager.ShowErrorMessageBox(response.Details);
                }
            }
            return response;
        }

    }
}
