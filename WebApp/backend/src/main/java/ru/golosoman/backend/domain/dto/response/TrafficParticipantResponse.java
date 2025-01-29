package ru.golosoman.backend.domain.dto.response;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Ответ, содержащий информацию об участнике движения.")
public class TrafficParticipantResponse {

    @Schema(description = "Идентификатор участника", example = "1")
    private Long id;

    @Schema(description = "Название модели участника", example = "CarType1")
    private String modelName;

    @Schema(description = "Направление движения", example = "North")
    private String direction;

    @Schema(description = "Позиция участника на дороге", example = "1")
    private String numberPosition;

    @Schema(description = "Тип участника", example = "Car")
    private String participantType;

    @Schema(description = "Позиция участника на дороге", example = "North")
    private String sidePosition;
}
