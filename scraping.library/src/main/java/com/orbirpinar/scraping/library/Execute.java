package com.orbirpinar.scraping.library;

import com.orbirpinar.scraping.library.facade.Interfaces.AutomateFacade;
import org.springframework.stereotype.Component;

@Component
public class Execute {

    private final AutomateFacade automateFacade;

    public Execute(AutomateFacade automateFacade) {
        this.automateFacade = automateFacade;
    }

    public void execute() {
        automateFacade.scrapingBestBooks();
    }
}
