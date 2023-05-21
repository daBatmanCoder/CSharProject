using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool    m_IsFreezeCargo;
        private float   m_VolumeOfCargo;

        internal Truck(string i_LicenseNumber, int i_NumOfWheels, float i_MaxAirPressure, object i_EnergyType) :
                              base(i_LicenseNumber, i_NumOfWheels, i_MaxAirPressure, i_EnergyType)
        {

        }

        public void SetVolumeOfCargo(float i_NewVolumeOfCargo)
        {
            if(i_NewVolumeOfCargo > 0)
            {
                m_VolumeOfCargo = i_NewVolumeOfCargo;
            }
            else
            {
                throw new ArgumentException("Volume of cargo isn't valid");
            }
        }

        public void SetIsFreezeCargo(bool i_NewModeForFreeze)
        {
            m_IsFreezeCargo = i_NewModeForFreeze;
        }

        public override string ToString()
        {
            string truck, stringOfContain = "";

            if (!m_IsFreezeCargo)
            {
                stringOfContain = "doesn't";
            }
            else
            {
                stringOfContain = "does";
            }
            
            truck = string.Format(
                "{0} The truck {1} contain freeze cargo, and has {2} volume of cargo!{3}",
                base.ToString(),
                stringOfContain,
                m_VolumeOfCargo.ToString(),
                Environment.NewLine);

            return truck;
        }
    }
}
