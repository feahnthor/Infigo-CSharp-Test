using System.Collections.Generic;
using OpenQA.Selenium;
using Infigo_api_sucks_solution.Config;
using System.Threading.Tasks;
using Infigo_api_sucks_solution.Helpers;
using Infigo_api_sucks_solution.Models;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace Infigo_api_sucks_solution.Pages
{
    public class Variants
    {
        public Dictionary<string, ProductJsonModel> productDict { get; set; } // accepts the json data a dictionary
        public ProductJsonModel products { get; set; }
        private WebDriverHelper wh = new WebDriverHelper();
        //private List<IWebDriver> WebDrivers { get; set; } // private so it can only be updated in this scope

        public bool IsComplete { get; set; }
        public void AddAttributes(List<IWebDriver> webDrivers)
        {
            foreach (IWebDriver driver in webDrivers)
            {
                Task addAttributes = Task.Run(() =>
                {
                    ProductJsonModel productData = productDict[driver.CurrentWindowHandle]; //data we need to use

                    GoToVariant(driver);
                    var attributesTab = driver.FindElements(By.XPath(Locators.variantHomeTags));
                    attributesTab[2].Click();
                    new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(Locators.variantAttributeBox)));
                    new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(Locators.variantAttributeBox)));
                    new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.ClassName("t-grid-add")));
                    IWebElement addAttribute = wh.GetText("Add new record", "a", driver);
                    IWebElement testAdd = driver.FindElement(By.ClassName("t-grid-add"));
                    testAdd.Click();
                    addAttribute.Click();
                    IWebElement newAttributeName = wh.GetText("Canvas", "span", driver);
                    newAttributeName.SendKeys("testMe");
                    wh.Write("Surface", "Canvas", driver, tagName: "span");
                    IWebElement insertButton = wh.GetText("Insert", "a", driver);
                    insertButton.Click();
                });
                addAttributes.Wait();
            }
        }

        public void GoToVariant(IWebDriver driver)
        {
            ProductJsonModel productData = productDict[driver.CurrentWindowHandle];
            driver.Navigate().GoToUrl($"{Constants.VariantsPageUrl}{productData.ProductId}");
            new WebDriverWait(driver, TimeSpan.FromSeconds(40)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Locators.variantHomeTags)));

        }




    }
}

