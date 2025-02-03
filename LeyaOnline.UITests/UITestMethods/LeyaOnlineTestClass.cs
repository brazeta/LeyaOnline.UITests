using DatabaseConnector;
using LeyaOnline.UITestsFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeyaOnline.UITests
{
    [TestClass]
    public class LeyaOnlineTestClass : BaseTestClass
    {
        [TestInitialize()]
        public void TestInitializationMethod()
        {
            /*Any initialization code that is valid for all tests in this Class can be setup in this method*/
        }

        [TestCleanup()]
        public void TestCleanupMethod()
        {
            /*Any cleanup code that is valid for all tests in this Class can be setup in this method*/
        }

        [TestMethod]
        [Description("Scenario 1")]
        public void LeyaOnline_TestMethod001()
        {
            var bookInformation = dbConnector.bookTable.GetByNameAndAuthor("O Triunfo dos Porcos", "GEORGE ORWELL").First();

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("George")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ValidateBreadcrumbText("Pesquisa")
                .ValidateTotalMatchingResultsText("38 resultados para “George”")
                .ValidateResultsItemPresent(bookInformation.BookId)
                .ValidateResultsItemImage(bookInformation.BookId, bookInformation.Image250px)
                .ValidateResultsItemTitle(bookInformation.BookId, bookInformation.BookTitle)
                .ValidateResultsItemAuthor(bookInformation.BookId, bookInformation.BookAuthor)
                .ValidateResultsItemPrice(bookInformation.BookId, bookInformation.BookPrice)
                .ClickResultsItem(bookInformation.BookId);

            bookPage
                .WaitForPageToLoad()
                .ValidateBreadcrumbText("O Triunfo dos Porcos")
                .ValidateBookImage(bookInformation.Image500px)
                .ValidateBookTitle("O Triunfo dos Porcos")
                .ValidateAuthorLinkText("GEORGE ORWELL")
                .ValidateAuthorLinkUrl(bookInformation.AuthorURL)

                .ValidateSinopseContainsText("Quinta Manor");

        }

        [TestMethod]
        [Description("Scenario 2")]
        public void LeyaOnline_TestMethod002()
        {
            var bookInformation = dbConnector.bookTable.GetByNameAndAuthor("1984", "GEORGE ORWELL").First();

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("1984")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ValidateResultsItemPresent(bookInformation.BookId)
                .ValidateResultsItemImage(bookInformation.BookId, bookInformation.Image250px)
                .ValidateResultsItemTitle(bookInformation.BookId, "1984")
                .ValidateResultsItemAuthor(bookInformation.BookId, "GEORGE ORWELL")
                .ValidateResultsItemPrice(bookInformation.BookId, bookInformation.BookPrice)
                .ClickResultsItem(bookInformation.BookId);

            bookPage
                .WaitForPageToLoad()
                .ValidateBreadcrumbText("1984")
                .ValidateBookImage(bookInformation.Image500px)
                .ValidateBookTitle("1984")
                .ValidateAuthorLinkText("GEORGE ORWELL")

                .ValidateISBNText("9789722071550")
                .ValidateEbookISBNText("9789722071567")
                .ValidateEditorText("DOM QUIXOTE")
                .ValidatePrintYearText("2021")
                .ValidateDimentionsText("235 x 157 x 23 mm")
                .ValidatePagesText("344");
        }

        [TestMethod]
        [Description("Scenario 3")]
        public void LeyaOnline_TestMethod003()
        {
            var book1Information = dbConnector.bookTable.GetByNameAndAuthor("1984", "GEORGE ORWELL").First();
            var book2Information = dbConnector.bookTable.GetByNameAndAuthor("O Triunfo dos Porcos", "GEORGE ORWELL").First();

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("1984")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ValidateResultsItemPresent(book1Information.BookId)
                .ClickResultsItem(book1Information.BookId);

            bookPage
                .WaitForPageToLoad()
                .ValidateBreadcrumbText("1984")
                .ValidateBookTitle("1984")
                .ValidateAuthorLinkText("GEORGE ORWELL")
                .ClickAuthorLink(); //Click on the Author link

            authorPage
                .WaitForPageToLoad()
                .ValidateAuthorHeaderText("GEORGE ORWELL")
                .ValidateBookPresent(book2Information.BookId)
                .ValidateBookImage(book2Information.BookId, "https://www.leyaonline.com/fotos/produtos/250_9789722071581_o_triunfo_dos_porcos.jpg")
                .ValidateBookTitle(book2Information.BookId, "O Triunfo dos Porcos")
                .ValidateBookAuthor(book2Information.BookId, "GEORGE ORWELL")
                .ValidateBookPrice(book2Information.BookId, book2Information.BookPrice);
        }

        [TestMethod]
        [Description("Scenario 4")]
        public void LeyaOnline_TestMethod004()
        {
            var book1Information = dbConnector.bookTable.GetByNameAndAuthor("1984", "GEORGE ORWELL").First();

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("1984")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ValidateResultsItemPresent(book1Information.BookId)
                .ClickResultsItemBuyButton(book1Information.BookId);

            mainMenu
                .WaitForShoppingCartPreviewAreaToLoad()

                .ValidateCartButtonDisplayedNumberOfItems(1)

                .ValidateShoppingCartPreviewItemImage(1, book1Information.Image250px)
                .ValidateShoppingCartPreviewItemTitleText(1, "1984")
                .ValidateShoppingCartPreviewItemTitleLink(1, book1Information.BookURL)
                .ValidateShoppingCartPreviewItemPrice(1, book1Information.BookPrice)
                .ValidateShoppingCartPreviewItemCount(1, "1")

                .ValidateShoppingCartPreviewTotalValue(book1Information.BookPrice);
        }

        [TestMethod]
        [Description("Scenario 5")]
        public void LeyaOnline_TestMethod005()
        {
            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()

                .ValidateDarkModeButtonSunIconDisplayed() //Sun icon should be dislayed in dark mode button
                .ValidateIfDarkThemeIsApplied(false) //dark theme css should be disabled

                .ClickDarkModeButton()

                .ValidateDarkModeButtonMoonIconDisplayed() //Moon icon should be dislayed in dark mode button
                .ValidateIfDarkThemeIsApplied(true); //dark theme css should be enabled

        }

        [TestMethod]
        [Description("Scenario 6 - Add different books to the shopping cart - Validate the total cart price")]
        public void LeyaOnline_TestMethod006()
        {
            var book_1984 = dbConnector.bookTable.GetByNameAndAuthor("1984", "GEORGE ORWELL").First();
            var book_OTriunfoDosPorcos = dbConnector.bookTable.GetByNameAndAuthor("O Triunfo dos Porcos", "GEORGE ORWELL").First();
            var totalBooksPriceDecimal = book_1984.BookDecimalPrice + book_OTriunfoDosPorcos.BookDecimalPrice;
            var booksTotalPrice = $"€{totalBooksPriceDecimal.ToString("F2").Replace(".", ",")}";

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("GEORGE ORWELL")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ClickResultsItemBuyButton(book_1984.BookId) /*Add a 1st book to the cart*/
                .ClickResultsItemBuyButton(book_OTriunfoDosPorcos.BookId); /*Add a 2nd book to the cart*/

            mainMenu
                .WaitForShoppingCartPreviewAreaToLoad()
                .ValidateCartButtonDisplayedNumberOfItems(2); //2 books should be added to the cart

            //Validate the last added book
            mainMenu
                .ValidateShoppingCartPreviewItemImage(1, book_OTriunfoDosPorcos.Image250px)
                .ValidateShoppingCartPreviewItemTitleText(1, "O Triunfo dos Porcos")
                .ValidateShoppingCartPreviewItemTitleLink(1, book_OTriunfoDosPorcos.BookURL)
                .ValidateShoppingCartPreviewItemPrice(1, book_OTriunfoDosPorcos.BookPrice)
                .ValidateShoppingCartPreviewItemCount(1, "1");

            //Validate the first added book
            mainMenu
                .ValidateShoppingCartPreviewItemImage(2, book_1984.Image250px)
                .ValidateShoppingCartPreviewItemTitleText(2, "1984")
                .ValidateShoppingCartPreviewItemTitleLink(2, book_1984.BookURL)
                .ValidateShoppingCartPreviewItemPrice(2, book_1984.BookPrice)
                .ValidateShoppingCartPreviewItemCount(2, "1");

            //Validate the totals
            mainMenu
                .ValidateShoppingCartPreviewTotalValue(booksTotalPrice); 

        }

        [TestMethod]
        [Description("Scenario 7 - Add the same book multiple times to the shopping cart - Validate the total cart price")]
        public void LeyaOnline_TestMethod007()
        {
            var book_1984 = dbConnector.bookTable.GetByNameAndAuthor("1984", "GEORGE ORWELL").First();
            var book_OTriunfoDosPorcos = dbConnector.bookTable.GetByNameAndAuthor("O Triunfo dos Porcos", "GEORGE ORWELL").First();

            var booksPriceDecimal = book_1984.BookDecimalPrice + (book_OTriunfoDosPorcos.BookDecimalPrice * 2);
            var booksPrice = $"€{booksPriceDecimal.ToString("F2").Replace(".", ",")}";

            homePage
                .NavigateToHomePage()
                .WaitForPageToLoad();

            cookiesOverlay
                .WaitForOverlayToLoad()
                .ClickAcceptButton();

            mainMenu
                .WaitForMainMenuToLoad()
                .TypeSearchQuery("GEORGE ORWELL")
                .WaitForSearchBarResultsAreaToLoad()
                .ClickSearchButton();

            searchResultsPage
                .WaitForPageToLoad()
                .ClickResultsItemBuyButton(book_OTriunfoDosPorcos.BookId) /*Add a 1st book to the cart*/
                .ClickResultsItemBuyButton(book_1984.BookId) /*Add a 2nd book to the cart*/
                .ClickResultsItemBuyButton(book_OTriunfoDosPorcos.BookId); /*Add a 1st book to the cart again*/

            mainMenu
                .WaitForShoppingCartPreviewAreaToLoad()
                .ValidateCartButtonDisplayedNumberOfItems(3); //3 books should be added to the cart

            //Validate the 2nd added book
            mainMenu
                .ValidateShoppingCartPreviewItemImage(1, book_1984.Image250px)
                .ValidateShoppingCartPreviewItemTitleText(1, "1984")
                .ValidateShoppingCartPreviewItemTitleLink(1, book_1984.BookURL)
                .ValidateShoppingCartPreviewItemPrice(1, book_1984.BookPrice)
                .ValidateShoppingCartPreviewItemCount(1, "1");

            //Validate the 1st added book
            mainMenu
                .ValidateShoppingCartPreviewItemImage(2, book_OTriunfoDosPorcos.Image250px)
                .ValidateShoppingCartPreviewItemTitleText(2, "O Triunfo dos Porcos")
                .ValidateShoppingCartPreviewItemTitleLink(2, book_OTriunfoDosPorcos.BookURL)
                .ValidateShoppingCartPreviewItemPrice(2, "€9,90")
                .ValidateShoppingCartPreviewItemCount(2, "2");

            //The totals should have the prices of the 3 selected books
            mainMenu
                .ValidateShoppingCartPreviewTotalValue(booksPrice); 

        }

    }
}
