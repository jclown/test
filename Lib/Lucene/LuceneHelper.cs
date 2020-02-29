//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Lib.Lucene
//{
//    public class LuceneHelper
//    {
//        public static List<string> GetSegmenterKeyWords(string keyWords)
//        {
//            List<string> kwList = new List<string>();
//            if (!string.IsNullOrWhiteSpace(keyWords))
//            {
//                kwList.AddRange(keyWords.ToSplitString(" "));
//            }
//            kwList.AddRange(Luosi.Framework.Lucene.JieBa.JiebaSegmenterKeyWords.GetSegmenterKeyWords(keyWords));
//            for (var i = kwList.Count-1; i > 0; i--)
//            {
//                if (keyWords.IndexOf(kwList[i]) < 0) kwList.RemoveAt(i);
//            }
//            kwList = kwList.Where(x => x.Length > 0).Distinct().ToList();
//            return kwList;
//        }

//    }
//}
