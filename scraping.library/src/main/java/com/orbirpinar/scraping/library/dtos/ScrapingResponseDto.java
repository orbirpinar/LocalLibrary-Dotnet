package com.orbirpinar.scraping.library.dtos;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ScrapingResponseDto {

        @JsonProperty("Author")
        private AuthorDto author;

        @JsonProperty("Book")
        private BookDto book;
}
