package ru.golosoman.backend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.request.CreateQuestionRequest;
import ru.golosoman.backend.domain.dto.request.CreateSignRequest;
import ru.golosoman.backend.domain.dto.request.CreateTrafficLightRequest;
import ru.golosoman.backend.domain.dto.request.CreateTrafficParticipantRequest;
import ru.golosoman.backend.domain.dto.response.QuestionResponse;
import ru.golosoman.backend.domain.model.*;
import ru.golosoman.backend.exception.ResourceNotFoundException;
import ru.golosoman.backend.repository.*;
import ru.golosoman.backend.util.MappingUtil;

import java.util.List;
import java.util.Set;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class QuestionService {
    private final QuestionRepository questionRepository;
    private final CategoryRepository categoryRepository;
    private final SignRepository signRepository;
    private final TrafficLightRepository trafficLightRepository;
    private final TrafficParticipantRepository trafficParticipantRepository;

    public List<QuestionResponse> getAllQuestions() {
        return questionRepository.findAll().stream()
                .map(MappingUtil::mapToQuestionResponse)
                .toList();
    }

    public QuestionResponse getQuestionById(Long id) {
        return questionRepository.findById(id)
                .map(MappingUtil::mapToQuestionResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Вопрос с ID " + id + " не найден."));
    }

    public QuestionResponse getRandomQuestion() {
        Question question = questionRepository.findRandomQuestion();
        return question != null ? MappingUtil.mapToQuestionResponse(question) : null;
    }

    public QuestionResponse createQuestion(CreateQuestionRequest request) {
        Question question = populateQuestionFromRequest(new Question(), request);
        question = questionRepository.save(question);
        return MappingUtil.mapToQuestionResponse(question);
    }

    public QuestionResponse updateQuestion(Long id, CreateQuestionRequest request) {
        Question question = questionRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Вопрос с ID " + id + " не найден."));
        question = populateQuestionFromRequest(question, request);
        question = questionRepository.save(question);
        return MappingUtil.mapToQuestionResponse(question);
    }

    public void deleteQuestion(Long id) {
        if (!questionRepository.existsById(id)) {
            throw new ResourceNotFoundException("Вопрос с ID " + id + " не найден.");
        }
        questionRepository.deleteById(id);
    }

    private Question populateQuestionFromRequest(Question question, CreateQuestionRequest request) {
        // Обновляем поля вопроса
        question.setQuestion(request.getQuestion());
        question.setExplanation(request.getExplanation());
        question.setIntersectionType(request.getIntersectionType());

        // Проверка и получение или создание категории
        Category category = categoryRepository.findByName(request.getCategoryName())
                .orElseGet(() -> categoryRepository.save(new Category(request.getCategoryName())));
        question.setCategory(category);

        // Обработка знаков
        Set<Sign> signs = request.getSigns().stream()
                .map(this::getOrCreateSign)
                .collect(Collectors.toSet());
        question.setSigns(signs);

        // Обработка светофоров
        Set<TrafficLight> trafficLights = request.getTrafficLights().stream()
                .map(this::getOrCreateTrafficLight)
                .collect(Collectors.toSet());
        question.setTrafficLights(trafficLights);

        // Обработка участников дорожного движения
        Set<TrafficParticipant> trafficParticipants = request.getTrafficParticipants().stream()
                .map(this::getOrCreateTrafficParticipant)
                .collect(Collectors.toSet());
        question.setTrafficParticipants(trafficParticipants);

        return question;
    }

    private Sign getOrCreateSign(CreateSignRequest request) {
        return signRepository.findByModelNameAndSidePosition(request.getModelName(), request.getSidePosition())
                .orElseGet(() -> signRepository.save(new Sign(request.getModelName(), request.getSidePosition())));
    }

    private TrafficLight getOrCreateTrafficLight(CreateTrafficLightRequest request) {
        return trafficLightRepository.findByModelNameAndSidePositionAndState(request.getModelName(), request.getSidePosition(), request.getState())
                .orElseGet(() -> trafficLightRepository.save(new TrafficLight(request.getModelName(), request.getSidePosition(), request.getState())));
    }

    private TrafficParticipant getOrCreateTrafficParticipant(CreateTrafficParticipantRequest request) {
        return trafficParticipantRepository.findByModelNameAndDirectionAndNumberPositionAndParticipantTypeAndSidePosition(
                        request.getModelName(), request.getDirection(), request.getNumberPosition(), request.getParticipantType(), request.getSidePosition())
                .orElseGet(() -> trafficParticipantRepository.save(new TrafficParticipant(
                        request.getModelName(), request.getDirection(), request.getNumberPosition(), request.getParticipantType(), request.getSidePosition())));
    }
}
