using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPGSync
{
    public class ProgressUpdater
    {

        public int ToBeUpdated { get; set; }
        public int ToBeCreated { get; set; }
        public int ToBeDeleted { get; set; }
        public string CurrentTask { get; set; }
        public string Status{ get; set; }
        public int VPContacts { get; set; }
        public int GoogleContacts { get; set; }
        public bool IsRunning { get; set; }

        public bool HasError { get; set; }
        public string Error { get; set; }
        public string ErrorTech { get; set; }

    }
}
