package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.Answer;

import java.util.List;

@Repository
public interface AnswerRepository extends JpaRepository<Answer, Long> {

    @Query("SELECT a FROM Answer a " +
            "JOIN a.attemptTicket at " +
            "JOIN at.user u " +
            "JOIN a.question q " +
            "JOIN q.category c " +
            "WHERE c.id = :categoryId AND u.id = :userId")
    List<Answer> findByUserIdAndCategoryId(@Param("userId") Long userId, @Param("categoryId") Long categoryId);
}
