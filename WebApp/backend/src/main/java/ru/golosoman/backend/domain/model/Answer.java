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
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "attempt_ticket_id") // Добавляем внешний ключ для связи с AttemptTicket
    private AttemptTicket attemptTicket;

    @ManyToOne
    @JoinColumn(name = "question_id")
    private Question question;

    @Column(updatable = false) // Дата не должна обновляться
    private LocalDateTime answerDate;

    private boolean result;

    @PrePersist
    protected void onCreate() {
        this.answerDate = LocalDateTime.now(); // Устанавливаем текущее время
    }
}