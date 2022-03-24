using Infigo_api_sucks_solution.Config;
using OpenQA.Selenium;
using Infigo_api_sucks_solution.Models;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; // https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
namespace Infigo_api_sucks_solution.Helpers
{
    internal class WebDriverHelper
    {
        public void SaveAndContinue(IWebDriver driver)
        {
            IWebElement saveAndContinueBtn = GetText("Save and Continue Edit", "button", driver);
            saveAndContinueBtn.Click();
            
        }

        /// <summary>
        /// Search for any text using element tag for more precision, the more precise the string the easier to find the correct node
        /// https://stackoverflow.com/questions/5074469/xpath-find-text-in-any-text-node
        /// </summary>
        /// <param name="value">text string to search for</param>
        /// <param name="tagName">direct tag that encapsulates the text</param>
        /// <param name="driver">current webdriver</param>
        /// <returns>Webdriver element based off text found</returns>
        public IWebElement GetText(string value, string tagName, IWebDriver driver) // returns element based off text
        {
            /***
             * Param: tagName - in the example here the tagName is "label"  ex: <label for="ShowProductInSearch">Show in Search Result</label>
            ***/
            return driver.FindElement(By.XPath($"//{tagName}[text() = '{value}']"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueToWrite">string to write into field</param>
        /// <param name="valueToSearchFor">text string to search for</param>
        /// <param name="driver">current webdriver</param>
        /// <param name="isTextArea">replaces fieldTagName, all you would need to do is not enter a value if the field is an input field, else make this true</param>
        public void Write(string valueToWrite, string valueToSearchFor, IWebDriver driver, bool isTextArea = false, string tagName = "input")
        {
            /***
             
              
             write("Hello World", "Meta description", driver, true);  // Examples apply to the SEO section of a product
             write("Hello World", "Meta title", driver);

            Note: this is a wrapper function, if this does not work calling the original method that does not allow text searching would work.
            ex: driver.FindElement(By.Id("SeoMetaData_MetaTitle").SendKeys("Hello Worold");
            ***/
            IWebElement element; // declares variable that is expected to be of type IWebElement
            switch (tagName)
            {
                case "textarea":
                    element = driver.FindElement(By.XPath($"//label[contains(., '{valueToSearchFor}')]/following::textarea")); // assumption is that all infigo input fields come directly after a label
                    break;
                case "span":
                    element = driver.FindElement(By.XPath($"//label[contains(., '{valueToSearchFor}')]/following::textarea")); // assumption is that all infigo input fields come directly after a label
                    break;

                default:
                    element = driver.FindElement(By.XPath($"//label[contains(., '{valueToSearchFor}')]/following::input[1]"));
                    break;
            }
            string filedText = element.GetAttribute("value"); // get current value in the text field
            if (!string.IsNullOrEmpty(filedText))
            {
                element.Clear();
            }
            element.SendKeys(valueToWrite);
        }

        /// <summary>
        /// This is a wrapper that allows you to use a IWebdriver element returned to after searching for the text that is to the left of the Field you want to write you.
        /// </summary>
        /// <param name="valueToWrite">string to write into field</param>
        /// <param name="driver">current webdriver</param>
        /// <param name="element">webdriver element that is found by GetText() that can be taken from either a call from GetText(...) or driver.FindElement(...)</param>
        /// <param name="tagName">direct tag that encapsulate the actaul field for the text to be written. "input" is default, "textarea" is another. this method allows for unseen tags that Write(...) does not</param>
        public void WriteUsingElement(string valueToWrite, IWebDriver driver, IWebElement element, string tagName = "input")
        {
            /***
             
             *  This is a wrapper that tries to reduce the amount of code used to write a statement. If there is a field that does not work
             
                example with default tagName of <input>:
                    IWebElement metaTitle = GetText("Meta title", "label", driver);
                    writeUsingElement("Hello World", driver, metaTitle);           
                   
                Example with tagName of <textarea>
                IWebElement metaDescription = GetText("Meta description", "label", driver);
                writeUsingElement("Steph Was Here", driver, metaDescription, tagName: "textarea");
            ***/
            IWebElement field;
            field = driver.FindElement(RelativeBy.WithLocator(By.TagName(tagName)).RightOf(element));
            string filedText = field.GetAttribute("value"); // get current value in the text field
            if (!string.IsNullOrEmpty(filedText))
            {
                field.Clear();
            }
            field.SendKeys(valueToWrite);
        }

        /// <summary>
        /// Gets the Backgroundtown designer based off code
        /// </summary>
        /// <param name="categoryList">List of category ids, this list contains strings</param>
        /// <param name="productData">currently unused</param>
        /// <returns></returns>
        public string GetDesigner(string[] categoryList, ProductJsonModel productData)
        {
            string designer = "";
            foreach (string categoryId in categoryList)
            {
                switch (categoryId)
                {
                    case "13":
                        designer = "ACI Collection";
                        break;
                    case "15":
                        designer = "Annie Marie";
                        break;
                    case "20":
                        designer = "Cherri Hammon";
                        break;
                    case "21":
                        designer = "Christie Newell";
                        break;
                    case "22":
                        designer = "Cindy Romano";
                        break;
                    case "23":
                        designer = "Dennis Hammon";
                        break;
                    case "24":
                        designer = "Jeni B";
                        break;
                    case "25":
                        designer = "Mark Lane";
                        break;
                    case "27":
                        designer = "Tessa Cole";
                        break;
                    case "193":
                        designer = "Carrie Perez";
                        break;
                    case "196":
                        designer = "Chris Garcia";
                        break;
                    case "199":
                        designer = "Thom Rouse";
                        break;
                    case "205":
                        designer = "Sherlyn Edwards";
                        break;
                    case "235":
                        designer = "Boler";
                        break;
                    case "243":
                        designer = "Serendipity";
                        break;
                    case "247":
                        designer = "Michelle Garcia";
                        break;


                }

            }
            return designer;
        }
    }
}
