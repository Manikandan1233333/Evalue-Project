using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSAAWeb.WebControls;
using CSAAWeb.Serializers;

namespace MSC {
	/// <summary>
	/// ItemsControl encapsulates the display of Item input controls.
	/// </summary>
	public class ItemsControl : System.Web.UI.WebControls.WebControl {
		///<summary></summary>
		protected Validator Valid;
		///<summary></summary>
		private string _ErrorMessage = string.Empty;
		///<summary>The error message for the validator.</summary>
		public string ErrorMessage {
			get {return _ErrorMessage;}
			set {_ErrorMessage=value; if (Valid!=null) Valid.ErrorMessage=value;}
		}
		///<summary>The validator function for this control.</summary>
		public event ValidatorEventHandler ServerValidate;

		/// <summary>
		/// The number of Items.  This is one less than the number of controls.
		/// </summary>
		public int Count {
			get {return Controls.Count-1;}
			set {while (Count<value) AddItem();}
		}
		///<summary>True if the control should render entirely as hidden.</summary>
		public bool Hidden = false;
		///<summary>True if the first element should render as hidden.</summary>
		public bool HideFirst = false;
		///<summary>The file path to the user control.</summary>
		public string ControlFile = string.Empty;
		
		///<summary>Init event builds spouse checker validator.</summary>
		protected override void OnInit(System.EventArgs e) {
			InitValidator();
			base.OnInit(e);
		}

		///<summary>Builds list validator.</summary>
		private void InitValidator() {
			if (Controls.Count>0) return;
			Valid = new Validator();
			Valid.Display = ValidatorDisplay.None;
			Valid.ErrorMessage=_ErrorMessage;
			Valid.ServerValidate += new ValidatorEventHandler(CheckItems);
			Valid.ID = "Valid";
			Controls.Add(Valid);
		}

		///<summary>Validator deligate calls the delegate specified for each control.</summary>
		protected void CheckItems(Object source, ValidatorEventArgs e) {
			if (ServerValidate == null) return;
			foreach (Control C in Controls)
				if (typeof(IItemControl).IsInstanceOfType(C)) {
					ValidatorEventArgs E = new ValidatorEventArgs(C);
					ServerValidate(this, E);
					if (!E.IsValid) {
						e.IsValid=false;
						break;
					}
				}
		}

		/// <summary>
		/// Adds an empty Item input structure
		/// </summary>
		public void AddItem() {
			AddItem(null);
		}

		/// <summary>
		/// Creates and Adds a new Item input structure and instantiates it from i.
		/// </summary>
		public void AddItem(SimpleSerializer i) {
			if (ControlFile==string.Empty) throw new Exception("No user control name provided.");
			IItemControl Item = (IItemControl)Page.LoadControl(Page.Request.ApplicationPath + ControlFile);
			Item.Number = Count;
			if (Hidden || (HideFirst && Item.Number==0)) Item.Hidden=true;
			Item.ShowDelete =(Item.Number!=0);
			Controls.Add((Control)Item);
			if (i!=null) Item.Data = i;
		}

		
		/// <summary>
		/// Handles removing a deleted Item when the Item's delete button is
		/// clicked.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		/// <returns></returns>
		protected override bool OnBubbleEvent(object source, EventArgs e) {
			if (e is ItemEventArgs) 
				switch (((ItemEventArgs)e).Command) {
					case ItemEvent.Delete:
						Controls.Remove((Control)source);
						return true;
					default: return false;
				}
			return false;
		}

		/// <summary>
		/// Any Item input structure that has had its delete button clicked will
		/// be deleted here, and all the following Items have their numbers
		/// decremented.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {	
			int n=0;
			for (int i=0; i<Controls.Count; i++) 
				if (typeof(IItemControl).IsInstanceOfType(Controls[i])) {
					((IItemControl)Controls[i]).Number=n;
					((IItemControl)Controls[i]).ShowDelete =(n!=0 || Controls.Count>2);
					n++;
				}
			base.OnPreRender(e);
		}

		/// <summary>
		/// Restore the Count from the viewstate.
		/// </summary>
		protected override void LoadViewState(object savedState) {
			if (savedState != null) Count=(int)savedState;
		}
		
		/// <summary>
		/// Save the Count property as the viewstate.
		/// </summary>
		protected override object SaveViewState() {  
			return Count;
		}

		/// <summary>
		/// Index property allows use of CopyTo
		/// </summary>
		public SimpleSerializer this[int index] {
			get {return ((IItemControl)Controls[index+1]).Data;}
		}

		///<summary>Copies items to Dest.</summary>
		public ArrayOfSimpleSerializer CopyTo(ArrayOfSimpleSerializer Dest) {
			Dest.Clear();
			for (int i=0; i<Count; i++) Dest.Add(this[i]);
			return Dest;
		} 

		///<summary>Copies items from Source.</summary>
		public void CopyFrom(ArrayOfSimpleSerializer Source) {
			Controls.Clear(); 
			InitValidator();
			foreach (SimpleSerializer i in Source) AddItem(i);
		}
	}

	/// <summary>
	/// IItemControl interface allows Item control to be manipulated
	/// without having the entire control definition.
	/// </summary>
	public interface IItemControl {
		/// <summary>
		/// The number of the control.
		/// </summary>
		int Number {get; set;}
		/// <summary>
		/// ItemInfo Item Information.
		/// </summary>
		SimpleSerializer Data {get; set;}
		/// <summary>
		/// True if the control should render as hidden.
		/// </summary>
		bool Hidden {get; set;}
		/// <summary>
		/// True if the control should show the delete button.
		/// </summary>
		bool ShowDelete {get; set;}
	}

	/// <summary>
	/// Events that can be bubbled by IItem
	/// </summary>
	public enum ItemEvent {
		/// <summary>
		/// IItem control should be deleted.
		/// </summary>
		Delete}

	/// <summary>
	/// Event args type used by IItem to bubble event to ItemsControl
	/// </summary>
	public class ItemEventArgs : System.EventArgs {
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ItemEventArgs() {}
		/// <summary>
		/// Constructor with a command.
		/// </summary>
		/// <param name="Command">The command.</param>
		public ItemEventArgs(ItemEvent Command) {
			this.Command=Command;
		}
		/// <summary>
		/// The command being bubbled.
		/// </summary>
		public ItemEvent Command;
	}
}
