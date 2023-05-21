using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    class DelegatesMenu
    {
		private MainMenu		m_MainMenu;
		private SubMenuItem		m_ShowVersionAndSpaces;
		private ItemFunc    	m_CountSpaces;
		private ItemFunc	    m_ShowVersion;
		private SubMenuItem		m_ShowDateAndTime;
		private ItemFunc	    m_ShowTime;
		private ItemFunc	    m_ShowDate;

		public DelegatesMenu()
		{
			this.m_MainMenu = new MainMenu("Delegates main menu");
			this.m_ShowDateAndTime = new SubMenuItem("Show Date/Time");
			this.m_ShowVersionAndSpaces = new SubMenuItem("Version and Spaces");
			this.m_ShowTime = new ItemFunc("Show Time");
			this.m_ShowTime.MenuFunctionInvoker += showTime_MenuClick;
			this.m_ShowDate = new ItemFunc("Show Date");
			this.m_ShowDate.MenuFunctionInvoker += showDate_MenuClick;
			this.m_CountSpaces = new ItemFunc("Count Spaces");
			this.m_CountSpaces.MenuFunctionInvoker += countSpaces_MenuClick;
			this.m_ShowVersion = new ItemFunc("Show Version");
			this.m_ShowVersion.MenuFunctionInvoker += showVersion_MenuClick;
		}

        private void showTime_MenuClick()
        {
			ShowTime showTheTime = new ShowTime();
			showTheTime.InvokeFunction();
		}

        private void showDate_MenuClick()
        {
			ShowDate showTheDate = new ShowDate();
			showTheDate.InvokeFunction();
		}

        private void showVersion_MenuClick()
        {
			ShowVersion showTheVersion = new ShowVersion();
			showTheVersion.InvokeFunction();
		}

        private void countSpaces_MenuClick()
        {
			CountSpaces countTheSpaces = new CountSpaces();
			countTheSpaces.InvokeFunction();
		}

		public void InitMenu()
		{
			m_MainMenu.AddItem(m_ShowDateAndTime);
			m_MainMenu.AddItem(m_ShowVersionAndSpaces);
			// adds the showtime and date functions to the submenu ShowDateAndTime
			m_ShowDateAndTime.AddItem(m_ShowTime);
			m_ShowDateAndTime.AddItem(m_ShowDate);
			// adds the showversion and countspaces functions to the submenu VersionAndSpaces
			m_ShowVersionAndSpaces.AddItem(m_CountSpaces);
			m_ShowVersionAndSpaces.AddItem(m_ShowVersion);
			m_MainMenu.Show();
		}
	}
}
