﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace GloboChallenge.Pages
{
    public class BasePage
    {
        private static readonly int WAIT_ELEMENT_SECONDS = 20;
        private readonly IWebDriver driver;

        

        public BasePage()
        {
            driver = (IWebDriver)ScenarioContext.Current["driver"];
        }

        public void WaitElement(By path)
        {
            var Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_ELEMENT_SECONDS));
            var element = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.PresenceOfAllElementsLocatedBy(path));
        }

        public bool WaitElementVisible(By path)
        {
            var Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_ELEMENT_SECONDS));
            var element = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(path));

            return ToLocate(path).Displayed;
        }

        public bool WaitElementIsClickabe(By path)
        {
            var Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_ELEMENT_SECONDS));
            var element = Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(path));

            return ToLocate(path).Displayed && ToLocate(path).Enabled;
        }

        public IWebElement ToLocate(By path)
        {
            return driver.FindElement(path);
        }

        public void ToClick(By path)
        {
            WaitElementVisible(path);
            WaitElementIsClickabe(path);
            ToLocate(path).Click();
        }

        public void ToWrite(By path, string text)
        {
            WaitElementVisible(path);
            ToLocate(path).Clear();
            ToLocate(path).SendKeys(text);
        }

        public string GetText(By path)
        {
            WaitElementVisible(path);
            return ToLocate(path).Text;
        }

        public void ToNavigate(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public void ToSelectByText(By path, String text)
        {
            WaitElementVisible(path);
            new SelectElement(ToLocate(path)).SelectByText(text);

        }

    }
}
