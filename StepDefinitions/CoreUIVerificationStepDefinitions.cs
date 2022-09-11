using BoDi;
using luatsqa.coreauto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SpecflowSeleniumEasy.Hooks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.StepDefinitions
{
    [Binding]
    public class CoreUIVerificationStepDefinitions
    {

        public LDriver _driver;
        public ScenarioContext currentcontext;
        public Webelementsandvars elementvars;

        public CoreUIVerificationStepDefinitions(ObjectContainer objectContainer, ScenarioContext currentcontext)
        {
            this.currentcontext = currentcontext;
            elementvars = new Webelementsandvars(currentcontext);
            IWebDriver driver = (IWebDriver)currentcontext["IWEBDRIVER"];
            //IWebDriver driver = objectContainer.Resolve<IWebDriver>("IWebDriver");
            _driver = new LDriver(driver);
        }

        [Then(@"Verify current web title is ""([^""]*)""")]
        public void ThenVerifyCurrentWebTitleIs(string title)
        {
            currentcontext["HTMLLOG"] = "TITLE "+ _driver.Title;
            Assert.IsTrue(_driver.Title==elementvars.convert(title));
        }

        [Then(@"Verify current web title contains ""([^""]*)""")]
        public void ThenVerifyCurrentWebTitleContains(string title)
        {
            currentcontext["HTMLLOG"] = "TITLE " + _driver.Title;
            StringAssert.Contains(_driver.Title, elementvars.convert(title));
        }
        [Then(@"Verify dropdown ""([^""]*)"" contains below values")]
        public void ThenVerifyDropdownHaveBelowValues(string elementname, Table table)
        {
            string options = _driver.FindElement(elementvars.findxpathid(elementname), elementname).GetallOptiontostring();
            currentcontext["HTMLLOG"] = options;
            List<string> results = table.Rows.Select(r => r[0]).ToList();
            foreach (string result in results)
            {
                currentcontext["HTMLLOG"] = currentcontext["HTMLLOG"] + ": finding " + elementvars.convert(result);
                StringAssert.Contains(options, elementvars.convert(result));
            }
        }

        [Then(@"Verify dropdown ""([^""]*)"" match and same order below values")]
        public void ThenVerifyDropdownMatchorderBelowValues(string elementname, Table table)
        {
            List<string> options = _driver.FindElement(elementvars.findxpathid(elementname), elementname).GetallOption();
            List<string> results = table.Rows.Select(r => elementvars.convert(r[0])).ToList();
            currentcontext["HTMLLOG"] = _driver.FindElement(elementvars.findxpathid(elementname), elementname).GetallOptiontostring();
            Assert.IsTrue(options.SequenceEqual(results));
        }
        
        [Then(@"Verify ""([^""]*)"" in dropdown ""([^""]*)"" is selected")]
        public void ThenVerifyInDropdownIsSelected(string value, string elementname)
        {
            string selected = _driver.FindElement(elementvars.findxpathid(elementname), elementname).Getselectedoption();
            currentcontext["HTMLLOG"] = selected;
            Assert.IsTrue(selected == elementvars.convert(value), "Selected option is "+ selected + " when expected "+ elementvars.convert(value));
        }

        [Then(@"Verify ""([^""]*)"" have text equal to ""([^""]*)""")]
        public void ThenVerifyelementHaveText(string elementname, string text)
        {
            string currentText = _driver.FindElement(elementvars.findxpathid(elementname), elementname).Text;
            currentcontext["HTMLLOG"] = currentcontext;
            Assert.AreEqual(elementvars.convert(text), currentText, "Current text is " + currentText + " when expected " + elementvars.convert(text));
        }

        [Then(@"Verify ""([^""]*)"" have text contains ""([^""]*)""")]
        public void ThenVerifyelementHaveTextContains(string elementname, string text)
        {
            string currentText = _driver.FindElement(elementvars.findxpathid(elementname), elementname).Text;
            currentcontext["HTMLLOG"] = currentcontext;
            StringAssert.Contains(currentText, elementvars.convert(text), "Current text is " + currentText + " when expected " + elementvars.convert(text));
        }

        [Then(@"Verify current state of this element ""([^""]*)"" is ""([^""]*)""")]
        public void ThenVerifyelementis(string elementname, string state)
        {
            var currentElement = _driver.FindElement(elementvars.findxpathid(elementname), elementname);
            switch (elementvars.convert(state))
            {
                case "Enable":
                    Assert.IsTrue(currentElement.Enabled, "Your Element is not enable");
                    break;
                case "Disabled":
                    Assert.IsFalse(currentElement.Enabled, "Your Element is enable");
                    break;
                case "Displayed":
                    Assert.IsTrue(currentElement.Displayed, "Your Element is not Displayed");
                    break;
                default:
                    throw new Exception("Not support this state. Please use one of Enable, Disabled, Displayed");
                //case "NonDisplayed":
                  //  Assert.IsFalse(currentElement.Displayed, "Your Element is Displayed");
                    //break;
            }
        }
        [Then(@"Verify element ""([^""]*)"" is not displayed")]
        public void ThenVerifyelementisnotdisplay(string elementname)
        {
            Assert.IsNull(_driver.FindElementMAYNULL(elementvars.findxpathid(elementname), elementname), "Element found!");
        }

        }
}
