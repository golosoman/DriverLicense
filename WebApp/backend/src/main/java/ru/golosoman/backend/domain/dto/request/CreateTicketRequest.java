package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
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
@Schema(description = "Запрос на создание билета")
public class CreateTicketRequest {

    @Schema(description = "Название билета", example = "Билет 1")
    @NotBlank(message = "Название не может быть пустым")
    @Size(min = 5, max = 30, message = "Название должно содержать от 5 до 30 символов")
    private String name;

    @Schema(description = "Список ID вопросов", example = "[1, 2, 3, 4, 5]")
    @NotNull(message = "Список ID вопросов не может быть пустым")
    @Size(min = 5, message = "Список ID вопросов должен содержать хотя бы 5 значений")
    private List<Long> questionIds;
}