package ru.golosoman.backend.domain.model;

import jakarta.persistence.*;
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

    @Column(length = 30)
    private String modelName;

    @Column(length = 12)
    private String sidePosition;

    @Column(length = 12)
    private String numberPosition;

    @Column(length = 12)
    private String direction;

    @Column(length = 12)
    private String participantType;

    public TrafficParticipant(String modelName, String direction, String numberPosition, String participantType, String sidePosition) {
        this.modelName = modelName;
        this.direction = direction;
        this.numberPosition = numberPosition;
        this.participantType = participantType;
        this.sidePosition = sidePosition;
    }
}
