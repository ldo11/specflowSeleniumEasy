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
    public class CoreUIVerificationStepDefinitions
    {

        public LDriver _driver;
        public ScenarioContext currentcontext;
        public Webelementsandvars transvariables;

        public CoreUIVerificationStepDefinitions(ObjectContainer objectContainer, ScenarioContext currentcontext)
        {
            this.currentcontext = currentcontext;
            transvariables = new Webelementsandvars(currentcontext);
            IWebDriver driver = objectContainer.Resolve<IWebDriver>("IWebDriver");
            _driver = new LDriver(driver);
        }

        [Then(@"Verify current web title is ""([^""]*)""")]
        public void ThenVerifyCurrentWebTitleIs(string title)
        {
            transvariables.convert(title);
        }
    }
}
