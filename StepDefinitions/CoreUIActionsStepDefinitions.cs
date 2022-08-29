using BoDi;
using luatsqa.coreauto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SpecflowSeleniumEasy.Hooks;
using System;
using System.Collections.Generic;
using System.IO;
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

    }
}
