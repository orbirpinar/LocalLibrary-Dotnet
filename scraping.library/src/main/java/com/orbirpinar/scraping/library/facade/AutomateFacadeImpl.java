package com.orbirpinar.scraping.library.facade;

import com.orbirpinar.scraping.library.PageObjects.BookListPO;
import com.orbirpinar.scraping.library.dtos.AuthorDto;
import com.orbirpinar.scraping.library.dtos.BookDto;
import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import com.orbirpinar.scraping.library.facade.Interfaces.AuthorDetailService;
import com.orbirpinar.scraping.library.facade.Interfaces.AutomateFacade;
import com.orbirpinar.scraping.library.facade.Interfaces.BookDetailService;
import com.orbirpinar.scraping.library.producer.IProducer;
import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;

@Service
public class AutomateFacadeImpl implements AutomateFacade {


    @Autowired
    private final BookListPO bookListPO;
    private final AuthorDetailService authorDetailService;
    private final BookDetailService bookDetailService;
    private final DriverCommonUtil driverCommonUtil;
    private final IProducer producer;

    @Value("${base-url}")
    private String BASE_URL;

    public AutomateFacadeImpl(BookListPO bookListPO,
                              AuthorDetailService authorDetailService, BookDetailService bookDetailService,
                              DriverCommonUtil driverCommonUtil,
                              IProducer producer) {

        this.bookListPO = bookListPO;
        this.authorDetailService = authorDetailService;
        this.bookDetailService = bookDetailService;
        this.driverCommonUtil = driverCommonUtil;
        this.producer = producer;
    }

    public List<ScrapingResponseDto> getAllData() {
        List<ScrapingResponseDto> scrapingResponseDtos = new ArrayList<>();
        for (int i = 1; i <= 1; i++) {
            bookListPO.navigateTo(BASE_URL + "?page=" + i);
            for (int j = 38; j < 41; j++) {
                ScrapingResponseDto scrapingResponseDto = new ScrapingResponseDto();
                driverCommonUtil.click(bookListPO.getListOfDetailLink().get(j));
                BookDto bookDto = bookDetailService.getData();
                bookDetailService.clickAuthorDetailLink();
                AuthorDto authorDto = authorDetailService.getData();
                scrapingResponseDto.setBook(bookDto);
                scrapingResponseDto.setAuthor(authorDto);
                scrapingResponseDtos.add(scrapingResponseDto);
                // send to rabbitmq
                producer.send(scrapingResponseDto);

                bookListPO.navigateTo(BASE_URL + "?page=" + i);
            }
        }
        return scrapingResponseDtos;
    }
}
