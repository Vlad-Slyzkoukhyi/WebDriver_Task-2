using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriver_Task_2
{
    [TestFixture]
    public class WebDriverTask2Test
    {
        private ChromeDriver driver;
        private const string chromeUrl = "https://pastebin.com/";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CreateNewPaste()
        {
            driver.Navigate().GoToUrl(chromeUrl);

            IWebElement newPasteButton = driver.FindElement(By.ClassName("header__btn"));
            newPasteButton.Click();

            IWebElement postformTextField = driver.FindElement(By.Id("postform-text"));
            postformTextField.SendKeys("git config --global user.name  \"New Sheriff in Town\"\r\n" +
                "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\n" +
                "git push origin master --force\r\n");

            var syntaxHighlightingValue = "Bash";

            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0,500)", "");
            var dropDownSyntaxHighlightElement = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-format')]//span"));
            dropDownSyntaxHighlightElement.Click();

            var dropDownSyntaxHighlightOption = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option']"));
            dropDownSyntaxHighlightOption?.FirstOrDefault(x => x.Text.Equals(syntaxHighlightingValue))?.Click();

            var expirationValue = "10 Minutes";

            var dropDownExpirationElement = driver.FindElement(By.XPath("//*[contains(@class, 'field-postform-expiration')]//span"));
            dropDownExpirationElement.Click();

            var dropDownExpirationOption = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option']"));
            dropDownExpirationOption?.FirstOrDefault(x => x.Text.Equals(expirationValue))?.Click();

            IWebElement titleField = driver.FindElement(By.Id("postform-name"));
            titleField.SendKeys("how to gain dominance among developers");

            IWebElement submitButtonElement = driver.FindElement(By.CssSelector(".btn.-big"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButtonElement);
            submitButtonElement.Click();

            var checkTitleElement = driver.FindElement(By.ClassName("info-top")).Text;
            var expectedTitle = "how to gain dominance among developers";

            IWebElement divCodeTextElement = driver.FindElement(By.CssSelector("div.source.bash"));
            string textCodeElement = divCodeTextElement.Text;

            string expectedText = "git config --global user.name  \"New Sheriff in Town\"\r\n" +
                "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\r\n" +
                "git push origin master --force\r\n";

            Assert.That(textCodeElement, Is.EqualTo(expectedText), "The text content of the <div> element does not match the expected text.");
            Assert.That(checkTitleElement, Is.EqualTo(expectedTitle), "The created title does not match the expected title.");
        }

        [TearDown]
        public void Cleanup()
        {
            driver.Close();
            driver.Quit();
        }
    }
}