using DataAccessLayer.Utils.Interfaces;
using ServiceLayer.Enumerations;
using ServiceLayer.Messages;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.IOFiles
{
    public class FileService : IFileService
    {
        private string? ENCRYPTED;
        private string? DECRYPTED;

        IDirectoryProvider _directoryProvider;

        public FileService(IDirectoryProvider directoryProvider)
        {
            _directoryProvider = directoryProvider;
            ENCRYPTED = Enum.GetName(typeof(CipherState), CipherState.Encrypted);
            DECRYPTED = Enum.GetName(typeof(CipherState), CipherState.Decrypted);
        }

        //Test
        public List<string> GetAllFilesInDirectory(string directory)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string file in _directoryProvider.GetFilesFromDirectory(directory))
                {
                    files.Add(file);
                }
                foreach (string dir in _directoryProvider.GetAllDirectoriesFromDirectory(directory))
                {
                    files.AddRange(GetAllFilesInDirectory(dir));
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                List<string> errors = new List<string>() { message };
                return errors;
            }

            return files;
        }

        //Test
        public string CreateCipherFileName(string originalDirectory, string fullPathOriginalFile, CipherState cipherState)
        {
            string outputDirectory = CreateCipherFolderName(originalDirectory, cipherState);
            string newPathFile = fullPathOriginalFile.Replace(originalDirectory, outputDirectory);
            string cleanNewPathFile = RemoveCipherSuffix(newPathFile);

            string newPathCipherFile = cleanNewPathFile + Notification.UNDERSCORE_CHARACTER + Enum.GetName(typeof(CipherState), cipherState);
            _directoryProvider.ResolveDirectoryIfNoExists(newPathCipherFile);
            return newPathCipherFile;
        }

        //Test
        public string RemoveCipherSuffix(string fullPathFileName)
        {
            if (String.IsNullOrEmpty(fullPathFileName))
            {
                throw new ArgumentException("Input path is invalid", "fullPathFileName");
            }

            int initialEncryptionPathIndex = (fullPathFileName.Length - ENCRYPTED.Length) > 0 ? (fullPathFileName.Length - ENCRYPTED.Length) : 0;
            int initialDecryptionPathIndex = (fullPathFileName.Length - DECRYPTED.Length) > 0 ? (fullPathFileName.Length - DECRYPTED.Length) : 0;
            int safeEncryptedPathLength = (fullPathFileName.Length - ENCRYPTED.Length) > 0 ? ENCRYPTED.Length : fullPathFileName.Length;
            int safeDecryptedPathLength = (fullPathFileName.Length - DECRYPTED.Length) > 0 ? DECRYPTED.Length : fullPathFileName.Length;

            string expectedEncryptedWord = fullPathFileName.Substring(initialEncryptionPathIndex, safeEncryptedPathLength);
            string expectedDecryptedWord = fullPathFileName.Substring(initialDecryptionPathIndex, safeDecryptedPathLength);

            if (expectedEncryptedWord == ENCRYPTED || expectedDecryptedWord == DECRYPTED)
            {
                int index = fullPathFileName.LastIndexOf('_');

                if (index != -1)
                {
                    string result = fullPathFileName.Substring(0, index);
                    return result;
                }
            }
            return fullPathFileName;
        }

        //Test
        public string CreateCipherFolderName(string originalDirectory, CipherState cipherState)
        {
            if (string.IsNullOrEmpty(originalDirectory))
            {
                throw new ArgumentException("You inserted and invalid Directory path", "originalDirectory");
            }
            string cleanOriginalDirectory = RemoveCipherSuffix(originalDirectory);
            string outputDirectory = cleanOriginalDirectory + Notification.UNDERSCORE_CHARACTER + Enum.GetName(typeof(CipherState), cipherState);
            bool exists = _directoryProvider.DirectoryExists(outputDirectory);
            if (!exists)
            {
                var directoryInfo = _directoryProvider.CreateDirectory(outputDirectory);

            }
            return outputDirectory;
        }
    }
}
