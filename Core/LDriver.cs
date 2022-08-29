using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luatsqa.coreauto
{
    public class LDriver
    {
        private readonly IWebDriver _driver;
        public Wait Wait;

        public LDriver(IWebDriver driver)
        {
            _driver = driver;
            Wait = new Wait(_driver, 200);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public IWebDriver GetDriver()
        {
            return _driver;
        }



        public IWebDriver Current => _driver ?? throw new NullReferenceException("_driver is null.");



        public string Title => Current.Title;



        public void Goto(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = $"http://{url}";
            }


            Console.WriteLine(DateTime.Now.ToString() +" --Navigate to "+ url);
            Current.Navigate().GoToUrl(url);
        }



        public void WaitPageLoadJS()
        {
            Console.WriteLine(DateTime.Now.ToString() + " --Wait for pageload");
            new WebDriverWait(Current, System.TimeSpan.FromSeconds(30.00)).Until(
            d => ExecuteComandJavascript("return document.readyState").Equals("complete"));
        }
        public Object ExecuteComandJavascript(string script)
        {
            Console.WriteLine(DateTime.Now.ToString() + " --Execute javascript"+ script);
            return ((IJavaScriptExecutor)Current).ExecuteScript(script);
        }

        public By TranslateBy(string xpathid)
        {
            if (xpathid.Substring(0, 1) == "/" || xpathid.Substring(0, 1) == "(")
            {
                return By.XPath(xpathid);
            }
            else
            {
                return By.Id(xpathid);
            }
        }
        
        public Element FindElement(string xpathid,string elementName)
        {
            By by = TranslateBy(xpathid);
            WaitPageLoadJS();
            try
            {
                try
                {
                    Wait.Until(drvr => drvr.FindElement(by));
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine(DateTime.Now.ToString() + "--Still waitting element! " + e.InnerException);
                }
                var element = Wait.Until(drvr => drvr.FindElement(by));



                Console.WriteLine(DateTime.Now.ToString() + "--Element was found by" + by.ToString());
                return new Element(element, elementName, Wait)
                {
                    FoundBy = by
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--Element not found by " + by.ToString() + " with error: " + e.Message);
                return null;
                //throw new Exception("Element not found by " + by.ToString() + " with error: " + e.InnerException);
            }
        }
        public Element FindElementMAYNULL(string xpathid,string elementName)
        {
            By by = TranslateBy(xpathid);
            WaitPageLoadJS();
            if (FindElementSafe(by) == null)
            {
                return null;
            }
            else
            {
                var element = FindElementSafe(by);
                return new Element(element, elementName, Wait)
                {
                    FoundBy = by
                };
            }
        }



        public bool IsElementPresent(By locatorKey)
        {
            try
            {
                if (FindElementSafe(locatorKey) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--IsElementPresent Element is not found " + e.InnerException);
                return false;
            }



        }
        public void Waituntildisappear(By locator)
        {
            if (FindElementSafe(locator) != null)
            {
                IWebElement a = _driver.FindElement(locator);
                Wait.Until(WaitConditions.ElementNotDisplayed(a));
            }
        }
        public void WaituntilEnable(By locator)
        {
            try
            {
                if (FindElementSafe(locator) != null)
                {
                    Wait.Until(WaitConditions.ElementIsClickable(locator));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--WaituntilEnable Element is not found " + e.InnerException);
            }
        }
        public IWebElement FindElementSafe(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        public Elements FindElements(By by)
        {
            return new Elements(Current.FindElements(by))
            {
                FoundBy = by
            };
        }



        public void Quit()
        {
            Current.Quit();
        }



        public string Takescreenshot64()
        {
            return ((ITakesScreenshot)Current).GetScreenshot().AsBase64EncodedString;
        }

    }
}
