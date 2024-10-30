using System;


namespace STACodingChallenge
{
    [TestFixture]
    public class InvalidResults
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
        public void invalidResults_OneInvLocal_OneValidLocal()
        {
            //Input nonsensible text in both fields to ensure an invalid result
            driver.FindElement(By.Id("InputFrom")).SendKeys("1234567");
            //Input valid location in field
            driver.FindElement(By.Id("InputTo")).SendKeys("Whitechapel");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("InputFrom")).SendKeys(Keys.ArrowDown + Keys.Enter);

            //Run Search
            driver.FindElement(By.Id("plan-journey-button")).Click();

            //wait for results
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.FindElement(By.Id("loader-window-wrapper")).Displayed);
            //Assertion to check if results is visible
            IWebElement invalidJourney = driver.FindElement(By.ClassName("field-validation-errors"));
            ClassicAssert.IsTrue(invalidJourney.Displayed);
            //Check if appropriate validation message is shown
            string invJourInnerText = invalidJourney.GetAttribute("innerHTML");
            Assert.That(invJourInnerText, Is.SameAs("Journey planner could not find any results to your search. Please try again"));
        }

        [Test]
        public void invalidResults_TwoInvLocals()
        {
            //Input nonsensible text in both fields to ensure an invalid result
            driver.FindElement(By.Id("InputFrom")).SendKeys("1234^&*=");
            driver.FindElement(By.Id("InputTo")).SendKeys("9876£$!^");

            //Run Search
            driver.FindElement(By.Id("plan-journey-button")).Click();

            //wait for results
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.FindElement(By.Id("loader-window-wrapper")).Displayed);
            //Assertion to check if results is visible
            IWebElement invalidJourney = driver.FindElement(By.ClassName("field-validation-errors"));
            ClassicAssert.IsTrue(invalidJourney.Displayed);
            //Assertion to check if valid results are not visible
            IWebElement validJourney = driver.FindElement(By.CssSelector("#full-width-content > div > div:nth-child(7) > div"));
            ClassicAssert.IsFalse(validJourney.Displayed);
        }
    }

}