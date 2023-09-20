using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using System.IO;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SelExample
{
    public abstract class DriverSetup
    {
        protected IWebDriver driver;
        protected string baseURL;
        //private static string projectPath;
        protected TestContext TestContext => TestContext.CurrentContext;
        protected ReportProvider _reportUtils = new ReportProvider();


        protected DriverSetup() { }

        [OneTimeSetUp]
        public void Init()
        {
            _reportUtils.LogNewExecutionStarted();
            ReportProvider.InitReporter();
        }

        [SetUp]
        public void Before()
        {
            _reportUtils.CreateTest(TestContext.CurrentContext.Test.Name);
            _reportUtils.LogTestStarted(this.TestContext.Test.Name);
            baseURL = "https://www.google.com/";
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(baseURL);
            //TODO: Add modal cookie 
            //driver.Manage().Cookies.AddCookie(new Cookie("AWSALBCORS", "3OcdpiNWlpEVrQCk9+LhTB1I5VMnxEH6Y0VYCiemkLBpAz0UxwI3G/IqLpNo3a2PhSMh6JEMZj6DXITqfjBG2j0cDT3zIJJZargkp+PpXgtTUSzEnheZeTuQhUMi", "m1.listrakbi.com"));
        }
        [TearDown]
        public void After()
        {
            _reportUtils.LogTestFinished();
            _reportUtils.GenerateExtentReport(TestContext.CurrentContext.Result.Outcome.Status.ToString(), TestContext.Result.Message, TestContext.CurrentContext.Test.Name, driver, false);
            //_webDriver.Quit();
            driver.Dispose();
        }
    }
}
