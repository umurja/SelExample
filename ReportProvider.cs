using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NLog;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using System;
using System.IO;

namespace SelExample
{
    public class ReportProvider
    {
        private readonly NLog.Logger Logger = NLog.LogManager.GetLogger("fileLogger");
        private static ExtentReports _extent;
        private static string projectPath;
        private static ExtentTest _test { get; set; }

        public static void InitReporter()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = path.Substring(0, path.LastIndexOf("bin"));
            projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            Directory.CreateDirectory(projectPath.ToString() + "Reports\\Screenshots");
            var reportPath = projectPath + "Reports\\ExtentReport.html";
            var htmlReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Environment", "SomeEnv");
            _extent.AddSystemInfo("UserName", "SomeUser");
        }

        public void GenerateExtentReport(String status, String message, String testName, IWebDriver _webDriver, bool isServiceTest)
        {
            Status logstatus;
            switch (status)
            {
                case "Failed":
                    logstatus = Status.Fail;
                    _test.Log(Status.Fail, message);
                    if (!isServiceTest)
                    {
                        var screenshotsPath = projectPath + "Reports\\Screenshots\\";
                        var screenShootFileFullPath = screenshotsPath + testName + ".bmp";
                        //_test.AddScreenCaptureFromPath(DriverExtensions.TakeScreenshot(_webDriver, screenShootFileFullPath));
                        //TODO uncomment line above

                    }
                    break;
                case "Inconclusive":
                    logstatus = Status.Warning;
                    break;
                case "Skipped":
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }
            string logdetails = "";
            _test.Log(logstatus, logdetails);
            FlushReport();
        }

        public void CreateTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        public void FlushReport()
        {
            _extent.Flush();
        }

        public void ReportLog(string message)
        {
            //_test.Log(Status.Info, message);    //For extentTest HTML report
            Logger.Info(message);               //For the Logger
        }

        public void LogNewExecutionStarted()
        {
            string line = "*************************************************";
            Logger.Info(line);
            Logger.Info("New Tests Execution");
            Logger.Info(line);
        }
        public void LogTestStarted(string testName)
        {
            Logger.Info($"---------------------Test Name: {testName}---------------------");
        }
        public void LogTestStarted(string testName, string environmentName)
        {
            Logger.Info($"---------------------Test Name: {testName}---------------------");
            ReportLog($"Environment variables");
            ReportLog($"env: {environmentName}");
        }
        public void LogTestStarted(string testName, string environmentName, string browser)
        {
            LogTestStarted(testName, environmentName);
            ReportLog($"browser: {browser}");
        }

        public void LogTestFinished()
        {
            Logger.Info("---------------------Test finished---------------------\n");
        }

    }

}
