﻿using MvvmCrossDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MvvmCrossDemo.Core.Services
{
    public class WeatherService
    {
        public async Task<List<LocationAutoCompleteResult>> GetLocations(string searchTerm)
        {
            WebRequest request = WebRequest.CreateHttp(String.Format("{0}?apikey={1}&q={2}"
                ,WeatherApp.AutoCompleteEndpoint
                ,WeatherApp.ApiKey
                ,WebUtility.HtmlEncode(searchTerm)));
            string responseValue = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
                var sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationAutoCompleteResult>>(responseValue);

            if (sresponse != null)
            {
                return sresponse;
            }
            else
            {
                return null;
            }
        }

        public async Task<Forecast> GetForecast(string locationKey)
        {
            WebRequest request = WebRequest.CreateHttp(String.Format("{0}{1}?apikey={2}&metric=true"
               , WeatherApp.ForecastEndpoint
               , WebUtility.HtmlEncode(locationKey)
               , WeatherApp.ApiKey
                ));
            string responseValue = null;
            using (var response = await request.GetResponseAsync())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            responseValue = await reader.ReadToEndAsync();
                        }
                    }
                }
            }
            Forecast sresponse;
            try
            {
                sresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<Forecast>(responseValue);
            }
            catch (Exception)
            {

                sresponse = null;
            }

            if (sresponse != null)
            {
                return sresponse;
            }
            else
            {
                return null;
            }
        }
    }
}
