package ru.golosoman.backend.domain;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.time.LocalDateTime;

import jakarta.persistence.EmbeddedId;
import jakarta.persistence.Entity;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.MapsId;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class AttemptTicket {
    @EmbeddedId
    AttemptTicketKey id;

    @ManyToOne
    @MapsId("userId")
    @JoinColumn(name = "user_id")
    User user;

    @ManyToOne
    @MapsId("ticketId")
    @JoinColumn(name = "ticket_id")
    Ticket ticket;

    private LocalDateTime lastAttemptDate;
    private boolean result;
    private Integer positiv_attempt;
    private Integer count_attempt;
}
