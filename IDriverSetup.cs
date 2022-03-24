using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Infigo_api_sucks_solution
{
    internal interface IDriverSetup // This is a type of glossary that lets you know whichever class implements it MUST have the methods within it.
    {
        void InitializeDriver(string browsername, string headless, string url); // promise of InitilizeDriver() must return nothing

        IWebDriver GetDriver();
    }
}
