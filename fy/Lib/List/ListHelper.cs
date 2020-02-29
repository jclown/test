using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.List
{
    public class ListHelper
    {

        public static List<int> Intersect(List<int> listA, List<int> listB)
        {
            if (listA == null || listA.Count == 0) return listB;
            if (listB == null || listB.Count == 0) return listA;
            return listA.Intersect(listB).ToList();
        }
    }
}
