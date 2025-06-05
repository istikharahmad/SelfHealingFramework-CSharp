// File: Tests/LoginTests.cs
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SelfHealingFramework.Core;
using System.Collections.Generic;

namespace SelfHealingFramework.Tests
{
    public class LoginTests
    {
        private IWebDriver _originalDriver;
        private SelfHealingDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _originalDriver = new ChromeDriver();
            _driver = new SelfHealingDriver(_originalDriver);
        }

        [Test]
        public void SubmitLoginForm()
        {
            _driver.Navigate("https://example.com/login");

            var usernameField = _driver.FindElement(
                By.Id("username"),
                new List<By> {
                    By.Name("user"),
                    By.XPath("//input[contains(@placeholder,'Username')]")
                });

            usernameField.SendKeys("testuser");

            var passwordField = _driver.FindElement(
                By.Id("password"),
                new List<By> {
                    By.Name("pass"),
                    By.CssSelector("input[type='password']")
                });

            passwordField.SendKeys("password123");

            var loginButton = _driver.FindElement(
                By.Id("login-button"),
                new List<By> {
                    By.XPath("//button[contains(text(),'Login')]"),
                    By.CssSelector(".btn-login")
                });

            loginButton.Click();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}

