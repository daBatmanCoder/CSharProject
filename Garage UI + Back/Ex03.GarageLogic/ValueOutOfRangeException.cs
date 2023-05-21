using System;

namespace Ex03.GarageLogic
{
    [Serializable]
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("Value was not in the range of {0} - {1} ", i_MinValue, i_MaxValue))
        {
            this.m_MaxValue = i_MaxValue;
            this.m_MinValue = i_MinValue;
        }

        public float GetMaxValue()
        {
            return m_MaxValue;
        }
        public float GetMinValue()
        {
            return m_MinValue;
        }
    }
}