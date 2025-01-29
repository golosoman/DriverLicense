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

    /**
     * Получает список всех категорий.
     *
     * @return список ответов категорий
     */
    public List<CategoryResponse> getAllCategories() {
        return categoryRepository.findAll().stream()
                .map(MappingUtil::mapToCategoryResponse)
                .collect(Collectors.toList());
    }

    /**
     * Получает категорию по идентификатору.
     *
     * @param id идентификатор категории
     * @return ответ категории
     * @throws ResourceNotFoundException если категория не найдена
     */
    public CategoryResponse getCategoryById(Long id) {
        return categoryRepository.findById(id)
                .map(MappingUtil::mapToCategoryResponse)
                .orElseThrow(() -> new ResourceNotFoundException("Категория с ID " + id + " не найдена"));
    }

    /**
     * Создает новую категорию.
     *
     * @param categoryDTO данные для создания категории
     * @return ответ с созданной категорией
     */
    public CategoryResponse createCategory(CreateCategoryRequest categoryDTO) {
        Category category = new Category();
        category.setName(categoryDTO.getName());
        Category savedCategory = categoryRepository.save(category);
        return MappingUtil.mapToCategoryResponse(savedCategory);
    }

    /**
     * Обновляет существующую категорию.
     *
     * @param id идентификатор категории
     * @param categoryDTO данные для обновления категории
     * @return ответ с обновленной категорией
     * @throws ResourceNotFoundException если категория не найдена
     */
    public CategoryResponse updateCategory(Long id, CreateCategoryRequest categoryDTO) {
        Category category = categoryRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Категория с ID " + id + " не найдена"));
        category.setName(categoryDTO.getName());
        Category updatedCategory = categoryRepository.save(category);
        return MappingUtil.mapToCategoryResponse(updatedCategory);
    }

    /**
     * Удаляет категорию по идентификатору.
     *
     * @param id идентификатор категории
     * @throws ResourceNotFoundException если категория не найдена
     */
    public void deleteCategory(Long id) {
        if (!categoryRepository.existsById(id)) {
            throw new ResourceNotFoundException("Категория с ID " + id + " не найдена");
        }
        categoryRepository.deleteById(id);
    }

}
