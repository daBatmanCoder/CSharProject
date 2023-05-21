using System;
using System.Text;
using static Ex03.GarageLogic.eNum;

namespace Ex03.GarageLogic
{
    public class Fuel
    {
        private readonly eFuelTypes     r_FuelType;
        private readonly float          r_MaxFuelTank;
        private float                   m_CurrentFuelTank;

        public Fuel(eFuelTypes i_FuelType, float i_MaxFuel)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelTank = i_MaxFuel;
            m_CurrentFuelTank = 0f;
        }

        internal eFuelTypes GetFuelType()
        {
            return this.r_FuelType;
        }

        public float GetMaxTank()
        {
            return this.r_MaxFuelTank;
        }

        public void SetCurrentFuelTank(float i_NewCurrentFuelTank)
        {
            if (i_NewCurrentFuelTank >= 0 && i_NewCurrentFuelTank <= r_MaxFuelTank)
            {
                m_CurrentFuelTank = i_NewCurrentFuelTank;
            }
            else
            {
                throw new ValueOutOfRangeException(r_MaxFuelTank, 0f);
            }
        }

        public bool FillTheFuelTank(float i_AmountToAdd, eFuelTypes i_FuelType)
        {
            bool FlagFilled = false;

            if (i_FuelType.Equals(GetFuelType()))
            {
                if (i_AmountToAdd + m_CurrentFuelTank <= r_MaxFuelTank)
                {
                    m_CurrentFuelTank += i_AmountToAdd;
                    FlagFilled = true;
                }
                else
                {
                    throw new ValueOutOfRangeException(r_MaxFuelTank - m_CurrentFuelTank, 0f);
                }
            }
            else
            {
                throw new ArgumentException("Fuel Type does not match");
            }

            return FlagFilled;
        }

        public static string ShowFuelTypes()
        {
            StringBuilder strNumberOfDoors = new StringBuilder();

            foreach (eFuelTypes doors in Enum.GetValues(typeof(eFuelTypes)))
            {
                strNumberOfDoors.Append(string.Format("[{0}] {1}{2}", (int)doors, doors.ToString(), Environment.NewLine));
            }

            return strNumberOfDoors.ToString();
        }

        public float FuelPercentage()
        {
            return (m_CurrentFuelTank / r_MaxFuelTank) * 100;
        }

        public override string ToString()
        {
            string fuel = string.Format(
                "Fuel type is {0}, it has {1} amount of gas left out of {2}{3}",
                r_FuelType.ToString(),
                m_CurrentFuelTank,
                r_MaxFuelTank,
                Environment.NewLine);

            return fuel;
        }

        public static bool IsFuelTypeOk(int i_WhichFuelType)
        {
            bool returnFlag = true;

            if(Enum.GetValues(typeof(eFuelTypes)).Length < i_WhichFuelType || i_WhichFuelType < 1)
            {
                returnFlag = !returnFlag;
            }

            return returnFlag;
        }
    }
}
     

