using System.Text.RegularExpressions;

public static class InputValidator
{
    // Метод для проверки наличия кириллицы
    public static bool ContainsCyrillic(string input)
    {
        return Regex.IsMatch(input, @"[а-яА-Я]");
    }

    // Метод для проверки на пустоту
    public static bool IsNullOrEmpty(string input)
    {
        return string.IsNullOrEmpty(input);
    }

    // Метод для проверки совпадения паролей
    public static bool ArePasswordsMatching(string password, string repeatPassword)
    {
        return password.Equals(repeatPassword);
    }
}
