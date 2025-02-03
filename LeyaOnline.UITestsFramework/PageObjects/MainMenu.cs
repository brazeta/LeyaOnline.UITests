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
    public class MainMenu : BasePageClass
    {
        public MainMenu(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait)
        {
            this.driver = Driver;
            this.wait = Wait;
        }

        #region Page Locators

        readonly By darkThemeCSS = By.XPath("/html/head/link[@data-role='darktheme']");

        readonly By headerLogoImage = By.XPath("//*[@class='header-logo header-logo-big align-self-end']/a/img[@src='/assets/images/logo.png']");
        readonly By hamburguerButton = By.XPath("//i[@class='icon-hamburguer nav-icon']");
        readonly By darkModeButton = By.XPath("//*[@id='darkmode']/a");
        readonly By darkModeButtonIcon = By.XPath("//*[@id='darkmode']/a/i");
        readonly By loginButton = By.XPath("//*[@class='dropdown']/a");
        readonly By searchButton = By.XPath("//*[@class='search-bar']/button");
        readonly By searchTextbox = By.XPath("//*[@id='searchbar-large']");
        readonly By wishListButton = By.XPath("//*[@id='header_favorite_counter']");
        readonly By cartButton = By.XPath("//*[@id='dropdownMenuButton100']");
        By NumberOfItemsInCart(int TotalItemsInCart) => By.XPath($"//*[@id='dropdownMenuButton100'][@data-tag='{TotalItemsInCart}']");


        readonly By searchBarResultsArea = By.XPath("//*[@class='search-content search-bar-results']");


        readonly By shoppingCartPreviewArea = By.XPath("//*[@id='atc-dropdown']");
        readonly By shoppingCartPreviewAreaNoItems = By.XPath("//*[@id='atc-dropdown']/div[contains(@style, 'color: #E30E19')][contains(@style, 'font-size: 1.25rem')]");

        By shoppingCartPreviewItemImage(int ItemPosition, string ImageUrl) => By.XPath($"//*[@class='addToCart-item'][{ItemPosition}]//img[@src='{ImageUrl}']");
        By shoppingCartPreviewItemTitleLink(int ItemPosition) => By.XPath($"//*[@class='addToCart-item'][{ItemPosition}]//div[@class='col-9']/h2/a");
        By shoppingCartPreviewItemPrice(int ItemPosition) => By.XPath($"//*[@class='addToCart-item'][{ItemPosition}]//*[@class='b-price']");
        By shoppingCartPreviewItemCount(int ItemPosition) => By.XPath($"//*[@class='addToCart-item'][{ItemPosition}]//*[@class='b-count']");
        By shoppingCartPreviewItemDeleteButton(int ItemPosition) => By.XPath($"//*[@class='addToCart-item'][{ItemPosition}]//*[@class='b-delete']");

        readonly By shoppingCartPreviewTotalHeader = By.XPath($"//*[@id='atc-dropdown']//*[@class='checkoutPayment-total'][text()='Total']");
        readonly By shoppingCartPreviewTotalValue = By.XPath($"//*[@id='atc-dropdown']//*[@class='checkoutPayment-total-amount']");
        readonly By shoppingCartPreviewCheckoutButton = By.XPath($"//*[@class='checkout-btn']//a");

        #endregion


        public MainMenu WaitForMainMenuToLoad()
        {
            WaitForElementVisible(headerLogoImage);
            WaitForElementVisible(hamburguerButton);
            WaitForElementVisible(darkModeButton);
            WaitForElementVisible(loginButton);
            WaitForElementVisible(searchButton);
            WaitForElementVisible(searchTextbox);
            WaitForElementVisible(wishListButton);
            WaitForElementVisible(cartButton);

            return this;
        }

        public MainMenu ValidateIfDarkThemeIsApplied(bool ExpectDarkModeApplied)
        {
            if (ExpectDarkModeApplied)
            {
                ValidateElementNotDisabled(darkThemeCSS);
            }
            else
            {
                ValidateElementDisabled(darkThemeCSS);
            }

            return this;
        }

        public MainMenu ClickHamburguerButton()
        {
            Click(hamburguerButton);

            return this;
        }

        public MainMenu ClickDarkModeButton()
        {
            Click(darkModeButton);

            return this;
        }

        public MainMenu ValidateDarkModeButtonSunIconDisplayed()
        {
            ValidateElementAttribute(darkModeButtonIcon, "class", "nav-icon icon-sun");

            return this;
        }

        public MainMenu ValidateDarkModeButtonMoonIconDisplayed()
        {
            ValidateElementAttribute(darkModeButtonIcon, "class", "nav-icon icon-moon");

            return this;
        }

        public MainMenu ClickLoginButton()
        {
            Click(loginButton);

            return this;
        }

        public MainMenu ClickSearchButton()
        {
            Click(searchButton);

            return this;
        }

        public MainMenu TypeSearchQuery(string SearchQuery)
        {
            InsertText(searchTextbox, SearchQuery);

            return this;
        }

        public MainMenu ClickWishListButton()
        {
            Click(wishListButton);

            return this;
        }

        public MainMenu ClickCartButton()
        {
            Click(cartButton);

            return this;
        }

        public MainMenu ValidateCartButtonDisplayedNumberOfItems(int ExpecteNumberOfItems)
        {
            WaitForElement(NumberOfItemsInCart(ExpecteNumberOfItems));

            return this;
        }



        public MainMenu WaitForSearchBarResultsAreaToLoad()
        {
            WaitForElementVisible(searchBarResultsArea);

            return this;
        }



        public MainMenu WaitForShoppingCartPreviewAreaToLoad()
        {
            System.Threading.Thread.Sleep(1000);
            WaitForElementVisible(shoppingCartPreviewArea);
            WaitForElementVisible(shoppingCartPreviewTotalHeader);
            WaitForElementVisible(shoppingCartPreviewTotalValue);
            WaitForElementVisible(shoppingCartPreviewCheckoutButton);
            ValidateElementAttribute(cartButton, "aria-expanded", "true");

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewItemImage(int ItemPosition, string ImageUrl)
        {
            WaitForElementVisible(shoppingCartPreviewItemImage(ItemPosition, ImageUrl));

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewItemTitleText(int ItemPosition, string ExpectedItemTitle)
        {
            WaitForElementVisible(shoppingCartPreviewItemTitleLink(ItemPosition));

            ValidateElementText(shoppingCartPreviewItemTitleLink(ItemPosition), ExpectedItemTitle);

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewItemTitleLink(int ItemPosition, string ExpectedItemLinkUrl)
        {
            WaitForElementVisible(shoppingCartPreviewItemTitleLink(ItemPosition));

            ValidateElementAttribute(shoppingCartPreviewItemTitleLink(ItemPosition), "href", ExpectedItemLinkUrl);

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewItemPrice(int ItemPosition, string ExpectedPrice)
        {
            WaitForElementVisible(shoppingCartPreviewItemPrice(ItemPosition));

            ValidateElementText(shoppingCartPreviewItemPrice(ItemPosition), ExpectedPrice);

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewItemCount(int ItemPosition, string ExpectedCount)
        {
            WaitForElementVisible(shoppingCartPreviewItemCount(ItemPosition));

            ValidateElementText(shoppingCartPreviewItemCount(ItemPosition), ExpectedCount);

            return this;
        }

        public MainMenu ClickShoppingCartPreviewItemDeleteButton(int ItemPosition)
        {
            Click(shoppingCartPreviewItemDeleteButton(ItemPosition));

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewTotalValue(string ExpectedTotals)
        {
            ValidateElementText(shoppingCartPreviewTotalValue, ExpectedTotals);

            return this;
        }

        public MainMenu ValidateShoppingCartPreviewNoItemsMessageVisibility(bool ExpectVisible, string ExpectedMessage)
        {
            if (ExpectVisible)
            {
                WaitForElementVisible(shoppingCartPreviewAreaNoItems);
                ValidateElementText(shoppingCartPreviewAreaNoItems, ExpectedMessage);
            }
            else
            {
                WaitForElementNotVisible(shoppingCartPreviewAreaNoItems, 3);
            }

            return this;
        }

        public MainMenu ClickShoppingCartPreviewCheckoutButton()
        {
            Click(shoppingCartPreviewCheckoutButton);

            return this;
        }
    }
}
