package ru.golosoman.backend.util;

import java.math.BigDecimal;
import java.math.RoundingMode;

/**
 * Утилитарный класс для математических операций.
 * Включает методы для округления чисел.
 */
public class MathUtil {
    /**
     * Округляет число до заданного количества десятичных знаков.
     *
     * @param value  значение для округления.
     * @param places количество десятичных знаков для округления.
     * @return округленное значение.
     * @throws IllegalArgumentException если количество десятичных знаков меньше 0.
     */
    public static double round(double value, int places) {
        if (places < 0) throw new IllegalArgumentException();
        BigDecimal bd = new BigDecimal(value);
        bd = bd.setScale(places, RoundingMode.HALF_UP);
        return bd.doubleValue();
    }
}
