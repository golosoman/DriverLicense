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
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @ManyToOne
    @JoinColumn(name = "user_id")
    private User user;

    @ManyToOne
    @JoinColumn(name = "ticket_id")
    private Ticket ticket;

    @Column(updatable = false) // Дата не должна обновляться
    private LocalDateTime attemptDate;

    private boolean result;

    @OneToMany(mappedBy = "attemptTicket", cascade = CascadeType.ALL)
    private List<Answer> answers;

    @PrePersist
    protected void onCreate() {
        this.attemptDate = LocalDateTime.now(); // Устанавливаем текущее время
    }
}
