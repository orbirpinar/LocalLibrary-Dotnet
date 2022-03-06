package com.orbirpinar.scraping.library;

import com.orbirpinar.scraping.library.configuration.RabbitMqConfig;
import com.orbirpinar.scraping.library.configuration.SeleniumConfig;
import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import com.orbirpinar.scraping.library.facade.Interfaces.AutomateFacade;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

import java.net.MalformedURLException;
import java.util.Collections;
import java.util.List;

@Component
public class Execute {

    private final AutomateFacade automateFacade;
    private final RabbitTemplate rabbitTemplate;

    public Execute(AutomateFacade automateFacade, RabbitTemplate rabbitTemplate) {
        this.automateFacade = automateFacade;
        this.rabbitTemplate = rabbitTemplate;
    }

    public void execute() throws MalformedURLException {
        List<ScrapingResponseDto> scrapingResponses = automateFacade.getAllData();
        rabbitTemplate.convertAndSend(RabbitMqConfig.EXCHANGE,RabbitMqConfig.ROUTING_KEY,scrapingResponses);
    }
}
