package ru.golosoman.backend.domain.model;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class TrafficParticipant {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String modelName;
    private String sidePosition;
    private String numberPosition;
    private String direction;
    private String participantType;

    public TrafficParticipant(String modelName, String direction, String numberPosition, String participantType, String sidePosition) {
        this.modelName = modelName;
        this.direction = direction;
        this.numberPosition = numberPosition;
        this.participantType = participantType;
        this.sidePosition = sidePosition;
    }
}
