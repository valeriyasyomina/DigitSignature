using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSignProject
{
    public class Comparator
    {
        /// <summary>
        /// Сравнивает 2 байтовых массива поэлементно
        /// </summary>
        /// <param name="firstArray">Первый массив</param>
        /// <param name="secondArray">Второй массив</param>
        /// <returns>1 - массивы одинаковы, 0 - иначе</returns>
        static public bool CompareByteArrays(byte[] firstArray, byte[] secondArray)
        {
            if (firstArray.Length != secondArray.Length)
                return false;
            for (int i = 0; i < firstArray.Length; i++)
                if (firstArray[i] != secondArray[i])
                    return false;
            return true;
        }
    }
}
