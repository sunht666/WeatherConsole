# WeatherConsole

真气网历史数据导出Excel

## 实现方式
在前端抓取关键加密区，并将加密代码解析出来，用加密的数据模拟请求，并将返回数据解密导出到excel

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
- 暂时解析js的解析器只支持windows。

## Todo

- [ ] 使用C#代码直接重写加解密方法，舍弃不跨平台的js解析器。
