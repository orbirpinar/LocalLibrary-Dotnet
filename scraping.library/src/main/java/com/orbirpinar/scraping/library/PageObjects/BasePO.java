package com.orbirpinar.scraping.library.PageObjects;

import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import com.orbirpinar.scraping.library.utils.WaitUtils;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.PageFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;
import java.net.MalformedURLException;

@Component
public abstract class BasePO {

    @Autowired
    protected WaitUtils waitUtils;

    protected WebDriver driver;

    @Autowired
    protected DriverCommonUtil driverCommonUtil;


    public BasePO(WaitUtils waitUtils, DriverCommonUtil driverCommonUtil) throws MalformedURLException {
        this.waitUtils = waitUtils;
        this.driverCommonUtil = driverCommonUtil;
        this.driver = driverCommonUtil.getDriver();
    }

    public void initElements() {
        PageFactory.initElements(driver,this);
    }

    public void navigateTo(String url) {
        driverCommonUtil.getDriver().navigate().to(url);
    }
}