using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecflowSeleniumEasy.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private readonly IObjectContainer objectContainer;
        private IWebDriver webDriver;

        public Hooks(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {
            this.webDriver = new ChromeDriver();
            objectContainer.RegisterInstanceAs<IWebDriver>(webDriver, "IWebDriver");
        }


        [AfterScenario("UI")]
        public void AfterScenario()
        {
            webDriver.Close();
            webDriver.Dispose();
        }
    }
}