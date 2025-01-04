package ru.golosoman.backend.domain.model;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.HashSet;
import java.util.Set;

@Entity
@Getter
@Setter
@NoArgsConstructor
public class Ticket {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;

    public Ticket(String name, String description) {
        this.name = name;
    }

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "TicketQuestion", joinColumns = @JoinColumn(name = "ticket_id"), inverseJoinColumns = @JoinColumn(name = "question_id"))
    private Set<Question> questions = new HashSet<>();

    @OneToMany(mappedBy = "ticket")
    Set<AttemptTicket> attemptTickets;
}
