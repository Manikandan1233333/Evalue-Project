using System;
using System.IO;
using System.Text;

namespace CSAAWeb.Filter
{
	/// <summary>
	/// Delegate for a filtering function to be called when the filtering stream's
	/// write method is called.
	/// </summary>
	public delegate void FilterEvent(FilterBuffer b);
	/// <summary>
	/// This class is a simple Html Response Filter.  I contains a property, OnWrite
	/// that is a delegate for a filter to be provided by the creating page class.  If
	/// this property is not null, when the filter's write function is called, this filter
	/// function will be called.
	/// </summary>
	public class HtmlFilter : Stream 
	{
		///<summary>The underlying output stream.</summary>
		protected Stream _sink;
		///<summary>Backer for Position property.</summary>
		private long _position;
		///<summary>Delegate for filter event.</summary>
		public FilterEvent OnWrite;

		/// <summary>
		/// Constructor accepting the stream to write to.
		/// </summary>
		/// <param name="sink">The output stream</param>
		public HtmlFilter(Stream sink) 
		{
			_sink = sink;
		}

		/// <summary>
		/// Constructor accepting the output stream and the filter delegate.
		/// </summary>
		/// <param name="sink">Output stream</param>
		/// <param name="OnWrite">Filter Delegate</param>
		public HtmlFilter(Stream sink, FilterEvent OnWrite) 
		{
			_sink = sink;
			this.OnWrite = OnWrite;
		}


		///<summary>This stream is readable.</summary>
		public override bool CanRead
		{
			get { return true; }
		}

		///<summary>This stream can seek.</summary>
		public override bool CanSeek
		{
			get { return true; }
		}

		///<summary>This stream can be written to.</summary>
		public override bool CanWrite
		{
			get { return true; }
		}

		///<summary>Stream has length of zero</summary>
		public override long Length
		{
			get { return 0; }
		}

		///<summary>Position in the stream.</summary>
		public override long Position
		{
			get { return _position; }
			set { _position = value; }
		}

		///<summary>Seek a position in the stream</summary>
		///<param name="offset">Number of bytes to move.</param>
		///<param name="direction">Direction to seek.</param>
		public override long Seek(long offset, System.IO.SeekOrigin direction)
		{
			return _sink.Seek(offset, direction);
		}

		///<summary>Sets the length of the stream</summary>
		///<param name="length">Length to set.</param>
		public override void SetLength(long length)
		{
			_sink.SetLength(length);
		}

		///<summary>Closes the stream.</summary>
		public override void Close()
		{
			_sink.Close();
		}

		///<summary>Flushes the stream to the output.</summary>
		public override void Flush()
		{
			_sink.Flush();
		}

		///<summary>Reads from the stream</summary>
		public override int Read(byte[] buffer, int offset, int count)
		{
			return _sink.Read(buffer, offset, count);
		}

		///<summary>Writes to the stream.  This is where the work is
		///accomplished.  If OnWrite delegate is set, calls that
		///delegate method to do the filtering.</summary>
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (OnWrite!=null) {
				OnWrite(new FilterBuffer(buffer, _sink, offset, count));
			} else _sink.Write(buffer, offset, count);
		}

	}

	/// <summary>
	/// FilterBuffer encapsulates the incoming byte buffer, positioning in that buffer, and
	/// the output stream.  Methods are provided to locate things in the buffer, to extract 
	/// strings from it, to tranfer from the buffer to the output stream, and to write strings
	/// to the output stream.
	/// </summary>
	public class FilterBuffer 
	{
		private byte[] buffer;
		private Stream Sink;
		///<summary>Offset in the buffer.</summary>
		public int offset;
		///<summary>Last position in the buffer.</summary>
		public int Last;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="sink"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public FilterBuffer(byte[] buffer, Stream sink, int offset, int count) 
		{
			this.buffer = buffer;
			Sink = sink;
			this.offset = offset;
			this.Last = offset + count;
		}

		/// <summary>
		/// Writes st to the output stream.
		/// </summary>
		/// <param name="st"></param>
		public void Write(string st) 
		{
			byte[] data = new byte[st.Length];
			for (int i=0; i<st.Length; i++) data[i]=(byte)st[i];
			Sink.Write(data,0,st.Length);
		}

		/// <summary>
		/// Transfers bytes from the buffer to the output stream.
		/// </summary>
		public void Write(int ToPosition) 
		{
			Sink.Write(buffer, offset, ToPosition-offset);
		}

		/// <summary>
		/// Transfers the remainder of the bytes from the buffer to the output.
		/// </summary>
		public void Write() 
		{
			Sink.Write(buffer, offset, Last-offset);
		}

		/// <summary>
		/// Returns the first position within buffer following offset that ch is found.
		/// </summary>
		/// <param name="ch">Char to seek.</param>
		/// <returns>The position</returns>
		public int IndexOf(char ch) 
		{
			return IndexOf(ch, offset);
		}

		/// <summary>
		/// Returns the first position within buffer following From that ch is found.
		/// </summary>
		/// <param name="ch">Char to seek.</param>
		/// <param name="From">position to seek from</param>
		/// <returns>The position</returns>
		public int IndexOf(char ch, int From) 
		{
			int j;
			for (j=From; j<Last && buffer[j]!=ch; j++);
			return j;
		}

		/// <summary>
		/// Returns the last position of ch prior to the end of the buffer
		/// </summary>
		public int LastIndexOf(char ch) 
		{
			return LastIndexOf(ch, Last);
		}

		/// <summary>
		/// Returns the last position of ch within buffer prior to From
		/// </summary>
		public int LastIndexOf(char ch, int From) 
		{
			int j;
			for (j=Last-1; j>=From && buffer[j]!=ch; j--);
			return j;
		}

		/// <summary>
		/// Moves the offset to i
		/// </summary>
		/// <param name="i"></param>
		public void Seek(int i) 
		{
			offset = i;
		}
		/// <summary>
		/// CheckCR backs up in the stream from the found tag passed any previous
		/// white space.
		/// </summary>
		/// <returns>Modified value of i</returns>
		public int SkipWhiteSpace(int i, bool backward) 
		{
			while (i>offset) {
				i--;
				char ch = (char)buffer[i];
				if (!(ch == ' ' || ch == '\r' || ch == '\n' || ch == '	')) break;
			}
			return i+1;
		}

		/// <summary>
		/// Skips forward in the buffer past any white space.
		/// </summary>
		/// <param name="i"></param>
		/// <returns></returns>
		public int SkipWhiteSpace(int i) 
		{
			while (i<Last) 
			{
				i++;
				char ch = (char)buffer[i];
				if (!(ch == ' ' || ch == '\r' || ch == '\n' || ch == '	')) break;
			}
			return i-1;
		}

		/// <summary>
		/// Returns a string of characters from the buffer.
		/// </summary>
		/// <param name="From">Start char</param>
		/// <param name="To">Last char</param>
		/// <returns></returns>
		public string ToString(int From, int To) 
		{
			StringBuilder st = new StringBuilder(To-From+1);
			for (int i=From; i<=To; i++) st.Append((char)buffer[i]);
			return st.ToString();
		}

	}
}
