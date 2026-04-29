using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    internal class DbInteraction
    {
        Bykov2307a2TechContext _context = new Bykov2307a2TechContext();
        public Registration? GetRegInfo(string login, string password, string repeatPassword)
        {
            List<Registration> allRegInfo = _context.Registrations.ToList();
            return allRegInfo.FirstOrDefault(x => x.RLogin.Equals(login) && x.RPassword.Equals(password) && x.RRepeatPassword.Equals(repeatPassword));
        }

        public void AddNewRegInfo(Registration regInfo)
        {
            _context.Registrations.Add(regInfo);
            _context.SaveChanges();
        }

        public void DeleteRegInfo(Registration reginfo)
        {
            _context.Registrations.Remove(reginfo);
            _context.SaveChanges();
        }
    }
}
