using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BeingZeroNunitParallelTests
{
    [TestFixture]
    [Parallelizable]
    public class WebTablesTest : TestBase
    {
        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void Gmail2Tabs(BrowserName browsers)
        {
            setup(browsers);

            driver.Url = "https://accounts.google.com/SignUp?continue=https%3A%2F%2Fwww.google.co.in%2F&hl=en";

            string parent_window = driver.CurrentWindowHandle;
            Console.WriteLine("Parent Window: "+parent_window);
            driver.FindElement(By.LinkText("Learn more")).Click();

            foreach (string window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);
                Console.WriteLine("All Windows: " + window);
                Thread.Sleep(5000);
            }

            Assert.True(driver.Url.Contains("1733224"));
            driver.Close();
            Thread.Sleep(5000);
            driver.SwitchTo().Window(parent_window);
            Assert.True(driver.Url.Contains("SignUp"));

            Thread.Sleep(5000);
        }


        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void CloseAllNaukriPopupsExceptMainPage(BrowserName browsers)
        {
            setup(browsers);
            driver.Url = "http://www.naukri.com";

            string parent_window = driver.CurrentWindowHandle;

            foreach (string window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);
                Console.WriteLine("All Windows:" + window);
                //Console.WriteLine();
                if (!window.Equals(parent_window))
                {
                    Console.WriteLine("Window Closed: " + window);
                    driver.Close();
                }
                 
            }
            Thread.Sleep(6000);
        }


        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void CloseAllWindowsExpectHomePage(BrowserName browsers)
        {
            setup(browsers);

            driver.Url = "http://www.naukri.com";

            for (int i = 1; i <= 5; i++)
            { 
            driver.FindElement(By.XPath("//div[@class=\"headGNBWrap\"]/div/ul/li["+i+"]/a")).Click();
            }
            Thread.Sleep(5000);


            String parent_window = driver.CurrentWindowHandle;

            foreach (String window in driver.WindowHandles)
            {
                driver.SwitchTo().Window(window);

                if (!window.Equals(parent_window))
                {
                    Console.WriteLine("Closed Windows Title: " + driver.Title);
                    driver.Close();
                    Thread.Sleep(2000);

                }
                
            }

            Assert.True(driver.Title.Equals(" Jobs - Recruitment - Job Search - Employment - Job Vacancies - Naukri.com "));

        }



        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void Test(BrowserName browsers)
        {
            setup(browsers);

            driver.Url = "http://www.ah.nl/";
            driver.Manage().Window.Maximize();
            driver.FindElement(By.XPath("//*[@id='mCSB_1_container']/div/div[2]/form/div/input")).Click();

        }
        

        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        [Order(3)]
        public void AddRandomRowsTest(BrowserName browsers)
        {
            setup(browsers);

            driver.Url = "http://beingzero.in/selenium-webtables";

            IWebElement table = driver.FindElement(By.Id("dataTable"));
            int initRowCount = table.FindElements(By.TagName("tr")).Count;
            Console.WriteLine("Initial Rows available: " +initRowCount);

            Random rand = new Random();
            int n = rand.Next(10, 21);
            Console.WriteLine("Randow Rows to be added: " + n);

            IWebElement AddBtn = driver.FindElement(By.Id("addRow"));

            for (int i=1; i<=n; i++)
            {
                AddBtn.Click();
            }

            int finalRowCount = table.FindElements(By.TagName("tr")).Count;
            Console.WriteLine("Final Rows available after addition of rows:" + finalRowCount);

            Assert.AreEqual(n, finalRowCount - initRowCount);
            
        }

        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        [Order(2)]
        public void DeleteRowsTest(BrowserName browsers)
        {
            setup(browsers);
            // Opening the Page
            driver.Url = "http://beingzero.in/selenium-webtables";

            // Counting the
            IWebElement table = driver.FindElement(By.Id("dataTable"));

            int initRowCount = table.FindElements(By.TagName("tr")).Count;

            // Count Random Rows to be added
            Random rand1 = new Random();
            int n = rand1.Next(10, 21);
            //Console.WriteLine("Randow Rows to be added: " + n);

            // Click Add button for each New Row
            IWebElement AddBtn = driver.FindElement(By.Id("addRow"));

            for (int i = 1; i <= n; i++)
            {
                AddBtn.Click();
            }

            //Randow Rows to be deleted
            Random rand2 = new Random();
            int m = rand2.Next(10, n+1);
            Console.WriteLine("Initial Row: " +initRowCount);
            Console.WriteLine("N (Newly Added Row) is: " +n);
            Console.WriteLine("M (To be Deleted Row) is: " +m);

            // Click the Check Box of rows to be deleted
            
            for (int j = 1; j <= m; j++)
            {

                if (!driver.FindElement(By.XPath("//*[@id='dataTable']/tbody/tr[" + j + "]/td[1]/input")).Selected)
                {
                    Thread.Sleep(1000);
                    //Console.WriteLine("Just Before the Click of Checkbox");
                    IWebElement Checkbox = driver.FindElement(By.XPath("//*[@id='dataTable']/tbody/tr[" + j + "]/td[1]/input"));

                    IJavaScriptExecutor js = driver as IJavaScriptExecutor;

                    //Console.WriteLine("Just Before the JS Execution Click");
                    js.ExecuteScript("arguments[0].click()", Checkbox);

                    Console.WriteLine("Deleted j: " + j);
                }

                
            }

            //Click on Delete Rows button
            driver.FindElement(By.Id("deleteRow")).Click();
            Thread.Sleep(2000);

            // Asser to check the current num of rows in the screen, should be equal to n-m
            int finalRowCount = table.FindElements(By.TagName("tr")).Count;
            Console.WriteLine("Final Row Count is:" +finalRowCount);
            Assert.AreEqual(finalRowCount, initRowCount + n - m);

        }

        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        [Order(1)]
        public void DeleteAllRows(BrowserName browsers)
        {
            setup(browsers);
            driver.Url = "http://beingzero.in/selenium-webtables";

            IWebElement table = driver.FindElement(By.Id("dataTable"));
            int initRowCount = table.FindElements(By.TagName("tr")).Count;
            Console.WriteLine("Initial Rows available: " + initRowCount);

            Random rand = new Random();
            int n = rand.Next(10, 21);
            Console.WriteLine("Randow Rows to be added: " + n);

            IWebElement AddBtn = driver.FindElement(By.Id("addRow"));

            for (int i = 1; i <= n; i++)
            {
                AddBtn.Click();
            }

            int CurrentRowCount = table.FindElements(By.TagName("tr")).Count;
            for (int j = 1; j <= CurrentRowCount ; j++)
            {
                Thread.Sleep(1000);
                IWebElement checkBox = driver.FindElement(By.XPath("//*[@id='dataTable']/tbody/tr[" + j + "]/td[1]/input"));

                if (!checkBox.Selected)
                {

                    IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                    //Console.WriteLine("Just Before the JS Execution Click");
                    js.ExecuteScript("arguments[0].click()", checkBox);
                }
                
                    
            }

            driver.FindElement(By.Id("deleteRow")).Click();
            Thread.Sleep(2000);

            // Asser to check the current num of rows in the screen, should be equal to n-m
            int finalRowCount = table.FindElements(By.TagName("tr")).Count;
            Console.WriteLine("Final Row Count is:" + finalRowCount);
            Assert.AreEqual(finalRowCount, 0);


        }

       
    }
}
