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
    public class CookiesOverlay : BasePageClass
    {
        public CookiesOverlay(IWebDriver Driver, OpenQA.Selenium.Support.UI.WebDriverWait Wait)
        {
            this.driver = Driver;
            this.wait = Wait;
        }

        #region Page Locators

        readonly By cookiesCloseButton = By.XPath("//*[@id='cookiescript_close']");
        readonly By cookiesHeatherText = By.XPath("//*[@id='cookiescript_header']");
        readonly By cookiesDescriptionText = By.XPath("//*[@id='cookiescript_description']");
        readonly By cookiesAcceptButton = By.XPath("//*[@id='cookiescript_accept']");
        readonly By cookiesRejectButton = By.XPath("//*[@id='cookiescript_reject']");
        readonly By cookiesManageButton = By.XPath("//*[@id='cookiescript_manage']");

        #endregion


        public CookiesOverlay WaitForOverlayToLoad()
        {
            WaitForElementVisible(cookiesCloseButton);
            WaitForElementVisible(cookiesHeatherText);
            WaitForElementVisible(cookiesDescriptionText);
            WaitForElementVisible(cookiesAcceptButton);
            WaitForElementVisible(cookiesRejectButton);
            WaitForElementVisible(cookiesManageButton);

            return this;
        }

        public CookiesOverlay ClickCloseButton()
        {
            Click(cookiesCloseButton);

            return this;
        }

        public CookiesOverlay ClickAcceptButton()
        {
            Click(cookiesAcceptButton);

            return this;
        }

        public CookiesOverlay ClickRejectButton()
        {
            Click(cookiesRejectButton);

            return this;
        }
    }
}
