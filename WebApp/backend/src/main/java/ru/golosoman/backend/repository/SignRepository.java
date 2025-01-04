package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import ru.golosoman.backend.domain.model.Sign;

@Repository
public interface SignRepository extends JpaRepository<Sign, Long> {

}
