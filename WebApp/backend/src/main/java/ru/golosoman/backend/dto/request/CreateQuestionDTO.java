package ru.golosoman.backend.dto.request;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;

@Setter
@Getter
@AllArgsConstructor
public class CreateQuestionDTO {
    private String title;
    private String question;
    private String explanation;
    private String intersectionType;
    private List<Long> trafficLights;
    private List<Long> trafficParticipants;
    private List<Long> signs;
}
