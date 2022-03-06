package com.orbirpinar.scraping.library.PageObjects;

import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import com.orbirpinar.scraping.library.utils.JsUtil;
import com.orbirpinar.scraping.library.utils.WaitUtils;
import lombok.extern.slf4j.Slf4j;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;
import org.springframework.stereotype.Component;

import java.net.MalformedURLException;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.Locale;
import java.util.Optional;

@Component
@Slf4j
public class AuthorDetailPO extends BasePO {

    public AuthorDetailPO(WaitUtils waitUtils, DriverCommonUtil driverCommonUtil) throws MalformedURLException {
        super(waitUtils, driverCommonUtil);
    }

    @FindBy(how = How.CSS, using = ".authorName span")
    private WebElement usingTag;

    @FindBy(how = How.XPATH, using = "//div[@itemProp='birthDate']")
    private WebElement dateOfBirthTag;

    @FindBy(how = How.XPATH, using = "//div[@itemProp='deathDate']")
    private WebElement dateOfDeathTag;

    @FindBy(how = How.XPATH, using = "//span[contains(@id,'freeTextauthor')]")
    private WebElement detailAuthorInfoTag;


    @FindBy(how = How.XPATH, using = "//span[contains(@id,'freeTextContainerauthor')]")
    private WebElement authorInfoTag;

    @FindBy(how = How.CSS, using = ".aboutAuthorInfo a")
    private WebElement moreButton;


    public String getAuthorName() {
        waitUtils.staticWait(1000);
        waitUtils.waitForElementToBeVisible(usingTag);
        return usingTag.getText();
    }

    public Optional<LocalDate> getDateOfBirth() {
        waitUtils.staticWait(1000);
        if (driverCommonUtil.doesElementExists(dateOfBirthTag)) {
            waitUtils.waitForElementToBeVisible(dateOfBirthTag);
            String strDate = dateOfBirthTag.getText();
            return Optional.of(toLocalDate(strDate));
        }
        return Optional.empty();
    }

    public Optional<LocalDate> getDateOfDeath() {
        waitUtils.staticWait(1000);
        if (driverCommonUtil.doesElementExists(dateOfDeathTag)) {
            waitUtils.waitForElementToBeVisible(dateOfDeathTag);
            String strDate = dateOfDeathTag.getText();
            return Optional.of(toLocalDate(strDate));
        }
        return Optional.empty();
    }

    private LocalDate toLocalDate(String strDate) {
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("MMMM d, yyyy", Locale.US);
        return LocalDate.parse(strDate, formatter);
    }


    public String getAuthorInfo() {
        waitUtils.staticWait(1000);
        if (isMoreButtonExists()) {
            JsUtil.displayNone(driver,authorInfoTag);
            JsUtil.displayInline(driver, detailAuthorInfoTag);
            waitUtils.waitForElementToBeVisible(detailAuthorInfoTag);
            return detailAuthorInfoTag.getText();
        }
        return authorInfoTag.getText();
    }

    public boolean isMoreButtonExists() {
        List<WebElement> moreButton = driver.findElements(By.xpath("//a[contains(@data-text-id,'author')]"));
        return !moreButton.isEmpty();
    }


}
