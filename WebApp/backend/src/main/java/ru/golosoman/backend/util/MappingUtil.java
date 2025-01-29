package ru.golosoman.backend.util;

import ru.golosoman.backend.domain.dto.response.*;
import ru.golosoman.backend.domain.model.*;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

/**
 * Утилитарный класс для преобразования доменных моделей в ответные DTO.
 * Содержит методы для маппинга различных сущностей, таких как вопросы, категории, знаки,
 * светофоры, участники дорожного движения и билеты.
 */
public class MappingUtil {

    /**
     * Преобразует объект Question в QuestionResponse.
     *
     * @param question объект вопроса для преобразования.
     * @return объект QuestionResponse.
     */
    public static QuestionResponse mapToQuestionResponse(Question question) {
        return new QuestionResponse(
                question.getId(),
                question.getQuestion(),
                question.getExplanation(),
                question.getIntersectionType(),
                mapToCategoryResponse(question.getCategory()),
                mapToSignResponseList(question.getSigns()),
                mapToTrafficLightResponseList(question.getTrafficLights()),
                mapToTrafficParticipantResponseList(question.getTrafficParticipants())
        );
    }

    /**
     * Преобразует объект Category в CategoryResponse.
     *
     * @param category объект категории для преобразования.
     * @return объект CategoryResponse или null, если категория равна null.
     */
    public static CategoryResponse mapToCategoryResponse(Category category) {
        if (category == null) {
            return null;
        }
        return new CategoryResponse(category.getId(), category.getName());
    }

    /**
     * Преобразует набор объектов Sign в список SignResponse.
     *
     * @param signs набор знаков для преобразования.
     * @return список объектов SignResponse.
     */
    public static List<SignResponse> mapToSignResponseList(Set<Sign> signs) {
        return signs.stream()
                .map(sign -> new SignResponse(sign.getId(), sign.getModelName(), sign.getSidePosition()))
                .collect(Collectors.toList());
    }

    /**
     * Преобразует набор объектов TrafficLight в список TrafficLightResponse.
     *
     * @param trafficLights набор светофоров для преобразования.
     * @return список объектов TrafficLightResponse.
     */
    public static List<TrafficLightResponse> mapToTrafficLightResponseList(Set<TrafficLight> trafficLights) {
        return trafficLights.stream()
                .map(light -> new TrafficLightResponse(light.getId(), light.getModelName(), light.getSidePosition(), light.getState()))
                .collect(Collectors.toList());
    }

    /**
     * Преобразует набор объектов TrafficParticipant в список TrafficParticipantResponse.
     *
     * @param trafficParticipants набор участников дорожного движения для преобразования.
     * @return список объектов TrafficParticipantResponse.
     */
    public static List<TrafficParticipantResponse> mapToTrafficParticipantResponseList(Set<TrafficParticipant> trafficParticipants) {
        return trafficParticipants.stream()
                .map(participant -> new TrafficParticipantResponse(
                        participant.getId(),
                        participant.getModelName(),
                        participant.getDirection(),
                        participant.getNumberPosition(),
                        participant.getParticipantType(),
                        participant.getSidePosition()))
                .collect(Collectors.toList());
    }

    /**
     * Преобразует объект Ticket в TicketResponse.
     *
     * @param ticket объект билета для преобразования.
     * @return объект TicketResponse.
     */
    public static TicketResponse mapToTicketResponse(Ticket ticket) {
        return new TicketResponse(
                ticket.getId(),
                ticket.getName(),
                ticket.getQuestions().stream()
                        .map(MappingUtil::mapToQuestionResponse)
                        .collect(Collectors.toList())
        );
    }
}
