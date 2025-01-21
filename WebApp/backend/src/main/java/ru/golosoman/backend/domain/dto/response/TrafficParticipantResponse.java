package ru.golosoman.backend.domain.dto.response;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class TrafficParticipantResponse {
    private Long id;
    private String modelName;
    private String direction;
    private String numberPosition;
    private String participantType;
    private String sidePosition;
}
