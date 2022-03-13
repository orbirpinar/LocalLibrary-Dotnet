package com.orbirpinar.scraping.library.PageObjects;

import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import com.orbirpinar.scraping.library.utils.WaitUtils;
import lombok.extern.slf4j.Slf4j;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;
import org.springframework.stereotype.Component;

import java.net.MalformedURLException;
import java.nio.file.WatchEvent;

@Component
@Slf4j
public class BookSearchPO extends BasePO{

    public BookSearchPO(WaitUtils waitUtils, DriverCommonUtil driverCommonUtil) throws MalformedURLException {
        super(waitUtils, driverCommonUtil);
    }

    @FindBy(how = How.CLASS_NAME, using = "bookTitle")
    private WebElement bookDetailLink;


    public void clickFirstBookDetailLink() {
        waitUtils.staticWait(500);
        waitUtils.waitForElementClickable(bookDetailLink);
        bookDetailLink.click();
        log.info("<BOOK DETAIL LINK CLICKED>");
    }
}
