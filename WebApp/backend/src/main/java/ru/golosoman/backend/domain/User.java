package ru.golosoman.backend.domain;

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
@Table(name = "\"user\"")
public class User {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(unique = true)
    private String login;
    private String password;

    @OneToMany(mappedBy = "user")
    Set<AttemptTicket> attemptTickets;

    @OneToMany(mappedBy = "user")
    Set<AttemptQuestion> attemptQuestions;
}
