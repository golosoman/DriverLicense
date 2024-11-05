package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.Category;

public interface CategoryRepository extends JpaRepository<Category, Long> {
}
