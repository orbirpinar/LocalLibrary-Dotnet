package com.orbirpinar.scraping.library.facade;

import com.orbirpinar.scraping.library.PageObjects.BookListPO;
import com.orbirpinar.scraping.library.dtos.AuthorDto;
import com.orbirpinar.scraping.library.dtos.BookDto;
import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import com.orbirpinar.scraping.library.dtos.SearchParamDto;
import com.orbirpinar.scraping.library.facade.Interfaces.AuthorDetailService;
import com.orbirpinar.scraping.library.facade.Interfaces.AutomateFacade;
import com.orbirpinar.scraping.library.facade.Interfaces.BookService;
import com.orbirpinar.scraping.library.producer.IProducer;
import com.orbirpinar.scraping.library.utils.DriverCommonUtil;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class AutomateFacadeImpl implements AutomateFacade {


    @Autowired
    private final BookListPO bookListPO;
    private final AuthorDetailService authorDetailService;
    private final BookService bookService;
    private final DriverCommonUtil driverCommonUtil;
    private final IProducer producer;

    @Value("${base-url}")
    private String BASE_URL;

    public AutomateFacadeImpl(BookListPO bookListPO,
                              AuthorDetailService authorDetailService, BookService bookDetailService,
                              DriverCommonUtil driverCommonUtil,
                              IProducer producer) {

        this.bookListPO = bookListPO;
        this.authorDetailService = authorDetailService;
        this.bookService = bookDetailService;
        this.driverCommonUtil = driverCommonUtil;
        this.producer = producer;
    }

    public void scrapingBestBooks() {
        for (int i = 1; i <= 1; i++) {
            bookListPO.navigateTo(BASE_URL + "/list/show/1.Best_Books_Ever?page=" + i);
            for (int j = 51; j < 55; j++) {
                String smallCoverLink = bookService.getSmallCoverImageLink();
                driverCommonUtil.click(bookListPO.getListOfDetailLink().get(j));
                sendData(smallCoverLink);
                bookListPO.navigateTo(BASE_URL + "/list/show/1.Best_Books_Ever?page=" + i);
            }
        }
    }


    @Override
    @RabbitListener(queues = "${rabbitMq.queue.searchData}")
    public void scrapingByBookTitle(SearchParamDto searchParamDto) throws Exception {
        bookListPO.navigateTo(BASE_URL + "/search?q=" + searchParamDto.getTitle());
        String smallCoverImageLink = bookService.getSmallCoverImageLink();
        bookService.clickBookDetail();
        sendData(smallCoverImageLink);

    }

    private void sendData(String smallCoverImageLink) {
        ScrapingResponseDto scrapingResponseDto = new ScrapingResponseDto();
        BookDto bookDto = bookService.getData();
        bookDto.setSmallCoverLink(smallCoverImageLink);
        bookService.clickAuthorDetailLink();
        AuthorDto authorDto = authorDetailService.getData();
        scrapingResponseDto.setBook(bookDto);
        scrapingResponseDto.setAuthor(authorDto);

        // send to rabbitmq
        producer.send(scrapingResponseDto);
    }

}
