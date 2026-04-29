using ConsoleApp1.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class OtherProgramInteraction : IOtherProgramInteraction
    {
        // имитация передачи ошибки на отдельный сервис
        public void SendRegInfoToErrorService(string? errorMessage)
        {
            Thread.Sleep(500);
            Log.Information("Данные отправлены");
        }
    }
}
