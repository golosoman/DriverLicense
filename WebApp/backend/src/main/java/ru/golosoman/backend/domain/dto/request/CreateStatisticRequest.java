package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Schema(description = "Запрос на создание статистики")
public class CreateStatisticRequest {

    @Schema(description = "ID билета", example = "12345")
    @NotNull(message = "ID билета не может быть пустым")
    private Long ticketId;

    @Schema(description = "Результат", example = "true")
    @NotNull(message = "Результат не может быть пустым")
    private Boolean result;

    @Schema(description = "Ответы на вопросы", example = "[{\"questionId\": 1, \"result\": true}]")
    @NotNull(message = "Ответы не могут быть пустыми")
    @Size(min = 1, message = "Ответы должны содержать хотя бы 1 значение")
    private List<StatisticAnswerRequest> answers;
}
