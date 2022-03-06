package com.orbirpinar.scraping.library.controller;

import com.orbirpinar.scraping.library.Execute;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

import java.net.MalformedURLException;

@RestController
public class SeedController {

    private final Execute execute;

    public SeedController(Execute execute) {
        this.execute = execute;
    }

    @GetMapping("/seed")
    public void seed() throws MalformedURLException {
        execute.execute();
    }
}
