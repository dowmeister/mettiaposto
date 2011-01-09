using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FixMi.Framework.Places
{
    public class Place
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual bool Status { get; set; }
    }
}
