package ru.golosoman.backend.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.model.Question;
import ru.golosoman.backend.domain.model.Sign;
import ru.golosoman.backend.domain.model.TrafficLight;
import ru.golosoman.backend.domain.model.TrafficParticipant;
import ru.golosoman.backend.repository.QuestionRepository;
import ru.golosoman.backend.repository.SignRepository;
import ru.golosoman.backend.repository.TrafficLightRepository;
import ru.golosoman.backend.repository.TrafficParticipantRepository;

import java.util.Random;
import java.util.HashSet;
import java.util.List;
import java.util.Optional;
import java.util.Set;

@Service
public class QuestionService {

    @Autowired
    private QuestionRepository questionRepository;

    @Autowired
    private TrafficLightRepository trafficLightRepository;

    @Autowired
    private TrafficParticipantRepository trafficParticipantRepository;

    @Autowired
    private SignRepository signRepository;

    public Question createQuestion(String title, String question, String explanation, String intersectionType,
                                   List<Long> trafficLightIds, List<Long> trafficParticipantIds, List<Long> signIds) {

        Question newQuestion = new Question(title, question, explanation, intersectionType);

        if (trafficLightIds != null) {
            Set<TrafficLight> trafficLights = new HashSet<>();
            for (Long id : trafficLightIds) {
                Optional<TrafficLight> trafficLightOptional = trafficLightRepository.findById(id);
                trafficLightOptional.ifPresent(trafficLights::add);
            }
            newQuestion.setTrafficLights(trafficLights);
        }

        if (trafficParticipantIds != null) {
            Set<TrafficParticipant> trafficParticipants = new HashSet<>();
            for (Long id : trafficParticipantIds) {
                Optional<TrafficParticipant> participantOptional = trafficParticipantRepository.findById(id);
                participantOptional.ifPresent(trafficParticipants::add);
            }
            newQuestion.setTrafficParticipants(trafficParticipants);
        }

        if (signIds != null) {
            Set<Sign> signs = new HashSet<>();
            for (Long id : signIds) {
                Optional<Sign> signOptional = signRepository.findById(id);
                signOptional.ifPresent(signs::add);
            }
            newQuestion.setSigns(signs);
        }

        return questionRepository.save(newQuestion);
    }

    public List<Question> getAllQuestions() {
        return questionRepository.findAll();
    }

    public Question getQuestionById(Long id) {
        Optional<Question> questionOptional = questionRepository.findById(id);
        return questionOptional.orElse(null);
    }

    public Question updateQuestion(Long id, Question question) {
        Optional<Question> existingQuestionOptional = questionRepository.findById(id);
        if (existingQuestionOptional.isPresent()) {
            Question existingQuestion = existingQuestionOptional.get();
            existingQuestion.setQuestion(question.getQuestion());
            existingQuestion.setExplanation(question.getExplanation());
            existingQuestion.setIntersectionType(question.getIntersectionType());
            return questionRepository.save(existingQuestion);
        }
        return null; // Вопрос не найден
    }

    // Удаление вопроса по ID
    public boolean deleteQuestion(Long id) {
        Optional<Question> questionOptional = questionRepository.findById(id);
        if (questionOptional.isPresent()) {
            questionRepository.deleteById(id);
            return true;
        }
        return false; // Вопрос не найден
    }

    public Question getRandomQuestion() {
        return questionRepository.findRandomQuestion();
    }
}
