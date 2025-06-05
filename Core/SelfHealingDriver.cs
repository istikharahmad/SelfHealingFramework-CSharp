// File: Core/SelfHealingDriver.cs
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelfHealingFramework.Core
{
    public class SelfHealingDriver
    {
        private readonly IWebDriver _driver;

        public SelfHealingDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FindElement(By primaryLocator, List<By> fallbackLocators = null)
        {
            try
            {
                return _driver.FindElement(primaryLocator);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine($"[WARNING] Primary locator failed: {primaryLocator}");

                if (fallbackLocators != null && fallbackLocators.Any())
                {
                    foreach (var fallback in fallbackLocators)
                    {
                        try
                        {
                            Console.WriteLine($"[INFO] Trying fallback: {fallback}");
                            return _driver.FindElement(fallback);
                        }
                        catch (NoSuchElementException)
                        {
                            continue;
                        }
                    }
                }

                throw new NoSuchElementException("Element not found with primary or fallback locators.");
            }
        }

        public void Quit() => _driver.Quit();
        public void Navigate(string url) => _driver.Navigate().GoToUrl(url);
    }
}

