using BoDi;
using luatsqa.coreauto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SpecflowSeleniumEasy.Hooks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.StepDefinitions
{
    [Binding]
    public class CoreUIActionsStepDefinitions
    {

        public LDriver _driver;
        public ScenarioContext currentcontext;
        public Webelementsandvars elementvars;

        public CoreUIActionsStepDefinitions(ObjectContainer objectContainer, ScenarioContext currentcontext)
        {
            this.currentcontext = currentcontext;
            elementvars = new Webelementsandvars(currentcontext);
            IWebDriver driver = objectContainer.Resolve<IWebDriver>("IWebDriver");
            _driver = new LDriver(driver);
        }

        [Given(@"Go to ""([^""]*)""")]
        public void GivenGoTo(string url)
        {
            _driver.Goto(elementvars.convert(url));
        }

        [Given(@"Access ""([^""]*)"" using ""([^""]*)"" and ""([^""]*)"" start by ""([^""]*)""")]
        public void GivenAccessUsingAndStartBy(string domain, string username, string password, string http)
        {
            string url = http + ":\\" + username + ":" + password + "@" + domain;
            _driver.Goto(elementvars.convert(url));
        }



        [When(@"Enter ""([^""]*)"" to ""([^""]*)""")]
        public void WhenEnterTo(string text, string elementname)
        {
            _driver.FindElement(elementvars.findxpathid(elementname), elementname).SendKeys(elementvars.convert(text));
        }

        [When(@"Click to ""([^""]*)""")]
        public void WhenClickTo(string elementname)
        {
            _driver.FindElement(elementvars.findxpathid(elementname), elementname).Click();
        }
        [When(@"Choose ""([^""]*)"" from the drop down ""([^""]*)""")]
        public void WhenChooseFromTheDropDown(string value, string elementname)
        {
            _driver.FindElement(elementvars.findxpathid(elementname), elementname).SelectByText(elementvars.convert(value));
        }

        [When(@"Sleep in ""([^""]*)"" seconds")]
        public void WhenSlepp(string sec)
        {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(Int32.Parse(elementvars.convert(sec))));
        }

        [When(@"Switch to iframe ""([^""]*)""")]
        public void WhenSwitchToIframe(string iframidorname)
        {
            _driver.Current.SwitchTo().Frame(elementvars.convert(iframidorname));
        }

        [When(@"Switch to default frame")]
        public void WhenSwitchToDefaultIframe()
        {
            _driver.Current.SwitchTo().DefaultContent();
        }

        [When(@"Switch to new Tab")]
        public void WhenSwitchTonewTab()
        {
            _driver.Current.SwitchTo().Window(_driver.Current.WindowHandles.Last());
        }

        [When(@"Switch to first Tab")]
        public void WhenSwitchToFirstTab()
        {
            _driver.Current.SwitchTo().Window(_driver.Current.WindowHandles.First());
        }

        [When(@"Save ""([^""]*)"" Text to variable ""([^""]*)""")]
        public void WhenSaveTexttovar(string elementname, string varname)
        {
            string currenttext = _driver.FindElement(elementvars.findxpathid(elementname), elementname).Text;
            currentcontext[varname] = currenttext;
        }

        [When(@"Save ""([^""]*)"" Text to variable ""([^""]*)"" filter by regex ""([^""]*)""")]
        public void WhenSaveTexttovarfilterregex (string elementname, string varname, string regex)
        {
            string currenttext = _driver.FindElement(elementvars.findxpathid(elementname), elementname).Text;
            string filtered = Regex.Match(currenttext, elementvars.convert(regex)).Value;
            currentcontext[varname] = filtered;
        }

        [When(@"Count elemnt in elements ""([^""]*)"" and save to variable ""([^""]*)""")]
        public void WhenCountandSave(string elementname, string varname)
        {
            int counter = _driver.FindElements(elementvars.findxpathid(elementname)).Count;
            currentcontext[varname] = counter;
        }
    }
}
