package ru.golosoman.backend.domain.dto.request;

import io.swagger.v3.oas.annotations.media.Schema;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Data;

@Data
@Schema(description = "Запрос на аутентификацию")
public class SignInRequest {

    @Schema(description = "Имя пользователя", example = "Jon")
    @Size(min = 4, max = 15, message = "Имя пользователя должно содержать от 4 до 15 символов")
    @NotBlank(message = "Имя пользователя не может быть пустыми")
    private String username;

    @Schema(description = "Пароль", example = "my_1secret1_password")
    @Size(min = 4, max = 15, message = "Длина пароля должна быть от 4 до 15 символов")
    @NotBlank(message = "Пароль не может быть пустыми")
    private String password;
}