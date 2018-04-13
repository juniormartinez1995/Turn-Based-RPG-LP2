using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGlib.Quest
{
    abstract public class Quest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string objective { get; set; }

        abstract public void StartQuest();

        abstract public void EndQuest();
    }

    
}
