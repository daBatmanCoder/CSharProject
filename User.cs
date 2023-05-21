using System.Collections.Generic;

namespace BackDamka
{
    public enum eUserTypes
    {
        O,
        X
    }

    public class User
    {
        eUserTypes userType;
        string nameOfUser;
        public List<Square> squareListOfCheckers;

        public User(eUserTypes UserType, string name)
        {
            this.nameOfUser = name;
            this.userType = UserType;
            this.squareListOfCheckers = new List<Square>();
        }

        public string GetName()
        {
            return this.nameOfUser;
        }

        public eUserTypes GetUserType()
        {
            return this.userType;
        }
    }
}
