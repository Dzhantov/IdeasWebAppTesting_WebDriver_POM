﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriverExamPrep1.Pages
{
    public class CreateIdeaPage : BasePage
    {
        public CreateIdeaPage(IWebDriver driver) : base(driver)
        {
            
        }
        public string Url = BaseUrl + "/Ideas/Create";

        public IWebElement TitleInput => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement ImageInput => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionInput => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement CreateButton => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));
        public IWebElement MainMessage => driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li"));
        public IWebElement TitleErrorMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[0];
        public IWebElement DescriptionErrorMessage => driver.FindElements(By.XPath("//span[@class='text-danger field-validation-error']"))[1];

        public void CreateIdea(string title, string imageUrl, string description)
        {
            TitleInput.SendKeys(title);
            ImageInput.SendKeys(imageUrl);
            DescriptionInput.SendKeys(description);
            CreateButton.Click();
        }

        public void AssertErrorMessages()
        {
            Assert.True(MainMessage.Text.Equals("Unable to create new Idea!"), "Error message not correct");
            Assert.True(TitleErrorMessage.Text.Equals("The Title field is required."), "Title Error message not correct");
            Assert.True(DescriptionErrorMessage.Text.Equals("The Description field is required."), "Description Error message not correct");


        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
