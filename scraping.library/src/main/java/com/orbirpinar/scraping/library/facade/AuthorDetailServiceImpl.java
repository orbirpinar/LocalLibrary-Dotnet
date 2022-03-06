package com.orbirpinar.scraping.library.facade;

import com.orbirpinar.scraping.library.PageObjects.AuthorDetailPO;
import com.orbirpinar.scraping.library.dtos.AuthorDto;
import com.orbirpinar.scraping.library.facade.Interfaces.AuthorDetailService;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.util.Optional;

@Service
public class AuthorDetailServiceImpl implements AuthorDetailService {


    private final AuthorDetailPO authorDetailPO;

    public AuthorDetailServiceImpl(AuthorDetailPO authorDetailPO) {
        this.authorDetailPO = authorDetailPO;
    }

    @Override
    public AuthorDto getData() {
        authorDetailPO.initElements();
        String authorName = authorDetailPO.getAuthorName();
        Optional<LocalDate> dateOfBirth = authorDetailPO.getDateOfBirth();
        Optional<LocalDate> dateOfDeath = authorDetailPO.getDateOfDeath();
        String authorInfo = authorDetailPO.getAuthorInfo();
        AuthorDto authorDto = new AuthorDto();
        authorDto.setName(authorName);
        dateOfBirth.ifPresent(authorDto::setDateOfBirth);
        dateOfDeath.ifPresent(authorDto::setDateOfDeath);
        authorDto.setInfo(authorInfo);
        return authorDto;

    }
}
