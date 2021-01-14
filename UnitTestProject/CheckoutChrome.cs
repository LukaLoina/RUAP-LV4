using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class CheckoutChrome
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            baseURL = "https://demo.opencart.com";
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
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheCheckoutTest()
        {
            driver.Navigate().GoToUrl("https://demo.opencart.com/index.php?route=common/home");
            driver.FindElement(By.XPath("//div[@id='top-links']/ul/li[2]/a/span")).Click();
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("input-email")).Click();
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys("test@gmail.com");
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys("test");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.Navigate().GoToUrl("https://demo.opencart.com/index.php?route=product/category&path=18");
            System.Threading.Thread.Sleep(1000);
            //driver.FindElement(By.LinkText("Show All Laptops & Notebooks")).Click();
            driver.FindElement(By.LinkText("HP LP3065")).Click();
            driver.FindElement(By.Id("button-cart")).Click();
            driver.FindElement(By.XPath("(//button[@type='button'])[5]")).Click();
            driver.FindElement(By.XPath("//div[@id='cart']/ul/li[2]/div/p/a[2]/strong")).Click();
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("button-payment-address")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("button-shipping-address")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("button-shipping-method")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Name("agree")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("button-payment-method")).Click();
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("button-confirm")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
