package com.orbirpinar.scraping.library.PageObjects;

import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import com.orbirpinar.scraping.library.utils.JsUtil;
import com.orbirpinar.scraping.library.utils.WaitUtils;
import lombok.extern.slf4j.Slf4j;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;
import org.openqa.selenium.support.PageFactory;
import org.springframework.stereotype.Component;

import java.net.MalformedURLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Component
@Slf4j
public class BookDetailPO extends BasePO {

    public BookDetailPO(WaitUtils waitUtils, DriverCommonUtil driverCommonUtil) throws MalformedURLException {
        super(waitUtils, driverCommonUtil);
    }

    @FindBy(how = How.ID, using = "bookTitle")
    private WebElement bookTitleTag;

    @FindBy(how = How.CSS, using = "#description a")
    private WebElement moreButton;

    @FindBy(how = How.CSS, using = "#description span:nth-child(1)")
    private WebElement shortDescription;

    @FindBy(how = How.CSS, using = "#description span:nth-child(2)")
    private WebElement longDescription;

    @FindBy(how = How.CLASS_NAME, using = "authorName")
    private WebElement authorDetailLink;

    @FindBy(how = How.XPATH, using = "//span[@item-prop='isbn']")
    private WebElement isbnTag;

    @FindBy(how = How.CSS, using = ".actionLinkLite.bookPageGenreLink")
    private List<WebElement> genreTags;

    @FindBy(how = How.CLASS_NAME, using = "modalOpened")
    private WebElement modal;

    @FindBy(how = How.CSS, using = ".modal__close button")
    private WebElement modalCloseButton;

    @FindBy(how = How.ID, using = "bookDataBoxShow")
    private WebElement bookDataBoxShow;

    @FindBy(how = How.ID, using = "coverImage")
    private WebElement mediumCoverImageTag;


    public String getBookTitle() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementToBeVisible(bookTitleTag);
        log.info("<GETTING BOOK TITLE>, <{}", bookTitleTag.getText());
        return bookTitleTag.getText();
    }

    public void clickMoreButton() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementClickable(moreButton);
        moreButton.click();
        log.info("<MORE BUTTON CLICKED>");
    }

    public void closeModalIfExists() {
        waitUtils.staticWait(1000);
        if(driverCommonUtil.doesElementExists(modal)) {
            JsUtil.click(driver,modalCloseButton);
        }
    }

    public String getSummary() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementToBeVisible(shortDescription);
        if(!driverCommonUtil.doesElementEnabled(longDescription)) {
            String shortDescriptionText = shortDescription.getText().replaceAll("/","").trim();
            log.info("<GETTING SUMMARY> <{}>", shortDescriptionText);
            return shortDescriptionText;
        }
        JsUtil.displayNone(driver,shortDescription);
        waitUtils.staticWait(500);
        JsUtil.displayInline(driver,longDescription);
        String descriptionText = longDescription.getText()
                .replaceAll("/", "")
                .trim();
        log.info("<GETTING SUMMARY> <{}>", descriptionText);
        return descriptionText;
    }

    public void clickAuthorDetailLink() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementClickable(authorDetailLink);
        authorDetailLink.click();
        log.info("<AUTHOR DETAIL LINK CLICKED>");
    }

    public Optional<String> getIsbn() {
        clickMoreDataButton();
        List<WebElement> isbn13Tag = driver.findElements(By.xpath("//div[contains(text(),'ISBN')]/following::span[@itemProp='isbn']"));
        List<WebElement> isbn10Tag = driver.findElements(By.xpath("//div[contains(text(),'ISBN')]/following::div"));
        if (!isbn13Tag.isEmpty()) {
            return Optional.of(isbn13Tag.get(0).getText());
        }

        if (!isbn10Tag.isEmpty()) {
            return Optional.of(isbn10Tag.get(0).getText());
        }
        return Optional.empty();
    }

    public List<String> getGenres() {
        if (!genreTags.isEmpty()) {
            return genreTags.stream()
                    .map(WebElement::getText)
                    .distinct()
                    .collect(Collectors.toList());
        }
        return new ArrayList<>();
    }

    public String getMediumCoverLink() {
        waitUtils.staticWait(500);
        if(driverCommonUtil.doesElementExists(mediumCoverImageTag)) {
            waitUtils.waitForElementToBeVisible(mediumCoverImageTag);
            return mediumCoverImageTag.getAttribute("src");
        }
        return "";
    }

    private void clickMoreDataButton(){
        bookDataBoxShow.click();
    }


}
