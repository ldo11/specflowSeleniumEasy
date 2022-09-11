using BoDi;
using luatsqa.coreauto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        //private readonly IObjectContainer objectContainer;
        private IWebDriver webDriver;
        public ScenarioContext currentcontext;
        public TestContext testContext;
        public HtmlReport htmlreport;
        public Hooks(ScenarioContext currentcontext, TestContext testContext)
        {
            this.currentcontext = currentcontext;
            this.testContext = testContext;
        }

        [BeforeScenario("UI")]
        public void BeforeScenarioUI()
        {
            this.webDriver = new ChromeDriver();
            currentcontext["IWEBDRIVER"] = this.webDriver;
            
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            htmlreport = new HtmlReport(currentcontext.ScenarioInfo.Title, currentcontext.ScenarioInfo.Title);
            currentcontext["HTMLREPORT"] = this.htmlreport;
        }
        [AfterStep("UI")]
        public void AfterStepUI()
        {
            currentcontext["HTMLSCREENSHOT"] = ((ITakesScreenshot)webDriver).GetScreenshot().AsBase64EncodedString;
        }
        [AfterStep]
        public void AfterStep()
        {
            if(currentcontext.TestError == null)
            {
                if (currentcontext.ContainsKey("HTMLSCREENSHOT"))
                {
                    htmlreport.AddStepResult(1, currentcontext.StepContext.StepInfo.Text, currentcontext["HTMLLOG"].ToString(), currentcontext["HTMLSCREENSHOT"].ToString());
                }
                else
                {
                    htmlreport.AddStepResult(1, currentcontext.StepContext.StepInfo.Text, currentcontext["HTMLLOG"].ToString(),null);
                }
                
            }
            else
            {
                if (currentcontext.ContainsKey("HTMLSCREENSHOT"))
                {
                    htmlreport.AddStepResult(2, currentcontext.StepContext.StepInfo.Text, currentcontext["HTMLLOG"].ToString(), currentcontext["HTMLSCREENSHOT"].ToString(), currentcontext.TestError.Message);
                }
                else
                {
                    htmlreport.AddStepResult(2, currentcontext.StepContext.StepInfo.Text, currentcontext["HTMLLOG"].ToString(), null, currentcontext.TestError.Message);
                }
            }
            currentcontext["HTMLLOG"] = "";
            currentcontext["HTMLSCREENSHOT"] = "";
        }


        [AfterScenario("UI")]
        public void AfterScenarioUi()
        {
            System.Diagnostics.Process.Start(@"cmd.exe ", @"/c " + htmlreport.generate()); 
            webDriver.Close();
            webDriver.Dispose();
        }
        [AfterScenario]
        public void AfterScenario()
        {
            testContext.AddResultFile(htmlreport.generate());
        }
    }
}