using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace LeyaOnline.UITestsFramework
{
    public abstract class BasePageClass
    {
        #region Internal Properties

        internal IWebDriver driver;
        internal OpenQA.Selenium.Support.UI.WebDriverWait wait;

        #endregion

        #region Internal Methods

        /// <summary>
        /// Navigate to an URL
        /// </summary>
        /// <param name="URL">The URL to navigate to</param>
        internal void NavigateToUrl(string URL)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Navigating to the URL {URL}");

            driver.Navigate().GoToUrl(URL);
        }

        /// <summary>
        /// Apply the wait.Until method to the supplied Element object
        /// </summary>
        /// <param name="Element">The element to wait for</param>
        internal void WaitForElement(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Waiting for the element {Element.ToString()}");

            wait.Until(c => c.FindElement(Element));
        }

        /// <summary>
        /// Wait for an element to be visible in the screen
        /// </summary>
        /// <param name="Element">The element to wait for</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException being thrown. This method can be executed recursively no more then 10 times. </param>
        internal void WaitForElementVisible(By Element, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Waiting for the element {Element.ToString()} to be visible");

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(Element));
                wait.Until(c => c.FindElement(Element).Displayed);
                return;
            }
            catch (StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                WaitForElementVisible(Element, RecursionCount);
            }
        }

        /// <summary>
        /// Wait until an UI element is no longer visible
        /// </summary>
        /// <param name="Element">The UI element to confirm not visible</param>
        /// <param name="Seconds">Timeout in seconds</param>
        /// <exception cref="Exception"></exception>
        internal void WaitForElementNotVisible(By Element, int Seconds)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Waiting for the element {Element.ToString()} to NOT be visible");

            try
            {
                var internalWait = new WebDriverWait(driver, TimeSpan.FromSeconds(Seconds));
                internalWait.Until(d => !GetElementVisibility(Element));
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Element {Element.ToString()} is still visible after {Seconds} second(s)");
            }
        }

        /// <summary>
        /// Get the current visibility of an UI Element
        /// </summary>
        /// <param name="Element">The UI element to get the visibility from</param>
        /// <returns></returns>
        internal bool GetElementVisibility(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Getting the element {Element.ToString()} visibility");

            bool flag;
            try
            {
                flag = driver.FindElement(Element).Displayed;
            }
            catch (NoSuchElementException)
            {
                flag = false;
            }
            catch (StaleElementReferenceException)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Wait until it is possible to click on a UI element
        /// </summary>
        /// <param name="Element">The UI element to wait for</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException being thrown. This method can be executed recursively no more then 10 times. </param>
        internal void WaitForElementToBeClickable(By Element, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Waiting the element {Element.ToString()} to be clickable");

            try
            {
                System.Threading.Thread.Sleep(200);
                wait.Until(c => c.FindElement(Element));
                wait.Until(ExpectedConditions.ElementExists(Element));
                wait.Until(ExpectedConditions.ElementIsVisible(Element));
                wait.Until(ExpectedConditions.ElementToBeClickable(Element));
            }
            catch (StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                WaitForElementToBeClickable(Element, RecursionCount);
            }
        }

        /// <summary>
        /// Switch to one of the Iframes in the current page
        /// </summary>
        /// <param name="IframeElement">The Iframe element to switch into</param>
        internal void SwitchToIframe(By IframeElement)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Switching to the Iframe {IframeElement.ToString()}");

            driver.SwitchTo().Frame(driver.FindElement(IframeElement));
        }

        /// <summary>
        /// Switch to the top element in page.
        /// This method can be used t navigate between Iframes
        /// </summary>
        internal void SwitchToTopFrame()
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Switching to the default content");

            try
            {
                this.driver.SwitchTo().DefaultContent();
            }
            catch (UnhandledAlertException)
            {
                this.driver.SwitchTo().DefaultContent();
            }
        }

        /// <summary>
        /// Wait for the browser to have a specific title
        /// </summary>
        /// <param name="ExpectedTitle">The expected browser title</param>
        internal void WaitForBrowserWindowTitle(string ExpectedTitle)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Waiting for the current window title to be: {ExpectedTitle}");

            wait.Until(c => c.Title == ExpectedTitle);
        }

        /// <summary>
        /// Click on a UI element
        /// </summary>
        /// <param name="Element">The UI element to click on</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException or ElementClickInterceptedException being thrown. This method can be executed recursively no more then 10 times.</param>
        internal void Click(By Element, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Clicking on the element {Element.ToString()}");

            WaitForElementToBeClickable(Element);

            try
            {
                this.driver.FindElement(Element).Click();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                Click(Element, RecursionCount);
            }
            catch (OpenQA.Selenium.ElementClickInterceptedException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                Click(Element, RecursionCount);
            }
        }

        /// <summary>
        /// Click on a UI element without performing any kind of waiting process
        /// </summary>
        /// <param name="Element">The UI element to click on</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException or ElementClickInterceptedException being thrown. This method can be executed recursively no more then 10 times.</param>
        internal void ClickWithoutWaiting(By Element, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Clicking on the element {Element.ToString()} without performing a wait opperation");

            try
            {
                this.driver.FindElement(Element).Click();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                ClickWithoutWaiting(Element, RecursionCount);
            }
        }

        /// <summary>
        /// Right click on a UI element
        /// </summary>
        /// <param name="Element">The UI element to Right-Click on</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException being thrown. This method can be executed recursively no more then 10 times.</param>
        internal void RightClick(By Element, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Right-Clicking on the element {Element.ToString()}");

            WaitForElementToBeClickable(Element);
            try
            {
                // Find the element on which to perform the right-click
                IWebElement element = this.driver.FindElement(Element);

                // Create an Actions object
                Actions actions = new Actions(driver);

                // Perform right-click action on the element
                actions.ContextClick(element).Build().Perform();
            }
            catch (OpenQA.Selenium.StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                RightClick(Element, RecursionCount);
            }
        }

        /// <summary>
        /// Click on a UI Element using a JavaScript call
        /// </summary>
        /// <param name="Element">The UI element to click on</param>
        internal void ClickByJavascript(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Clicking on the element {Element.ToString()} via a Javascript call");

            var element = driver.FindElement(Element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
        }

        /// <summary>
        /// Remove the text of a UI Element (input or TextArea)
        /// </summary>
        /// <param name="Element">The UI element to remove the text from</param>
        internal void ClearElementText(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Clearing the element {Element.ToString()} text");

            WaitForElement(Element);

            this.driver.FindElement(Element).Clear();
        }

        /// <summary>
        /// Type text into a UI element (input or TextArea)
        /// </summary>
        /// <param name="Element">The UI element to insert the text into</param>
        /// <param name="TextToInsert">The text to insert into the UI element</param>
        internal void InsertText(By Element, string TextToInsert)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Inserting the text {TextToInsert} into the element {Element.ToString()}");

            WaitForElement(Element);

            ClearElementText(Element);

            this.driver.FindElement(Element).SendKeys(TextToInsert);
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// Type text into a UI element using a javascript command. Use this method when it is not possible to utilize the regular "InsertText" method
        /// </summary>
        /// <param name="Element">The UI element to insert the text into</param>
        /// <param name="TextToInsert">The text to insert into the UI element</param>
        internal void InsertTextViaJavascript(By Element, string TextToInsert)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Inserting the text {TextToInsert} into the element {Element.ToString()} via a Javascript call");

            WaitForElement(Element);

            ClearElementText(Element);

            var element = driver.FindElement(Element);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].setAttribute('value', '" + TextToInsert + "');", element);
        }

        /// <summary>
        /// Verify the text content of a UI Element
        /// </summary>
        /// <param name="Element">The UI element to get the current text from</param>
        /// <param name="ExpectedText">The expected text for the UI element</param>
        /// <param name="RecursionCount">Identifies the number of times this method executed itself due to a StaleElementReferenceException being thrown. This method can be executed recursively no more then 10 times.</param>
        /// <exception cref="Exception">Throws a System.Exception if the Element text does not match the expected text</exception>
        internal void ValidateElementText(By Element, string ExpectedText, int RecursionCount = 0)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Validating the element {Element.ToString()} has the text {ExpectedText}");

            string elementText = "";

            try
            {
                elementText = this.driver.FindElement(Element).Text;
            }
            catch (StaleElementReferenceException)
            {
                RecursionCount++;
                if (RecursionCount > 9) /*if we reach a point where we get the same Stale Element Exception for more than 10 tries, then we will throw the exception.*/
                    throw;

                System.Threading.Thread.Sleep(1000);
                ValidateElementText(Element, ExpectedText, RecursionCount);
            }

            if (ExpectedText != elementText)
            {
                throw new Exception($"Failed to validate the element {Element.ToString()} text. Expected Text: {ExpectedText} || Actual Text: {elementText}");
            }
        }

        /// <summary>
        /// Validate that the specified sub-string exists inside an UI element text
        /// </summary>
        /// <param name="Element">The UI element that contains text information</param>
        /// <param name="ExpectedText">The expected sub-string</param>
        /// <exception cref="Exception"></exception>
        internal void ValidateElementContainsText(By Element, string SubString)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Validating the element {Element.ToString()} text has the sub-string {SubString}");

            string elementText = this.driver.FindElement(Element).Text;
            if (!elementText.Contains(SubString))
            {
                throw new Exception($"Failed to validate that the sub-string '{SubString}' exists inside the element {Element.ToString()}.");
            }
        }

        /// <summary>
        /// Validate the value of an UI element attribute
        /// </summary>
        /// <param name="Element">The UI Element that contains an attribute</param>
        /// <param name="AttributeName">The atribute that belongs to the UI element </param>
        /// <param name="ExpectedAttributeValue">The expected value for the UI Element attribute</param>
        internal void ValidateElementAttribute(By Element, string AttributeName, string ExpectedAttributeValue)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Validating the element {Element.ToString()} contains the attribute {AttributeName} with the value {ExpectedAttributeValue}");

            var webElement = this.driver.FindElement(Element);
            string attributeValue = webElement.GetAttribute(AttributeName);

            if (!attributeValue.Contains(ExpectedAttributeValue))
                throw new Exception($"Failed to validate the element '{Element.ToString()}' attribute '{AttributeName}' value. Expected: {ExpectedAttributeValue} || Actual: {attributeValue}");
        }

        /// <summary>
        /// Get the value of an UI element attribute
        /// </summary>
        /// <param name="Element">The UI Element that contains an attribute</param>
        /// <param name="AttributeName">The atribute that belongs to the UI element </param>
        internal string GetElementAttributeValue(By Element, string AttributeName)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Getting the value for the attribute {AttributeName} for the element {Element.ToString()}");

            return this.driver.FindElement(Element).GetAttribute(AttributeName);
        }

        /// <summary>
        /// Validate that the UI element is marked as disabled
        /// </summary>
        /// <param name="Element">The UI element to search for</param>
        internal void ValidateElementDisabled(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Validating that the element {Element.ToString()} is marked as disabled");

            bool elementDisabled = GetElementAttributeValue(Element, "disabled") == "true";

            if (!elementDisabled)
                throw new Exception($"Element '{Element.ToString()}' is not marked as disabled");
        }

        /// <summary>
        /// Validate that the UI element is NOT marked as disabled
        /// </summary>
        /// <param name="Element">The UI element to search for</param>
        internal void ValidateElementNotDisabled(By Element)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Validating that the element {Element.ToString()} is NOT marked as disabled");

            bool elementDisabled = GetElementAttributeValue(Element, "disabled") == "true";

            if (elementDisabled)
                throw new Exception($"Element '{Element.ToString()}' is marked as disabled");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the identifiers of all windows that are currently managed by this driver instance
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllBrowserTabsIdentifiers()
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Get all window handles for all open browser windows");

            return this.driver.WindowHandles.ToList();
        }

        /// <summary>
        /// Select one browser tab
        /// </summary>
        /// <param name="Window">The browser tab identifier</param>
        public void SwitchToBrowserTab(string Window)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Switching to the window {Window}");

            driver.SwitchTo().Window(Window);
        }

        /// <summary>
        /// Close the current browser tab
        /// </summary>
        public void CloseCurrentBrowserTab()
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Closing the current browser window");

            driver.Close();
        }

        /// <summary>
        /// Get the title for the one of the browser open tabs
        /// </summary>
        /// <param name="Window">The browser tab identifier</param>
        /// <returns></returns>
        public string GetCurrentWindowTitle(string Window)
        {
            LeyaOnline.UITestsFramework.Logger.Logger.LogInfo($"Getting the title for the window {Window}");

            return driver.SwitchTo().Window(Window).Title;
        }

        #endregion


    }
}
