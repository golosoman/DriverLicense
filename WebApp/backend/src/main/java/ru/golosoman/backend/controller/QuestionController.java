package ru.golosoman.backend.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateQuestionRequest;
import ru.golosoman.backend.domain.dto.response.QuestionResponse;
import ru.golosoman.backend.service.QuestionService;

import java.util.List;

@RestController
@RequestMapping("/api/questions")
public class QuestionController {

    @Autowired
    private QuestionService questionService;

    // Получение всех вопросов
    @GetMapping
    public ResponseEntity<List<QuestionResponse>> getAllQuestions() {
        List<QuestionResponse> questions = questionService.getAllQuestions();
        return new ResponseEntity<>(questions, HttpStatus.OK);
    }

    // Получение случайного вопроса
    @GetMapping("/random")
    public ResponseEntity<QuestionResponse> getRandomQuestion() {
        QuestionResponse question = questionService.getRandomQuestion();
        return question != null ? new ResponseEntity<>(question, HttpStatus.OK) : new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    // Получение вопроса по ID
    @GetMapping("/{id}")
    public ResponseEntity<QuestionResponse> getQuestionById(@PathVariable Long id) {
        QuestionResponse question = questionService.getQuestionById(id);
        return question != null ? new ResponseEntity<>(question, HttpStatus.OK) : new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    // Создание нового вопроса
    @PostMapping
    public ResponseEntity<QuestionResponse> createQuestion(@RequestBody CreateQuestionRequest request) {
        QuestionResponse questionResponse = questionService.createQuestion(request);
        return new ResponseEntity<>(questionResponse, HttpStatus.CREATED);
    }

    // Обновление вопроса
    @PutMapping("/{id}")
    public ResponseEntity<QuestionResponse> updateQuestion(@PathVariable Long id, @RequestBody CreateQuestionRequest request) {
        QuestionResponse questionResponse = questionService.updateQuestion(id, request);
        return new ResponseEntity<>(questionResponse, HttpStatus.OK);
    }

    // Удаление вопроса
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteQuestion(@PathVariable Long id) {
        questionService.deleteQuestion(id);
        return new ResponseEntity<>(HttpStatus.NO_CONTENT);
    }
}
