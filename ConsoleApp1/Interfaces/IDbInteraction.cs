using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    interface IDbInteraction
    {
        Registration? GetRegInfo(string? login, string? password, string? repeatPassword);
        void AddNewRegInfo(Registration regInfo);
        void DeleteRegInfo(Registration reginfo);
    }
}
