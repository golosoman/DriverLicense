package ru.golosoman.backend.domain.dto.response;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class QuestionResponse {
    private Long id;
    private String question;
    private String explanation;
    private String intersectionType;
    private CategoryResponse category;
    private List<SignResponse> signs;
    private List<TrafficLightResponse> trafficLights;
    private List<TrafficParticipantResponse> trafficParticipants;
}
