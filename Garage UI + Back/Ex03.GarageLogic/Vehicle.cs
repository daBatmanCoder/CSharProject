using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private readonly string  r_LicenseNumber;
        private readonly Wheel[] r_Wheels;
        private readonly object  r_EnergyType;
        private string           m_ModelName;
        private float            m_PercentagesOfEnergyRemaining;

        public Vehicle(string i_LicenseNumber, int i_NumOfWheels, float io_MaxAirPressure, object i_EnergyType)
        {
            m_PercentagesOfEnergyRemaining = 0f;
            m_ModelName = string.Empty;
            r_EnergyType = i_EnergyType;
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new Wheel[i_NumOfWheels];
            for (int i = 0; i < r_Wheels.Length; i++)
            {
                r_Wheels[i] = new Wheel(io_MaxAirPressure);
            }
        }

        public Wheel[] GetWheels()
        {
            return r_Wheels;
        }

        public object GetEnergyType()
        {
            return r_EnergyType;
        }

        public string GetLicenseNumber()
        {
            return r_LicenseNumber;
        }

        public void SetModelName(string i_ModelName)
        {
            if (i_ModelName != string.Empty)
            {
                m_ModelName = i_ModelName;
            }
            else
            {
                throw new FormatException("Model name's input is invalid.");
            }
        }

        public float GetPercentagesOfEnergyRemaining()
        {
            return m_PercentagesOfEnergyRemaining;
        }

        public void SetPercentagesOfEnergyRemaining(float i_NewValue)
        {
            if (i_NewValue >= 0 && i_NewValue <= 100)
            {
                m_PercentagesOfEnergyRemaining = i_NewValue;
            }
            else
            {
                throw new ValueOutOfRangeException(100f, 0f);
            }
        }

        public override string ToString()
        {
            StringBuilder StrVehicle = new StringBuilder();

            StrVehicle.Append(string.Format(
                "License Number is: {0}{1}Model Name is: {2}{1}",
                r_LicenseNumber,
                Environment.NewLine,
                m_ModelName));
            StrVehicle.Append(string.Format("Wheels {0}", r_Wheels[0].ToString()));
            StrVehicle.Append(string.Format("Enregy is ({0})% full. {1}", GetPercentagesOfEnergyRemaining().ToString("F"), r_EnergyType.ToString()));

            return StrVehicle.ToString();
        }

        public static float ReimaingEnergyPercentage(float i_CurrentPercentage, float i_MaxPercentage)
        {
            return ((i_CurrentPercentage / i_MaxPercentage) * 100); 
        }
    }
}
