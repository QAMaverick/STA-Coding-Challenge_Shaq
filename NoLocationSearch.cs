using System;


namespace STACodingChallenge
{
    [TestFixture]
    public class NoLocationSearch
    {
        private IWebDriver driver;

        [SetUp]
        //Browser setup before tests are ran
        public void BrowserSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");
            driver = new ChromeDriver(options);
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                driver.Navigate().GoToUrl("https://tfl.gov.uk/plan-a-journey/");
                Assert.That(driver.Url, Is.EqualTo("https://tfl.gov.uk/plan-a-journey/"));
                Thread.Sleep(1000);
                driver.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline")).Click();
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void blankJourneyFields()
        {

            //Run Search with blank fields
            driver.FindElement(By.Id("plan-journey-button")).Click();

            //Assertion to check if appropriate validation messages are shown
            IWebElement blankFromField = driver.FindElement(By.Id("InputFrom-error"));
            ClassicAssert.IsTrue(blankFromField.Displayed);
            string bff = blankFromField.Text;
            Assert.That(bff, Is.EquivalentTo("The From field is required."));

            IWebElement blankToField = driver.FindElement(By.Id("InputTo-error"));
            ClassicAssert.IsTrue(blankToField.Displayed);
            string btf = blankToField.Text;
            Assert.That(btf, Is.EquivalentTo("The To field is required."));

        }            
    }
}