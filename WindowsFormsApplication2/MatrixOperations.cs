using System;
using System.Collections.Generic;

namespace MatrixCalculator
{
    public class MatrixOperationis
    {
        public static Dictionary<int, Operation[]> Priorities { get; }
        public string SrcStr { get; }

        public class Operation
        {
            public string Name { get; set; }
            public bool NeedLeftOperand { get; set; }
            public bool NeedRightOperand { get; set; }
            public Operation(string name,bool nl,bool nr)
            {
                Name = name;
                NeedLeftOperand = nl;
                NeedRightOperand = nr;
            }
        }
        static MatrixOperationis()
        {
            Priorities = new Dictionary<int, Operation[]>
            {
                {0, new[] {
                        new Operation ("gauss",false,true),
                        new Operation ("getroots",false,true),
                        new Operation ("getmatrix",false,true),
                        new Operation ("getconsts",false,true),
                        new Operation ("getinvbymod",true,true),
                        new Operation("det", false, true),
                        new Operation("T", false, true),
                        new Operation("mod",false, true),
                        new Operation("scal",true, true),
                    }
                },
                {1, new[] {new Operation("^", true, true)}},
                {2, new[] {new Operation("*", true, true), new Operation("/", true, true)}},
                {3, new[] {new Operation("+", true, true), new Operation("-", true, true)}}
            };
        }

        public MatrixOperationis(string sourceStr)
        {
            SrcStr = sourceStr;
        }

        private int _curIndex;
        public enum FindMethod
        {
            IndexOf,
            LastIndexOf
        }

        public static bool IsOperation(string src)
        {
            for (int i = 0; i < Priorities.Keys.Count; i++)
            {
                var priority = Priorities[i];
                foreach (var operation in priority)
                {
                    if (string.Equals(src, operation.Name, StringComparison.Ordinal))
                        return true;
                }
            }
            return false;
        }

        public static int FindAnyLastIndexOf(string src, int startIndex)
        {
            int resultIndex = -1;
            try
            {
               
                for (int i = startIndex; i >= 0; i--)
                {
                    if (IsOperation(src[i].ToString()) || src[i] == '(')
                    {
                        return i;
                    }

                    if (src[i] == ')')
                    {
                        int rCount = 1;
                        int lCount = 0;
                        for (int j = i-1; j >=0 && rCount != lCount ; j--)
                        {
                            if (src[j] == ')')
                            {
                                rCount++;
                            }
                            else if (src[j] == '(')
                            {
                                resultIndex = j;
                                lCount++;
                            }
                        }
                        if (lCount != rCount)
                        {
                            throw new ArgumentException("Не совпадает количество открывающих и закрывающих скобок");
                        }
                        return resultIndex - 1;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {   
            }

            return resultIndex;
        }

        public static int FindAnyIndexOf(string src, int startIndex)
        {
            int resultIndex = -1;
            try
            {

                for (int i = startIndex; i < src.Length; i++)
                {
                    if (IsOperation(src[i].ToString()) || src[i] == ')')
                    {
                        return i;
                    }

                    if (src[i] == '(')
                    {
                        int rCount = 0;
                        int lCount = 1;
                        for (int j = i+1; j <src.Length && rCount != lCount; j++)
                        {
                            if (src[j] == '(')
                            {
                                lCount++;
                            }
                            else if (src[j] == ')')
                            {
                                resultIndex = j;
                                rCount++;
                            }
                        }
                        if (lCount != rCount)
                        {
                            throw new ArgumentException("Не совпадает количество открывающих и закрывающих скобок");
                        }
                            
                        return resultIndex + 1;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            return resultIndex;
        }

        public KeyValuePair<Operation, int>? GetNext()
        {
            for (int i = 0; i < Priorities.Keys.Count; i++)
            {
                var priority = Priorities[i];
                foreach (var operation in priority)
                {
                    var index = SrcStr.IndexOf(operation.Name, _curIndex, StringComparison.Ordinal);
                    if (index != -1)
                    {
                        _curIndex = index+1;
                        return new KeyValuePair<Operation, int>(operation, index);
                    }
                }
            }

            return null;
        }
    }
}
