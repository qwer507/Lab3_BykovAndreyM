using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.Classes
{
    public class UserInteraction : IUserInteraction
    {
        // Возвращает массив, где первый - логин, второй - пароль, третий - повторение пароля
        public List<string?> GetRegData()
        {

            Console.WriteLine("Введите логин: ");
            string? login = Console.ReadLine();
            Console.WriteLine("Введите пароль: ");
            string? password = Console.ReadLine();
            Console.WriteLine("Введите пароль еще раз: ");
            string? passwordRepeat = Console.ReadLine();
            
           return new List<string?> {login, password, passwordRepeat };
        }
    }
}
