using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Serilog;

namespace ConsoleApp1.Classes
{
    public static class RegistrationNewUser
    {
        public static Registration RegisterUser(string? login, string? password, string? passwordRepeat)
        {
            string maskedPassword = PasswordHasher.MaskPassword(password);
            string maskedPasswordRepeat = PasswordHasher.MaskPassword(passwordRepeat);
            string? error = CheckRegInfo(login, password, passwordRepeat);

            Registration newReg = new Registration() {
                RLogin = login,
                RPassword = password,
                RRepeatPassword = passwordRepeat,
                RResult = false,
                RErrorMessage = error
            };

            if (string.IsNullOrEmpty(error))
            {
                Log.Information("Логин: {Login} | Пароль: {Password} | Подтверждение: {ConfirmPassword} | Успешная регистрация", login, maskedPassword, maskedPasswordRepeat);
                newReg.RResult = true;
            }
            else
            {
                Log.Information("Логин: {Login} | Пароль: {Password} | Подтверждение: {ConfirmPassword} | Успешная регистрация", login, maskedPassword, maskedPasswordRepeat);
            }

            return newReg;
        }

        private static string? CheckRegInfo(string? login, string? password, string? passwordRepeat)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string phonePattern = @"^\+[1-9]\d{0,2}-\d{3}-\d{3}-\d{4}$";
            string defaultLoginPattern = @"^[a-zA-Z0-9_]+$";
            string defaultPasswordPattern = @"^[а-яА-ЯёЁ0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?]+$";
            string allSpecSymbols = @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>/?]";
            List<string> redectedLogins = ["test", "admin", "guest"];

            if (string.IsNullOrWhiteSpace(login))
                return "Логин не может быть пустым.";

            if (!Regex.IsMatch(login, phonePattern) && !Regex.IsMatch(login, emailPattern))
            {
                if (login.Length < 5)
                    return "Логин должен содержать минимум 5 символов.";

                if (!Regex.IsMatch(login, defaultLoginPattern))
                    return "Логин может содержать только латиницу, цифры и знак подчеркивания.";

                if (redectedLogins.FirstOrDefault(x => x.Equals(login)) != null)
                    return "Данный логин занят. Пожалуйста, выберите другой.";
            }

            if (string.IsNullOrWhiteSpace(password))
                return "Пароль не может быть пустым.";

            if (password.Length < 7)
                return "Пароль должен содержать минимум  7 символов.";

            if (!Regex.IsMatch(password, defaultPasswordPattern))
                return "Пароль может содержать только кириллицу, цифры и спецсимволы.";

            if (!Regex.IsMatch(password, @"[А-ЯЁ]"))
                return "Пароль должен содержать минимум одну букву в верхнем регистре.";

            if (!Regex.IsMatch(password, @"[а-яё]"))
                return "Пароль должен содержать минимум одну букву в нижнем регистре.";

            if (!Regex.IsMatch(password, @"[0-9]"))
                return "Пароль должен содержать минимум одну цифру.";

            if (!Regex.IsMatch(password, allSpecSymbols))
                return "Пароль должен содержать минимум один спецсимвол.";

            if (!password.Equals(passwordRepeat))
                return "Введены разные пароли.";

            return null;
        }
    }
}
