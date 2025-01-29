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
@Schema(description = "Запрос на создание светофора")
public class CreateTrafficLightRequest {

    @Schema(description = "Название модели светофора", example = "TrafficLightType1Green")
    @NotBlank(message = "Название модели светофора не может быть пустым")
    @Size(min = 3, max = 30, message = "Название модели светофора должно содержать от 3 до 30 символов")
    private String modelName;

    @Schema(description = "Позиция светофора на дороге", example = "North")
    @NotBlank(message = "Позиция светофора не может быть пустой")
    @Size(min = 3, max = 12, message = "Позиция светофора должна содержать от 3 до 12 символов")
    private String sidePosition;

    @Schema(description = "Состояние светофора", example = "Green")
    @NotBlank(message = "Состояние светофора не может быть пустым")
    @Size(min = 3, max = 12, message = "Состояние светофора должно содержать от 3 до 12 символов")
    private String state;
}
