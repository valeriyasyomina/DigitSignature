using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitSignProject
{
    public interface IFileWriter
    {
        void Write(string fileName, byte[] data);
    }
}
