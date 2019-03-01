using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using NUnit.Framework;
using Enjoeat.Pages;
using System.Drawing;
using System.IO;
using System.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.Reporter.Configuration;

namespace Enjoeat.Base
{
    public class BaseTest
    {
        public IWebDriver driver;
        public HomePage homePage;
        public ListaRestaurantePage listaRestaurantePage;
        public RestaurantePage restaurantePage;
        public PedidosPage pedidosPage;
        public OrderPage orderPage;

        private static ExtentReports extent;
        private static string reportPath;


        [OneTimeSetUp]
        public void SetupReport()
        {
            if(extent == null)
            {
                reportPath = TestContext.CurrentContext.TestDirectory + "\\reports\\";

                if (!Directory.Exists(reportPath))
                {
                    Directory.CreateDirectory(reportPath);
                }

                var reporter = new ExtentHtmlReporter(reportPath);
                reporter.Config.Theme = Theme.Dark;

                extent = new ExtentReports();
                extent.AttachReporter(reporter);
            }
        }

        [SetUp]
        public void Setup()
        {

            var browser = ConfigurationManager.AppSettings["browser"];
            var chromeOptions = new ChromeOptions();

            if (browser == "headless")
            {  
                chromeOptions.AddArgument("--headless");
            } 

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Size = new Size(1440, 900);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            homePage = new HomePage(driver);
            listaRestaurantePage = new ListaRestaurantePage(driver);
            restaurantePage = new RestaurantePage(driver);
            pedidosPage = new PedidosPage(driver);
            orderPage = new OrderPage(driver);
        }

        [TearDown]
        public void Finish()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            string screenshotPath = TakeScreenshot(testName);

            var test = extent.CreateTest(testName);

            var status = TestContext.CurrentContext.Result.Outcome.Status;

            switch (status)
            {
                case TestStatus.Failed:
                    test.Fail("bug").AddScreenCaptureFromPath(screenshotPath);
                    test.Info(TestContext.CurrentContext.Result.Message);
                    test.Info(TestContext.CurrentContext.Result.StackTrace);
                    break;
                case TestStatus.Passed:
                    test.Pass("passed").AddScreenCaptureFromPath(screenshotPath);
                    break;
            }

            extent.Flush();
            driver.Close();
        }

        public string TakeScreenshot(string testName)
        {
            var ts = (ITakesScreenshot)driver;
            var shot = ts.GetScreenshot();

            var detinoPath = reportPath + testName + ".png";
            shot.SaveAsFile(detinoPath, ScreenshotImageFormat.Png);
            return detinoPath;
        }
    }
}
