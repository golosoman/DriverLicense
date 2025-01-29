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

    /**
     * Получает список всех вопросов.
     *
     * @return список ответов вопросов
     */
    public List<QuestionResponse> getAllQuestions() {
        return questionRepository.findAll().stream()
                .map(MappingUtil::mapToQuestionResponse)
                .toList();
    }

    /**
     * Получает вопрос по идентификатору.
     *
     * @param id идентификатор вопроса
     * @return ответ вопроса
     * @throws ResourceNotFoundException если вопрос не найден
     */
    public QuestionResponse getQuestionById(Long id) {
        return questionRepository.findById(id)
                .map(MappingUtil::mapToQuestionResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Вопрос с ID " + id + " не найден."));
    }

    /**
     * Получает случайный вопрос.
     *
     * @return ответ случайного вопроса
     * @throws ResourceNotFoundException если не удалось найти случайный вопрос
     */
    public QuestionResponse getRandomQuestion() {
        Question question = questionRepository.findRandomQuestion();
        if (question == null) {
            throw new ResourceNotFoundException("Не удалось найти случайный вопрос");
        }
        return MappingUtil.mapToQuestionResponse(question);
    }

    /**
     * Создает новый вопрос.
     *
     * @param request данные для создания вопроса
     * @return ответ с созданным вопросом
     */
    public QuestionResponse createQuestion(CreateQuestionRequest request) {
        Question question = populateQuestionFromRequest(new Question(), request);
        question = questionRepository.save(question);
        return MappingUtil.mapToQuestionResponse(question);
    }

    /**
     * Обновляет существующий вопрос.
     *
     * @param id идентификатор вопроса
     * @param request данные для обновления вопроса
     * @return ответ с обновленным вопросом
     * @throws ResourceNotFoundException если вопрос не найден
     */
    public QuestionResponse updateQuestion(Long id, CreateQuestionRequest request) {
        Question question = questionRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Вопрос с ID " + id + " не найден."));
        question = populateQuestionFromRequest(question, request);
        question = questionRepository.save(question);
        return MappingUtil.mapToQuestionResponse(question);
    }

    /**
     * Удаляет вопрос по идентификатору.
     *
     * @param id идентификатор вопроса
     * @throws ResourceNotFoundException если вопрос не найден
     */
    public void deleteQuestion(Long id) {
        if (!questionRepository.existsById(id)) {
            throw new ResourceNotFoundException("Вопрос с ID " + id + " не найден.");
        }
        questionRepository.deleteById(id);
    }

    /**
     * Заполняет объект вопроса данными из запроса.
     *
     * @param question объект вопроса
     * @param request данные запроса
     * @return обновленный объект вопроса
     */
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

    /**
     * Получает существующий знак дорожного движения по имени модели и позиции стороны,
     * или создает новый, если он не найден.
     *
     * @param request объект запроса для создания знака, содержащий информацию о модели и позиции стороны.
     * @return объект знака дорожного движения, найденный или созданный.
     */
    private Sign getOrCreateSign(CreateSignRequest request) {
        return signRepository.findByModelNameAndSidePosition(request.getModelName(), request.getSidePosition())
                .orElseGet(() -> signRepository.save(new Sign(request.getModelName(), request.getSidePosition())));
    }

    /**
     * Получает существующий светофор по имени модели, позиции стороны и состоянию,
     * или создает новый, если он не найден.
     *
     * @param request объект запроса для создания светофора, содержащий информацию о модели, позиции стороны и состоянии.
     * @return объект светофора, найденный или созданный.
     */
    private TrafficLight getOrCreateTrafficLight(CreateTrafficLightRequest request) {
        return trafficLightRepository.findByModelNameAndSidePositionAndState(request.getModelName(), request.getSidePosition(), request.getState())
                .orElseGet(() -> trafficLightRepository.save(new TrafficLight(request.getModelName(), request.getSidePosition(), request.getState())));
    }

    /**
     * Получает существующего участника дорожного движения по имени модели, направлению, номеру позиции,
     * типу участника и позиции стороны, или создает нового, если он не найден.
     *
     * @param request объект запроса для создания участника дорожного движения, содержащий информацию о модели, направлении,
     *                номере позиции, типе участника и позиции стороны.
     * @return объект участника дорожного движения, найденный или созданный.
     */
    private TrafficParticipant getOrCreateTrafficParticipant(CreateTrafficParticipantRequest request) {
        return trafficParticipantRepository.findByModelNameAndDirectionAndNumberPositionAndParticipantTypeAndSidePosition(
                        request.getModelName(), request.getDirection(), request.getNumberPosition(), request.getParticipantType(), request.getSidePosition())
                .orElseGet(() -> trafficParticipantRepository.save(new TrafficParticipant(
                        request.getModelName(), request.getDirection(), request.getNumberPosition(), request.getParticipantType(), request.getSidePosition())));
    }
}
