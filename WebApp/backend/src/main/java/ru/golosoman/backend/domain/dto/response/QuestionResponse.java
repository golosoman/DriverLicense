package ru.golosoman.backend.domain.dto.response;

import io.swagger.v3.oas.annotations.media.Schema;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Schema(description = "Ответ на вопрос, содержащий детали вопроса и связанные объекты.")
public class QuestionResponse {

    @Schema(description = "Идентификатор вопроса", example = "1")
    private Long id;

    @Schema(description = "Текст вопроса", example = "Определите правильную последовательность движения транспортных средств на перекрестке.")
    private String question;

    @Schema(description = "Объяснение к вопросу", example = "По правилам ПДД из постановления ...")
    private String explanation;

    @Schema(description = "Тип перекрестка", example = "Регулируемый перекресток")
    private String intersectionType;

    @Schema(description = "Категория вопроса", implementation = CategoryResponse.class)
    private CategoryResponse category;

    @Schema(description = "Список дорожных знаков",
            example = "[{\"id\": 1, \"modelName\": \"YieldSignType1\", \"sidePosition\": \"West\"}, " +
                    "{\"id\": 2, \"modelName\": \"StopSignType1\", \"sidePosition\": \"South\"}]")
    private List<SignResponse> signs;

    @Schema(description = "Список светофоров",
            example = "[{\"id\": 1, \"modelName\": \"TrafficLightType1Green\", \"sidePosition\": \"North\", \"state\": \"Green\"}, " +
                    "{\"id\": 2, \"modelName\": \"TrafficLightType1Red\", \"sidePosition\": \"South\", \"state\": \"Red\"}]")
    private List<TrafficLightResponse> trafficLights;

    @Schema(description = "Список участников движения",
            example = "[{\"id\": 1, \"modelName\": \"CarType1\", \"direction\": \"North\", \"numberPosition\": \"1\", \"participantType\": \"Car\", \"sidePosition\": \"North\"}, " +
                    "{\"id\": 2, \"modelName\": \"BikeType1\", \"direction\": \"East\", \"numberPosition\": \"2\", \"participantType\": \"Bike\", \"sidePosition\": \"East\"}]")
    private List<TrafficParticipantResponse> trafficParticipants;
}
