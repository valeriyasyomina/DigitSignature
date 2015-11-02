using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using EncryptionAlgorithmsLib;

namespace DigitSignProject
{
    public class DigitSignature
    {
        private string MESSAGE_HASH_FILE_NAME = "./MessageHash.txt";                // Файл для хранения кэша сообщения
        public string ORIGIN_DIGIT_SIGNATURE_FILE_NAME = "./DSign.txt";             // Файл для хранения ЭЦП
        private string CHECK_DIGIT_SIGNATURE_FILE_NAME = "./CheckDSign.txt";        // Промежуточный файл для хранения проверяемой ЭЦП
        public EncryptionAlgorithmsLib.RSA rsa { get; set; }
        public IFileReader fileReader { get; set; }
        public IFileWriter fileWriter { get; set; }
        public MD5 md5Hash { get; set; }
        public DigitSignature()
        {
            rsa = new EncryptionAlgorithmsLib.RSA();
            fileReader = new FileReader();
            fileWriter = new FileWriter();
            md5Hash = MD5.Create();
            rsa.GenerateKeys();
        }
        /// <summary>
        /// Генерирует цифровую подпись для документа и записывает ее в файл ORIGIN_DIGIT_SIGNATURE_FILE_NAME
        /// </summary>
        /// <param name="filePath">Путь к документу, для кот. генерируется ЭЦП</param>
        public void GenerateSignature(string filePath)
        {
            byte[] message = fileReader.Read(filePath);
            byte[] messageHash = md5Hash.ComputeHash(message);
            fileWriter.Write(MESSAGE_HASH_FILE_NAME, messageHash);      
            rsa.Encrypt(MESSAGE_HASH_FILE_NAME, ORIGIN_DIGIT_SIGNATURE_FILE_NAME, rsa.PRIVATE_KEY_FILE_NAME);
        }
        /// <summary>
        /// Проверяет корректиность ЭЦП документа
        /// </summary>
        /// <param name="filePath">Путь к документу</param>
        /// <param name="signPath">Путь к файлу с ЭЦП</param>
        /// <returns>1 - ЭЦП верна, 0 - нет</returns>
        public bool Check(string filePath, string signPath)
        {
            byte[] message = fileReader.Read(filePath);
            byte[] messageHash = md5Hash.ComputeHash(message);
            rsa.Decrypt(signPath, CHECK_DIGIT_SIGNATURE_FILE_NAME, rsa.PUBLIC_KEY_FILE_NAME);
            byte[] digitSignature = fileReader.Read(CHECK_DIGIT_SIGNATURE_FILE_NAME);
            return Comparator.CompareByteArrays(messageHash, digitSignature);
        }   
    }
}
