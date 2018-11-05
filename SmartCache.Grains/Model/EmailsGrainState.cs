using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCache.Grains.Model
{
    /// <summary>
    /// Holds grain state (emails)
    /// </summary>
    public class EmailsGrainState
    {
        public EmailsGrainState()
        {
            BreachedEmails = new HashSet<string>();
        }

        /// <summary>
        /// List of breached emails
        /// </summary>
        public HashSet<string> BreachedEmails { get; set; }
    }
}
