using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luatsqa.coreauto
{
    public class Wait
    {
        private readonly WebDriverWait _wait;



        public Wait(IWebDriver Driver, int waitSeconds)
        {
            _wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };



            _wait.IgnoreExceptionTypes(
            //typeof(NoSuchElementException),
            typeof(ElementNotVisibleException),
            typeof(StaleElementReferenceException)
            );
            _wait.Message = "Element to be searched not found";
        }



        public bool Until(Func<IWebDriver, bool> condition)
        {
            return _wait.Until(condition);
        }



        public IWebElement Until(Func<IWebDriver, IWebElement> condition)
        {
            return _wait.Until(condition);
        }
    }
}
