package ru.golosoman.backend.domain.dto.response;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
@Schema(description = "Ответ, содержащий информацию о категории.")
public class CategoryResponse {

    @Schema(description = "Идентификатор категории", example = "1")
    private Long id;

    @Schema(description = "Название категории", example = "Регулируемые перекрестки")
    private String name;
}

