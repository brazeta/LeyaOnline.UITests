using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyaOnline.UITestsFramework
{
    public class AuthorPage : BasePageClass
    {
        public AuthorPage(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait)
        {
            this.driver = Driver;
            this.wait = Wait;
        }

        #region Page Locators

        readonly By authorHeader = By.XPath("//*[@class='author-banner-content-heading']/h2");
        readonly By authorDescription = By.XPath("//*[@class='author-banner-content align-self-center']/*[@class='show-more show-more-hided']/div");


        readonly By sameAuthorBooksHeader = By.XPath("//*[@id='pjax-container']//*[@class='similar-books']//*[@class='h0']");
        By book(string ItemId) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']");
        By bookImage(string ItemId, string ImagePath) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']//img[@src='{ImagePath}']");
        By bookTitle(string ItemId) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']//*[@class='book-title']");
        By bookAuthor(string ItemId) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']//*[@class='book-author']");
        By bookPrice(string ItemId) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']//*[@class='single-book-price']/h6");
        By bookBuyButon(string ItemId) => By.XPath($"//*[@id='pjax-container']//*[@id='bookcard_{ItemId}']//*[@class='more buy-button']");

        #endregion

        #region Public Methods

        public AuthorPage WaitForPageToLoad()
        {

            WaitForElementVisible(authorHeader);
            WaitForElementVisible(authorDescription);

            WaitForElementVisible(sameAuthorBooksHeader);

            return this;
        }

        public AuthorPage ValidateAuthorHeaderText(string ExpectedText)
        {
            ValidateElementText(authorHeader, ExpectedText);

            return this;
        }



        public AuthorPage ValidateBookPresent(string ItemId)
        {
            WaitForElementVisible(book(ItemId));

            return this;
        }

        public AuthorPage ValidateBookImage(string ItemId, string ImagePath)
        {
            WaitForElementVisible(bookImage(ItemId, ImagePath));

            return this;
        }

        public AuthorPage ValidateBookTitle(string ItemId, string ExpectedTitle)
        {
            ValidateElementText(bookTitle(ItemId), ExpectedTitle);

            return this;
        }

        public AuthorPage ValidateBookAuthor(string ItemId, string ExpectedAuthor)
        {
            ValidateElementText(bookAuthor(ItemId), ExpectedAuthor);

            return this;
        }

        public AuthorPage ValidateBookPrice(string ItemId, string ExpectedPrice)
        {
            ValidateElementText(bookPrice(ItemId), ExpectedPrice);

            return this;
        }


        #endregion
    }
}
