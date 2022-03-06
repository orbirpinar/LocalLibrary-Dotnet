package com.orbirpinar.scraping.library.facade;

import com.orbirpinar.scraping.library.PageObjects.BookDetailPO;
import com.orbirpinar.scraping.library.dtos.BookDto;
import com.orbirpinar.scraping.library.dtos.GenreDto;
import com.orbirpinar.scraping.library.facade.Interfaces.BookDetailService;
import com.orbirpinar.scraping.library.utils.Mapper;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public class BookDetailServiceImpl implements BookDetailService {

    private final BookDetailPO bookDetailPO;

    public BookDetailServiceImpl(BookDetailPO bookDetailPO) {
        this.bookDetailPO = bookDetailPO;
    }




    @Override
    public BookDto getData() {
        bookDetailPO.initElements();
        String bookTitle = bookDetailPO.getBookTitle();
        Optional<String> isbn = bookDetailPO.getIsbn();
        String summary = bookDetailPO.getSummary();
        List<String> genres = bookDetailPO.getGenres();
        BookDto bookDto = new BookDto();
        isbn.ifPresent(bookDto::setIsbn);
        bookDto.setTitle(bookTitle);
        bookDto.setSummary(summary);
        List<GenreDto> genreDtos = Mapper.mapAll(genres, GenreDto.class);
        bookDto.setGenres(genreDtos);
        return bookDto;
    }

    @Override
    public void clickAuthorDetailLink() {
        bookDetailPO.clickAuthorDetailLink();
    }
}
