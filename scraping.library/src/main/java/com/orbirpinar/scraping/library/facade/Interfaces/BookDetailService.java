package com.orbirpinar.scraping.library.facade.Interfaces;

import com.orbirpinar.scraping.library.dtos.BookDto;
import org.springframework.stereotype.Service;

@Service
public interface BookDetailService {

    BookDto getData();
    void clickAuthorDetailLink();
}
