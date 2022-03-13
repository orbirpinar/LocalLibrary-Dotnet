package com.orbirpinar.scraping.library.facade.Interfaces;

import com.orbirpinar.scraping.library.dtos.BookDto;
import org.springframework.stereotype.Service;

@Service
public interface BookService {

    BookDto getData();
    void clickAuthorDetailLink();
    void clickBookDetail();
}
