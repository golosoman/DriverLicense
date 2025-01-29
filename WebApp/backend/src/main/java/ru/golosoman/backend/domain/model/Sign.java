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
public class Sign {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(length = 30)
    private String modelName;

    @Column(length = 12)
    private String sidePosition;

    public Sign(String modelName, String sidePosition) {
        this.modelName = modelName;
        this.sidePosition = sidePosition;
    }
}
