using System;
using System.Web;
using System.Collections;

namespace CSAAWeb.Web
{
	/// <summary>
	/// Class used by HttpApplication to allow progamatic addition of
	/// Modules, since ASP.NET modules collection is read only.
	/// </summary>
	public class ExtendableModuleCollection
	{
		/// <summary/>
		private HttpModuleCollection Modules;
		/// <summary/>
		private ArrayList ExtraModules;
		/// <summary/>
		private ArrayList ModuleKeys = null;

		/// <summary>
		/// Constructor creates new instance of class and adds all the
		/// modules in M to its collection.
		/// </summary>
		public ExtendableModuleCollection(HttpModuleCollection M)
		{
			Modules = M;
			ModuleKeys = new ArrayList(M.AllKeys);
			ExtraModules = new ArrayList();
		}

		/// <summary>Adds Module to the collection of modules.</summary>
		public void Add(IHttpModule Module) {
			ExtraModules.Add(Module);
			ModuleKeys.Add(Module.GetType().Name);
		}

		///<summary>
		///Calls Dispose on all the added modules, but not the original
		///modules provided to the constructor.  Allows ASP.NET to dispose of
		///them
		///</summary>
		public void Dispose() {
			foreach (IHttpModule M in ExtraModules) M.Dispose();
			ExtraModules.Clear();
			ModuleKeys.Clear();
			ExtraModules=null;
			ModuleKeys=null;
			Modules=null;
		}
		/// <summary>Returns true if the module is installed.</summary>
		public bool Contains(string ModuleName) {
			return ModuleKeys.Contains(ModuleName);
		}

		/// <summary>Returns the module Name.</summary>
		public IHttpModule this[string Name] {
			get {
				IHttpModule Result = Modules[Name];
				if (Result!=null) return Result;
				foreach (IHttpModule M in ExtraModules)
					if (M.GetType().Name==Name) return M;
				return null;
			}
		}

		/// <summary>Returns the module at index</summary>
		public IHttpModule this[int index] {
			get {
				if (index<Modules.Count) return Modules[index];
				index-=Modules.Count;
				if (index<ExtraModules.Count) return (IHttpModule)ExtraModules[index];
				return null;
			}
		}

	}
}
