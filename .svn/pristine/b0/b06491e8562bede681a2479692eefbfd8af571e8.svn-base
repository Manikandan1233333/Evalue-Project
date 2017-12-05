using System;
using System.Web;
using CSAAWeb.AppLogger;
using CSAAWeb;
using System.Collections;


namespace CSAAWeb.Web
{
	/// <summary>
	/// Module automatically closes objects of type IClosableWeb. Web Services based on
	/// SqlWebService implement this interface.  Web Pages can also be built that implement
	/// this interface to close any open Sql connections within the Close Method.
	/// </summary>
	/// <remarks>
	/// <para>
	/// ClosableModule must be installed within an application through web.config, in the httpModules
	/// section of the system.web configuration section.  
	/// </para>
	/// <seealso cref="Web.IClosableWeb"/>
	/// <seealso cref="Web.SqlWebService"/>
	/// </remarks>
	/// 
	/// <example>
	/// <para>Here is a sample web.config file for using ClosableModule:</para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;configuration&gt;
	///
	///    &lt;system.web&gt;
	///        &lt;!--Install ClosableModule with the following tag:--&gt;
	///
	///        &lt;httpModules&gt;
	///            &lt;add name="ClosableModule" type="CSAAWeb.Web.ClosableModule, CSAAWeb"/&gt;
	///        &lt;/httpModules&gt;
	///
	///     &lt;/system.web&gt;
	///
	///&lt;/configuration&gt;
	///</code>
	///</example>
	public class ClosableModule : IHttpModule
	{
		///<summary>Default constructor for ClosableModule</summary>
		public ClosableModule() : base() {}
		///<summary>Constructs instance of ClosableModule and calls Init.</summary>
		public ClosableModule(System.Web.HttpApplication Application) : base() {
			Init(Application);
		}

		///<summary>
		///Implements IHttpModule.Init.  Hooks to Application_OnBeginRequest,
		///Application_OnError, Application_PostRequestHandlerExecute and
		///Application_EndRequest.
		///</summary>
		public void Init(System.Web.HttpApplication Application) {
			Application.BeginRequest += new EventHandler(OnBeginRequest);
			Application.Error += new EventHandler(CheckClose);
			Application.PostRequestHandlerExecute += new EventHandler(CheckClose);
			Application.EndRequest += new EventHandler(CheckClose);
		}
		
		private bool Closed = true;
		
		private void OnBeginRequest(object Source, EventArgs e) {
			Closed=false;
			((System.Web.HttpApplication)Source).Context.Items.Add("ClosableModule",this);
		}

		///<summary>Placeholder for IClosable page to install itself.</summary>
		private ArrayList Handlers;

		/// <summary>
		/// Call this method from a web service that needs to be closed.
		/// </summary>
		private void Handle(IClosableWeb H) {
			if (Handlers==null) Handlers = new ArrayList();
			if (!Handlers.Contains(H)) Handlers.Add(H);
		}

		/// <summary>
		/// Call this method from a web service that needs to be closed. Will throw
		/// an exception if module isn't installed.
		/// </summary>
		public static bool SetHandler(IClosableWeb H) {
			ClosableModule M = Closer;
			if (M==null) throw new Exception("ClosableModule is not installed.");
			else M.Handle(H);
			return (M!=null);
		}

		/// <summary>
		/// Removes H from the list that needs to be closed.
		/// </summary>
		private void StopHandle(IClosableWeb H) {
			if (Handlers==null) return;
			Handlers.Remove(H);
		}

		/// <summary>
		/// Call this method from a web service that needs to be closed.
		/// </summary>
		public static void RemoveHandler(IClosableWeb H) {
			ClosableModule M = Closer;
			if (M!=null) M.StopHandle(H);
		}

		/// <summary>
		/// Returns the current Instance from the context.
		/// </summary>
		private static ClosableModule Closer {
			get {return HttpContext.Current.Items["ClosableModule"] as ClosableModule;}
		}

		/// <summary>
		/// Called on PostRequestHandlerExecute event;
		/// </summary>
		private void CheckClose(object Source, EventArgs e) {
			HttpContext Context = (HttpContext)((System.Web.HttpApplication)Source).Context;
			if (Closed) return;
			if (typeof(IClosableWeb).IsInstanceOfType(Context.Handler))
				DoClose(((IClosableWeb)Context.Handler));
			while (Handlers!=null && Handlers.Count>0) 
				DoClose((IClosableWeb)Handlers[0]);
			Handlers=null;
			Closed=true;
			Context.Items["ClosableModule"]=null;
		}

		private void DoClose(IClosableWeb H) {
			try {
				H.Close();
				StopHandle(H);
			} catch (Exception e) {AppLogger.Logger.Log(new Exception("ClosableModule trapped exception while trying to close " + H.GetType().FullName, e));}
		}

		///<summary>Implements IHttpModule.Dispose.</summary>
		public void Dispose() {
		}

	}

	/// <summary>
	/// Interface to use for web pages that want to have resources cleaned up
	/// automatically on completion.
	/// </summary>
	public interface IClosableWeb {
		/// <summary>
		/// Method called to close resources.  Typically used to close database
		/// connections.
		/// </summary>
		void Close();
	}
}
