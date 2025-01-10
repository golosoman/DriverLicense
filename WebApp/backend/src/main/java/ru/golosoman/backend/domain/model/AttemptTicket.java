package ru.golosoman.backend.domain.model;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.List;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class AttemptTicket {
    @EmbeddedId
    private AttemptTicketKey id;

    @ManyToOne
    @MapsId("userId")
    @JoinColumn(name = "user_id")
    private User user;

    @ManyToOne
    @MapsId("ticketId")
    @JoinColumn(name = "ticket_id")
    private Ticket ticket;

    @Column(updatable = false) // Дата не должна обновляться
    private LocalDateTime attemptDate; // Дата попытки

    private boolean result; // Результат попытки

    @OneToMany(mappedBy = "attemptTicket")
    private List<Answer> answers; // Список ответов

    @PrePersist
    protected void onCreate() {
        this.attemptDate = LocalDateTime.now(); // Устанавливаем текущее время
    }
}