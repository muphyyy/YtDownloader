using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YtDownloader.Data
{
    public class Client
    {
        public bool hasPath { get; set; }
        public string path { get; set; } = null;

        public Client()
        {
            CheckIfHasPath();
        }

        public void CheckIfHasPath()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + @"\downloadfolder.txt"))
            {
                hasPath = true;
                path = File.ReadLines(Directory.GetCurrentDirectory() + @"\downloadfolder.txt").First() + @"\";
            }
            else
            {
                hasPath = false;
            }
        }
    }
}
