namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using CSAAWeb;
	using CSAAWeb.WebControls;


	/// <summary>
	///	Encapsulates data entry for the state.
	/// </summary>
	public partial  class State : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.DropDownList _State;
		///<summary/>
		private static ArrayList States;

		///<summary/>
		static State() 
		{
			States = Config.SettingArray("States");
		}

		///<summary/>
		public override string Text {get {return ListC.GetListValue(_State);} set {ListC.SetListIndex(_State, value);}}

		///<summary/>
		protected override void OnInit(EventArgs e) 
		{
			InitializeComponent();
			base.OnInit(e);
			if (_State.Items.Count==0)
				foreach (string i in States) {
					string [] st = i.Replace(" ","").Split(':');
					if (st.Length==1) {
						_State.Items.Add(new ListItem(st[0]));
					} else _State.Items.Add(new ListItem(st[0],st[1]));
				}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
