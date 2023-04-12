using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Furion.RemoteRequest.Extensions;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WeatherConsole.Model;
using WeatherConsole.Service.Interface;
using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using NPOI.SS.Formula.Functions;
using WeatherConsole.Util;

namespace WeatherConsole.Service.Impl
{
    public class WeatherService : IWeatherService, IScoped
    {
        public WeatherService() 
        {
            
        }

        /// <summary>
        /// 获取天气
        /// </summary>
        /// <returns></returns>
        public async Task GetWeatherAsync()
        {
            await ExportCityWeatherAsync("济南", new DateTime(2022, 11, 1), new DateTime(2022, 11, 30));
        }

        public async Task ExportCityWeatherAsync(string cityName, DateTime startTime, DateTime endTime)
        {
            Console.WriteLine("1.加载解析器(1/6)");
            using (var engine = new V8ScriptEngine())
            {
                try
                {
                    //js解析器的一些配置
                    engine.DocumentSettings.AccessFlags = Microsoft.ClearScript.DocumentAccessFlags.EnableFileLoading;
                    engine.DefaultAccess = Microsoft.ClearScript.ScriptAccess.Full;

                    //读取js文件
                    string scriptContent = string.Empty;
                    using (FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "RealWeather20221125.js", FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            scriptContent = sr.ReadToEnd();
                        }
                    }

                    //取得脚本解析一下
                    engine.Execute(scriptContent);

                    Console.WriteLine("2.加密参数(2/6)");
                    //执行getParams方法，根据参数获取到加密后的参数，具体参数等信息详见js 
                    var st = startTime.ToString("yyyy-MM-dd 00:00:00");
                    var et = endTime.AddDays(1).AddMilliseconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
                    var result = engine.Script.getHistory(cityName, st, et);

                    //将加密后的内容url编码后，post请求，请求头中注意添加必要信息，详见post请求代码
                    string body = "param=" + HttpUtility.UrlEncode(result);

                    Console.WriteLine("3.请求数据(3/6)");

                    var jsonStr = await Post("https://www.zq12369.com/api/newzhenqiapi.php", body);

                    Console.WriteLine("4.解密数据(4/6)");
                    var json = engine.Script.getResultJson(jsonStr);
                    JObject jValue = JObject.Parse(json);
                    if (Convert.ToBoolean(jValue["success"].ToString()))
                    {
                        var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                        List<WeatherHistoryModel> list = JsonConvert.DeserializeObject<List<WeatherHistoryModel>>(jValue["result"]["data"]["rows"].ToString(), dateTimeConverter);
                        list = list.OrderBy(t => t.time).ToList();

                        Console.WriteLine("5.导出数据(5/6)");
                        new ExcelHelper<WeatherHistoryModel>().ExportToExcel(cityName + "天气数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls", cityName + "天气数据", list);

                        Console.WriteLine("6.成功(6/6)");
                        Open(Path.Combine(System.Environment.CurrentDirectory, "Excel"));
                    }
                    else
                    {
                        Console.WriteLine("返回出错啦！");
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine("出错啦！" + err.Message);
                }
            }
        }

        /// <summary>
        /// 获取某地市天气
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns></returns>
        private async Task<WeatherModel> GetCityWeatherAsync(string cityName)
        {
            WeatherModel weatherModel = new WeatherModel();
            var request = await "https://www.zq12369.com/"
                .SetQueries(new Dictionary<string, object>() 
                {
                    { "city", cityName },
                    { "tab", "city" }
                })
                .GetAsync();
            if (request.IsSuccessStatusCode)
            { 
                var html = await request.Content.ReadAsStringAsync();

                try
                {
                    System.Text.RegularExpressions.Match matches = null;
                    matches = Regex.Match(html, "体感.*℃");
                    weatherModel.Temperature = matches.Groups.Count > 0 ? GetDoubleNumber(matches.Groups[0].Value) : null;

                    matches = Regex.Match(html, "湿度.*%");
                    weatherModel.Humidity = matches.Groups.Count > 0 ? GetDoubleNumber(matches.Groups[0].Value) : null;

                    matches = Regex.Match(html, "<span.*风");
                    weatherModel.WindDirection = matches.Groups.Count > 0 ? (matches.Groups[0].Value.Split('>').Length > 1 ? matches.Groups[0].Value.Split('>')[1] : null) : null;

                    matches = Regex.Match(html, weatherModel.WindDirection + ".*级");
                    weatherModel.WindLevel = matches.Groups.Count > 0 ? GetIntNumber(matches.Groups[0].Value) : null;
                }
                catch(Exception ex) { }
            }

            return weatherModel;
        }

        /// <summary>
        /// 从字符串获取浮点数字
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private double? GetDoubleNumber(string buff)
        {
            var matches = Regex.Replace(buff, @"[^\d.\d]", "");
            // 如果是数字，则转换为decimal类型
            if (Regex.IsMatch(matches, @"^[+-]?\d*[.]?\d*$") && double.TryParse(matches, out double value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// 从字符串获取浮点数字
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private int? GetIntNumber(string buff)
        {
            var matches = Regex.Replace(buff, @"[^\d.\d]", "");
            // 如果是数字，则转换为decimal类型
            if (Regex.IsMatch(matches, @"^[+-]?\d*[.]?\d*$") && int.TryParse(matches, out int value))
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Post方法
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="body">body内容</param>
        /// <returns></returns>
        private async Task<string> Post(string url, string body)
        {
            string ret = "";

            var httpclientHandler = new HttpClientHandler();
            var bodyContent = new StringContent(body);

            //设置请求类型ContectType
            bodyContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using (var httpClient = new HttpClient(httpclientHandler))
            {
                //请求头添加User-Agent，这个头主要记录了你是用的什么系统，什么浏览器，什么内核等，一般由浏览器自动生成。
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36");

                //异步请求等待返回
                var result = await httpClient.PostAsync(url, bodyContent);

                //将返回的内容读出来，除了内容，还可以获取到状态码，返回头等信息
                ret = await result.Content.ReadAsStringAsync();
                httpClient.Dispose();
            }

            return ret;
        }

        /// <summary>
        /// 打开文件夹或者网页
        /// </summary>
        /// <param name="url"></param>
        private static void Open(string url)
        {
            var psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
