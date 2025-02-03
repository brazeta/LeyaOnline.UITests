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
    public class BookPage : BasePageClass
    {
        public BookPage(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait)
        {
            this.driver = Driver;
            this.wait = Wait;
        }

        #region Page Locators

        readonly By breadcrumbInfo = By.XPath("//*[@class='breadcrumb-item active']");

        By BookImage(string ImageURL) => By.XPath($"//div[@class='row banner-large']/div/div/img[@src='{ImageURL}']");
        readonly By bookTitle = By.XPath("//div[@class='row banner-large']//div[@class='h1']");
        readonly By bookAuthorLink = By.XPath("//div[@class='row banner-large']//h3[@class='leya_h2']/a");
        readonly By addToFavoritesLink = By.XPath("//div[@class='row banner-large']//div/a[@class='add-to-favorite nopjax']");

        readonly By sinopseHeader = By.XPath("//section[@class='sinopse']//*[@class='show-more show-more-hided']");
        readonly By sinopseText = By.XPath("//section[@class='sinopse']//*[@class='show-more show-more-hided']");

        readonly By detailsHeader = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/h2[text()='Detalhes']");
        readonly By isbnText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[1]");
        readonly By ebookISBNText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[2]");
        readonly By editorText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[3]");
        readonly By printYearText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[4]");
        readonly By DimentionsText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[5]");
        readonly By PagesText = By.XPath("//section[@class='sinopse']//*[@class='_sinpose-address']/ul/li[6]");

        #endregion

        #region Public Methods

        public BookPage WaitForPageToLoad()
        {
            WaitForElementVisible(breadcrumbInfo);

            WaitForElementVisible(bookTitle);
            WaitForElementVisible(bookAuthorLink);
            WaitForElementVisible(addToFavoritesLink);

            WaitForElementVisible(sinopseHeader);
            WaitForElementVisible(sinopseText);

            WaitForElementVisible(detailsHeader);
            WaitForElementVisible(isbnText);
            WaitForElementVisible(ebookISBNText);
            WaitForElementVisible(editorText);
            WaitForElementVisible(printYearText);
            WaitForElementVisible(DimentionsText);
            WaitForElementVisible(PagesText);

            return this;
        }

        public BookPage ValidateBreadcrumbText(string ExpectedText)
        {
            ValidateElementText(breadcrumbInfo, ExpectedText);

            return this;
        }


        public BookPage ValidateBookImage(string ImageURL)
        {
            WaitForElementVisible(BookImage(ImageURL));

            return this;
        }

        public BookPage ValidateBookTitle(string ExpectedText)
        {
            ValidateElementText(bookTitle, ExpectedText);

            return this;
        }

        public BookPage ValidateAuthorLinkText(string ExpectedText)
        {
            ValidateElementText(bookAuthorLink, ExpectedText);

            return this;
        }

        public BookPage ValidateAuthorLinkUrl(string ExpectedURL)
        {
            ValidateElementAttribute(bookAuthorLink, "href", ExpectedURL);

            return this;
        }

        public BookPage ClickAuthorLink()
        {
            Click(bookAuthorLink);

            return this;
        }

        public BookPage ValidateSinopseContainsText(string TextSample)
        {
            ValidateElementContainsText(sinopseText, TextSample);

            return this;
        }


        public BookPage ValidateISBNText(string ExpectedText)
        {
            ValidateElementText(isbnText, $"ISBN: {ExpectedText}");

            return this;
        }

        public BookPage ValidateEbookISBNText(string ExpectedText)
        {
            ValidateElementText(ebookISBNText, $"ISBN do Ebook: {ExpectedText}");

            return this;
        }

        public BookPage ValidateEditorText(string ExpectedText)
        {
            ValidateElementText(editorText, $"Editora: {ExpectedText}");

            return this;
        }

        public BookPage ValidatePrintYearText(string ExpectedText)
        {
            ValidateElementText(printYearText, $"Ano de Edição / Impressão: {ExpectedText}");

            return this;
        }

        public BookPage ValidateDimentionsText(string ExpectedText)
        {
            ValidateElementText(DimentionsText, $"Dimensões: {ExpectedText}");

            return this;
        }

        public BookPage ValidatePagesText(string ExpectedText)
        {
            ValidateElementText(PagesText, $"Páginas: {ExpectedText}");

            return this;
        }

        #endregion
    }
}
