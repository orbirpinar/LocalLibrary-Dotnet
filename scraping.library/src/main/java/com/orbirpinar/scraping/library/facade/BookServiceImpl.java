package com.orbirpinar.scraping.library.facade;

import com.orbirpinar.scraping.library.PageObjects.BookDetailPO;
import com.orbirpinar.scraping.library.PageObjects.BookListPO;
import com.orbirpinar.scraping.library.dtos.BookDto;
import com.orbirpinar.scraping.library.dtos.GenreDto;
import com.orbirpinar.scraping.library.facade.Interfaces.BookService;
import com.orbirpinar.scraping.library.utils.Mapper;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class BookServiceImpl implements BookService {

    private final BookDetailPO bookDetailPO;
    private final BookListPO bookListPO;

    public BookServiceImpl(BookDetailPO bookDetailPO, BookListPO bookListPO) {
        this.bookDetailPO = bookDetailPO;
        this.bookListPO = bookListPO;
        bookDetailPO.initElements();
        bookListPO.initElements();
    }


    @Override
    public BookDto getData() {
        bookDetailPO.closeModalIfExists();
        String bookTitle = bookDetailPO.getBookTitle();
        Optional<String> isbn = bookDetailPO.getIsbn();
        String summary = bookDetailPO.getSummary();
        List<String> genres = bookDetailPO.getGenres();
        String mediumCoverLink = bookDetailPO.getMediumCoverLink();
        BookDto bookDto = new BookDto();
        isbn.ifPresent(bookDto::setIsbn);
        bookDto.setTitle(bookTitle);
        bookDto.setSummary(summary);
        bookDto.setMediumCoverLink(mediumCoverLink);
        List<GenreDto> genreDtos = Mapper.mapAll(genres, GenreDto.class);
        bookDto.setGenres(genreDtos);
        return bookDto;
    }

    @Override
    public void clickAuthorDetailLink() {
        bookDetailPO.clickAuthorDetailLink();
    }

    @Override
    public void clickBookDetail() {
        bookListPO.clickDetailLink();
    }

    @Override
    public String getSmallCoverImageLink() {
        return bookListPO.getSmallCoverLink();
    }
}
