using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EncryptionAlgorithms;
using System.IO;

namespace DigitSignProject
{
    public class DigitSignature
    {
        private  string MESSAGE_HASH_FILE_NAME = "./MessageHash.txt";
        public  string ORIGIN_DIGIT_SIGNATURE_FILE_NAME = "./DSign.txt";
        private  string CHECK_DIGIT_SIGNATURE_FILE_NAME = "./CheckDSign.txt";
        public EncryptionAlgorithms.RSA rsa { get; set; }
        public DigitSignature()
        {
            rsa = new EncryptionAlgorithms.RSA();
            rsa.GenerateKeys();
        }
        public void GenerateSignature(string filePath)
        {
            byte[] message = null;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                message = new byte[file.Length];
                file.Read(message, 0, (int)file.Length);
                file.Close();
            }            
            byte[] messageHash = ComuteHash(message);
            using (FileStream file = new FileStream(MESSAGE_HASH_FILE_NAME, FileMode.Create, FileAccess.Write))
            {
                file.Write(messageHash, 0, messageHash.Length);
                file.Close();
            }
            rsa.Encrypt(MESSAGE_HASH_FILE_NAME, ORIGIN_DIGIT_SIGNATURE_FILE_NAME, rsa.PRIVATE_KEY_FILE_NAME);
        }
        public bool Check(string filePath, string signPath)
        {
            byte[] message = null;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                message = new byte[file.Length];
                file.Read(message, 0, (int)file.Length);
                file.Close();
            }
            byte[] messageHash = ComuteHash(message);
            rsa.Decrypt(signPath, CHECK_DIGIT_SIGNATURE_FILE_NAME, rsa.PUBLIC_KEY_FILE_NAME);

            byte[] digitSignature = null;
            using (FileStream file = new FileStream(CHECK_DIGIT_SIGNATURE_FILE_NAME, FileMode.Open, FileAccess.Read))
            {
                digitSignature = new byte[file.Length];
                file.Read(digitSignature, 0, (int)file.Length);
                file.Close();
            }
            return CompareByteArrays(messageHash, digitSignature);
        }
        private bool CompareByteArrays(byte[] firstArray, byte[] secondArray)
        {
            if (firstArray.Length != secondArray.Length)
                return false;
            for (int i = 0; i < firstArray.Length; i++)
                if (firstArray[i] != secondArray[i])
                    return false;
            return true;
        }
        private byte[] ComuteHash(byte[] message)
        {
            MD5 md5Hash = MD5.Create();
            byte[] messageHash = md5Hash.ComputeHash(message);

            return messageHash;
        }
    }
}
