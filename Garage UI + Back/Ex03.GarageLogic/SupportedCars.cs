using System;
using System.Text;
using static Ex03.GarageLogic.eNum;

namespace Ex03.GarageLogic
{
    public class SupportedCars
    {
        //Gas and Electric Motorcycles
        private const float k_MotorcycleAirPressure = 31; 
        private const eFuelTypes k_MotorCycleFuelType = eFuelTypes.Octan98;
        private const int k_MotorcycleNumOfWheels = 2;
        private const float k_ElectricMotorcycleMaxHours = 2.5f;
        private const float k_MotorcycleMaxFuel = 6.2f;

        //Gas and Electric Cars
        private const float k_ElectricCarMaxHours = 2.1f;
        private const float k_CarMaxFuel = 38f;
        private const eFuelTypes k_CarFuelType = eFuelTypes.Octan95;
        private const int k_CarNumOfWheels = 4;
        private const float k_CarMaxAirPressure = 29f;

        //Truck 
        private const float k_TruckMaxFuel = 120f;
        private const eFuelTypes k_TruckFuelType = eFuelTypes.Soler;
        private const int k_TruckNumOfWheels = 16;
        private const float k_TruckMaxAirPressure = 24f;

        public static Vehicle CreateVehicle(string i_LicenseNumber, eVehicleType i_VehicleType)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.CarElectric:
                    Electric electricEngineFprCar = new Electric(k_ElectricCarMaxHours);
                    vehicle = new Car(i_LicenseNumber, k_CarNumOfWheels, k_CarMaxAirPressure, electricEngineFprCar);
                    break;

                case eVehicleType.CarFuel:
                    Fuel fuelEngineForCar = new Fuel(k_CarFuelType, k_CarMaxFuel);
                    vehicle = new Car(i_LicenseNumber, k_CarNumOfWheels, k_CarMaxAirPressure, fuelEngineForCar);
                    break;

                case eVehicleType.MotorcycleElectric:
                    Electric electricEngineForMotorcycle = new Electric(k_ElectricMotorcycleMaxHours);
                    vehicle = new Motorcycle(i_LicenseNumber, k_MotorcycleNumOfWheels, k_MotorcycleAirPressure, electricEngineForMotorcycle);
                    break;

                case eVehicleType.MotorcycleFuel:
                    Fuel fuelEngineForMotorcycle = new Fuel(k_MotorCycleFuelType, k_MotorcycleMaxFuel);
                    vehicle = new Motorcycle(i_LicenseNumber, k_MotorcycleNumOfWheels, k_MotorcycleAirPressure, fuelEngineForMotorcycle);
                    break;

                case eVehicleType.TruckFuel:
                    Fuel fuelEngineForTruck = new Fuel(k_TruckFuelType, k_TruckMaxFuel);
                    vehicle = new Truck(i_LicenseNumber, k_TruckNumOfWheels, k_TruckMaxAirPressure, fuelEngineForTruck);
                    break;
            }

            return vehicle;
        }

        public static string ShowVehicleTypes()
        {
            StringBuilder vehicleTypes = new StringBuilder();

            foreach (eVehicleType color in Enum.GetValues(typeof(eVehicleType)))
            {
                vehicleTypes.Append(string.Format("[{0}] {1}{2}", (int)color, color.ToString(), Environment.NewLine));
            }

            return vehicleTypes.ToString();
        }
    }
}
