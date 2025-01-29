package ru.golosoman.backend.util;

import ru.golosoman.backend.domain.dto.response.*;
import ru.golosoman.backend.domain.model.*;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

public class MappingUtil {

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

    public static CategoryResponse mapToCategoryResponse(Category category) {
        if (category == null) {
            return null;
        }
        return new CategoryResponse(category.getId(), category.getName());
    }

    public static List<SignResponse> mapToSignResponseList(Set<Sign> signs) {
        return signs.stream()
                .map(sign -> new SignResponse(sign.getId(), sign.getModelName(), sign.getSidePosition()))
                .collect(Collectors.toList());
    }

    public static List<TrafficLightResponse> mapToTrafficLightResponseList(Set<TrafficLight> trafficLights) {
        return trafficLights.stream()
                .map(light -> new TrafficLightResponse(light.getId(), light.getModelName(), light.getSidePosition(), light.getState()))
                .collect(Collectors.toList());
    }

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
}
