using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infigo_api_sucks_solution.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers; // https://stackoverflow.com/questions/49866334/c-sharp-selenium-expectedconditions-is-obsolete

namespace Infigo_api_sucks_solution.Pages
{
    public class Login
    {
        private List<IWebDriver> WebDrivers { get; set; }
        public void LogMeIn(List<IWebDriver> webDrivers) // Log into to each available Webdriver/Browser
        {

            this.WebDrivers = webDrivers;
            

            foreach (var driver in webDrivers)
            {
                Task login = Task.Run(() =>
                {
                    driver.FindElement(By.Id(Locators.emailId)).SendKeys(Constants.email);
                    driver.FindElement(By.Id(Locators.passwordId)).SendKeys(Constants.password);
                    driver.FindElement(By.ClassName(Locators.loginBtnClass)).Click();
                    
                    try
                    {
                        new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementToBeClickable(By.Id(Locators.adminBurgerId)));
                        //update log with successful access
                        driver.FindElement(By.Id(Locators.adminBurgerId)).Click();
                    }
                    catch (Exception)
                    {
                        driver.Navigate().Refresh();
                        new WebDriverWait(driver, TimeSpan.FromSeconds(60)).Until(ExpectedConditions.ElementToBeClickable(By.Id(Locators.adminBurgerId)));
                        driver.FindElement(By.Id(Locators.adminBurgerId)).Click();
                        throw;
                    }
                });

                login.Wait(1000);
                login.GetAwaiter().GetResult(); // Waits for each loging to be done
                // Asynchronously switches to a different url, so all browsers switch at together
            };
            
        }
    }
}
