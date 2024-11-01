using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;

namespace STACodingChallenge
{
    [TestFixture]
    public class ValidJourneyAndResults
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
        //Clears down instance of ChromeDriver once tests are ran
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void A1_enterJourneyLocations()
        {
            //Entering Leicester Square Underground Station value in From field
            driver.FindElement(By.Id("InputFrom")).SendKeys("Leicester Square ");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("InputFrom")).SendKeys(Keys.ArrowDown + Keys.Enter);

            //Entering Covent Garden Underground Station value in To Field
            driver.FindElement(By.Id("InputTo")).SendKeys("Covent Garden ");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("InputTo")).SendKeys(Keys.ArrowDown + Keys.Enter);

            //Run Search
            driver.FindElement(By.Id("plan-journey-button")).Click();
            //Checks URL for correct request
            Assert.That(driver.Url, Is.EqualTo("https://tfl.gov.uk/plan-a-journey/results?InputFrom=Leicester+Square+Underground+Station&FromId=1000135&InputTo=Covent+Garden+Underground+Station&ToId=1000056"));
            //Waits for page to fully load before assertion by waiting until loader anim is not visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => !d.FindElement(By.Id("loader-window-wrapper")).Displayed);
            //Assertion to check if results is visible
            IWebElement validJourney = driver.FindElement(By.CssSelector("#full-width-content > div > div:nth-child(7) > div"));
            ClassicAssert.IsTrue(validJourney.Displayed);
        }

        [Test]
        public void A2_confirmJourneyResults()
        {
            //Entering Leicester Square Underground Station value in From field
            driver.FindElement(By.Id("InputFrom")).SendKeys("Leicester Square ");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("InputFrom")).SendKeys(Keys.ArrowDown + Keys.Enter);

            //Entering Covent Garden Underground Station value in To Field
            driver.FindElement(By.Id("InputTo")).SendKeys("Covent Garden ");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("InputTo")).SendKeys(Keys.ArrowDown + Keys.Enter);

            //Run Search
            driver.FindElement(By.Id("plan-journey-button")).Click();
            //Checks URL for correct request
            Assert.That(driver.Url, Is.EqualTo("https://tfl.gov.uk/plan-a-journey/results?InputFrom=Leicester+Square+Underground+Station&FromId=1000135&InputTo=Covent+Garden+Underground+Station&ToId=1000056"));

            //Waits for page to fully load before assertion by waiting until loader anim is not visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(d => !d.FindElement(By.Id("loader-window-wrapper")).Displayed);

            //Getting Cycling and Walking times and logging into Debug 
            IWebElement cyclingResults = driver.FindElement(By.CssSelector("#full-width-content > div > div:nth-child(7) > div > div.journey-row-container.left-journey-options > a.journey-box.cycling > div.two-col > div.col2.journey-info"));
            string cResults = cyclingResults.Text;
            IWebElement walkingResults = driver.FindElement(By.CssSelector("#full-width-content > div > div:nth-child(7) > div > div.journey-row-container.left-journey-options > a.journey-box.walking > div.two-col > div.col2.journey-info"));
            string wResults = walkingResults.Text;

            Debug.WriteLine("Cycling Time = " + cResults);
            Debug.WriteLine("Walking Time = " + wResults);

            IWebElement results = driver.FindElement(By.CssSelector("#full-width-content > div > div:nth-child(7) > div"));

            // Find all <p> tags in results element
            IList<IWebElement> pTags = results.FindElements(By.TagName("p"));

            // Iterate through the <p> tags and extract text
            foreach (IWebElement pTag in pTags)
            {
                string pText = pTag.Text;
                Debug.WriteLine(pText);
            }

        }
    }
}