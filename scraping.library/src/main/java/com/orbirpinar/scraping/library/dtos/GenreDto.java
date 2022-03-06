package com.orbirpinar.scraping.library.dtos;

import com.fasterxml.jackson.annotation.JsonProperty;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class GenreDto {

    @JsonProperty("Name")
    private String name;
}
