using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Classes
{
    public class Controller
    {
        private DbInteraction _dbInteraction;
        private IUserInteraction _userInteraction;
        private IOtherProgramInteraction _otherProgramInteraction;
        public Controller()
        {
            _dbInteraction = new DbInteraction();
            _userInteraction = new UserInteraction();
            _otherProgramInteraction = new OtherProgramInteraction();
        }
        public Controller(DbInteraction dbInteraction, IUserInteraction userInteraction, IOtherProgramInteraction otherProgramInteraction)
        {
            _dbInteraction = dbInteraction;
            _userInteraction = userInteraction;
            _otherProgramInteraction = otherProgramInteraction;
        }
        public bool RegistrationUser()
        {
            List<String?> inputData = _userInteraction.GetRegData();
            Registration? newRegInfo = _dbInteraction.GetRegInfo(inputData[0], inputData[1], inputData[2]);
            if (newRegInfo == null)
            {
                newRegInfo = RegistrationNewUser.RegisterUser(inputData[0], inputData[1], inputData[2]);
                // сохраняем новые данные о регистрации
                _dbInteraction.AddNewRegInfo(newRegInfo);
            }

            // отправка данных
            _otherProgramInteraction.SendRegInfoToErrorService(newRegInfo.RErrorMessage);

            return newRegInfo.RResult;
        }
    }
}
