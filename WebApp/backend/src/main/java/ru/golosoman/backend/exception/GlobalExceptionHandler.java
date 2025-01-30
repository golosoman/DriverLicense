package ru.golosoman.backend.exception;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

import javax.naming.AuthenticationException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ErrorResponse> handleValidationExceptions(MethodArgumentNotValidException ex) {
        List<String> messages = new ArrayList<>();
        ex.getBindingResult().getFieldErrors().forEach(error -> messages.add(error.getDefaultMessage()));

        String message = "Ошибка валидации: " + String.join(", ", messages);
        int code = HttpStatus.BAD_REQUEST.value();
        String date = new SimpleDateFormat("dd.MM.yyyy HH:mm:ss").format(new Date());

        ErrorResponse errorResponse = new ErrorResponse(message, code, date);
        return new ResponseEntity<>(errorResponse, HttpStatus.BAD_REQUEST);
    }

    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<ErrorResponse> handleResourceNotFoundException(ResourceNotFoundException ex) {
        String message = ex.getMessage();
        int code = HttpStatus.NOT_FOUND.value();
        String date = new SimpleDateFormat("dd.MM.yyyy HH:mm:ss").format(new Date());

        ErrorResponse errorResponse = new ErrorResponse(message, code, date);
        return new ResponseEntity<>(errorResponse, HttpStatus.NOT_FOUND);
    }

    @ExceptionHandler(AuthenticationException.class)
    public ResponseEntity<ErrorResponse> handleAuthenticationExceptions(AuthenticationException ex) {
        String message = "Ошибка аутентификации: " + ex.getMessage();
        int code = HttpStatus.UNAUTHORIZED.value();
        String date = new SimpleDateFormat("dd.MM.yyyy HH:mm:ss").format(new Date());

        ErrorResponse errorResponse = new ErrorResponse(message, code, date);
        return new ResponseEntity<>(errorResponse, HttpStatus.UNAUTHORIZED);
    }

    @ExceptionHandler(UserAlreadyExistsException.class)
    public ResponseEntity<ErrorResponse> handleUserAlreadyExistsException(UserAlreadyExistsException ex) {
        String message = ex.getMessage();
        int code = HttpStatus.CONFLICT.value();
        String date = new SimpleDateFormat("dd.MM.yyyy HH:mm:ss").format(new Date());

        ErrorResponse errorResponse = new ErrorResponse(message, code, date);
        return new ResponseEntity<>(errorResponse, HttpStatus.CONFLICT);
    }

    @ExceptionHandler(UserNotFoundException.class)
    public ResponseEntity<ErrorResponse> handleUserNotFoundException(UserNotFoundException ex) {
        String message = ex.getMessage();
        int code = HttpStatus.NOT_FOUND.value();
        String date = new SimpleDateFormat("dd.MM.yyyy HH:mm:ss").format(new Date());

        ErrorResponse errorResponse = new ErrorResponse(message, code, date);
        return new ResponseEntity<>(errorResponse, HttpStatus.NOT_FOUND);
    }

    @Getter
    @Setter
    @AllArgsConstructor
    public static class ErrorResponse {
        private String message;
        private int code;
        private String date;
    }
}
