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
    public class CartChrome
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com/";
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
        public void TheCartChromeTest()
        {
            driver.Navigate().GoToUrl("https://demo.opencart.com/index.php?route=common/home");
            driver.FindElement(By.XPath("//img[@alt='MacBook']")).Click();
            driver.FindElement(By.Id("input-quantity")).Click();
            driver.FindElement(By.Id("input-quantity")).Clear();
            driver.FindElement(By.Id("input-quantity")).SendKeys("5");
            driver.FindElement(By.Id("button-cart")).Click();
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.Id("input-quantity")).Click();
            driver.FindElement(By.Id("input-quantity")).Clear();
            driver.FindElement(By.Id("input-quantity")).SendKeys("10");
            driver.FindElement(By.Id("button-cart")).Click();
            System.Threading.Thread.Sleep(10000);
            driver.FindElement(By.Id("cart-total")).Click();
            driver.FindElement(By.XPath("//div[@id='cart']/ul/li/table/tbody/tr/td[3]")).Click();
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
