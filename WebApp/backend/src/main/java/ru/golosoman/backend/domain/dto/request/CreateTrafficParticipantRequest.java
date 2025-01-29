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
@Schema(description = "Запрос на создание участника движения")
public class CreateTrafficParticipantRequest {

    @Schema(description = "Название модели участника движения", example = "CarType1")
    @NotBlank(message = "Название модели не может быть пустым")
    @Size(min = 3, max = 30, message = "Название модели должно содержать от 3 до 30 символов")
    private String modelName;

    @Schema(description = "Направление движения", example = "North")
    @NotBlank(message = "Направление не может быть пустым")
    @Size(min = 3, max = 12, message = "Направление должно содержать от 3 до 12 символов")
    private String direction;

    @Schema(description = "Позиция участника на дороге (может быть многополосное движение)", example = "1")
    @NotBlank(message = "Позиция не может быть пустой")
    @Size(min = 3, max = 12, message = "Позиция должна содержать от 3 до 12 символов")
    private String numberPosition;

    @Schema(description = "Тип участника движения", example = "Car")
    @NotBlank(message = "Тип участника не может быть пустым")
    @Size(min = 3, max = 12, message = "Тип участника должен содержать от 3 до 12 символов")
    private String participantType;

    @Schema(description = "Позиция участника на дороге", example = "North")
    @NotBlank(message = "Позиция участника не может быть пустой")
    @Size(min = 3, max = 12, message = "Позиция участника должна содержать от 3 до 12 символов")
    private String sidePosition;
}
