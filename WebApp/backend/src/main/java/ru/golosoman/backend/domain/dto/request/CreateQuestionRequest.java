package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotEmpty;
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
@Schema(description = "Запрос на создание вопроса")
public class CreateQuestionRequest {

    @Schema(description = "Текст вопроса", example = "Определите правильную последовательность движения транспортных средств на перекрестке.")
    @NotBlank(message = "Вопрос не может быть пустым")
    @Size(min = 10, max = 150, message = "Вопрос должен содержать от 10 до 150 символов")
    private String question;

    @Schema(description = "Объяснение к вопросу", example = "По правилам ПДД из постановления ...")
    @Size(min = 10, max = 150, message = "Объяснение должно содержать от 10 до 150 символов")
    private String explanation;

    @Schema(description = "Тип перекрестка", example = "Регулируемый перекресток")
    @NotBlank(message = "Тип перекрестка не может быть пустым")
    @Size(min = 5, max = 30, message = "Тип перекрестка должен содержать от 5 до 30 символов")
    private String intersectionType;

    @Schema(description = "Название категории", example = "Регулируемые перекрестки")
    @NotBlank(message = "Название категории не может быть пустым")
    @Size(min = 5, max = 30, message = "Название категории должно содержать от 5 до 30 символов")
    private String categoryName;

    @Schema(description = "Список светофоров",
            example = "[{\"modelName\": \"TrafficLightType1Green\", \"sidePosition\": \"North\", \"state\": \"Green\"}, " +
                    "{\"modelName\": \"TrafficLightType1Red\", \"sidePosition\": \"South\", \"state\": \"Red\"}]")
    private List<CreateTrafficLightRequest> trafficLights;

    @Schema(description = "Список участников движения",
            example = "[{\"modelName\": \"CarType1\", \"direction\": \"North\", \"numberPosition\": \"1\", \"participantType\": \"Car\", \"sidePosition\": \"North\"}, " +
                    "{\"modelName\": \"BikeType1\", \"direction\": \"East\", \"numberPosition\": \"2\", \"participantType\": \"Bike\", \"sidePosition\": \"East\"}]")
    @NotEmpty(message = "Список участников движения не может быть пустым")
    private List<CreateTrafficParticipantRequest> trafficParticipants;

    @Schema(description = "Список знаков",
            example = "[{\"modelName\": \"YieldSignType1\", \"sidePosition\": \"West\"}, " +
                    "{\"modelName\": \"StopSignType1\", \"sidePosition\": \"South\"}]")
    private List<CreateSignRequest> signs;
}

