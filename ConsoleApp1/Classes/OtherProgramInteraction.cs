using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class OtherProgramInteraction : IOtherProgramInteraction
    {
        public void SendRegInfoToEmail(string login, string password)
        {
            Thread.Sleep(1000);
        }
    }
}
