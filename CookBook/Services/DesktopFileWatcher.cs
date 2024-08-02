using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public class DesktopFileWatcher
    {
        private static DesktopFileWatcher _instance;
        public static DesktopFileWatcher Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new DesktopFileWatcher();
                }
                return _instance;
            }
        }
        private DesktopFileWatcher() { }
    }
}
