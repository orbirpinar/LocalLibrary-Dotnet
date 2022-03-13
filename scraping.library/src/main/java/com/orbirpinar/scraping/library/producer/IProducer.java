package com.orbirpinar.scraping.library.producer;

import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;

public interface IProducer {
    void send(ScrapingResponseDto scrapingResponseDto);
}
