package com.orbirpinar.scraping.library.PageObjects;

import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import com.orbirpinar.scraping.library.utils.WaitUtils;
import lombok.extern.slf4j.Slf4j;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;
import org.openqa.selenium.support.PageFactory;
import org.springframework.stereotype.Component;

import javax.annotation.PostConstruct;
import java.net.MalformedURLException;
import java.util.List;

@Slf4j
@Component
public class BookListPO extends BasePO{

    public BookListPO(WaitUtils waitUtils, DriverCommonUtil driverCommonUtil) throws MalformedURLException {
        super(waitUtils, driverCommonUtil);
        PageFactory.initElements(driverCommonUtil.getDriver(), this);
    }



    @FindBy(how = How.CLASS_NAME,using = "bookTitle")
    private WebElement detailLink;

    @FindBy(how = How.CLASS_NAME,using = "bookTitle")
    private List<WebElement> detailLinks;

    @FindBy(how = How.CLASS_NAME, using = "bookCover")
    private WebElement smallCoverImage;

    public void clickDetailLink() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementClickable(detailLink);
        detailLink.click();
        log.info("<DETAIL LINK CLICKED>");
    }

    public List<WebElement> getListOfDetailLink() {
        PageFactory.initElements(driverCommonUtil.getDriver(),this);
        return detailLinks;
    }

    public String getSmallCoverLink() {
        waitUtils.staticWait(500);
        waitUtils.waitForElementToBeVisible(smallCoverImage);
        if(driverCommonUtil.doesElementExists(smallCoverImage)) {
            return smallCoverImage.getAttribute("src");
        }
        return "";
    }



}
