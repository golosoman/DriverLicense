package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class QuestionStatistics {
    private Long id;
    private String question;
    private double percentage; // Процент успешных ответов
}
