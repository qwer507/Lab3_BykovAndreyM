using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public static class Controller
    {
        public static bool RegistrationUser()
        {
            DbInteraction dbInteraction = new DbInteraction();
            UserInteraction userInteraction = new UserInteraction();
            OtherProgramInteraction otherProgramInteraction = new OtherProgramInteraction();

            List<String?> inputData = userInteraction.GetRegData();
            Registration? newRegInfo = dbInteraction.GetRegInfo(inputData[0], inputData[1], inputData[2]);
            if (newRegInfo == null)
            {
                newRegInfo = RegistrationNewUser.RegisterUser(inputData[0], inputData[1], inputData[2]);
                // сохраняем новые данные о регистрации
                dbInteraction.AddNewRegInfo(newRegInfo);
            }

            // отправка данных
            otherProgramInteraction.SendRegInfoToEmail(newRegInfo.RErrorMessage);

            return newRegInfo.RResult;
        }
    }
}
