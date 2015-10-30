using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSignProject
{
    public class FileWriter : IFileWriter
    {
        /// <summary>
        /// Записывает в файл массив байт
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="data">Массив байт для записи</param>
        public void Write(string fileName, byte[] data)
        {
            using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                file.Write(data, 0, data.Length);
                file.Close();
            }
        }
    }
}
