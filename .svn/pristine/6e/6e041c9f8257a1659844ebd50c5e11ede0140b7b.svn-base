using System;
using System.IO;
using System.Web;
using System.Text;
using System.Web.UI;

namespace CSAAWeb.Filter
{
	/// <summary>
	/// HttpFilteredWriter is a class used to replace the page's HtmlTextWriter's
	/// InnerWriter property.  It wraps the stream of innerwriter with a filter supplied
	/// by the TagFilter Filter provided to the constructor.  This class replaces all
	/// the methods provided by the InnerWriter with its own.  This class is provided because
	/// the OutputStream property of the InnerWriter, an HttpWriter class object, is
	/// read only, and so can't be replaced directly.  These methods are not documented
	/// here because they are identical in definition to those of the HttpWriter, although
	/// they do not operate identically, because the HttpWriter flushes after each call to write.
	/// This does not flush until the flush method is called.
	/// </summary>
	internal class HttpFilteredWriter : TextWriter
	{
		private StreamWriter Output = null;
		private Stream O = null;
		private Encoding _Encoding;
		private HttpWriter InnerWriter = null;

		public HttpFilteredWriter(HttpWriter writer, TagFilter Filter)
		{
			_Encoding = writer.Encoding;
			O = new HtmlFilter(writer.OutputStream, new FilterEvent(Filter.DoFilter));
			Output = new StreamWriter(O);
			InnerWriter = writer;
		}

		public override Encoding Encoding { get {return _Encoding; }}

		public Stream OutputStream { get {return O;}}

		public override void Close() { Output.Close(); InnerWriter.Close();}

		public override void Flush() { Output.Flush(); }

		public override void Write(char ch) { Output.Write(ch); }

		public override void Write(Object obj) {Output.Write(obj);}

		public override void Write(string s) { Output.Write(s); }

		public override void Write(char[] buffer, int index, int count) {Output.Write(buffer, index, count); }

		public void WriteBytes(byte[] buffer, int index, int count) 
		{
			O.Write(buffer, index, count); 
		}

		public override void WriteLine() {Write("\r\n");}

		public void WriteString(string s, int index, int count) {Write(s.Substring(index, count));}

	}
}
