using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnector;
using LeyaOnline.UITestsFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace LeyaOnline.UITests
{
    public abstract class BaseTestClass
    {

        #region Private Properties

        private IWebDriver driver;
        private TestContext testContextInstance;
        private OpenQA.Selenium.Support.UI.WebDriverWait wait;
        private string appURL;

        #region Page Objects

        private HomePage _homePage;
        private CookiesOverlay _cookiesOverlay;
        private MainMenu _mainMenu;
        private SearchResultsPage _searchResultsPage;
        private BookPage _bookPage;
        private AuthorPage _authorPage;

        #endregion

        #endregion

        #region Public Properties

        public DBConnector dbConnector;

        /// <summary>
        ///Gets or sets the test context which provides information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Page Objects

        public HomePage homePage
        {
            get
            {
                if (_homePage == null)
                    _homePage = new HomePage(driver, wait, appURL);
                return _homePage;
            }
        }

        public CookiesOverlay cookiesOverlay
        {
            get
            {
                if (_cookiesOverlay == null)
                    _cookiesOverlay = new CookiesOverlay(driver, wait);
                return _cookiesOverlay;
            }
        }

        public MainMenu mainMenu
        {
            get
            {
                if (_mainMenu == null)
                    _mainMenu = new MainMenu(driver, wait);
                return _mainMenu;
            }
        }

        public SearchResultsPage searchResultsPage
        {
            get
            {
                if (_searchResultsPage == null)
                    _searchResultsPage = new SearchResultsPage(driver, wait);
                return _searchResultsPage;
            }
        }

        public BookPage bookPage
        {
            get
            {
                if (_bookPage == null)
                    _bookPage = new BookPage(driver, wait);
                return _bookPage;
            }
        }

        public AuthorPage authorPage
        {
            get
            {
                if (_authorPage == null)
                    _authorPage = new AuthorPage(driver, wait);
                return _authorPage;
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize a new browser instance
        /// </summary>
        /// <param name="browser">The browser to start. E.g. "Chrome"; "Edge"; "FireFox"</param>
        /// <param name="DownloadsDir"></param>
        private void SetDriver(string Browser, string DownloadsDir)
        {
            var runTestsInHeadlessMode = ConfigurationManager.AppSettings["RunTestsInHeadlessMode"];

            switch (Browser)
            {
                case "Chrome":

                    var options = new ChromeOptions();

                    if (runTestsInHeadlessMode.Equals("true"))
                    {
                        options.AddArguments("--headless=new");
                        options.AddArguments("window-size=1920,1080");
                    }

                    options.SetLoggingPreference(LogType.Driver, LogLevel.Severe);
                    driver = new ChromeDriver(options);
                    driver.Manage().Cookies.DeleteAllCookies();

                    break;
                default:
                    break;
            }

            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Close the browser and shut down the ChromeDriver.exe
        /// </summary>
        private void CloseDriver()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                /*if, for some reason, the Quit() method fails, as a last resort we will call this method to kill all driver processes */
                ShutDownAllBrowserRelatedProcesses();

                throw;
            }

            
        }

        /// <summary>
        /// This method will shut down all process with the name chromedriver
        /// </summary>
        public void ShutDownAllBrowserRelatedProcesses()
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("chromedriver"))
            {
                try { process.Kill(); } catch { }
            }
        }

        #endregion

        #region Public Methods

        [TestInitialize()]
        public void DefaultInitializationMethod()
        {
            /*Reset all logs*/
            LeyaOnline.UITestsFramework.Logger.Logger.ClearLogs();

            /*Setup the Browser*/
            var browser = ConfigurationManager.AppSettings["browser"];
            var downloadsDirectory = TestContext.TestRunDirectory + "\\Downloads";
            SetDriver(browser, downloadsDirectory);

            /*Setup the necessary properties to be used by the page objects*/
            appURL = ConfigurationManager.AppSettings["appURL"];
            var defaultTimeoutSeconds = int.Parse(ConfigurationManager.AppSettings["defaultTimeoutSeconds"]);
            wait = new OpenQA.Selenium.Support.UI.WebDriverWait(driver, new TimeSpan(0, 0, defaultTimeoutSeconds));

            /*Setup the Database Connector class*/
            dbConnector = new DBConnector();
        }

        [TestCleanup()]
        public void DefaultCleanupMethod()
        {
            CloseDriver();
        }

        #endregion



    }
}
