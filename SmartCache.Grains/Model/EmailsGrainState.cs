using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCache.Grains.Model
{
    public class EmailsGrainState
    {
        public EmailsGrainState()
        {
            BreachedEmails = new HashSet<string>();
        }
        public HashSet<string> BreachedEmails { get; set; }
    }
}
