package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.Answer;

import java.util.List;

@Repository
public interface AnswerRepository extends JpaRepository<Answer, Long> {

    @Query("SELECT a FROM Answer a WHERE a.question.category.id = :categoryId")
    List<Answer> findByCategoryId(@Param("categoryId") Long categoryId);

    @Query("SELECT COUNT(a) FROM Answer a WHERE a.question.category.id = :categoryId AND a.result = true")
    Long countCorrectAnswersByCategoryId(@Param("categoryId") Long categoryId);
}
