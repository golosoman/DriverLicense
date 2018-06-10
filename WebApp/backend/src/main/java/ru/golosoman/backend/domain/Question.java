package ru.golosoman.backend.domain;

import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import java.util.HashSet;
import java.util.Set;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.CascadeType;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.ManyToMany;
import jakarta.persistence.OneToMany;
import jakarta.persistence.JoinTable;
import jakarta.persistence.JoinColumn;

@Entity
@Getter
@Setter
@NoArgsConstructor
public class Question {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String title;
    private String question;
    private String explanation;
    private String intersectionType;

    public Question(String title, String question, String explanation, String intersectionType) {
        this.title = title;
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

    @OneToMany(mappedBy = "question")
    Set<AttemptQuestion> attemptQuestions;
}
