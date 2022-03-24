using System;
using System.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;

namespace Infigo_api_sucks_solution
{
    /// <summary>
    /// Implements interface IDriverSetup 
    /// </summary>
    public class DriverSetup : IDriverSetup
    {
        private IWebDriver? driver; // this variable must contain a type of IWebDriver, IWebDriver is the interface OpenQA.Selenium uses

        /// <summary>
        /// Creates a new driver, type of drive will be determined by browser name. Driver instance will be stored in driver and returned by GetDriver
        /// </summary>
        /// <param name="browsername">browser to use, ex: firefox,chrome,edge</param>
        /// <param name="headless">option to make the driver not visible to users. STATUS: NOT IMPLEMENTED</param>
        /// <param name="url">not much to explain</param>
        void IDriverSetup.InitializeDriver(string browsername, string? headless, string url)
        {

            switch (browsername.ToLower())
            {
                case "chrome":
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true; // hides command prompt window https://stackoverflow.com/questions/53218843/stop-chromedriver-console-window-from-appearing-selenium-c-sharp
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig()); // should install a new chromedriver if there is an update
                                        var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("--no-sandbox", "--disable-web-security", "--disable-gpu", "--hide-scrollbars", "window-size=1920,1080");
                    driver = new ChromeDriver(chromeDriverService, chromeOptions); // driver does not need IWebDriver as its already been set to have the type IWebDriver above
                    driver.Url = url;
                    //driver.Manage().Window.Maximize();
                    break;
                case "firefox":
                    var firefoxDriverService = FirefoxDriverService.CreateDefaultService();
                    firefoxDriverService.HideCommandPromptWindow = true;
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("--no-sandbox", "--disable-web-security", "--disable-gpu", "--hide-scrollbars", "window-size=1920,1080");
                    driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);
                    driver.Url = url;
                    //driver.Manage().Window.Maximize();
                    break;
                case "edge":
                    var edgeDriverService = EdgeDriverService.CreateDefaultService();
                    edgeDriverService.HideCommandPromptWindow = true;
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();            
                    edgeOptions.AddArguments("--no-sandbox", "--disable-web-security", "--disable-gpu", "--hide-scrollbars", "window-size=1920,1080", "headless");
                    driver = new EdgeDriver(edgeDriverService, edgeOptions);
                    driver.Url = url;
                    //driver.Manage().Window.Maximize();
                    //driver.Manage().Window.Size()
                    break;
                case "internet explorer":
                    var ieDriverService = InternetExplorerDriverService.CreateDefaultService();
                    ieDriverService.HideCommandPromptWindow = true; // there is no AddArguments() method for InternetExplorerOptions as there are for the other browserss
                    driver = new InternetExplorerDriver();
                    driver.Url = url;
                    driver.Manage().Window.Maximize();
                    break;
                    
                default:
                    MessageBox.Show($"{browsername} is not a valid browser");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>driver instance</returns>
        public IWebDriver GetDriver() // return driver instace, if called and stored, we can effect mutiple browsers
        {
            return driver;
        }
    }
    }
