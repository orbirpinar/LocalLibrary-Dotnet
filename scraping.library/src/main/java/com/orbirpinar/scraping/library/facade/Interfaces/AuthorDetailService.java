package com.orbirpinar.scraping.library.facade.Interfaces;

import com.orbirpinar.scraping.library.dtos.AuthorDto;
import org.springframework.stereotype.Service;

@Service
public interface AuthorDetailService {

    AuthorDto getData();
}
