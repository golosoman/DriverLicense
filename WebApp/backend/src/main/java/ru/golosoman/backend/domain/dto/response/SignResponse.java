package ru.golosoman.backend.domain.dto.response;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class SignResponse {
    private Long id;
    private String modelName;
    private String sidePosition;
}
