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
    private String question;
    private String explanation;
    private String intersectionType;

    public Question(String title, String question, String explanation, String intersectionType) {
        this.question = question;
        this.explanation = explanation;
        this.intersectionType = intersectionType;
    }

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionTrafficLight", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "traffic_light_id"))
    private Set<TrafficLight> trafficLights = new HashSet<>();

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionTrafficParticipant", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "traffic_participant_id"))
    private Set<TrafficParticipant> trafficParticipants = new HashSet<>();

    @ManyToMany(fetch = FetchType.LAZY, cascade = { CascadeType.PERSIST, CascadeType.MERGE })
    @JoinTable(name = "QuestionSign", joinColumns = @JoinColumn(name = "question_id"), inverseJoinColumns = @JoinColumn(name = "sign_id"))
    private Set<Sign> signs = new HashSet<>();

    @ManyToOne(cascade = CascadeType.ALL)
    @JoinColumn(name = "category_id")
    private Category category;
}