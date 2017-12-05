/*History
 * 
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block

*/
using System;
using System.Threading;
using System.Collections;
using CSAAWeb.AppLogger;
namespace CSAAWeb
{
	/// <summary>
	/// Queue starts a single additional thread to allow web applications to complete
	/// lengthy processes outside of normal page processing, and also to allow sequential
	/// access of fixed resources (such as log files) by multiple threads without holding 
	/// up the main processing thread of the request.  The Application logging facility
	/// provided within CSAAWeb utilizes this queue, and must be used if use of queue is
	/// desired.  Applications can make other uses of Queue by implementing IQueued or
	/// inheriting from QueueItem then placing these objects on the queue by calling
	/// the static method Queue.Enqueue.
	/// </summary>
	/// <remarks>
	/// <seealso cref="IQueued"/>
	/// <see cref="Web.LogModule"/>
	/// <seealso cref="QueueItem"/>
	/// </remarks>
	public class Queue {
		private static ArrayList Queues = new ArrayList();
		private System.Collections.Queue Q = null;
		private Thread WorkerThread = null;
		private bool Abort = false;
		/// <summary>Modify this to change the sleep time.</summary>
		public int SleepTime = 10;

		/// <summary>
		/// Default constructor initializes worker thread and Queue.
		/// </summary>
		private Queue() {
			Q = new System.Collections.Queue();
			WorkerThread = new Thread(new ThreadStart(ThreadMain));
			WorkerThread.Start();
			Queues.Add(this);
		}

		/// <summary>Finalizer aborts worker thread.</summary>
		~Queue() {
			Stop();
		}

		/// <summary>Places item onto the queue</summary>
		public static void Enqueue(IQueued Item) {
			if (Web.LogModule.Instances>0) {
				if (Queues.Count==0) new Queue();
				((Queue)Queues[0]).Enq(Item);
			}
		}

		/// <summary>Stops and clears all the Queues</summary>
		public static void StopAll() {
			foreach (Queue Q in Queues) Q.Stop();
			Queues.Clear();
		}

		/// <summary>Aborts worker thread and clears the Queue</summary>
		private void Stop() {
			System.GC.SuppressFinalize(this);
			if (!Abort) {
				Abort=true;
				WorkerThread.Abort();
				Q.Clear();
			}
		}
		/// <summary>Places item onto the queue</summary>
		private void Enq(IQueued Item) {
			System.Collections.Queue.Synchronized(Q).Enqueue(Item);
		}

		/// <summary>Thread delegate for replication.</summary>
		public void ThreadMain() {
			while (!Abort && Thread.CurrentThread.ThreadState==System.Threading.ThreadState.Running && !AppDomain.CurrentDomain.IsFinalizingForUnload()) {
				IQueued Item=null;
				while (Q.Count>0 && (Item==null || Q.Count>1)) 
					try {
						Item = (IQueued)System.Collections.Queue.Synchronized(Q).Dequeue();
						Item.Run();
						if (Item.Status==QueuedResult.Retry) {
							Item.Reset();
							Enqueue(Item);			
						}
					}
                    //PC Security Defect Fix -CH1 START- Modified the below code to add logging inside the catch block 
                    catch (Exception ex) { Logger.Log(ex.ToString()); }
                //PC Security Defect Fix -CH1 END- Modified the below code to add logging inside the catch block
				Thread.Sleep(SleepTime);
				if (Web.LogModule.Instances<=0) Abort=true;
			}
		}
	}

	/// <summary>Enum for results of IQueued.Run</summary>
	public enum QueuedResult {
		///<summary>The item is waiting to run.</summary>
		Waiting,
		///<summary>Completed successfully</summary>
		Complete,
		///<summary>Completed with failures.</summary>
		Failed,
		///<summary>Requeue the item.</summary>
		Retry
	}

	/// <summary>Interface for objects that are to be queued.</summary>
	public interface IQueued {
		/// <summary>Action to take on dequeue.</summary>
		void Run();
		/// <summary>Resets status to waiting.</summary>
		void Reset();
		/// <summary>Results of run.</summary>
		QueuedResult Status {get;}
	}

	/// <summary>Forms a base class for queued items.</summary>
	public abstract class QueueItem : IQueued {
		private QueuedResult _Status = QueuedResult.Waiting;
		/// <summary/>
		public QueuedResult Status {get {return _Status;} set{_Status=value;}}
		/// <summary>Action to take on dequeue.</summary>
		public abstract void Run();
		/// <summary>Resets status to waiting.</summary>
		public void Reset() {
			_Status=QueuedResult.Waiting;
		}
	}

}
