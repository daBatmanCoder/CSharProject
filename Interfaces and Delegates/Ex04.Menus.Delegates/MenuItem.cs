namespace Ex04.Menus.Delegates
{
    public class MenuItem
    {
        private string m_DescriptionOfItem; 

        public string DescriptionOfItem
        {
            get { return m_DescriptionOfItem; }
        }

        public MenuItem(string i_DescriptionOfItem)
        {
            m_DescriptionOfItem = i_DescriptionOfItem;
        }
    }
}
