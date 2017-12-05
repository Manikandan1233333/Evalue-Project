using System;
using System.Collections;
using System.Collections.Specialized;
using CSAAWeb.Serializers;
using PaymentClasses;

namespace PaymentService
{
	/// <summary>
	/// Class used to persist card information from denied authorizations in memory for
	/// a brief time.  This allows the service to compare card information for subsequent
	/// auth requests on the same Merchant Ref Num to determine eligibility to continue
	/// without having to persist this data in the database.  The data is actually stored
	/// in the WebService.Application property as a dictionary.  This class allows 
	/// manipulating of that data.
	/// </summary>
	public class CardFile {
		private static System.DateTime LastCleanup;
		private static HybridDictionary Cards = new HybridDictionary();
		///<summary/>
		static CardFile() {
			LastCleanup = System.DateTime.Now;
		}
		
		/// <summary>
		/// Adds an entry in the dicationary.
		/// </summary>
		/// <param name="Payment">The payment to add.</param>
		internal static void Add(Payment Payment) {
			lock (Cards) {
				string Ref = Reference(Payment);
				if (Cards[Ref]==null) {
					Cards.Add(Ref, new SavedCard(Payment));
				} else Cards[Ref] = new SavedCard(Payment);
			}
		}

		/// <summary>
		/// Removes the entry for MerchantRefNum
		/// </summary>
		/// <param name="Payment">The payment to remove</param>
		public static void Remove(Payment Payment) {
			lock(Cards) {
				Cards.Remove(Reference(Payment));
			}
		}

		/// <summary>
		/// Called at the end of each request to clean up stale values.  If the last call was
		/// less that a minute ago, just returns without doing anything.
		/// </summary>
		public static void CleanUp() {
			lock (Cards) {
				if (LastCleanup.AddMinutes(1).CompareTo(System.DateTime.Now)>0) {
					return;
				} else {
					LastCleanup=System.DateTime.Now;
				}
				ArrayList DeleteList = new ArrayList();
				foreach (DictionaryEntry e in Cards)
					if (((SavedCard)e.Value).TimeStamp.CompareTo(System.DateTime.Now.AddHours(-1))<=0)
						DeleteList.Add(e.Key);
				foreach (object i in DeleteList) Cards.Remove(i);
			}
		}

		/// <summary>
		/// Returns a number indicating how Card compares to the card in the previous
		/// auth request for this transaction.
		/// 0 - No saved value or card number doesn't match
		/// 1 - Card number matches
		/// 3 - Card and CC_CV match
		/// 5 - Card and Expiration date match
		/// 7 - Exact match.
		/// This information is sent to the stored procedure Check_ReAuth, which uses it in
		/// conjunction with data from the database to determine if auth or reauth is OK, or
		/// to return the appropriate exception.
		/// </summary>
		/// <param name="Payment">The payment to be checked.</param>
		/// <returns></returns>
		public static int CheckCard(Payment Payment) {
			SavedCard S = (SavedCard)Cards[Reference(Payment)];
			if (S==null) return 0;
			CardInfo c = S.Card;
			int result=0;
			if (c==null || c.CCNumber!=Payment.Card.CCNumber) return result;
			result ++;
			if (c.CCCVNumber==Payment.Card.CCCVNumber) result += 2;
			if (c.CCExpMonth==Payment.Card.CCExpMonth && c.CCExpYear==Payment.Card.CCExpYear)
				result += 4;
			return result;
		}

		private static string Reference(Payment Payment) {
			return Payment.Application + "_" + Payment.Reference;
		}

	}

	/// <summary>
	/// Helper class for perstisting the CardFile information into and out of XML.
	/// </summary>
	public class ArrayOfSavedCard : ArrayOfSimpleSerializer {
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ArrayOfSavedCard() {}
		/// <summary>
		/// Restores from Xml
		/// </summary>
		/// <param name="xml"></param>
		public ArrayOfSavedCard(string xml) {LoadXML(xml);}

		/// <summary>
		/// Gets or sets the array item at index.
		/// </summary>
		public new SavedCard this[int index] {
			get {return (SavedCard) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>
		/// Adds the item to the list.
		/// </summary>
		/// <param name="item">The card to add.</param>
		public void Add(SavedCard item) {
			InnerList.Add(item);
		}
	}

	/// <summary>
	/// Class used to store an card in the application.  Wraps CardInfo with a timestamp
	/// that is used for cleaning up stale data.
	/// </summary>
	public class SavedCard : SimpleSerializer {
		/// <summary>The card information.</summary>
		public CardInfo Card = null;
		///<summary>The time this record was created.</summary>
		public System.DateTime TimeStamp = System.DateTime.Now;
		///<summary>Merchant reference number for the payment.</summary>
		public string Reference = string.Empty;
		
		/// <summary>
		/// Initializes a new instance of SavedCard with the information from Payment.
		/// </summary>
		/// <param name="Payment">The Payment to save.</param>
		public SavedCard(Payment Payment) {	
			this.Card = Payment.Card;
			this.Reference = Payment.Reference;
		}

		/// <summary>
		/// Initializes a new instance of SavedCard with default parameters.
		/// </summary>
		public SavedCard() {}

	}

}
