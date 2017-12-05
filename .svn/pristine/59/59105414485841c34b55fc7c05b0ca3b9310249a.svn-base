/* History
 * 
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
 * MAIG - CH1 - Added New Luhn Algorithm that works for every Credit Card which is used in PC Application 11/17/2014
*/
using System;
using System.Runtime.InteropServices;
using System.Text;
using CSAAWeb.AppLogger;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace CSAAWeb
{
	/// <summary>
	/// Provides class methods for encrypting and decrypting.
	/// </summary>
	public class Cryptor
	{
		/// <summary>
		/// Decyrpts Value encrypted with Encrypt using Key.
		/// </summary>
		public static string Decrypt(string Value, string Key) 
		{
			return new Crypto().Decrypt(Value, Key);
		}

		/// <summary>
		/// Encyrpts Value using Key.
		/// </summary>
		public static string Encrypt(string Value, string Key) 
		{
			return new Crypto().Encrypt(Value, Key);
		}

		/// <summary>
		/// Generates a Mod-10 (Luhn) check digit for st
		/// </summary>
		/// <param name="st">Input string (must be an integer)</param>
		/// <returns>Check digit character</returns>
		public static string CheckDigit(string st) {
			if (!CSAAWeb.Validate.IsAllNumeric(st,false))
				throw new Exception("'" + st + "' invalid input. CheckDigit can only calculate for numbers.");
			//This is the initial multiplier
			int Sum = 0;
			//This For/Next loop calculates (Weight * Digit) for each position starting from the right and adds each Result to the Sum to be used in the mod 10 calculation 
			for (int i = st.Length-1; i>=0; i--) {
				int k = Convert.ToInt32(st.Substring(i,1)) * ((i%2==0)?2:1);
				//If k is more than 1 digit then the two digits are added together or subtract 9 from k (k is the same)
				if (k > 9) k -= 9;
				//Add all the ks together
				Sum += k;
			}
			//Divide Sum by modulus 10, if the Result is zero then the the check digit is zero, else the check digit is ten minus the Result
			return ((Sum % 10) == 0)?"0":(10 - (Sum % 10)).ToString();
            
		}

        //MAIG - CH1 - BEGIN - Added New Luhn Algorithm that works for every Credit Card which is used in PC Application 11/17/2014
        /// <summary>
        /// Method that validates the given Credit Number is valid or not
        /// </summary>
        /// <param name="st">Credit Card input parameter</param>
        /// <returns>0 means Success else failure</returns>
        public static string CreditCardCheckDigit(string st)
        {
            if (!CSAAWeb.Validate.IsAllNumeric(st, false))
                throw new Exception("'" + st + "' invalid input. CheckDigit can only calculate for numbers.");
            int[,] sumTable = new int[,]{ { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                        { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 } };
            int sum = 0, flip = 0;
            for (int i = st.Length - 1; i >= 0; i--, flip++)
            {
                int n = st[i] - '0';
                if (n < 0 || n > 9)
                {
                    return "1";
                }
                sum += sumTable[flip & 0x1, n];
            }
            return ((sum % 10) == 0) ? "0" : (10 - (sum % 10)).ToString();
        }
        //MAIG - CH1 - END - Added New Luhn Algorithm that works for every Credit Card which is used in PC Application 11/17/2014

	}
	/// <summary>
	/// Class for Encrypting and Decrypting data through the windows Crypto API
	/// </summary>
	internal class Crypto {
        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
		//private uint hProvider;
        IntPtr hProvider = new IntPtr();
		//private uint hHash;
        IntPtr hHash = new IntPtr();
		//private uint hKey;
        IntPtr hKey = new IntPtr();
        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
		private ASCIIEncoding Encoder;

		/// <summary>
		/// Set to true if an exception should be thrown when encryption/decryption errors are encountered.
		/// When false, encrypt/decrypt will simply return an empty string on error.
		/// </summary>
		public bool ThrowEncryptionErrors = false;
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public Crypto() {
			Encoder = new ASCIIEncoding();
		}

		///<summary>Release any handles and free the Encoder</summary>
		~Crypto() {
			Encoder = null;
            //PC Security Defect Fix -CH1 - START Modified the below code to add logging inside the catch block
            //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
			try {if (hHash != IntPtr.Zero) CryptDestroyHash(hHash);} catch (Exception ex){Logger.Log(ex.ToString());}
			try {if (hKey != IntPtr.Zero) CryptDestroyKey(hKey);} catch (Exception ex){Logger.Log(ex.ToString());}
            try { if (hProvider != IntPtr.Zero) CryptReleaseContext(hProvider, 0); } catch (Exception ex) { Logger.Log(ex.ToString()); }
            //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
            //PC Security Defect Fix -CH1 - END Modified the below code to add logging inside the catch block
		}

		/// <summary>
		/// Encrypt st using password to derive a 128-bit key.  Result is converted to a string of Hex values
		/// </summary>
		/// <param name="st">The string to encrypt</param>
		/// <param name="Password">The password from which to derive the key.</param>
		/// <returns>The encrypted string.</returns>
        //public string Encrypt(string st, string Password)
        //{
        //    if (st.Length == 0 || Password.Length == 0) throw new Exception("A required argument is missing.");
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
        //    uint StrLen = (uint)st.Length;
        //    uint BufLen = StrLen;
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
        //    DeriveKey(Password);
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
        //    CryptEncrypt(hKey, IntPtr.Zero, 1, 0, null, ref BufLen, StrLen);
        //    int bflen = (int)BufLen;
        //    st = st.PadRight(bflen);
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
        //    byte[] buffer = Encoder.GetBytes(st);
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
        //    if (!CryptEncrypt(hKey, IntPtr.Zero, 1, 0, buffer, ref StrLen, BufLen) && ThrowEncryptionErrors)
        //        throw new Exception(Win32Err("Could not encrypt data."));
        //    return ToHex(buffer);
        //}

        /// <summary>
        /// Added method based on RijndaelManaged standard
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        private byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

		/// <summary>
		/// Decrypt st using password to derive a 128-bit key.  St must have previously encrypted using the
		/// same password, and converted to a hex string.
		/// </summary>
		/// <param name="st">The encrypted hex string to decrypt.</param>
		/// <param name="Password">The password from which to derive the key.</param>
		/// <returns>The original unencrypted string.</returns>
        //public string Decrypt(string st, string Password)
        //{

        //    if (st.Length == 0 || Password.Length == 0) throw new Exception("A required argument is missing.");
        //    DeriveKey(Password);
        //    byte[] buffer = FromHex(st);
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
        //    uint BufLen = (uint)buffer.Length;
        //    if (!CryptDecrypt(hKey, IntPtr.Zero, 1, 0, buffer, ref BufLen) && ThrowEncryptionErrors)
        //        throw new Exception(Win32Err("Could not decrypt data"));
        //    return Encoder.GetString(buffer, 0, buffer.Length);
        //    //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
        //}

        /// <summary>
        /// Added method based on RijndaelManaged standard
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="passPhrase"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string passPhrase)
        {

            //cipherText = cipherText.Replace(" ", "+");
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }


		/// <summary>
		/// Derives a 128-bit private key from password using the Microsoft Enhanced provider with
		/// MD5 hash and RC2 encryption.  Handles to the provider, hash and key are placed in the 
		/// appropritate private properties, which can then be used by the calling method.  These
		/// handles are released when this object is terminated.
		/// </summary>
		/// <param name="Password">The password from which the private key is derived.</param>
		private void DeriveKey(string Password) {
			// Aquire a context.
			if (!CryptAcquireContext(ref hProvider, null, MS_ENHANCED_PROV, PROV_RSA_FULL, CRYPT_VERIFYCONTEXT))
				throw new Exception(Win32Err("Could not acquire context."));
			// Create a handle to a hash object using the MD5 algorithm
			if (!CryptCreateHash(hProvider, CALG_MD5, 0, 0, ref hHash)) 
				throw new Exception(Win32Err("Could not create hash."));
			// Add the password to the hash
			byte[] buffer = Encoder.GetBytes(Password);
            //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
            uint BufLen = (uint)Password.Length;
			if (!CryptHashData(hHash, buffer, BufLen, 0))
				throw new Exception(Win32Err("Could not create hash data."));
			// Generate a session key for use with the cypher
			if (!CryptDeriveKey(hProvider, CALG_RC2, hHash, CRYPT_NO_SALT, ref hKey))
				throw new Exception(Win32Err("Could not derive key."));
		}

		/// <summary>
		/// Converts the byte array to a hexadecimal string.
		/// </summary>
		/// <param name="st">The byte array to convert.</param>
		/// <returns>Hex string representation of st.</returns>
		private string ToHex(byte[] st) {
			StringBuilder Result = new StringBuilder();
			for (int i=0; i<st.Length; i++) {
				string sHex = (st[i]).ToString("X");
				Result.Append(((sHex.Length==1)?("0"+sHex):sHex));
			}
			return Result.ToString();
		}

		/// <summary>
		/// Converts the hexadecimal string st to a byte array.
		/// </summary>
		/// <param name="st">The hex string to convert.</param>
		/// <returns>Array of bytes represented in st.</returns>
		private byte[] FromHex(string st) {
            if ((st.Length % 2) != 0) throw new Exception("Not a valid hex string.");
            byte[] Result = new byte[st.Length / 2];
            for (int i = 0; i * 2 < st.Length; i++)
                Result[i] = (Byte.Parse(st.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber));
            return Result;
		}

		// The provider
		private const string MS_ENHANCED_PROV = "Microsoft Enhanced Cryptographic Provider v1.0";
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
		// For the context
		private const uint CRYPT_VERIFYCONTEXT = 0xF0000000;
		private const uint PROV_RSA_FULL = 1;

		// Hashing algorithms for the session key
		private const uint ALG_CLASS_HASH = 32768;              // (4 << 13)
		private const uint ALG_TYPE_ANY = 0;
		private const uint ALG_SID_MD5 = 3;
		private const uint CALG_MD5 = (ALG_CLASS_HASH | ALG_TYPE_ANY | ALG_SID_MD5);

        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
		// For the session key
		private const int CRYPT_NO_SALT = 0x10;
        IntPtr handleSessionKey = new IntPtr();
		// Encryption algorithms
		private const int ALG_CLASS_DATA_ENCRYPT = 24576;        // (3 << 13)
		private const int ALG_TYPE_BLOCK = 1536;                 // (3 << 9)
		private const int ALG_TYPE_STREAM = 2048;                // (4 << 9)
		private const int ALG_SID_RC2 = 2;
		private const uint ALG_SID_RC4 = 1;
		private const int CALG_RC2 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_BLOCK | ALG_SID_RC2);
        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
		private const uint CALG_RC4 = (ALG_CLASS_DATA_ENCRYPT | ALG_TYPE_STREAM | ALG_SID_RC4);
        
		private static string Win32Err(string Msg) {
			long Err = Marshal.GetLastWin32Error();
			string Result = (Err<0)?(" Crypto API error " + (Err & 0xFFFFFFFF).ToString("X")):(" Runtime error: " + Err.ToString());
			return Msg + Result;
		}

        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - Start
		// Declarations for the CryptoAPI functions
		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptAcquireContext 
			(ref IntPtr phProv, string pszContainer, string pszProvider, uint dwProvType, uint dwFlags);

        [DllImport("advapi32", CharSet = CharSet.Ansi, SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        private static extern bool CryptCreateHash
            (IntPtr hProv, uint algID, uint hKey, uint dwFlags, ref IntPtr phHash);


		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptDeriveKey
            (IntPtr hProv, int algID, IntPtr hBaseData, int dwFlags, ref IntPtr phKey); 

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptDestroyHash (IntPtr hHash);

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptDestroyKey (IntPtr hKey);

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptEncrypt
            (IntPtr hKey, IntPtr hHash, int Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen, uint dwBufLen);

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptDecrypt
            (IntPtr hKey, IntPtr hHash, int Final, uint dwFlags, byte[] pbData, ref uint pdwDataLen);

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptHashData (IntPtr hHash, byte[] pbData, uint dwDataLen, uint dwFlags);

		[DllImport("advapi32", CharSet=CharSet.Ansi, SetLastError=true, CallingConvention=CallingConvention.Winapi)]
		private static extern bool CryptReleaseContext (IntPtr hProv, uint dwFlags);
        //CHGXXXXXXX - Updated datatype as part of Upgrade to .Net Framework 4.6.1 - End
	}
}
