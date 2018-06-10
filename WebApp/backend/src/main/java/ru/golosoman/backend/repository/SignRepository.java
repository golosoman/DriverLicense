package ru.golosoman.backend.repository;

import ru.golosoman.backend.domain.Sign;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SignRepository extends JpaRepository<Sign, Long> {

}
