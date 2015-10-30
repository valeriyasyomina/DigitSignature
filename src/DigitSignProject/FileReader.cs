using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSignProject
{
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Считывает из файла массив байт
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Считанный массив байт</returns>
        public byte[] Read(string fileName)
        {
            byte[] data = null;
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                data = new byte[file.Length];
                file.Read(data, 0, (int)file.Length);
                file.Close();
            }
            return data;
        }
    }
}
