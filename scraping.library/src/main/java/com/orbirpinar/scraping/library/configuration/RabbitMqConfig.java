package com.orbirpinar.scraping.library.configuration;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.springframework.amqp.core.*;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.amqp.support.converter.MessageConverter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;


@Configuration
public class RabbitMqConfig {

    public static final String QUEUE = "seedDataQueue";
    public static final String EXCHANGE = "seedDataExchange";
    public static final String ROUTING_KEY = "seedDataRoutingKey";
    public static final String DEAD_LETTER_QUEUE = "seedDataQueue.deadLetter";
    public static final String DEAD_LETTER_EXCHANGE = "seedDataExchange.deadLetter";
    private static final String DEAD_LETTER_ROUTING_KEY = "seedDataRoutingKey.deadLetter" ;

    @Bean
    public Queue queue() {
        return QueueBuilder.durable(QUEUE)
                .withArgument("x-dead-letter",DEAD_LETTER_EXCHANGE)
                .withArgument("x-dead-letter-routing-key",DEAD_LETTER_ROUTING_KEY)
                .build();
    }

    @Bean
    public Queue deadLetterQueue() {
        return QueueBuilder.durable(DEAD_LETTER_QUEUE).build();
    }

    @Bean
    public TopicExchange exchange() {
        return new TopicExchange(EXCHANGE);
    }

    @Bean
    public DirectExchange deadLetterExchange() {
        return new DirectExchange(DEAD_LETTER_EXCHANGE);
    }

    @Bean
    public Binding binding(Queue queue) {
        return BindingBuilder.bind(queue).to(exchange()).with(ROUTING_KEY);
    }

    @Bean
    public Binding deadLetterbinding(Queue queue) {
        return BindingBuilder.bind(queue).to(deadLetterExchange()).with(DEAD_LETTER_ROUTING_KEY);
    }


    @Bean
    public MessageConverter converter() {
        ObjectMapper mapper = new ObjectMapper().findAndRegisterModules();
        return new Jackson2JsonMessageConverter(mapper);
    }

    @Bean
    public AmqpTemplate template(ConnectionFactory connectionFactory) {
        final RabbitTemplate rabbitTemplate = new RabbitTemplate(connectionFactory);
        rabbitTemplate.setMessageConverter(converter());
        return rabbitTemplate;
    }
}
