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
public class Question {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(length = 150)
    private String question;

    @Column(length = 150)
    private String explanation;

    @Column(length = 30)
    private String intersectionType;

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionTrafficLight", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "traffic_light_id"))
    private Set<TrafficLight> trafficLights = new HashSet<>();

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionTrafficParticipant", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "traffic_participant_id"))
    private Set<TrafficParticipant> trafficParticipants = new HashSet<>();

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionSign", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "sign_id"))
    private Set<Sign> signs = new HashSet<>();

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "category_id")
    private Category category;

    @OneToMany(mappedBy = "question")
    private Set<Answer> answers;
}
