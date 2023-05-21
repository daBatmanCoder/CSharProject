using System;
using static Ex03.GarageLogic.eNum;

namespace Ex03.ConsoleUI
{
    public class Validation
    {
        public static bool ValidateLicenseNumber(string i_LicenseNumber)
        {
            bool returnFlag = true;

            foreach(char oneChar in i_LicenseNumber)
            {
                if (!Char.IsLetterOrDigit(oneChar))
                {
                    returnFlag = !returnFlag;
                    break;
                }
            }

            return returnFlag;
        }

        internal static bool ValidateTypeOfVehicle(int i_TypeOfVehicle)
        {
            bool returnFlag = true;
            int numberOfVehiclesInGarage = Enum.GetValues(typeof(eVehicleType)).Length;

            if(i_TypeOfVehicle < 0 || i_TypeOfVehicle > numberOfVehiclesInGarage)
            {
                returnFlag = !returnFlag;
            }

            return returnFlag;
        }
    }
}
