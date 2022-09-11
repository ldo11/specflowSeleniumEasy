using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SpecflowSeleniumEasy.Hooks;
using System;
using System.Text.RegularExpressions;
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

        [Given(@"Assign value ""([^""]*)"" to local variables ""([^""]*)""")]
        [When(@"Assign value ""([^""]*)"" to local variables ""([^""]*)""")]
        public void WhenAssignValueToLocalVariables(string value, string varname)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            currentcontext[varname] = value;
            currentcontext["HTMLLOG"] = "SAVE " + value + " ===> " + varname;
        }
        [Given(@"Assign value ""([^""]*)"" to global variables ""([^""]*)""")]
        [When(@"Assign value ""([^""]*)"" to global variables ""([^""]*)""")]
        public void WhenAssignValueToGlobalVariables(string value, string varname)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            Environment.SetEnvironmentVariable(varname,value);
            currentcontext["HTMLLOG"] = "SAVE GLOBAL " + value + " ===> " + varname;
        }

        [Then(@"Verify local variable ""([^""]*)"" has value ""([^""]*)""")]
        public void ThenVerifyLocalVariableHasValue(string varname, string value)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            currentcontext["HTMLLOG"] = "COMPARE expected : " + value + " with current value:" + currentcontext[varname];
            Assert.AreEqual(value, currentcontext[varname].ToString());
            
        }

        [Then(@"Verify Global variable ""([^""]*)"" has value ""([^""]*)""")]
        public void ThenVerifyGlobalVariableHasValue(string varname, string value)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            currentcontext["HTMLLOG"] = "COMPARE expected : " + value + " with current value:" + Environment.GetEnvironmentVariable(varname);
            Assert.AreEqual(value, Environment.GetEnvironmentVariable(varname));
        }

        [Then(@"Verify local variable ""([^""]*)"" = ""([^""]*)""")]
        public void ThenVerifyLocalVariableEqual(string varname, string value)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            currentcontext["HTMLLOG"] = "COMPARE expected : " + value + " = current value:" + currentcontext[varname].ToString();
            Assert.AreEqual(value, currentcontext[varname].ToString());
        }

        [Then(@"Verify local variable ""([^""]*)"" > ""([^""]*)""")]
        public void ThenVerifyLocalVariableBigerthan(string varname, string value)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            Assert.IsTrue(Int32.Parse(currentcontext[varname].ToString()) > Int32.Parse(value), "Variable value is not bigger than expected");
        }

        [Then(@"Verify local variable ""([^""]*)"" < ""([^""]*)""")]
        public void ThenVerifyLocalVariableSmallerthan(string varname, string value)
        {
            if (currentcontext.ContainsKey(value))
            {
                value = currentcontext[value].ToString();
            }
            Assert.IsTrue(Int32.Parse(currentcontext[varname].ToString()) < Int32.Parse(value), "Variable value is not smaller than expected");
        }

        [When(@"Filter local variable ""([^""]*)"" using regex ""([^""]*)""")]
        public void WhenFilterLocalVariableUsingRegex(string varname, string regex)
        {
            string currenttext = currentcontext[varname].ToString();
            if (currentcontext.ContainsKey(regex))
            {
                regex = currentcontext[regex].ToString();
            }
            string filtered = Regex.Match(currenttext, regex).Value;
            currentcontext[varname] = filtered;
        }
        [When(@"Filter local variable ""([^""]*)"" using regex ""([^""]*)"" and save to variable ""([^""]*)""")]
        public void WhenFilterLocalVariableUsingRegex(string varname, string regex, string newvar)
        {
            string currenttext = currentcontext[varname].ToString();
            if (currentcontext.ContainsKey(regex))
            {
                regex = currentcontext[regex].ToString();
            }
            string filtered = Regex.Match(currenttext, regex).Value;
            currentcontext[newvar] = filtered;
        }
    }
}
