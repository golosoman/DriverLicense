package ru.golosoman.backend.domain.dto;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public class CreateQuestionDTO {
    private String title;
    private String question;
    private String explanation;
    private String intersectionType;
    private List<Long> trafficLights;
    private List<Long> trafficParticipants;
    private List<Long> signs;
}