package ru.golosoman.backend.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.response.JwtAuthenticationResponse;
import ru.golosoman.backend.domain.dto.request.SignInRequest;
import ru.golosoman.backend.domain.dto.request.SignUpRequest;
import ru.golosoman.backend.domain.dto.response.UserResponse;
import ru.golosoman.backend.domain.model.User;
import ru.golosoman.backend.service.AuthenticationService;

import javax.swing.text.html.parser.Entity;

@RestController
@RequestMapping("/auth")
@RequiredArgsConstructor
@Tag(name = "Аутентификация")
public class AuthController {
    private final AuthenticationService authenticationService;

    @Operation(summary = "Регистрация пользователя")
    @PostMapping("/sign-up")
    public JwtAuthenticationResponse signUp(@RequestBody @Valid SignUpRequest request) {
        return authenticationService.signUp(request);
    }

    @Operation(summary = "Авторизация пользователя")
    @PostMapping("/sign-in")
    public JwtAuthenticationResponse signIn(@RequestBody @Valid SignInRequest request) {
        return authenticationService.signIn(request);
    }

    @GetMapping("/me")
    public ResponseEntity<UserResponse> me() {
        UserResponse user = authenticationService.getCurrentUser();
        return ResponseEntity.status(HttpStatus.OK).body(user);
    }
}

