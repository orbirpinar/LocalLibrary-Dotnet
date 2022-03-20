package com.orbirpinar.scraping.library.facade.Interfaces;

import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import com.orbirpinar.scraping.library.dtos.SearchParamDto;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface AutomateFacade {

    void scrapingBestBooks();
    void scrapingByBookTitle(String title) throws Exception;

}
