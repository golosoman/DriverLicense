package ru.golosoman.backend.service;

import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import ru.golosoman.backend.domain.dto.request.CreateCategoryRequest;
import ru.golosoman.backend.domain.dto.response.CategoryResponse;
import ru.golosoman.backend.domain.model.Category;
import ru.golosoman.backend.exception.ResourceNotFoundException;
import ru.golosoman.backend.repository.CategoryRepository;
import ru.golosoman.backend.util.MappingUtil;

import java.util.List;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class CategoryService {
    private final CategoryRepository categoryRepository;

    public List<CategoryResponse> getAllCategories() {
        return categoryRepository.findAll().stream()
                .map(MappingUtil::mapToCategoryResponse)
                .collect(Collectors.toList());
    }

    public CategoryResponse getCategoryById(Long id) {
        return categoryRepository.findById(id)
                .map(MappingUtil::mapToCategoryResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Категория с ID " + id + " не найдена"));
    }

    public CategoryResponse createCategory(CreateCategoryRequest categoryDTO) {
        Category category = new Category();
        category.setName(categoryDTO.getName());
        Category savedCategory = categoryRepository.save(category);
        return MappingUtil.mapToCategoryResponse(savedCategory);
    }

    public CategoryResponse updateCategory(Long id, CreateCategoryRequest categoryDTO) {
        Category category = categoryRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Категория с ID " + id + " не найдена"));
        category.setName(categoryDTO.getName());
        Category updatedCategory = categoryRepository.save(category);
        return MappingUtil.mapToCategoryResponse(updatedCategory);
    }

    public void deleteCategory(Long id) {
        if (!categoryRepository.existsById(id)) {
            throw new ResourceNotFoundException("Категория с ID " + id + " не найдена");
        }
        categoryRepository.deleteById(id);
    }

}
