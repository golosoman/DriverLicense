package ru.golosoman.backend.domain.dto.response.statistic;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDateTime;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class TicketStatistics {
    private Long id;
    private String ticketName;
    private LocalDateTime date;
    private int countErrors;
    private boolean status;
}