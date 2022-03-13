package com.orbirpinar.scraping.library.utils;

import com.orbirpinar.scraping.library.configuration.SeleniumConfig;
import org.openqa.selenium.By;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.List;

@Component
public class DriverCommonUtil {


    @Autowired
    private SeleniumConfig seleniumConfig;


    @Autowired
    private WaitUtils waitUtils;


    public WebDriver getDriver() {
        return seleniumConfig.getDriver();
    }


    public boolean doesElementExists(WebElement element) {
        try {
            return element.isDisplayed() && element.isEnabled();
        } catch (Exception e) {
            return false;
        }
    }

    public boolean doesElementEnabled(WebElement element) {
        try {
            return element.isEnabled();
        } catch (Exception e) {
            return false;
        }
    }

    public boolean doesElementExists(By by) {
        return getDriver().findElements(by).size() > 0;
    }

    public void click(WebElement element) {
        try {
            waitUtils.waitForElementClickable(element);
            element.click();
        } catch (StaleElementReferenceException ex) {
            element = reInitializeStaleElement(element);
            element.click();
        }
    }

    public WebElement reInitializeStaleElement(WebElement element) {
        String elementStr = element.toString();
        elementStr = elementStr.split("->")[1];

        String byType = elementStr.split(":")[0].trim();
        String locator = elementStr.split(":")[1].trim();
        locator = locator.substring(0, locator.length() - 1);


        switch (byType) {
            case "xpath":
                return getDriver().findElement(By.xpath(locator));
            case "css selector":
                return getDriver().findElement(By.cssSelector(locator));
            case "id":
                return getDriver().findElement(By.id(locator));
            case "name":
                return getDriver().findElement(By.name(locator));
            default:
                return getDriver().findElement(By.className(locator));
        }
    }


    public void closeBrowser() {
        getDriver().close();
    }

    public void quitDriver() {
        getDriver().quit();
    }

    public String getCurrentUrl() {
        return getDriver().getCurrentUrl();
    }

    public boolean isBrowserOpen() {
        try {
            getDriver().getCurrentUrl();
            return true;
        } catch (Exception e) {
            return false;
        }
    }
}
