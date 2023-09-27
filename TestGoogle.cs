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

        public static IEnumerable<TestCaseData> LocatorsForGoogleHeaderLinkTests
        {
            get
            {
                yield return new TestCaseData("/", Locators.HeaderAbout, "https://about.google/");
                yield return new TestCaseData("/", Locators.HeaderStore, "https://store.google.com/US/");
                yield return new TestCaseData("/", Locators.HeaderGmail, "https://www.google.com/gmail/about/");
                //yield return new TestCaseData(report, report.Merchants[5461324658456716].AggregateTotals, 5461324658456716, "WirelessPerItem").SetName("ReportMerchantsAggregateTotals");
            }
        }
        [TestCaseSource("LocatorsForGoogleHeaderLinkTests")]
        public void GoogleHeaderLinkTests(string url, By byLoc, string expectedUrl)
        {
            
            driver.Navigate().GoToUrl(baseURL + url);
            Thread.Sleep(2000);
            driver.FindElement(byLoc).Click();
            Thread.Sleep(2000);
            Assert.That(driver.Url.Split('?')[0], Is.EqualTo(expectedUrl));
        }
    }
}