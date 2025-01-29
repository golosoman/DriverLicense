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
@Schema(description = "Ответ, содержащий информацию о билете и связанных вопросах.")
public class TicketResponse {

    @Schema(description = "Идентификатор билета", example = "1")
    private Long id;

    @Schema(description = "Название билета", example = "Билет по правилам дорожного движения")
    private String name;

    @Schema(description = "Список вопросов, связанных с билетом",
            example = "[{\"id\": 1, \"question\": \"Определите правильную последовательность движения транспортных средств на перекрестке.\", " +
                    "\"explanation\": \"По правилам ПДД из постановления ...\", \"intersectionType\": \"Регулируемый перекресток\", " +
                    "\"category\": {\"id\": 1, \"name\": \"Регулируемые перекрестки\"}, " +
                    "\"signs\": [{\"id\": 1, \"modelName\": \"YieldSignType1\", \"sidePosition\": \"West\"}], " +
                    "\"trafficLights\": [{\"id\": 1, \"modelName\": \"TrafficLightType1Green\", \"sidePosition\": \"North\", \"state\": \"Green\"}], " +
                    "\"trafficParticipants\": [{\"id\": 1, \"modelName\": \"CarType1\", \"direction\": \"North\", \"numberPosition\": \"1\", \"participantType\": \"Car\", \"sidePosition\": \"North\"}]}]")
    private List<QuestionResponse> questions; // Список вопросов
}