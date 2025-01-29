package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Schema(description = "Запрос на создание категории")
public class CreateCategoryRequest {

    @Schema(description = "Название категории", example = "Транспорт")
    @NotBlank(message = "Название категории не может быть пустым")
    @Size(max = 30, message = "Название категории должно содержать максимум 30 символов")
    private String name;
}

