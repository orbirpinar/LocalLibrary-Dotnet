package com.orbirpinar.scraping.library.dtos;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class BookDto {

    @JsonProperty("Title")
    private String title;
    @JsonProperty("Summary")
    private String summary;

    @JsonProperty("Isbn")
    private String isbn;

    @JsonProperty("Genres")
    private List<GenreDto> genres;
}
