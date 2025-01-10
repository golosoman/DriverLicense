package ru.golosoman.backend.domain.model;

import jakarta.persistence.*;
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
public class Answer {
    @EmbeddedId
    private AnswerKey id;

    @ManyToOne
    @JoinColumns({
            @JoinColumn(name = "user_id", referencedColumnName = "user_id", insertable = false, updatable = false),
            @JoinColumn(name = "ticket_id", referencedColumnName = "ticket_id", insertable = false, updatable = false)
    })
    private AttemptTicket attemptTicket;

    @ManyToOne
    @MapsId("questionId")
    @JoinColumn(name = "question_id")
    private Question question;

    @Column(updatable = false) // Дата не должна обновляться
    private LocalDateTime answerDate; // Дата ответа на вопрос

    private boolean result; // true если ответ правильный, false если неправильный

    @PrePersist
    protected void onCreate() {
        this.answerDate = LocalDateTime.now(); // Устанавливаем текущее время
    }
}