package com.orbirpinar.scraping.library.dtos;

import java.time.LocalDate;
import java.time.LocalDateTime;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.databind.annotation.JsonSerialize;
import com.fasterxml.jackson.datatype.jsr310.ser.LocalDateSerializer;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class AuthorDto {

    @JsonProperty("Name")
    private String name;

    @JsonProperty("Info")
    private String info;

    @JsonFormat(pattern="yyyy-MM-dd")
    @JsonProperty("DateOfBirth")
    private LocalDate dateOfBirth;

    @JsonFormat(pattern="yyyy-MM-dd")
    @JsonProperty("DateOfDeath")
    private LocalDate dateOfDeath;
}
