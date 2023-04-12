# WeatherConsole

真气网历史数据导出Excel
根据前端抓出加密方式，模拟请求API获取历史数据。

## 环境
需要.net6的运行环境，若没有请[点击这里下载环境](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)，Windows建议使用Hosting Bundle

## 使用方式
- 编译打开WeatherConsole之后，在控制台中输入对应内容即可。
- 格式为：城市名称(不带市字)【空格】开始时间【空格】结束时间
- 例如：济南 2022-11-25 2022-11-25

## FAQ
### 为什么请求数据之后没有返回？
- 先打开真气网网站看看，如果打不开就是你的ip被封了，换个ip试试。
### 为什么Windows以外的系统加载不了解析器？
- 暂时解析js的解析器只支持windows，其他系统等换了解析器再试试。
