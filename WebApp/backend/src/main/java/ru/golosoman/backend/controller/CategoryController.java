package ru.golosoman.backend.controller;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import jakarta.validation.constraints.Positive;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import ru.golosoman.backend.domain.dto.request.CreateCategoryRequest;
import ru.golosoman.backend.domain.dto.response.CategoryResponse;
import ru.golosoman.backend.service.CategoryService;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/categories")
@Tag(name = "Категории", description = "Управление категориями вопросов")
public class CategoryController {
    private final CategoryService categoryService;

    @Operation(summary = "Получить все категории")
    @GetMapping
    public ResponseEntity<List<CategoryResponse>> getAllCategories() {
        List<CategoryResponse> categories = categoryService.getAllCategories();
        return ResponseEntity.status(HttpStatus.OK).body(categories);
    }

    @Operation(summary = "Получить категорию по ID")
    @GetMapping("/{id}")
    public ResponseEntity<CategoryResponse> getCategoryById(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        CategoryResponse categoryResponse = categoryService.getCategoryById(id);
        return ResponseEntity.status(HttpStatus.OK).body(categoryResponse);
    }

    @Operation(summary = "Создать новую категорию")
    @PostMapping
    public ResponseEntity<CategoryResponse> createCategory(@RequestBody @Valid CreateCategoryRequest categoryDTO) {
        CategoryResponse createdCategory = categoryService.createCategory(categoryDTO);
        return ResponseEntity.status(HttpStatus.CREATED).body(createdCategory);
    }

    @Operation(summary = "Обновить категорию по ID")
    @PutMapping("/{id}")
    public ResponseEntity<CategoryResponse> updateCategory(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id, @RequestBody @Valid CreateCategoryRequest categoryDTO) {
        CategoryResponse updatedCategory = categoryService.updateCategory(id, categoryDTO);
        return ResponseEntity.status(HttpStatus.OK).body(updatedCategory);
    }

    @Operation(summary = "Удалить категорию по ID")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteCategory(@PathVariable @Positive(message = "ID должен быть положительным числом") Long id) {
        categoryService.deleteCategory(id);
        return ResponseEntity.status(HttpStatus.OK).build();
    }
}
