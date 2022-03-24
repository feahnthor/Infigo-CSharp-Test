using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Infigo_api_sucks_solution.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; // https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete
using Infigo_api_sucks_solution.Helpers;
using Infigo_api_sucks_solution.Models;
using System.IO;
using Infigo_api_sucks_solution;

namespace Infigo_api_sucks_solution.Pages
{
    public class BaseProduct
    {
        public Dictionary<string, ProductJsonModel> productDict { get; set; } // accepts the json data a dictionary
        public ProductJsonModel products { get; set; }
        private List<IWebDriver> WebDrivers { get; set; } // private so it can only be updated in this scope
        private WebDriverHelper wh = new WebDriverHelper();
        
        public bool IsComplete { get; set; }

        #region UpdateSeo
        public async Task UpdateSeo(List<IWebDriver> webDrivers) // not making this entire function async allows for it to be called synchronously
        {
            WebDrivers = webDrivers;


            foreach (var driver in WebDrivers)
            {
                
                Task updateSeo = Task.Run(() =>
                {
                    // use write() or this.Text() to make sure each instnace is called
                    // XPATH use https://coolcheatsheet.com/toolkit/xpath#:~:text=Order%20selectors%20%20%20%20Xpath%20%20,%5B1%5D%20%7C%20li%5B%40%23id%3Afirst-child%20%202%20more%20rows%20
                    // Find element using text https://stackoverflow.com/questions/38661830/find-element-with-selenium-by-display-text
                    
                    ProductJsonModel productData = productDict[driver.CurrentWindowHandle];
                    
                    //try
                    //{
                    try
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Locators.productTabs)));
                        var tabs = wh.GetText("SEO", "a", driver); // Finds the string SEO using the closest node in the DOM which is the anchor(a) tag
                        tabs.Click(); // SEO tab
                    }
                    catch (Exception ex) // Need to create logger email notification
                    {
                        driver.Navigate().Refresh();
                        new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Locators.productTabs)));
                        var tabs = wh.GetText("SEO", "a", driver); // Finds the string SEO using the closest node in the DOM which is the anchor(a) tag
                        tabs.Click(); // SEO tab
                    }
                    string productType;
                    string keywordType;
                        

                    if (!productData.Name.Contains("Floor")){ productType = Locators.seoBackdrop; keywordType = Locators.seoBackground; }
                    else {productType = Locators.seoFloor;  keywordType = Locators.seoFloor; }

                    IWebElement metaTitle = wh.GetText(Locators.seoMetaTitle, "label", driver);
                    IWebElement metaDescription = wh.GetText(Locators.seoMetaDescription, "label", driver);
                    driver.FindElement(By.XPath("//label[contains(., 'Meta description')]/following::textarea"));
                    //CategoryDetail response = api.
                    string metaTitleText = $"{productData.Name} - {Locators.seoPhotography} {productType}";
                    string desinger = wh.GetDesigner(productData.CategoryIds, productData);
                    
                    string metaDescriptionText = $"{productData.Tags[1]} {productData.Tags[0]} {Locators.seoPhotography} {productType} by {desinger} For Sale - {productData.Name}";
                    string seoFriendlyPageNameText = $"{productData.Name} - {Locators.seoPhotography} {productType}";
                    string metaKeywordText = $"{keywordType},{productType},Large,{Locators.seoPhotography} {Locators.seoBackdrop}";
                    string titleText = $"{productData.Name} Backgroundtown {productType}";

                    wh.WriteUsingElement(metaTitleText, driver, metaTitle);
                    wh.WriteUsingElement(metaDescriptionText, driver, metaDescription, tagName: "textarea");

                    wh.Write(seoFriendlyPageNameText, Locators.seoPageName, driver);
                    wh.Write(metaKeywordText, Locators.seoMetaKeywords, driver);
                    wh.Write(titleText, Locators.seoGraphDataTitle, driver);

                    try
                    {
                        wh.SaveAndContinue(driver);
                        //IsComplete = true;
                        File.Move($"{Variables.bgtProcessingFolder}{productData.Name}_{productData.ProductId}.json", $"{Variables.bgtDoneFolder}{productData.Name}_{productData.ProductId}.json");
                    }
                    catch (Exception)
                    {
                        File.Move($"{Variables.bgtProcessingFolder}{productData.Name}_{productData.ProductId}.json", $"{Variables.bgtErrorFolder}{productData.Name}_{productData.ProductId}.json");

                        throw;
                    }
                    
                    //GoToVariant(driver);
                    //var attributesTab = driver.FindElements(By.XPath(Locators.variantHomeTags));
                    //attributesTab[2].Click();
                    //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(Locators.variantAttributeBox)));
                    //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(Locators.variantAttributeBox)));
                    //IWebElement addAttribute = wh.GetText("Add new record", "a", driver);
                    //addAttribute.Click();
                    //IWebElement newAttributeName = wh.GetText("Canvas", "span", driver);
                    //newAttributeName.SendKeys("testMe");
                    //wh.Write("Surface", "Canvas", driver, tagName: "span");
                    //IWebElement insertButton = wh.GetText("Insert", "a", driver);
                    //insertButton.Click();
                        
                        


                   
                        

                    
                    //catch (Exception)
                    //{
                    //    File.Move($"{Variables.bgtProcessingFolder}{productData.Name}_{productData.ProductId}.json", $"{Variables.bgtErrorFolder}{productData.Name}_{productData.ProductId}.json");
                    //    throw;
                    //    //driver.Navigate().Refresh(); 
                    //    //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(Locators.productTabs)));
                    //    //var tabs = driver.FindElements(By.XPath(Locators.productTabs));
                    //    //tabs[1].Click();
                    //    //wh.Write("Retry Meta Title", "Meta title", driver);
                    //    //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementToBeClickable(tabs));
                    //    //tabs.Click(); // SEO tab
                    //    //throw;
                    //}
                    
                });
                //wh.SaveAndContinue(driver)
                //updateSeo.Wait();
            }


        }
        #endregion

        /// <summary>
        /// Will add tags to products
        /// </summary>
        /// <param name="driver"></param>
        public void AddTags(IWebElement driver)
        {

        }

        public void GoToVariant(IWebDriver driver)
        {
            ProductJsonModel productData = productDict[driver.CurrentWindowHandle];
            driver.Navigate().GoToUrl($"{Constants.VariantsPageUrl}{productData.ProductId}");
            new WebDriverWait(driver, TimeSpan.FromSeconds(40)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath(Locators.variantHomeTags)));

        }

        
    }

}
