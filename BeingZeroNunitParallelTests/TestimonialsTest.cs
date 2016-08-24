using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeingZeroNunitParallelTests
{
    [TestFixture]
    [Parallelizable]
    public class TestimonialsTest : TestBase
    {
        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
       
        public void TestimonialsCountTest(BrowserName browsers)
        {
            setup(browsers);
            driver.Url = "http://beingzero.in/testimonials";

            IReadOnlyCollection<IWebElement> testimonials = driver.FindElements(By.ClassName("testimonial-item"));
            Console.WriteLine("Total Testimonials Count are:" + testimonials.Count);

            Assert.Greater(testimonials.Count, 10);
        }

        [Test]
        [TestCaseSource(typeof(TestBase), "BrowsersToRunWith")]
        public void QuestionsCountTest(BrowserName browsers)
        {
            setup(browsers);
            driver.Url = "http://beingzero.in/java-quiz/arrays-quiz";
            IReadOnlyCollection<IWebElement> Questions = driver.FindElements(By.ClassName("mtq_question.mtq_scroll_item-1"));

            Console.WriteLine("Total Questions Count are:" + Questions.Count);

            Assert.Greater(Questions.Count, 10);

        }

    }
}
