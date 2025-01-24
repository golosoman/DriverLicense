package ru.golosoman.backend.domain.dto.request;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public class CreateQuestionRequest {
    private String question;
    private String explanation;
    private String intersectionType;
    private String categoryName;
    private List<CreateTrafficLightRequest> trafficLights;
    private List<CreateTrafficParticipantRequest> trafficParticipants;
    private List<CreateSignRequest> signs;
}
