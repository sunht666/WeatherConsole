using Furion;
using Microsoft.Extensions.DependencyInjection;
using WeatherConsole.Service.Impl;
using WeatherConsole.Service.Interface;

namespace WeatherConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Serve.Run(silence: true);
            Console.WriteLine("Sebastian's Present");
            Console.WriteLine("孙昊天制作");
            Console.WriteLine("V0.0.4");
            Console.WriteLine("真气网的加密方式可能会发生改变，届时可能需重新破解");

            var mainService = App.GetService<IMainService>();
            while (true)
            {
                PrintFormat();
                var str = Console.ReadLine();
                if (str.Length > 0)
                {
                    var strs = str.Split(' ');
                    if (strs.Length == 3)
                    {
                        if (DateTime.TryParse(strs[1], out DateTime startTime) && DateTime.TryParse(strs[2], out DateTime endTime))
                        {
                            mainService.Run(strs[0], startTime, endTime);
                        }
                        else
                        {
                            Console.WriteLine("参数错误！");
                        }
                    }
                    else
                    {
                        Console.WriteLine("参数错误！");
                    }
                }
                else
                { 
                    Console.WriteLine("参数错误！");
                }
            }
        }

        static void PrintFormat()
        { 
            Console.WriteLine();
            Console.WriteLine("格式为：城市名称(不带市字)【空格】开始时间【空格】结束时间");
            Console.WriteLine("例如：济南 2022-11-25 2022-11-25");
            Console.WriteLine("请输入(按↑快速复制上次输入的内容)：");
        }
    }
}