using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
	public class InterfaceMenu
	{
		private MainMenu		m_MainMenu; 
		private SubMenuItem		m_ShowVersionAndSpaces;
		private ItemFunc		m_CountSpaces;
		private ItemFunc		m_ShowVersion;
		private SubMenuItem		m_ShowDateAndTime;
		private ItemFunc		m_ShowTime;
		private ItemFunc		m_ShowDate;

		public InterfaceMenu()
		{
			this.m_MainMenu = new MainMenu("Interface main menu");
			this.m_ShowDateAndTime = new SubMenuItem("Show Date/Time");
			this.m_ShowVersionAndSpaces = new SubMenuItem("Version and Spaces");
			this.m_ShowTime = new ItemFunc(
				"Show Time",
				new ShowTime()
				);
			this.m_ShowDate = new ItemFunc(
				"Show Date",
				new ShowDate()
				);
			this.m_CountSpaces = new ItemFunc(
				"Count Spaces",new CountSpaces()
				);
			this.m_ShowVersion = new ItemFunc(
				"Show Version",
				new ShowVersion()
				);
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
