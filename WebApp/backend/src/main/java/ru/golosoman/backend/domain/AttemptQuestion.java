package ru.golosoman.backend.domain;

import jakarta.persistence.EmbeddedId;
import jakarta.persistence.Entity;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.MapsId;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.time.LocalDateTime;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class AttemptQuestion {
    @EmbeddedId
    AttemptQuestionKey id;

    @ManyToOne
    @MapsId("userId")
    @JoinColumn(name = "user_id")
    User user;

    @ManyToOne
    @MapsId("questionId")
    @JoinColumn(name = "question_id")
    Question question;

    private LocalDateTime lastAttemptDate;
    private boolean result;
    private Integer positiv_attempt;
    private Integer count_attempt;
}
