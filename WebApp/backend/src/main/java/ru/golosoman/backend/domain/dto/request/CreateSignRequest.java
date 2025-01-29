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
@Schema(description = "Запрос на создание дорожного знака")
public class CreateSignRequest {

    @Schema(description = "Название модели знака", example = "YieldSignType1")
    @NotBlank(message = "Название модели знака не может быть пустым")
    @Size(min = 3, max = 30, message = "Название модели знака должно содержать от 3 до 30 символов")
    private String modelName;

    @Schema(description = "Позиция знака на дороге", example = "North")
    @NotBlank(message = "Позиция знака не может быть пустой")
    @Size(min = 3, max = 12, message = "Позиция знака должна содержать от 3 до 12 символов")
    private String sidePosition;
}
