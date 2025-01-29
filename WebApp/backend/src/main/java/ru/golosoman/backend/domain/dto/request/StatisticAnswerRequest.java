package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Schema(description = "Ответ на вопрос статистики")
public class StatisticAnswerRequest {

    @Schema(description = "ID вопроса", example = "1")
    @NotNull(message = "ID вопроса не может быть пустым")
    private Long questionId;

    @Schema(description = "Результат ответа", example = "true")
    @NotNull(message = "Результат ответа не может быть пустым")
    private Boolean result;
}