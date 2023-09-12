using DomainLayer.Utils.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using static ServiceLayer.DelegateTypess.CustomDelegates;

namespace DomainLayer
{
    public class CipherHelper : ICipherHelper
    {
        public CipherHelper()
        {            
        }
        // Tag to make sure this file is readable/decryptable by this class
        private const ulong FC_TAG = 0xFC010203040506CF;
        // Now will will initialize a buffer and will be processing the input file in chunks. 
        // This is done to avoid reading the whole file (which can be huge) into memory. 
        private static int BUFFER_LENGTH = 4096;
        public string ResolveEncryption(FileStream fileStreamIn, FileStream fileStreamOut, SymmetricAlgorithm sma, ProgressCallback callback)
        {
            try
            {
                long lSize = fileStreamIn.Length;  // the size of the input file for storing
                // create the hashing and crypto streams
                HashAlgorithm hasher = SHA256.Create();

                byte[] bytes = new byte[BUFFER_LENGTH];

                int read = -1; // the amount of bytes read from the input file
                CryptoStream cout = new CryptoStream(fileStreamOut, sma.CreateEncryptor(), CryptoStreamMode.Write);
                CryptoStream chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write);

                // write the size of the file to the output file
                BinaryWriter bw = new BinaryWriter(cout);
                bw.Write(lSize);

                // write the file cryptor tag to the file
                bw.Write(FC_TAG);

                // read and the write the bytes to the crypto stream in BUFFER_SIZEd chunks
                while ((read = fileStreamIn.Read(bytes, 0, bytes.Length)) != 0)
                {
                    cout.Write(bytes, 0, read);
                    chash.Write(bytes, 0, read);
                    callback(read);
                }
                // flush and close the hashing object
                chash.Flush();
                chash.Close();

                // read the hash
                byte[] hash = hasher.Hash;

                // write the hash to the end of the file
                cout.Write(hash, 0, hash.Length);

                // flush and close the cryptostream
                cout.Flush();
                cout.Close();
                fileStreamIn.Close();
                fileStreamOut.Close();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string ResolveDecryption(FileStream fileStreamIn, FileStream fileStreamOut, SymmetricAlgorithm sma, ProgressCallback callback)
        {
            byte[] bytes = new byte[BUFFER_LENGTH]; // byte buffer
            int read = -1; // the amount of bytes read from the stream       
            long lSize = -1; // the size stored in the input stream

            // create the hashing object, so that we can verify the file
            HashAlgorithm hasher = SHA256.Create();

            // create the cryptostreams that will process the file
            try
            {
                using (CryptoStream cin = new CryptoStream(fileStreamIn, sma.CreateDecryptor(), CryptoStreamMode.Read),
                     chash = new CryptoStream(Stream.Null, hasher, CryptoStreamMode.Write))
                {
                    // read size from file
                    BinaryReader br = new BinaryReader(cin);
                    lSize = br.ReadInt64();
                    ulong tag = br.ReadUInt64();

                    if (FC_TAG != tag)
                        throw new Exception("File Corrupted!");

                    //determine number of reads to process on the file
                    long numReads = lSize / BUFFER_LENGTH;

                    // determine what is left of the file, after numReads
                    long slack = (long)lSize % BUFFER_LENGTH;

                    // read the buffer_sized chunks
                    for (int i = 0; i < numReads; ++i)
                    {
                        read = cin.Read(bytes, 0, bytes.Length);
                        fileStreamOut.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        callback(read);
                    }

                    // now read the slack
                    if (slack > 0)
                    {
                        read = cin.Read(bytes, 0, (int)slack);
                        fileStreamOut.Write(bytes, 0, read);
                        chash.Write(bytes, 0, read);
                        callback(read);
                    }
                    // flush and close the hashing stream
                    chash.Flush();
                    chash.Close();

                    // flush and close the output file
                    fileStreamOut.Flush();
                    fileStreamOut.Close();

                    // read the current hash value
                    byte[] curHash = hasher.Hash;

                    // get and compare the current and old hash values
                    byte[] oldHash = new byte[hasher.HashSize / 8];
                    read = cin.Read(oldHash, 0, oldHash.Length);

                    bool readLengthError = (oldHash.Length != read);
                    if (readLengthError)
                        throw new Exception("File Corrupted!");
                    //var hashesAreEqual = CheckByteArrays(oldHash, curHash);
                    //if (readLengthError || (!hashesAreEqual))
                    //    throw new Exception("File Corrupted!");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region PrivateMethods

        /// <summary>
        /// Checks to see if two byte array are equal
        /// </summary>
        /// <param name="b1">the first byte array</param>
        /// <param name="b2">the second byte array</param>
        /// <returns>true if b1.Length == b2.Length and each byte in b1 is
        /// equal to the corresponding byte in b2</returns>
        private static bool CheckByteArrays(byte[] b1, byte[] b2)
        {
            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                        return false;
                }
                return true;
            }
            return false;
        }
        #endregion Private Methods

    }
}
