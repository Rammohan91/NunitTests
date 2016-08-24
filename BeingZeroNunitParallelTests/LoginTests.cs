using NUnit.Framework;
using OpenQA.Selenium;
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
    public class LoginTests : TestBase
    {
        [Test]
        public void ValidLogin()
        {

            driver.Url = "http://beingzero.in/selenium-login-form";
            driver.FindElement(By.Id("txtUsername")).SendKeys("beingzero");
            driver.FindElement(By.Id("pwdPassword")).SendKeys("beingzero");
            driver.FindElement(By.Id("btnLogin")).Click();

            //Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();


            //Thread.Sleep(2000);
            //Assert.That(alert.Text, Does.Match("SUCCESS"));
            Assert.AreEqual(alert.Text, "SUCCESS");
            
        }
        [Test]
        public void InValidLogin()
        {

            driver.Url = "http://beingzero.in/selenium-login-form";
            driver.FindElement(By.Id("txtUsername")).SendKeys("test");
            driver.FindElement(By.Id("pwdPassword")).SendKeys("test");
            driver.FindElement(By.Id("btnLogin")).Click();

            //Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();


            //Thread.Sleep(2000);
            //Assert.That(alert.Text, Does.Match("SUCCESS"));
            Assert.AreEqual(alert.Text, "FAILURE");

        }



    }
}
