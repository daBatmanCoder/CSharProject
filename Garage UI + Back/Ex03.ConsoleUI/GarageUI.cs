using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.eNum;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        public static void RunGarage()
        {
            int count = 1;
            garageLogo();
            Console.WriteLine("Hello, Welcome to BENJONI Garage!!!");
            Garage theGarage = new Garage();

            runMainMenu(theGarage, count);
        }

        private static void runMainMenu(Garage i_TheGarage, int i_NumberOfTimeCalled)
        {
            string mainMenu;
            int userChoice;
            bool userInputFlag;
            bool flagToHelp = true;

            if (i_NumberOfTimeCalled != 1)
            {
                Console.Clear();
                garageLogo();
            }

            i_NumberOfTimeCalled++;
            Console.WriteLine("What action would you like to do?");
            mainMenu = string.Format(@"{0}[1]Enter a new vehicle to the garage{0}[2]Display the vehicle's status in the garage
[3]Change a vehicle status{0}[4]Inflate the air-pressure to the maximum{0}[5]Fuel a vehicle{0}[6]Charge an electric vehicle{0}[7]Display all vehicle statistics{0}[8]Exit The BenJoni Garage", Environment.NewLine);
            Console.WriteLine(mainMenu);
            userInputFlag = int.TryParse(Console.ReadLine(), out userChoice);
            while (flagToHelp)
            {
                if (userInputFlag)
                {
                    if (userChoice >= 1 && userChoice <= 8)
                    {
                        break;
                    }
                }

                Console.WriteLine("Invalid choice, please try again");
                userInputFlag = int.TryParse(Console.ReadLine(), out userChoice);
            }

            switch (userChoice)
            {
                case 1:
                    enterANewVehicleToTheGarage(i_TheGarage);
                    Console.WriteLine(string.Format("{0}Thank you for the details, your car is being fixed..",Environment.NewLine));
                    break;
                case 2:
                    displaySpecificStatus(i_TheGarage);
                    break;
                case 3:
                    changeCarStatus(i_TheGarage, i_NumberOfTimeCalled);
                    break;
                case 4:
                    inflateTireAirPressureToMax(i_TheGarage, i_NumberOfTimeCalled);
                    break;
                case 5:
                    gasACar(i_TheGarage);
                    break;
                case 6:
                    chargeACar(i_TheGarage, i_NumberOfTimeCalled);
                    break;
                case 7:
                    displayCarDetails(i_TheGarage, ref i_NumberOfTimeCalled);
                    break;
                case 8:
                    flagToHelp = !flagToHelp;
                    break;
            }

            if(flagToHelp)
            {
                returnToMainMenu(i_TheGarage, i_NumberOfTimeCalled);
            }
        }

        private static void returnToMainMenu(Garage i_TheGarage, int i_NumberOfTimeCalled)
        {
            string userChoiceAfterAddedVehicle;

            Console.WriteLine("to return to the main menu, please press 'M' or 'm', or anything else to exit.");
            userChoiceAfterAddedVehicle = Console.ReadLine();
            if (userChoiceAfterAddedVehicle.Equals("M") || userChoiceAfterAddedVehicle.Equals("m"))
            {
                runMainMenu(i_TheGarage, i_NumberOfTimeCalled);
            }
        }

        private static void returnToMainMenuFromInside(Garage i_TheGarage, ref int i_TheNumberOfTimeMenuCalled, out string o_LicenseNumber)
        {
            Console.WriteLine(string.Format("Invalid License number entered.{0}to return to the main menu, please press 'M' or 'm'{0}Or try another license number.", Environment.NewLine));
            o_LicenseNumber = Console.ReadLine();
            if (o_LicenseNumber.Equals("M") || o_LicenseNumber.Equals("m"))
            {
                runMainMenu(i_TheGarage, i_TheNumberOfTimeMenuCalled);
            }
        }

        //1
        private static void enterANewVehicleToTheGarage(Garage i_TheGarage)
        {

            Console.Clear();
            garageLogo();
            bool flagToHelp = true;
            // To add a new car we need to ask the following questions:
            Console.WriteLine("\nPlease enter the license number of your vehicle");
            string i_LicenseNumber = Console.ReadLine();
            int TypeOfVehicle;
            bool flagOfOkType;

            while (flagToHelp)
            {
                if (Validation.ValidateLicenseNumber(i_LicenseNumber))
                {
                    break;
                }

                Console.WriteLine("Invalid license number, please try again");
                i_LicenseNumber = Console.ReadLine();
            }

            Console.WriteLine(string.Format("{0}Please choose the Vehicle Type", Environment.NewLine));
            string VehicleTypes = SupportedCars.ShowVehicleTypes();
            Console.WriteLine(VehicleTypes);
            flagOfOkType = int.TryParse(Console.ReadLine(), out TypeOfVehicle);
            while (flagToHelp)
            {
                if (Validation.ValidateTypeOfVehicle(TypeOfVehicle) && flagOfOkType)
                {
                    break;
                }

                Console.WriteLine("Invalid license number, please try again");
                flagOfOkType = int.TryParse(Console.ReadLine(), out TypeOfVehicle);
            }

            if (i_TheGarage.IsVehicleInGarage(i_LicenseNumber))
            {
                Console.WriteLine("Vehicle is already in the garage, switching the vehicle status to repair");
                i_TheGarage.ChangeCarStatus(i_LicenseNumber,  eVehicleStatus.inRepair);
            }
            else // so the vehicle isn't in the garage so we add him.
            {
                Vehicle oneVehicle = SupportedCars.CreateVehicle(i_LicenseNumber, (eVehicleType)TypeOfVehicle);
                i_TheGarage.EnterANewVehicleToTheGarage(oneVehicle);
                Console.WriteLine(string.Format("{0}Please enter your name", Environment.NewLine));
                string customerName = Console.ReadLine();
                int countName = 0;

                while (flagToHelp)
                {
                    countName = 0;
                    foreach (char oneChar in customerName)
                    {
                        if (char.IsLetter(oneChar))
                        {
                            countName++;
                        }
                    }

                    if (countName == customerName.Length)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid name, please enter only letters for your name..");
                        customerName = Console.ReadLine();
                    }
                }

                Console.WriteLine(string.Format("{0}Please enter your phone number", Environment.NewLine));
                string customerPhoneNumber = Console.ReadLine();
                int countDigitsOfPhoneNumber;

                while (flagToHelp)
                {
                    countDigitsOfPhoneNumber = 0;
                    foreach (char oneChar in customerPhoneNumber)
                    {
                        if (char.IsDigit(oneChar))
                        {
                            countDigitsOfPhoneNumber++;
                        }
                    }

                    if (countDigitsOfPhoneNumber == customerPhoneNumber.Length && customerPhoneNumber.Length > 7)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid phone number, please enter only digits for your phone number..");
                        customerPhoneNumber = Console.ReadLine();
                    }
                }

                Customer c1 = new Customer(customerName, customerPhoneNumber, i_TheGarage.GetVehicle(i_LicenseNumber)); // we save the details for the user ( vehicle phone and name )

                askAndUpdateGeneralVehicleQuestions(i_TheGarage.GetVehicle(i_LicenseNumber));
                getAllVehicleWiseDetails(i_TheGarage.GetVehicle(i_LicenseNumber), (eVehicleType)TypeOfVehicle);
            }
        } 

        //2
        private static void displaySpecificStatus(Garage i_TheGarage)
        {
            Console.Clear();
            garageLogo();
            bool isStatusOk;
            int theStatus;
            bool flagToHelp = true;
            eVehicleStatus v1;
            string messageToUser = "";

            StringBuilder VehicleMessage = new StringBuilder();
            Console.WriteLine("Please choose which status would you like to see:");
            Console.WriteLine(Garage.ShowVehicleStatus());
            Console.WriteLine(string.Format("[{0}] All", Enum.GetValues(typeof(eVehicleStatus)).Length + 1));
            isStatusOk = int.TryParse(Console.ReadLine(), out theStatus);

            while(flagToHelp)
            {
                if(isStatusOk)
                {
                    try
                    {
                        i_TheGarage.IsRangeOfVehicleStatus(theStatus, out v1);
                        messageToUser = displayCarStatus(i_TheGarage, v1);
                        
                        break;
                    }
                    catch(ValueOutOfRangeException ex)
                    {
                        if(theStatus == Enum.GetValues(typeof(eVehicleStatus)).Length + 1)
                        {
                            flagToHelp = !flagToHelp;
                            foreach (eVehicleStatus status in Enum.GetValues(typeof(eVehicleStatus)))
                            {
                                VehicleMessage.Append(displayCarStatus(i_TheGarage, status));
                            }
                            break;
                        }
                        else
                        {
                            messageToUser = ex.Message;
                            messageToUser += Environment.NewLine;
                            messageToUser += "Please enter a valid status";
                        }
                    }
                }
                else
                {
                    messageToUser = "Invalid input entered, please enter a range number..";
                }

                Console.WriteLine(messageToUser);
                isStatusOk = int.TryParse(Console.ReadLine(), out theStatus);
            }
            if (flagToHelp)
            {
                Console.WriteLine(messageToUser);
            }
            else
            {
                Console.WriteLine(VehicleMessage.ToString());
            }
        }

        //2.1
        private static string displayCarStatus(Garage i_TheGarage, eVehicleStatus i_TheVehicleStatus)
        {
            List<Vehicle> theListToPrint;
            StringBuilder theMessageToPrint = new StringBuilder();

            theListToPrint = i_TheGarage.DisplayCarStatus(i_TheVehicleStatus);
            foreach (Vehicle licenseN in theListToPrint)
            {
                theMessageToPrint.Append(string.Format("The status of the vehicle with license number {0} is: {1}{2}", licenseN.GetLicenseNumber(), i_TheVehicleStatus.ToString(), Environment.NewLine));
            }

            return theMessageToPrint.ToString();
        }

        //3
        private static void changeCarStatus(Garage i_TheGarage, int i_TheNumberOfTimeMenuCalled)
        {
            Console.Clear();
            garageLogo();

            Console.WriteLine("Please enter your license number");
            string licenseNumber = Console.ReadLine();


            bool flagToHelp = true;

            while (flagToHelp)
            {
                if (i_TheGarage.IsVehicleInGarage(licenseNumber))
                {
                    Console.WriteLine("alright, the vehicle is in the garage.");
                    bool theNewStateFlag;
                    int theNewState;
                    Console.WriteLine("Please enter the new desired state");
                    Console.WriteLine(Garage.ShowVehicleStatus());
                    theNewStateFlag = int.TryParse(Console.ReadLine(), out theNewState);
                    eVehicleStatus v1;
                    string messageToUser = "";

                    if (theNewStateFlag)
                    {
                        try
                        {
                            i_TheGarage.IsRangeOfVehicleStatus(theNewState, out v1);
                            i_TheGarage.ChangeCarStatus(licenseNumber, v1);
                            break;
                        }
                        catch (Exception ex)
                        {
                            if (ex is ArgumentException)
                            {
                                messageToUser = "The vehicle is already with the status you choosed, please choose different status.";
                            }
                            else
                            {
                                messageToUser = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        messageToUser = string.Format("Invalid input, please try again and choose a type");
                    }

                    Console.WriteLine(messageToUser);
                    Console.WriteLine(Garage.ShowVehicleStatus());
                    theNewStateFlag = int.TryParse(Console.ReadLine(), out theNewState);
                }
                else
                {
                    returnToMainMenuFromInside(i_TheGarage, ref i_TheNumberOfTimeMenuCalled, out licenseNumber);
                }
            }

            Console.WriteLine("The new status has been saved.");
        }

        //4
        private static void inflateTireAirPressureToMax(Garage i_TheGarage, int i_TheNumberOfTimeMenuCalled)
        {
            Console.Clear();
            garageLogo();
            Console.WriteLine("Please enter your license number");
            string licenseNumber = Console.ReadLine();
            bool flagToHelp = true;
            Vehicle theVehicleToInflateTo;
            string messageToUser;

            while (flagToHelp && !(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                if (i_TheGarage.IsVehicleInGarage(licenseNumber))
                {
                    Console.WriteLine("alright, the vehicle is in the garage.");
                    theVehicleToInflateTo = i_TheGarage.GetVehicle(licenseNumber);

                    foreach (Wheel oneWheel in theVehicleToInflateTo.GetWheels())
                    {
                        oneWheel.SetMaxPressure();
                    }

                    break;
                }
                else
                {
                    messageToUser = "Invalid license number or the vehicle isn't in the garage...Please enter a new license number";
                    messageToUser += Environment.NewLine;
                    messageToUser += "or press 'M' to return to the main menu";
                }

                returnToMainMenuFromInside(i_TheGarage, ref i_TheNumberOfTimeMenuCalled, out licenseNumber);
            }

            if (!(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                messageToUser = string.Format("Vehicle number {0} wheels has been inflated to the maximum", licenseNumber);
                Console.WriteLine(messageToUser);
            }
        }

        //5
        private static void gasACar(Garage i_TheGarage)
        {
            Console.Clear();
            garageLogo();
            Console.WriteLine("Please enter your license number");
            string licenseNumber = Console.ReadLine();
            Vehicle i_TheVehicle;
            string messageToUser = "";
            int WhichFuelType, count = 1;
            bool isAmoutToFuel, isFuelTypeOk, didEverythingFlag = false, flagToHelp = true;
            float amoutToFuel;

            while (flagToHelp && !(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                if (i_TheGarage.IsVehicleInGarage(licenseNumber) && (i_TheGarage.GetVehicle(licenseNumber).GetEnergyType() is Fuel))
                {
                    i_TheVehicle = i_TheGarage.GetVehicle(licenseNumber);
                    if (count == 1)
                    {
                        Console.WriteLine(string.Format("alright, the vehicle is in the garage, and can be fueled{0}", Environment.NewLine));
                        count++;
                    }

                    Console.WriteLine(string.Format("Please enter the fuel type:{0}", Environment.NewLine));
                    Console.WriteLine(Fuel.ShowFuelTypes());
                    isFuelTypeOk = int.TryParse(Console.ReadLine(), out WhichFuelType);
                    if (isFuelTypeOk && Fuel.IsFuelTypeOk(WhichFuelType))
                    {
                        Console.WriteLine(string.Format("Please enter amout to fuel:{0}", Environment.NewLine));
                        isAmoutToFuel = float.TryParse(Console.ReadLine(), out amoutToFuel);
                        if (isAmoutToFuel)
                        {
                            try
                            {
                                (i_TheVehicle.GetEnergyType() as Fuel).FillTheFuelTank(amoutToFuel, (eFuelTypes)WhichFuelType);
                                i_TheVehicle.SetPercentagesOfEnergyRemaining((i_TheVehicle.GetEnergyType() as Fuel).FuelPercentage());
                                messageToUser = "The vehicle has been fueled";
                                didEverythingFlag = !didEverythingFlag;
                                break;
                            }
                            catch (Exception ex)
                            {
                                messageToUser = ex.Message;
                            }
                        }
                        else
                        {
                            messageToUser = string.Format("Invalid input, please enter a correct decimal number:{0}", Environment.NewLine);
                            //isAmoutToFuel = float.TryParse(Console.ReadLine(), out amoutToFuel);
                        }
                    } 
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a correct type, according to the list:");
                    }
                }
                else
                {
                    if (!(i_TheGarage.GetVehicle(licenseNumber).GetEnergyType() is Fuel) && i_TheGarage.IsVehicleInGarage(licenseNumber))
                    {
                        Console.WriteLine("The vehicle you entered is in the garage but cannot be fueled, because it isn't fuel type.");
                        Console.WriteLine("Please enter an fuel-based license number vehicle in order to charge the vehicle");
                        Console.WriteLine("Or enter 'M' to return to the main menu");
                    }
                    else
                    {
                        Console.WriteLine("The license number you entered isn't valid, please try again");
                        Console.WriteLine("Or enter 'M' to return to the main menu");
                    }

                    licenseNumber = Console.ReadLine();
                }

                if (didEverythingFlag || licenseNumber.Equals("m") || licenseNumber.Equals("M"))
                {
                    break;
                }
               
                Console.WriteLine(string.Format("{0}{1}", Environment.NewLine, messageToUser));
            }

            if (!(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                Console.WriteLine(messageToUser);
            }
        }

        //6
        private static void chargeACar(Garage i_TheGarage, int i_TheNumberOfTimeMenuCalled)
        {
            Console.Clear();
            garageLogo();
            Console.WriteLine("Please enter your license number");
            string licenseNumber = Console.ReadLine();
            bool flagToHelp = true;
            Vehicle theVehicleToString;
            string messageToUser = "";
            float amoutToCharge;
            bool isAmoutToChargeOK;

            while (flagToHelp && !(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                if (i_TheGarage.IsVehicleInGarage(licenseNumber) && (i_TheGarage.GetVehicle(licenseNumber).GetEnergyType() is Electric))
                {
                    Console.WriteLine(string.Format("alright, the vehicle is in the garage, and can be charged{0}please enter the amount to charge.", Environment.NewLine));
                    isAmoutToChargeOK = float.TryParse(Console.ReadLine(), out amoutToCharge);
                    while (flagToHelp)
                    {
                        if (isAmoutToChargeOK)
                        {
                            try
                            {
                                theVehicleToString = i_TheGarage.GetVehicle(licenseNumber);
                                (theVehicleToString.GetEnergyType() as Electric).ChargeBattery(amoutToCharge);
                                theVehicleToString.SetPercentagesOfEnergyRemaining((theVehicleToString.GetEnergyType() as Electric).ElectricPercentage());
                                messageToUser = "The Battery as been charged.";
                                break;
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                messageToUser = ex.Message;
                                messageToUser += "Please enter an in-range number.";
                            }
                        }
                        else
                        {
                            messageToUser = "invalid input, please enter a correct form decimal number.";
                        }

                        Console.WriteLine(messageToUser);
                        isAmoutToChargeOK = float.TryParse(Console.ReadLine(), out amoutToCharge);
                    }

                    break;
                }
                else
                {
                    if (!(i_TheGarage.GetVehicle(licenseNumber).GetEnergyType() is Electric) && i_TheGarage.IsVehicleInGarage(licenseNumber))
                    {
                        Console.WriteLine("The vehicle you entered is in the garage but cannot be charged, because it isn't Electric type.");
                        Console.WriteLine("Please enter an electric license number in order to charge the vehicle");
                    }
                    else
                    {
                        Console.WriteLine("The license number you entered isn't valid, please try again");
                    }

                    returnToMainMenuFromInside(i_TheGarage, ref i_TheNumberOfTimeMenuCalled, out licenseNumber);
                }
            }

            if (!(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                Console.WriteLine(messageToUser);
            }
        }

        //7
        private static void displayCarDetails(Garage i_TheGarage, ref int i_TheNumberOfTimeMenuCalled)
        {
            Console.Clear();
            garageLogo();
            Console.WriteLine("Please enter your license number");
            string licenseNumber = Console.ReadLine();
            bool flagToHelp = true;
            Vehicle theVehicleToString;
            string messageToUser = "";

            while (flagToHelp && !(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                if (i_TheGarage.IsVehicleInGarage(licenseNumber))
                {
                    theVehicleToString = i_TheGarage.GetVehicle(licenseNumber);
                    messageToUser = theVehicleToString.ToString();
                    break;
                }
                else
                {
                    returnToMainMenuFromInside(i_TheGarage, ref i_TheNumberOfTimeMenuCalled, out licenseNumber);
                }
            }

            if (!(licenseNumber.Equals("M") || licenseNumber.Equals("m")))
            {
                Console.WriteLine(messageToUser);
            }
        }

        //General questions for all vehicles
        private static void askAndUpdateGeneralVehicleQuestions(Vehicle i_TheVehicleToAskFor)
        {
            string WheelManu, ModelName;
            float WheelsPressure;
            bool flagToHelp = true;
            Console.WriteLine();
            Console.WriteLine("Please enter the wheel manufacturer");
            WheelManu = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the current wheels pressure");
            bool isAirPressure = float.TryParse(Console.ReadLine(), out WheelsPressure);
            StringBuilder airPressureMessage;

            while(flagToHelp)
            {
                if (isAirPressure)
                {
                    airPressureMessage = new StringBuilder();
                    try
                    {
                        foreach (Wheel oneWheel in i_TheVehicleToAskFor.GetWheels())
                        {
                            oneWheel.InflatingAirPressure(WheelsPressure);
                            oneWheel.SetManufactureName(WheelManu);
                        }

                        break;
                    }
                    catch(ValueOutOfRangeException ex)
                    {
                        airPressureMessage.Append(string.Format("Invalid air pressure, the correct airpressure is in range of {0} - {1}", ex.GetMinValue(), ex.GetMaxValue()));
                        Console.WriteLine(airPressureMessage.ToString());
                        isAirPressure = float.TryParse(Console.ReadLine(), out WheelsPressure);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter correct air pressure value");
                    isAirPressure = float.TryParse(Console.ReadLine(), out WheelsPressure);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Please enter the vehicle model name");
            ModelName = Console.ReadLine();
            i_TheVehicleToAskFor.SetModelName(ModelName);
            Console.WriteLine();

        }

        // all other questions MENU
        private static void getAllVehicleWiseDetails(Vehicle i_TheVehicleToAskFor, eVehicleType i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.MotorcycleElectric:
                    electricMotorcycleQuestions(i_TheVehicleToAskFor);
                    break;
                case eVehicleType.MotorcycleFuel:
                    fuelMotorcycleQuestions(i_TheVehicleToAskFor);
                    break;
                case eVehicleType.CarElectric:
                    electricCarQuestions(i_TheVehicleToAskFor);
                    break;
                case eVehicleType.CarFuel:
                    fuelCarQuestions(i_TheVehicleToAskFor);
                    break;
                case eVehicleType.TruckFuel:
                    fuelTruckQuestions(i_TheVehicleToAskFor);
                    break;
            }
        }

        //FuelBased Questions
        private static void fuelBasedQuestions(Vehicle i_TheVehicle)
        {
            bool flagToHelp = true;
            bool isCurrentFuelOk;
            float currentFuelLevel;

            Console.WriteLine("Please enter the current fuel in the vehicle");
            isCurrentFuelOk = float.TryParse(Console.ReadLine(), out currentFuelLevel);
            while (flagToHelp)
            {
                if (isCurrentFuelOk)
                {
                    try
                    {
                        (i_TheVehicle.GetEnergyType() as Fuel).SetCurrentFuelTank(currentFuelLevel);
                        i_TheVehicle.SetPercentagesOfEnergyRemaining(Vehicle.ReimaingEnergyPercentage(currentFuelLevel, (i_TheVehicle.GetEnergyType() as Fuel).GetMaxTank()));
                        break;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a correct format of fuel level");
                }

                isCurrentFuelOk = float.TryParse(Console.ReadLine(), out currentFuelLevel);
            }
        }

        //Electric Questions
        private static void electricQuestions(Vehicle i_TheVehicle)
        {
            bool flagToHelp = true;
            bool isCurrentChargeOk;
            float currentChargeLevel;

            Console.WriteLine("Please enter the current charge of the vehicle");
            isCurrentChargeOk = float.TryParse(Console.ReadLine(), out currentChargeLevel);
            while (flagToHelp)
            {
                if (isCurrentChargeOk)
                {
                    try
                    {
                        (i_TheVehicle.GetEnergyType() as Electric).SetHoursLeftInBattery(currentChargeLevel);
                        i_TheVehicle.SetPercentagesOfEnergyRemaining(Vehicle.ReimaingEnergyPercentage(currentChargeLevel, (i_TheVehicle.GetEnergyType() as Electric).GetMaxHoursInBattery()));
                        break;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a correct format of charge level");
                }

                isCurrentChargeOk = float.TryParse(Console.ReadLine(), out currentChargeLevel);
            }
        }

        //Motorcycle Questions
        private static void motorcycleQuestions(Vehicle i_TheVehicle)
        {
            int licenseType, EngineVolumeInCC;
            bool flagToHelp = true;
            bool isLicenseTypeOK, isVolumeInCCOK;

            Console.WriteLine("Please enter the license type");
            Console.Write(Motorcycle.ShowLicenseTypes());
            Console.WriteLine();
            isLicenseTypeOK = int.TryParse(Console.ReadLine(), out licenseType);
            StringBuilder airPressureMessage = new StringBuilder();

            while (flagToHelp)
            {
                try
                {
                    if (isLicenseTypeOK)
                    {
                        (i_TheVehicle as Motorcycle).SetLicenseType((eLicenseType)licenseType);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a correct license type from the list:");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(Motorcycle.ShowLicenseTypes());
                isLicenseTypeOK = int.TryParse(Console.ReadLine(), out licenseType);
            }

            Console.WriteLine();

            Console.WriteLine("Please enter the engine CC (normal number e.g 200,300..)");
            isVolumeInCCOK = int.TryParse(Console.ReadLine(), out EngineVolumeInCC);
            while (flagToHelp)
            {
                try
                {
                    if (isVolumeInCCOK)
                    {
                        (i_TheVehicle as Motorcycle).SetEngineVolume(EngineVolumeInCC);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a correct engine volume in CC");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                isVolumeInCCOK = int.TryParse(Console.ReadLine(), out EngineVolumeInCC);
            }
        }

        //Car Questions
        private static void carQuestions(Vehicle i_TheVehicle)
        {
            int howManyDoors;
            bool flagToHelp = true;
            bool isDoorOkay;

            Console.WriteLine("Please enter the number of doors your car has");
            Console.Write(Car.ShowNumberOfDoors());
            Console.WriteLine();
            isDoorOkay = int.TryParse(Console.ReadLine(), out howManyDoors);

            while (flagToHelp)
            {
                try
                {
                    if (isDoorOkay)
                    {
                        (i_TheVehicle as Car).SetDoors((eDoors)howManyDoors);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a correct number of doors from the list:");
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(Car.ShowNumberOfDoors());
                isDoorOkay = int.TryParse(Console.ReadLine(), out howManyDoors);
            }

            Console.WriteLine();
            Console.WriteLine("Please enter the color of your car");
            Console.Write(Car.ShowColorsOptions());
            Console.WriteLine();
            isDoorOkay = int.TryParse(Console.ReadLine(), out howManyDoors);

            while (flagToHelp)
            {
                try
                {
                    if (isDoorOkay)
                    {
                        (i_TheVehicle as Car).SetColor((eColors)howManyDoors);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a correct number of doors from the list:");
                    }
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine(Car.ShowColorsOptions());
                isDoorOkay = int.TryParse(Console.ReadLine(), out howManyDoors);
            }

            Console.WriteLine();
        }

        //Truck Questions
        private static void truckQuestions(Vehicle i_TheVehicle)
        {
            string isFreezeString;
            bool flagToHelp = true;
            bool isVolumeOfTruckOK;
            float volumeOfTheTruck;

            Console.WriteLine("Please enter 'Y' if the truck contains Freeze, 'N' for no");
            isFreezeString = Console.ReadLine();
            while (flagToHelp)
            {
                if (isFreezeString.Equals("Y") || isFreezeString.Equals("N"))
                {
                    if (isFreezeString.Equals("Y"))
                    {
                        (i_TheVehicle as Truck).SetIsFreezeCargo(true);
                        break;
                    }

                    (i_TheVehicle as Truck).SetIsFreezeCargo(false);
                    break;
                }

                Console.WriteLine("Invalid command entered, please enter only 'Y' or 'N'.");
                isFreezeString = Console.ReadLine();
            }

            Console.WriteLine("Please enter the truck volume");
            isVolumeOfTruckOK = float.TryParse(Console.ReadLine(), out volumeOfTheTruck);
            while (flagToHelp)
            {
                if (isVolumeOfTruckOK)
                {
                    try
                    {
                        (i_TheVehicle as Truck).SetVolumeOfCargo(volumeOfTheTruck);
                        break;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid volume of cargo entered, please enter a valid volume cargo");
                }

                isVolumeOfTruckOK = float.TryParse(Console.ReadLine(), out volumeOfTheTruck);
            }
        }

        //Fuel Motorcycle
        private static void fuelMotorcycleQuestions(Vehicle i_TheVehicle)
        {
            motorcycleQuestions(i_TheVehicle);
            fuelBasedQuestions(i_TheVehicle);
        }

        //Electric Motorcycle
        private static void electricMotorcycleQuestions(Vehicle i_TheVehicle)
        {
            motorcycleQuestions(i_TheVehicle);
            electricQuestions(i_TheVehicle);
        }

        //Fuel Car
        private static void fuelCarQuestions(Vehicle i_TheVehicle)
        {
            carQuestions(i_TheVehicle);
            fuelBasedQuestions(i_TheVehicle);
        }

        // Electric Car
        private static void electricCarQuestions(Vehicle i_TheVehicle)
        {
            carQuestions(i_TheVehicle);
            electricQuestions(i_TheVehicle);
        }

        // Fuel Truck
        private static void fuelTruckQuestions(Vehicle i_TheVehicle)
        {
            truckQuestions(i_TheVehicle);
            Console.WriteLine();
            fuelBasedQuestions(i_TheVehicle);
        }

        private static void garageLogo()
        {
            string garageLogo;
            garageLogo = string.Format(@"#################################################################
##                                                             ##
##    ██████╗░███████╗███╗░░██╗░░░░░██╗░█████╗░███╗░░██╗██╗    ##
##    ██╔══██╗██╔════╝████╗░██║░░░░░██║██╔══██╗████╗░██║██║    ##
##    ██████╦╝█████╗░░██╔██╗██║░░░░░██║██║░░██║██╔██╗██║██║    ##
##    ██╔══██╗██╔══╝░░██║╚████║██╗░░██║██║░░██║██║╚████║██║    ##
##    ██████╦╝███████╗██║░╚███║╚█████╔╝╚█████╔╝██║░╚███║██║    ##
##    ╚═════╝░╚══════╝╚═╝░░╚══╝░╚════╝░░╚════╝░╚═╝░░╚══╝╚═╝    ##
##                                                             ##
#################################################################
");
            Console.WriteLine(garageLogo);
        }
    }
}

