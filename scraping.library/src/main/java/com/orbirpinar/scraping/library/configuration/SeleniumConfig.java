package com.orbirpinar.scraping.library.configuration;

import lombok.extern.slf4j.Slf4j;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;
import java.net.MalformedURLException;
import java.net.URL;
import java.sql.Driver;

@Component
@Slf4j
@Order(1)
public class SeleniumConfig {

    private static SeleniumConfig seleniumConfig;
    private WebDriver driver;

    @Value("${selenium.url.chrome}")
    private String SELENIUM_CHROME_URL;

    @PostConstruct
    public void initializeDriver() throws MalformedURLException {
        log.info("Initializing to driver");
        driver = new RemoteWebDriver(new URL(SELENIUM_CHROME_URL),getRemoteOptions());
        driver.manage().window().maximize();
    }

    private ChromeOptions getRemoteOptions() {
        ChromeOptions options = new ChromeOptions();
        options.addArguments("--no-sandbox");
        options.addArguments("--disable-dev-shm-usage");
        options.addArguments("--disable-setuid-sandbox");
        options.addArguments("--disable-gpu");
        options.addArguments("--disable-dev-shm-usage");
        return options;
    }

    public static SeleniumConfig getSeleniumManager() throws MalformedURLException {
        if(seleniumConfig == null)
            seleniumConfig = new SeleniumConfig();
        return seleniumConfig;
    }

    public WebDriver getDriver() {
        return driver;
    }
}
