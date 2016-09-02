using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeingZeroNunitParallelTests
{
    public enum BrowserName
    {
        Firefox,
        Chrome,
        IE
    }

    public class TestBase
    {
        public IWebDriver driver;
        
        public void setup(BrowserName Browser)
        {
            BrowserSelection(Browser);
            //driver = new FirefoxDriver();
            //driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

        public IWebDriver BrowserSelection(BrowserName Browser)
        {
            if (Browser == BrowserName.Chrome)
                return driver = new ChromeDriver();
            else if (Browser == BrowserName.Firefox)
                return driver = new FirefoxDriver();
            else if (Browser == BrowserName.IE)
                return driver = new InternetExplorerDriver();
            else
                return driver = new ChromeDriver();
        }

        public static IEnumerable<BrowserName> BrowsersToRunWith()
        {
        BrowserName[] browsers = { BrowserName.Chrome, };

        foreach (BrowserName b in browsers)
	    {
                yield return b;
	    }
            
        //yield return b;
        }

    }
}
