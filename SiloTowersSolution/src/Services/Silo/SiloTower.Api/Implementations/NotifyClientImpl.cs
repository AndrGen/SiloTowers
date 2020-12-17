using Common.Helper;
using Microsoft.Extensions.Configuration;
using SiloTower.Interfaces.Silo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiloTower.Api.Implementations
{
    /// <summary>
    /// реализация простейшего оповещения о готовности данных
    /// </summary>
    public class NotifyClientImpl: INotifyClient
    {
        private readonly string _notifyUrl;
        private readonly HttpClient _httpClient;
        public NotifyClientImpl(IConfiguration configuration, HttpClient httpClient)
        {
            _notifyUrl = configuration["Notify:PostUrl"];
            _httpClient = httpClient;
            
        }
        public async void PostNotify()
        {
            await HttpRestApiClientHelper.PostAsync(_notifyUrl, _httpClient, new StringContent("Ready"));
        }
    }
}
