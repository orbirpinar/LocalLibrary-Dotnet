package com.orbirpinar.scraping.library;

import com.orbirpinar.scraping.library.configuration.SeleniumConfig;
import com.orbirpinar.scraping.library.dtos.SearchParamDto;
import com.orbirpinar.scraping.library.facade.Interfaces.AutomateFacade;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Component
public class Execute {

    private final AutomateFacade automateFacade;
    private final SeleniumConfig seleniumConfig;

    public Execute(AutomateFacade automateFacade, SeleniumConfig seleniumConfig) {
        this.automateFacade = automateFacade;
        this.seleniumConfig = seleniumConfig;
    }

    public void getBestBooks() {
        automateFacade.scrapingBestBooks();
    }


    @RabbitListener(queues = "${rabbitMq.queue.searchData}")
    public void consumeByBookTitle(SearchParamDto searchParamDto) throws Exception {
        automateFacade.scrapingByBookTitle(searchParamDto.getTitle());
    }
}
