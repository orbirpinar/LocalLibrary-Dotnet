package com.orbirpinar.scraping.library.utils;

import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

public class JsUtil {

    private static JavascriptExecutor js;

    public static void scrollDown(WebDriver driver) {
        js = (JavascriptExecutor) driver;
        js.executeScript("window.scrollBy(0,50)");
    }

    public static void displayInline(WebDriver driver, WebElement element) {
        js = (JavascriptExecutor)  driver;
        js.executeScript("arguments[0].style.display = 'inline';",element);
    }


    public static void displayNone(WebDriver driver, WebElement element) {
        js = (JavascriptExecutor)  driver;
        js.executeScript("arguments[0].style.display = 'none';",element);
    }

    public static void click(WebDriver driver, WebElement element) {
        js = (JavascriptExecutor)  driver;
        js.executeScript("arguments[0].click();",element);
    }
}
