

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.WebRequestMethods;

namespace SeleniumTests
{
    public class SeleniumTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
           driver.Quit();
        }

        [Test]
        public void TestChechFirstContactIsSteveJobs()
        //Navigate to "Contacts" and assert that the first contact is "Steve Jobs" (10 score).
        {
            driver.Url = "http://localhost:8080";
            var contactsLink = driver.FindElement(By.XPath("//a[@href='/contacts']"));
            contactsLink.Click();
            var firstColoum = driver.FindElement(By.CssSelector("#contact1 > tbody > tr.fname > td")).Text;
            Assert.AreEqual("Steve", firstColoum);
        }

        [Test]
        public void TestCheckKeyWordAlbert()
        //Search tasks by keyword "albert" and assert that the first result first result holds "Albert Einstein" (6 score).
        {
            driver.Url = "http://localhost:8080";
            var searchLink = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a"));
            searchLink.Click();
            var Keyword = driver.FindElement(By.CssSelector("input#keyword"));
            Keyword.SendKeys("albert");
            var buttonSearch = driver.FindElement(By.CssSelector("button#search"));
            buttonSearch.Click();
            var firstName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("#contact3 > tbody > tr.lname > td")).Text;
            Assert.AreEqual("Albert Einstein", firstName + " " + lastName);
        }

        [Test]
        public void TestCheckkeywordMissingRandnum()
        //Search contacts by keyword "missing{randnum}" and assert that there is no such contact (5 score).
        {
            driver.Url = "http://localhost:8080";
            var searchLink = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(4) > a"));
            searchLink.Click();
            var Keyword = driver.FindElement(By.CssSelector("input#keyword"));
            Keyword.SendKeys("missing{randnum}");
            var buttonSearch = driver.FindElement(By.CssSelector("button#search"));
            buttonSearch.Click();
            var result = driver.FindElement(By.CssSelector("div#searchResult")).Text;
            Assert.AreEqual("No contacts found.", result);
        }

        [Test]
        public void TestCreateInvalidContact()
        //Try to create a new invalid contact, for example without name, and assert an error is returned (5 score).
        {
            driver.Url = "http://localhost:8080";
            var craeteLink = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            craeteLink.Click();
            var firstName = driver.FindElement(By.CssSelector("input#firstName"));
            firstName.SendKeys("");
            var createButton = driver.FindElement(By.CssSelector("button#create"));
            createButton.Click();
            var errorMessage = driver.FindElement(By.CssSelector("body > main > div")).Text;
            Assert.AreEqual("Error: First name cannot be empty!", errorMessage);
        }

        [Test]
        public void TestCreateNewContactAndChectItListedLast()
        //Create a new contact, holding valid data (first name, last name, email), and assert that the new contact is added and listed last in the contact book (14 score).
        {
            driver.Url = "http://localhost:8080";
            var craeteLink = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            craeteLink.Click();
            var firstName = driver.FindElement(By.CssSelector("input#firstName"));
            firstName.SendKeys("Ivan");
            var lastName = driver.FindElement(By.CssSelector("input#lastName"));
            lastName.SendKeys("Ivanov");
            var email = driver.FindElement(By.CssSelector("input#email"));
            email.SendKeys("i.ivanov@gmail.com");
            var phoneNumber = driver.FindElement(By.CssSelector("input#phone"));
            phoneNumber.SendKeys("+359 123456789");
            var comment = driver.FindElement(By.CssSelector("textarea#comments"));
            comment.SendKeys("Ivan lives in Sofia");
            var createButton = driver.FindElement(By.CssSelector("button#create"));
            createButton.Click();
            var contactGrid = driver.FindElements(By.CssSelector("body > main > div > a")).Last();
            var LastContactFirstName = contactGrid.FindElement(By.CssSelector("table > tbody > tr.fname > td")).Text;
            var LastContactLastName = contactGrid.FindElement(By.CssSelector("table > tbody > tr.lname > td")).Text;
            Assert.AreEqual("Ivan Ivanov", LastContactFirstName+ " " + LastContactLastName);
        }
    }
}
