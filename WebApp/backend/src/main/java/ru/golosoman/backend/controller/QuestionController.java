package ru.golosoman.backend.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Positive;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateQuestionRequest;
import ru.golosoman.backend.domain.dto.response.QuestionResponse;
import ru.golosoman.backend.service.QuestionService;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/questions")
@Tag(name = "Вопросы", description = "Управление вопросами")
public class QuestionController {
    private final QuestionService questionService;

    @Operation(summary = "Получить все вопросы")
    @GetMapping
    public ResponseEntity<List<QuestionResponse>> getAllQuestions() {
        List<QuestionResponse> questions = questionService.getAllQuestions();
        return ResponseEntity.status(HttpStatus.OK).body(questions);
    }

    @Operation(summary = "Получить случайный вопрос")
    @GetMapping("/random")
    public ResponseEntity<QuestionResponse> getRandomQuestion() {
        QuestionResponse question = questionService.getRandomQuestion();
        return ResponseEntity.status(HttpStatus.OK).body(question);
    }

    @Operation(summary = "Получить вопрос по ID")
    @GetMapping("/{id}")
    public ResponseEntity<QuestionResponse> getQuestionById(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        QuestionResponse question = questionService.getQuestionById(id);
        return ResponseEntity.status(HttpStatus.OK).body(question);
    }

    @Operation(summary = "Создать новый вопрос")
    @PostMapping
    public ResponseEntity<QuestionResponse> createQuestion(@RequestBody @Valid CreateQuestionRequest request) {
        QuestionResponse questionResponse = questionService.createQuestion(request);
        return ResponseEntity.status(HttpStatus.CREATED).body(questionResponse);
    }

    @Operation(summary = "Обновить вопрос")
    @PutMapping("/{id}")
    public ResponseEntity<QuestionResponse> updateQuestion(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id, @RequestBody @Valid CreateQuestionRequest request) {
        QuestionResponse questionResponse = questionService.updateQuestion(id, request);
        return ResponseEntity.status(HttpStatus.OK).body(questionResponse);
    }

    @Operation(summary = "Удалить вопрос")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteQuestion(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        questionService.deleteQuestion(id);
        return ResponseEntity.status(HttpStatus.OK).build();
    }
}
