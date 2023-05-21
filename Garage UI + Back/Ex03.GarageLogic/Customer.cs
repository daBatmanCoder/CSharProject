using System;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        private string m_CustomerName { get; set; }

        private string m_CustomerPhone { get; set; }

        private Vehicle m_CustomerVehicle { get; set; }

        public Customer(string i_CustomerName, string i_CustomerPhoneNumber, Vehicle i_OneVehicle)
        {
            this.m_CustomerName = i_CustomerName;
            this.m_CustomerPhone = i_CustomerPhoneNumber;
            this.m_CustomerVehicle = i_OneVehicle;
        }
    }
}
