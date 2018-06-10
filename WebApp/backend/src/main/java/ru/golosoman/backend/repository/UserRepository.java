package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.User;

public interface UserRepository extends JpaRepository<User, Long> {

}
