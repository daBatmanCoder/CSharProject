namespace Ex04.Menus.Interfaces
{
    public class MainMenu : SubMenuItem
    {
		public MainMenu(string i_ItemDescription) : base(i_ItemDescription)
		{
			m_BackOrExitMsg = "Exit";
		}

		public void Show()
        {
			LoadMenu();
		}
	}
}
