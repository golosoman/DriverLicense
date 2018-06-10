package ru.golosoman.backend.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import ru.golosoman.backend.domain.Statistic;

public interface StatisticRepository extends JpaRepository<Statistic, Long> {

}
