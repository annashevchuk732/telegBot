using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;

namespace Bot
{
    class MyBot
    {
        readonly string botKey = "1464473013:AAE9loZtrNy2hwP4atMBvgFrYHLbMoovTCo";
        readonly string weatherKey = "c8b078403fab40a5752700b584cf4035";
        static ITelegramBotClient botClient;
        HttpClient httpClient = new HttpClient();
        Uri uri = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Moscow&appid=c8b078403fab40a5752700b584cf4035");

        OpenWeather oW;
        public MyBot()
        {   
            botClient = new TelegramBotClient(botKey);
            var me = botClient.GetMeAsync().Result;
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }
        private string SendRequest(string message)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.GetAsync(new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + message + "&appid=c8b078403fab40a5752700b584cf4035")).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }    
            var content = response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(content.Result);
            oW = JsonConvert.DeserializeObject<OpenWeather>(content.Result);
            return message;
        }
        async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message.Text;
            message = SendRequest(message);
            if (message != null)
            {
                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "Город - : " + message + "\n" +
                  "Температура: " + (int)oW.main.temp + " °C" + "\n" +
                  "Скорость ветра: " + (int)oW.wind.speed + " м/с" + "\n" +
                  "Влажность: " + (int)oW.main.humidity + " %" + "\n" +
                  "Давление: " + (int)oW.main.pressure + " мм рт. ст." + "\n"
                );
            }
        }
    }
}
