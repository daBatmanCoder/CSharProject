using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float  m_MaxAirPressure;
        private string          m_ManufactureName;
        private float           m_CurrAirPressure;

        internal Wheel(float i_MaxAirPressure)
        {
            this.m_MaxAirPressure = i_MaxAirPressure;
        }

        internal float GetMaxAirPressure()
        {
            return m_MaxAirPressure;
        }

        internal string GetManufactureName()
        {
            return m_ManufactureName;
        }

        internal float GetCurrentAirPressure()
        {
            return m_CurrAirPressure;
        }

        public void SetManufactureName(string i_NewManufactureName)
        {
            if (i_NewManufactureName != string.Empty)
            {
                m_ManufactureName = i_NewManufactureName;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure, 0f); // need to change to logic error ----> string.length = 0 what then
            }
        }

        public void SetMaxPressure()
        {
            m_CurrAirPressure = m_MaxAirPressure;
        }

        public void InflatingAirPressure(float i_AddPressure)
        {
            if (i_AddPressure + m_CurrAirPressure <= m_MaxAirPressure && i_AddPressure >= 0)
            {
                m_CurrAirPressure += i_AddPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaxAirPressure - m_CurrAirPressure, 0f);
            }
        }

        public override string ToString()
        {
            string wheel = string.Format(
                "Manufacturer is {0}, current air pressure is {1} out of {2}.{3}",
                GetManufactureName(),
                GetCurrentAirPressure(),
                GetMaxAirPressure(),
                Environment.NewLine);

            return wheel;
        }
    }
}
        
      
