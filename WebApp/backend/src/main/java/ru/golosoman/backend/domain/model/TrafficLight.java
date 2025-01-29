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
public class TrafficLight {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(length = 30)
    private String modelName;

    @Column(length = 12)
    private String sidePosition;

    @Column(length = 12)
    private String state;

    public TrafficLight(String modelName, String sidePosition, String state) {
        this.modelName = modelName;
        this.sidePosition = sidePosition;
        this.state = state;
    }
}
