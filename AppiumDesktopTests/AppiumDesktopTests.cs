
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace AppiumDesktopTests
{
    public class AppiumDesktopTests
    {
        private const string AppiumServerUrl = "http://127.0.0.1:4723/wd/hub";
        private const string AppServer = "http://localhost:8080/api";
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> driverDesktop;

        [OneTimeSetUp]
        public void SetUp()
        {
            var appiumOptions = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumOptions.AddAdditionalCapability("app", @"C:\ContactBook-DesktopClient.NET6\ContactBook-DesktopClient.exe");
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            var appiumDriverOptions = new AppiumOptions()
            {
                PlatformName = "Windows"
            };
            appiumDriverOptions.AddAdditionalCapability("app", "Root");
            driverDesktop = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUrl), appiumDriverOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            //driver.CloseApp();
            driverDesktop.CloseApp();
            
        }

        [Test]

        public void TestSearchUser()
        {
            var inputAppUrl = driver.FindElementByAccessibilityId("textBoxApiUrl");
            inputAppUrl.Clear();
            inputAppUrl.SendKeys(AppServer);
            var connectButton = driver.FindElementByAccessibilityId("buttonConnect");
            connectButton.Click();

            Thread.Sleep(2000);

            var Searchfield = driverDesktop.FindElementByAccessibilityId("textBoxSearch");
            Searchfield.SendKeys("steve");
            var buttonSearch = driverDesktop.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();
            var firstName = driverDesktop.FindElementByXPath("/Pane/Window/DataGrid/Custom/DataItem[@Name=\"FirstName Row 0, Not sorted.\"]").Text;
            //Assert.AreEqual("Steve", firstName);
            driverDesktop.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var lastName = driverDesktop.FindElementByXPath("/Pane/Window/DataGrid/Custom/DataItem[@Name=\"LastName Row 0, Not sorted.\"]").Text;
            //Assert.AreEqual("Jobs", lastName);
            driverDesktop.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Assert.AreEqual("Steve Jobs", firstName + " " + lastName);

        }

    }
}


