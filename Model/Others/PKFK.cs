using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RelationKey
    {
        public string Type { get; set; }
        public int PrimaryKey { get; set; }
        public int ForeignKey { get; set; }
    }
}
