using OpenQA.Selenium;

namespace SelExample
{
    public class Locators
    {
        //Header Area
        public static readonly By HeaderAbout = By.XPath("//a[text()='About']");
        public static readonly By HeaderStore = By.XPath("//a[text()='Store']");
        public static readonly By HeaderGmail = By.XPath("//a[text()='Gmail']");
        public static readonly By HeaderImages = By.XPath("//a[text()='Images']");
        public static readonly By HeaderApps = By.XPath("//a[@aria-label='Google apps']");
        public static readonly By HeaderSignIn = By.XPath("//a[contains(@href, 'accounts.google')]");

        //Footer Area
        public static readonly By FooterAdvert = By.XPath("//a[text()='Advertising']");
        public static readonly By FooterBusiness = By.XPath("//a[text()='Business']");
        public static readonly By FooterHowSearch = By.XPath("//a[text()='How Search works']");
        public static readonly By FooterPrivacy = By.XPath("//a[text()='Privacy']");
        public static readonly By FooterTerms = By.XPath("//a[text()='Terms']");
        public static readonly By FooterSettings = By.XPath("//a[text()='Settings']");


    }
}
