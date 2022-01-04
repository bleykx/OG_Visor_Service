using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Net.Http;
using System.Net;
using Serilog.Core;
using Serilog.Context;
using System.IO;
using Newtonsoft.Json;

namespace OG_Visor_Service.Helpers
{
    public class Loggerizer
    {
        private readonly ILogger<Loggerizer> _logger;
        private LogContent _logContent; 
        private string _errorPath = "D:/Workspace/Log/OG_Visor_Service/Errors/Errors_" + DateTime.Today.Year + "_" + DateTime.Today.Month + "_" + DateTime.Today.Day+ ".json";
        private string _infoPath = "D:/Workspace/Log/OG_Visor_Service/Infos/Infos_" + DateTime.Today.Year + "_" + DateTime.Today.Month + "_" + DateTime.Today.Day+ ".json";
        public Object content = new Object();

        public Loggerizer(ILogger<Loggerizer> logger)
        {
            _logger = logger;
        }

        public void LogAPIError(Object logContent)
        {
            //string msg = "Method " + methodName + " sending " + httpMethod + " request at " + url + " get statusCode " + statusCode + " with response " + @content;
            LogContent log = (LogContent)logContent;

            _logger.LogError(log.GetJson());
            SaveErrorInFile(log.GetJson());
        }

        public void LogAPIResponse(Object logContent)
        {
            //string msg = "Method " + methodName + " sending " + httpMethod + " request at " + url + " get statusCode " + statusCode + " with response " + @content;

            _logger.LogInformation(JsonConvert.SerializeObject(logContent));
        }

        public void LogAPICantConnect(Object logContent)
        {
            //string msg = $"Method: {_logContent._methodName} sending {_logContent._httpMethod} request at {_logContent._url}  can't connect. Request Exception : {_logContent._content}";
            LogContent log = (LogContent)logContent;

            _logger.LogError(log.GetJson());
            SaveErrorInFile(log.GetJson());
        }

        public void SaveErrorInFile(string msg)
        {
            string path = _errorPath + "Errors_" + DateTime.UtcNow.Year + DateTime.UtcNow + ".json";
            File.AppendAllText(_errorPath, msg + Environment.NewLine);
        }

        public void SaveInfoInFile(string msg)
        {
            File.AppendAllText(_errorPath + "Infos_" + DateTime.Today, msg);
        }

    }

    public class LogContent
    {
        public DateTime _time { get; set; }
        public LogLevel _logLevel { get; set; }
        public string _methodName { get; set; }
        public HttpMethod _httpMethod { get; set; }
        public Uri _url { get; set; }
        public HttpStatusCode _statusCode { get; set; }
        public string _content { get; set; }

        public LogContent(string methodName, LogLevel logLevel, HttpMethod httpMethod, Uri url, HttpStatusCode statusCode, string content)
        {
            _time = DateTime.Now;
            _logLevel = logLevel;
            _methodName = methodName;
            _httpMethod = httpMethod;
            _url = url;
            _statusCode = statusCode;
            _content = content;
        }

        public string GetJson() { return JsonConvert.SerializeObject(this); }
    }
}