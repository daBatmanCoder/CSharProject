using System;
using System.Collections.Generic;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class SubMenuItem : MenuItem
    {
        private readonly List<MenuItem> r_subItemsList;

		protected internal string m_BackOrExitMsg;

        public SubMenuItem(string i_ItemDescription) : base(i_ItemDescription)
        {
            r_subItemsList = new List<MenuItem>();
            m_BackOrExitMsg = "Back";
        }

		public void AddItem(MenuItem i_ItemToAdd)
		{
			r_subItemsList.Add(i_ItemToAdd);
		}

		public void PrintMenu()
		{
			StringBuilder menuToPrint = new StringBuilder();
			int currItemIndex = 1;

			menuToPrint.Append(string.Format("{0}{1}", headlineInit(DescriptionOfItem), Environment.NewLine));
			menuToPrint.Append(generateSeperator());
			foreach (MenuItem oneItem in r_subItemsList)
			{
				menuToPrint.AppendFormat("{0} -> {1}{2}", currItemIndex, oneItem.DescriptionOfItem, Environment.NewLine);
				currItemIndex++;
			}

			menuToPrint.Append(string.Format(
						@"{0} -> {1}{2}",
						0,
						m_BackOrExitMsg,
						Environment.NewLine));
			menuToPrint.Append(generateSeperator());
			Console.Write(menuToPrint.ToString());
		}

        private string generateSeperator()
        {
			return string.Format("-----------------------{0}", Environment.NewLine);
        }

        private string headlineInit(string i_ItemHeadline)
        {
			return string.Format("**{0}**", i_ItemHeadline);
        }

		public void LoadMenu()
		{
			bool isUserWantsToQuit, isInCollection;
			bool noneZero = true;
			int itemIndex;

			while (noneZero)
			{
				PrintMenu();
				try
				{
					itemIndex = GetItemIndexFromUser();
					isUserWantsToQuit = itemIndex == 0;
					Console.Clear();
					if (isUserWantsToQuit)
					{
						break;
					}

					isInCollection = r_subItemsList[itemIndex - 1] is SubMenuItem;
					noneZero = itemIndex != 0;
					if (isInCollection)
					{
						(r_subItemsList[itemIndex - 1] as SubMenuItem).LoadMenu();
					}
					else
					{
						(r_subItemsList[itemIndex - 1] as ItemFunc).GetFunction.InvokeFunction();
						Console.WriteLine("Press any key to return to the previous menu");
						Console.ReadLine();
						Console.Clear();
					}
				}
				catch (Exception ex)
				{
					Console.Clear();
					Console.Write(string.Format("{1}{0}{0}{2}", Environment.NewLine, ex.Message, generateSeperator()));
				}
			}
		}

		public int GetItemIndexFromUser()
		{
			int itemIndex;
			int itemCountInList = r_subItemsList.Count;
			string itemFromUser;

			Console.WriteLine("Enter your request: (1 to {0} or press '0' to {1}).", itemCountInList, m_BackOrExitMsg);
			itemFromUser = Console.ReadLine();
			IsNumberInRange(itemFromUser, 0, itemCountInList); // Check if input is correct
			itemIndex = int.Parse(itemFromUser);

			return itemIndex;
		}

        private void IsNumberInRange(string i_TheItemFromTheUser, int i_FirstIndex, int i_LastIndex)
        {
			int inputInIntForm;
			bool isNumberInRange;
			bool isStringOk = int.TryParse(i_TheItemFromTheUser, out inputInIntForm);

			if (isStringOk)
			{
				isNumberInRange = inputInIntForm >= i_FirstIndex && inputInIntForm <= i_LastIndex;
				if (!isNumberInRange)
				{
					throw new ValueOutOfRangeException(i_LastIndex, i_FirstIndex);
				}
			}
			else
			{
				throw new FormatException("Your input is invalid, please enter a correct number!");
			}
		}
    }
}
