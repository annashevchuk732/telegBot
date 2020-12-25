using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Bot
{
    class OpenWeather
    {
        public coord coord;

        public weather[] weather;

        [JsonProperty("base")]
        public string Base;

        public Main main;

        public double visibility;

        public wind wind;

        public clouds clouds;

        public double dt;

        public sys sys;

        public int id;

        public string name;

        public int cod;

    }
}
