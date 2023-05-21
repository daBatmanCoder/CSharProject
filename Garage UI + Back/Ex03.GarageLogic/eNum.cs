namespace Ex03.GarageLogic
{
    public class eNum
    {
        public enum eVehicleStatus
        {
            inRepair = 1,
            Repaired,
            Paid
        }

        public enum eVehicleType
        {
            MotorcycleFuel = 1,
            MotorcycleElectric,
            CarFuel,
            CarElectric,
            TruckFuel
        }

        public enum eColors
        {
            Red = 1,
            White,
            Gray,
            Green
        }

        public enum eDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            BB
        }

        public enum eFuelTypes
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }
    }
}
