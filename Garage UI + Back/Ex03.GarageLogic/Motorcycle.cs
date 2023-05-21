using System;
using System.Text;
using static Ex03.GarageLogic.eNum;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int          m_EngineVolumeInCC;

        public Motorcycle(string i_LicenseNumber, int i_NumOfWheels, float i_MaxAirPressure, object i_EnergyType) :
                              base(i_LicenseNumber, i_NumOfWheels, i_MaxAirPressure, i_EnergyType)
        {

        }

        public void SetLicenseType(eLicenseType i_NewLicenseType)
        {
            if (Enum.IsDefined(typeof(eLicenseType), i_NewLicenseType))
            {
                m_LicenseType = i_NewLicenseType;
            }
            else
            {
                throw new ArgumentException("License type is invalid");
            }
        }

        public void SetEngineVolume(int i_NewEngineVolume)
        {
            if (i_NewEngineVolume > 0)
            {
                m_EngineVolumeInCC = i_NewEngineVolume;
            }
            else
            {
                throw new ArgumentException("Invalid input, please enter a correct engine volume in CC");
            }
        }

        public static string ShowLicenseTypes()
        {
            StringBuilder strLicenseTypes = new StringBuilder();

            foreach (eLicenseType licenseType in Enum.GetValues(typeof(eLicenseType)))
            {
                strLicenseTypes.Append(string.Format("[{0}] {1}{2}", (int)licenseType, licenseType.ToString(), Environment.NewLine));
            }

            return strLicenseTypes.ToString();
        }

        public override string ToString()
        {
            string motorcycle = string.Format(
                "{0}The motorcycle is {1} type, and has {2} CC of power!{3}",
                base.ToString(),
                m_LicenseType.ToString(),
                m_EngineVolumeInCC.ToString(),
                Environment.NewLine);

            return motorcycle;
        }
    }
}
