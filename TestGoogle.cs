using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SelExample
{
    public class TestGoogle : DriverSetup
    {
        private StringBuilder verificationErrors;

        [SetUp]
        public void SetupTest()
        {
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString()); //not sure what this does
        }

        [TestCase("/", TestName = "TestHeaders")]
        public void LoginTestRequired(string url)
        {
            driver.Navigate().GoToUrl(baseURL + url);
            Thread.Sleep(2000);
            driver.FindElement(Locators.HeaderAbout).Click();
            Thread.Sleep(2000);
            Assert.That(driver.Url.Split('?')[0], Is.EqualTo("https://about.google/"));
        }
    }
}