package com.orbirpinar.scraping.library.producer;

import com.orbirpinar.scraping.library.configuration.RabbitMqConfig;
import com.orbirpinar.scraping.library.dtos.ScrapingResponseDto;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Service;

@Service
public class Producer implements IProducer {

  private final RabbitTemplate rabbitTemplate;

  public Producer(RabbitTemplate rabbitTemplate, RabbitTemplate rabbitTemplate1) {

      this.rabbitTemplate = rabbitTemplate1;
  }

  public void send(ScrapingResponseDto scrapingResponseDto) {
      rabbitTemplate.convertAndSend(RabbitMqConfig.EXCHANGE,RabbitMqConfig.ROUTING_KEY,scrapingResponseDto);
  }
}
