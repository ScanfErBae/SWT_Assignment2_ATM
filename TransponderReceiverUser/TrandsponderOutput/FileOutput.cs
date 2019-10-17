using System;
using System.Collections.Generic;
using System.Text;

namespace TransponderOutput
{
    public class FileOutput
    {
        public FileOutput()
        {

        }

        public void Print(string path, string contents)
        {
            System.IO.File.AppendAllText(path, contents);
        }
    }
}
