using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luatsqa.coreauto
{
    public class Element : IWebElement
    {
        private readonly IWebElement _element;
        private readonly Wait Wait;



        public readonly string Name;



        public By FoundBy { get; set; }



        public Element(IWebElement element, string name, Wait w)
        {
            _element = element;
            Name = name;
            Wait = w;
        }



        public IWebElement Current => _element ?? throw new System.NullReferenceException("_element is null.");



        public string TagName => Current.TagName;



        public string Text => Current.Text;



        public bool Enabled => Current.Enabled;



        public bool Selected => Current.Selected;



        public Point Location => Current.Location;



        public Size Size => Current.Size;



        public bool Displayed => Current.Displayed;



        public void Clear()
        {
            try
            {
                if (Enabled)
                {
                    Current.Clear();
                }
                else
                {
                    Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                    Current.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--"+Name + "didn't clear due to error " + e.Message + e.InnerException);
                throw new Exception(Name + " didn't clear due to error " + e.Message + e.InnerException);
            }



        }



        public void Click()
        {
            try
            {
                if (Enabled)
                {
                    Current.Click();
                    Console.WriteLine(DateTime.Now.ToString() + "--"+ Name + " was clicked successful");
                }
                else
                {
                    Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                    Current.Click();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--" + Name + " didn't click due to error " + e.Message + e.InnerException);
                throw new Exception(DateTime.Now.ToString() + "--" + Name + " didn't click due to error " + e.Message + e.InnerException);
            }



        }




        public void ClickJS(IWebDriver driver)
        {
            // FW.Log.Step($"Click {Name}");
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", Current);
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--" + Name + " didn't clickJS due to error " + e.Message + e.InnerException);
                throw new Exception(DateTime.Now.ToString() + "--" + Name + " didn't clickJS due to error " + e.Message + e.InnerException);
            }
        }
   
        public bool Exists()
        {
            if (Current == null)
                return false;
            else
                return true;
        }
        public void SelectText(string text)
        {
            string ListOptions = "";
            try
            {
                if (Enabled)
                {
                    SelectElement oSel = new SelectElement(Current);
                    ListOptions = getTextoflist(oSel.Options);
                    oSel.SelectByText(text, true);
                }
                else
                {
                    Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                    SelectElement oSel = new SelectElement(Current);
                    ListOptions = getTextoflist(oSel.Options);
                    oSel.SelectByText(text, true);
                }
            }
            catch (Exception e)
            {
                throw new Exception(DateTime.Now.ToString() + "--Can not find value " + text + " in drop downlist value :" + ListOptions + "; Error:" + e.Message);
            }
        }
        public string Getselectedoption()
        {
            SelectElement oSel = new SelectElement(Current);
            return oSel.SelectedOption.ToString();
        }



        public string getTextoflist(IList<IWebElement> list)
        {
            string result = "";
            if (Enabled)
            {
                foreach (IWebElement e in list)
                {
                    result = result + ";" + e.Text;
                }
            }
            else
            {
                Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                foreach (IWebElement e in list)
                {
                    result = result + ";" + e.Text;
                }
            }



            return result;
        }
        public List<string> GetallOption()
        {
            SelectElement oSel = new SelectElement(Current);
            List<string> options = new List<string>();
            foreach (IWebElement e in oSel.Options)
            {
                options.Add(e.Text);
            }
            return options;
        }



        public void SelectbyIndex(int index)
        {
            SelectElement oSel = new SelectElement(Current);
            oSel.SelectByIndex(index);
        }




        public IWebElement FindElement(By by)
        {
            return Current.FindElement(by);
        }



        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Current.FindElements(by);
        }



        public void SendKeys(string text)
        {
            try
            {
                if (Enabled)
                {
                    Current.SendKeys(text);
                    Console.WriteLine(DateTime.Now.ToString() + "--Succeful Enter" + text + " to " + Name);
                }
                else
                {
                    Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                    Current.SendKeys(text);
                    Console.WriteLine(DateTime.Now.ToString() + "--Succeful Enter" + text + " to " + Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--Can NOT enter" + text + " to " + Name + "Due to error:" + e.Message + e.InnerException);
                throw new Exception("Can NOT enter" + text + " to " + Name + "Due to error:" + e.Message + e.InnerException);
            }
        }
        public void Enterpassword(string text)
        {
            try
            {
                if (Enabled)
                {
                    Current.SendKeys(text);
                }
                else
                {
                    Wait.Until(WaitConditions.ElementIsClickable(FoundBy));
                    Current.SendKeys(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now.ToString() + "--Can NOT enter" + text + " to " + Name + "Due to error:" + e.Message + e.InnerException);
                throw new Exception("Can NOT enter" + text + " to " + Name + "Due to error:" + e.Message + e.InnerException);
            }
        }



        public void SetTextJS(IWebDriver driver, string text)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].value='" + text + "';", Current);
        }



        public void Submit()
        {
            Current.Submit();
        }

        public string GetDomAttribute(string attributeName)
        {
            return Current.GetDomAttribute(attributeName);
        }

        public string GetDomProperty(string propertyName)
        {
            return Current.GetDomProperty(propertyName);
        }

        public ISearchContext GetShadowRoot()
        {
            return Current.GetShadowRoot();
        }

        public string GetAttribute(string attributeName)
        {
            return Current.GetAttribute(attributeName);
        }



        public string GetCssValue(string propertyName)
        {
            return Current.GetCssValue(propertyName);
        }

        [Obsolete]
        public string GetProperty(string propertyName)
        {
            return Current.GetDomProperty(propertyName);
        }

    }
}
