using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeingZeroNunitParallelTests
{
    public class TestBase
    {
        public IWebDriver driver;
        [SetUp]
        public void setup()
        {
            driver = new FirefoxDriver();
            //driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }


        [TearDown]
        public void Close()
        {
            driver.Quit();
        }

    }
}
