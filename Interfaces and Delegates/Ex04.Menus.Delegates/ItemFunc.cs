using System;

namespace Ex04.Menus.Delegates
{
	public class ItemFunc : MenuItem
    {
		public event Action MenuFunctionInvoker;

		public ItemFunc(string i_ItemDesc) : base(i_ItemDesc) { }

		public void ActiveFunc()
		{
			OnMenuClick();
		}

		protected virtual void OnMenuClick()
		{
			if (MenuFunctionInvoker != null)
			{
				MenuFunctionInvoker.Invoke();
			}
		}
	}
}
