package com.orbirpinar.scraping.library.utils;

import com.orbirpinar.scraping.library.configuration.SeleniumConfig;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.util.concurrent.TimeUnit;

@Component
public class WaitUtils {

    @Value("${explicit-wait:20}")
    public int explicitWaitDefault;

    private final SeleniumConfig seleniumConfig;

    public WaitUtils(SeleniumConfig seleniumConfig) {
        this.seleniumConfig = seleniumConfig;
    }


    private WebDriver getDriver() {
        return seleniumConfig.getDriver();
    }
    public void staticWait(final long millis) {
        try {
            TimeUnit.MILLISECONDS.sleep(millis);
        } catch (final InterruptedException e) {
        }
    }

    public void waitForElementClickable(WebElement element) {
        new WebDriverWait(getDriver(),this.explicitWaitDefault)
                .until(ExpectedConditions.elementToBeClickable(element));
    }
    public void waitForElementToBeVisible(final WebElement element) {
        long s = System.currentTimeMillis();
        new WebDriverWait(getDriver(), this.explicitWaitDefault).until(ExpectedConditions.visibilityOf(element));
    }

    public void waitForElementToBeVisible(WebDriver driver, final WebElement element) {
        long s = System.currentTimeMillis();
        new WebDriverWait(driver, this.explicitWaitDefault).until(ExpectedConditions.visibilityOf(element));
    }

}
