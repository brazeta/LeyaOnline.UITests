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
    public class HomePage : BasePageClass
    {
        public HomePage(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait, string HomePageUrl)
        {
            this.driver = Driver;
            this.wait = Wait;
            this.homePageUrl = HomePageUrl;
        }

        #region Private Properties

        private string homePageUrl;

        #endregion

        #region Page Locators

        readonly By newsHeader = By.XPath("//*[@class='similar-books'][1]//div[@class='h0']");
        
        #endregion

        #region Public Methods

        public HomePage NavigateToHomePage()
        {
            NavigateToUrl(homePageUrl);

            return this;
        }

        public HomePage WaitForPageToLoad()
        {
            WaitForElement(newsHeader);

            return this;
        }

        #endregion
    }
}
