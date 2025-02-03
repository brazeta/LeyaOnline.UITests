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
    public class SearchResultsPage : BasePageClass
    {
        public SearchResultsPage(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait)
        {
            this.driver = Driver;
            this.wait = Wait;
        }

        #region Page Locators

        readonly By breadcrumbInfo = By.XPath("//*[@class='breadcrumb-item active']");
        readonly By searchResultsHeader = By.XPath("//*[@class='similar-books'][1]//div[@class='h0']");
        readonly By totalMatchingResults = By.XPath("//*[@class='search-result-nmbr']");

        By searchResultItem(string ItemId) => By.XPath($"//*[@id='bookcard_{ItemId}']");
        By searchResultItemImage(string ItemId, string ImagePath) => By.XPath($"//*[@id='bookcard_{ItemId}']//img[@src='{ImagePath}']");
        By searchResultItemTitle(string ItemId) => By.XPath($"//*[@id='bookcard_{ItemId}']//*[@class='book-title']");
        By searchResultItemAuthor(string ItemId) => By.XPath($"//*[@id='bookcard_{ItemId}']//*[@class='book-author']");
        By searchResultItemPrice(string ItemId) => By.XPath($"//*[@id='bookcard_{ItemId}']//*[@class='single-book-price']/h6");
        By searchResultItemBuyButon(string ItemId) => By.XPath($"//*[@id='bookcard_{ItemId}']//*[@class='more buy-button']");

        #endregion

        #region Public Methods

        public SearchResultsPage WaitForPageToLoad()
        {
            WaitForElementVisible(breadcrumbInfo);
            WaitForElementVisible(searchResultsHeader);
            WaitForElementVisible(totalMatchingResults);

            return this;
        }

        public SearchResultsPage ValidateBreadcrumbText(string ExpectedText)
        {
            ValidateElementText(breadcrumbInfo, ExpectedText);

            return this;
        }

        public SearchResultsPage ValidateTotalMatchingResultsText(string ExpectedText)
        {
            ValidateElementText(totalMatchingResults, ExpectedText);

            return this;
        }

        public SearchResultsPage ValidateResultsItemPresent(string ItemId)
        {
            WaitForElementVisible(searchResultItem(ItemId));

            return this;
        }

        public SearchResultsPage ValidateResultsItemImage(string ItemId, string ImagePath)
        {
            WaitForElementVisible(searchResultItemImage(ItemId, ImagePath));

            return this;
        }

        public SearchResultsPage ValidateResultsItemTitle(string ItemId, string ExpectedTitle)
        {
            ValidateElementText(searchResultItemTitle(ItemId), ExpectedTitle);

            return this;
        }

        public SearchResultsPage ValidateResultsItemAuthor(string ItemId, string ExpectedAuthor)
        {
            ValidateElementText(searchResultItemAuthor(ItemId), ExpectedAuthor);

            return this;
        }

        public SearchResultsPage ValidateResultsItemPrice(string ItemId, string ExpectedPrice)
        {
            ValidateElementText(searchResultItemPrice(ItemId), ExpectedPrice);

            return this;
        }

        public SearchResultsPage ClickResultsItem(string ItemId)
        {
            Click(searchResultItemTitle(ItemId));

            return this;
        }

        public SearchResultsPage ClickResultsItemBuyButton(string ItemId)
        {
            System.Threading.Thread.Sleep(500);
            Click(searchResultItemBuyButon(ItemId));


            return this;
        }

        #endregion
    }
}
