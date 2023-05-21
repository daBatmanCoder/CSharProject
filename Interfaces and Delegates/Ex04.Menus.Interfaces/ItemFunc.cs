namespace Ex04.Menus.Interfaces
{
    public class ItemFunc : MenuItem
    {
        private readonly IFunctionality r_TheMethod;

        public ItemFunc( string i_ItemDesc, IFunctionality i_TheMethod) : base(i_ItemDesc)
        {
            r_TheMethod = i_TheMethod;
        }
        
        public IFunctionality GetFunction
        {
            get
            {
                return r_TheMethod;
            }
        }
    }
}
