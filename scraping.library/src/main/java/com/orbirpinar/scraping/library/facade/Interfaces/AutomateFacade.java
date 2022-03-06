package com.orbirpinar.scraping.library.facade.Interfaces;

import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public interface AutomateFacade {

    List<ScrapingResponseDto> getAllData();
}
