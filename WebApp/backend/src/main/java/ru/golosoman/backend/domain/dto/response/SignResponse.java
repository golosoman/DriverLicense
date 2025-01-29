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
@Schema(description = "Ответ, содержащий информацию о дорожном знаке.")
public class SignResponse {

    @Schema(description = "Идентификатор знака", example = "1")
    private Long id;

    @Schema(description = "Название модели знака", example = "YieldSignType1")
    private String modelName;

    @Schema(description = "Позиция знака на дороге", example = "West")
    private String sidePosition;
}

