using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverExamPrep1.Pages;

namespace WebDriverExamPrep1.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        public string LastCreatedIdeaTitle;
        public string LastCreatedIdeaDescription;

        [Test, Order(1)]
        public void CreateIdeaWithInvalidDataTest()
        {
            createIdeaPage.OpenPage();

            createIdeaPage.CreateIdea("", "", "");

            createIdeaPage.AssertErrorMessages();
        }

        [Test, Order(2)]
        public void CreateIdeaWithValidDataTest()
        {
            LastCreatedIdeaTitle = "Idea " + GenerateRandomString(5);
            LastCreatedIdeaDescription = "Description " + GenerateRandomString(5);

            createIdeaPage.OpenPage();

            createIdeaPage.CreateIdea(LastCreatedIdeaTitle, "", LastCreatedIdeaDescription);

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not as expected");
            Assert.That(myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(LastCreatedIdeaDescription), "Descriptions not match");
        }

        [Test, Order(3)]
        public void ViewLastCreatedIdeaTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(LastCreatedIdeaTitle), "Title is not as expected");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(LastCreatedIdeaDescription), "Description is not as expected");
        }

        [Test, Order(4)]
        public void EditIdeaTitleTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string updatedTitle = "Changed title: " + LastCreatedIdeaTitle;

            ideasEditPage.TitleInput.Clear();
            ideasEditPage.TitleInput.SendKeys(updatedTitle);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not as expected");

            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle), "Title does not match");
        }

        [Test, Order(5)]
        public void EditIdeaDescriptionTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.EditButtonLastIdea.Click();

            string updatedDescription = "Changed description: " + LastCreatedIdeaDescription;

            ideasEditPage.DescriptionInput.Clear();
            ideasEditPage.DescriptionInput.SendKeys(updatedDescription);
            ideasEditPage.EditButton.Click();

            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not as expected");

            myIdeasPage.ViewButtonLastIdea.Click();

            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(updatedDescription), "Description does not match");
        }
        [Test, Order(6)]
        public void DeleteIdeaTest()
        {
            myIdeasPage.OpenPage();
            myIdeasPage.DeleteButtonLastIdea.Click();

            bool isIdeaDeleted = myIdeasPage.IdeasCards.All(card => card.Text.Contains(LastCreatedIdeaDescription));

            Assert.IsFalse(isIdeaDeleted, "Idea was not deleted");
        }
    }
}
