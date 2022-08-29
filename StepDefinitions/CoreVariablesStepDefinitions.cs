using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SpecflowSeleniumEasy.Hooks;
using System;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.StepDefinitions
{
    [Binding]
    public class CoreVariablesStepDefinitions
    {
        public ScenarioContext currentcontext;
        public Webelementsandvars variables;

        public CoreVariablesStepDefinitions(ScenarioContext sc)
        {
            currentcontext = sc;
            variables = new Webelementsandvars(sc);
        }

        [When(@"Assign value ""([^""]*)"" to local variables ""([^""]*)""")]
        public void WhenAssignValueToLocalVariables(string value, string varname)
        {
            currentcontext[varname] = value;
        }

        [Then(@"Verify variable ""([^""]*)"" has value ""([^""]*)""")]
        public void ThenVerifyVariableHasValue(string varname, string value)
        {
            Assert.AreEqual(value, currentcontext[varname]);
        }

    }
}
