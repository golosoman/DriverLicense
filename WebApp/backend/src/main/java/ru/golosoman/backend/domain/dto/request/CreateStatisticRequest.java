package ru.golosoman.backend.domain.dto.request;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

import java.util.List;

@NoArgsConstructor
@AllArgsConstructor
@Getter
@Setter
public class CreateStatisticRequest {
    private Long ticketId;
    private Boolean result;
    private List<StatisticAnswerRequest> answers;
}
