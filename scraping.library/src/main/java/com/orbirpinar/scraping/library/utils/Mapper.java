package com.orbirpinar.scraping.library.utils;

import com.orbirpinar.scraping.library.dtos.GenreDto;
import org.modelmapper.Converter;
import org.modelmapper.ModelMapper;
import org.modelmapper.convention.MatchingStrategies;
import org.modelmapper.spi.MatchingStrategy;

import java.util.Collection;
import java.util.List;
import java.util.stream.Collectors;

public class Mapper {

    private static ModelMapper mapper = new ModelMapper();

    static Converter<String, GenreDto> converter = context -> {
        GenreDto genreDto = new GenreDto();
        genreDto.setName(context.getSource());
        return genreDto;
    };

    static {
        mapper = new ModelMapper();
        mapper.getConfiguration().setMatchingStrategy(MatchingStrategies.STRICT);
        mapper.createTypeMap(String.class, GenreDto.class).setConverter(converter);
    }


    /**
     * Hide from public usage.
     */
    private Mapper() {
    }

    /**
     * <p>Note: outClass object must have default constructor with no arguments</p>
     *
     * @param <D>      type of result object.
     * @param <T>      type of source object to map from.
     * @param entity   entity that needs to be mapped.
     * @param outClass class of result object.
     * @return new object of <code>outClass</code> type.
     */
    public static <D, T> D map(final T entity, Class<D> outClass) {
        return mapper.map(entity, outClass);
    }

    /**
     * <p>Note: outClass object must have default constructor with no arguments</p>
     *
     * @param entityList list of entities that needs to be mapped
     * @param outCLass   class of result list element
     * @param <D>        type of objects in result list
     * @param <T>        type of entity in <code>entityList</code>
     * @return list of mapped object with <code><D></code> type.
     */
    public static <D, T> List<D> mapAll(final Collection<T> entityList, Class<D> outCLass) {
        return entityList.stream()
                .map(entity -> map(entity, outCLass))
                .collect(Collectors.toList());
    }



    /**
     * Maps {@code source} to {@code destination}.
     *
     * @param source      object to map from
     * @param destination object to map to
     */
    public static <S, D> D map(final S source, D destination) {
        mapper.map(source, destination);
        return destination;
    }


}
