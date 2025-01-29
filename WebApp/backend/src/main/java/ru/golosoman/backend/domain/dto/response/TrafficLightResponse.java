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
@Schema(description = "Ответ, содержащий информацию о светофоре.")
public class TrafficLightResponse {

    @Schema(description = "Идентификатор светофора", example = "1")
    private Long id;

    @Schema(description = "Название модели светофора", example = "TrafficLightType1Green")
    private String modelName;

    @Schema(description = "Позиция светофора на дороге", example = "North")
    private String sidePosition;

    @Schema(description = "Состояние светофора", example = "Green")
    private String state;
}

