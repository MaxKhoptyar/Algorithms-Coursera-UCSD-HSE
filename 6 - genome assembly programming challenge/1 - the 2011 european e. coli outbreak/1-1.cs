using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication25
{
    class Program
    {
        static void Main(string[] args)
        {

            //var random = new Random();
            //var nucleodsArr = new char[] { 'T', 'C', 'A', 'G' };
            //var patternCount = 4;
            //var patternLength = 4;


            //var patternArr = new char[patternCount][];
            //for (int i = 0; i < patternCount; i++)
            //{
            //    patternArr[i] = new char[patternLength];
            //    for (int j = 0; j < patternLength; j++)
            //    {
            //        var next = random.Next(nucleodsArr.Length);
            //        patternArr[i][j] = nucleodsArr[next];
            //    }
            //}

            var patternCount = 1618;
            var patternLength = 100;
            var patternArr = new char[patternCount][];
            for (int i = 0; i < patternCount; i++)
            {
                patternArr[i] = Console.ReadLine().ToCharArray();
            }
            //patternArr = new char[][]
            //{
            //                new[] {'A', 'G', 'T', 'C'}, new[] {'G', 'T', 'C', 'T'},
            //                new[] {'T', 'A', 'G', 'A'} ,new[] {'T', 'C', 'T', 'A'}
            //};

            //            patternArr = new char[][]
            //{
            //                            new[] {'A', 'A', 'C'}, new[] {'A', 'C', 'G'}, new[] {'G', 'A', 'A'},
            //                            new[] {'G', 'T', 'T'}, new[] {'T', 'C', 'G'}
            //};


            var patternArrCode = new int[patternCount][];
            for (int i = 0; i < patternCount; i++)
            {
                patternArrCode[i] = new int[patternLength];
                for (int j = 0; j < patternLength; j++)
                {
                    if (patternArr[i][j] == 'A')
                    {
                        patternArrCode[i][j] = 0;
                        continue;
                    }
                    if (patternArr[i][j] == 'C')
                    {
                        patternArrCode[i][j] = 1;
                        continue;
                    }
                    if (patternArr[i][j] == 'G')
                    {
                        patternArrCode[i][j] = 2;
                        continue;
                    }
                    patternArrCode[i][j] = 3;
                }
            }

            var sl = new SortedList();
            //var d1 = DateTime.Now;
            for (int i = 0; i < patternCount; i++)
            {
                sl.Add(new SortedElement()
                {
                    Chars = patternArr[i],
                    CodeChars = patternArrCode[i],
                    Id = i,
                    PositionInList = i
                });
            }
            sl.UpdateIndexes();
            //EditErrors(sl);
            var resultList = BuildStringBase(sl);
            Console.WriteLine(resultList.ToArray());
            //Console.WriteLine((DateTime.Now - d1).TotalMilliseconds);
            //Console.WriteLine(list.Count);
            //Console.ReadLine();
        }

        class FullList
        {
            public List<int[]> Src;

            public FullList()
            {
                Src = new List<int[]>();
            }

            public void Update(int index, int value)
            {
                var e = Src[index];
                e[value]++;
            }

            public void Add(int value)
            {
                var n = new int[4];
                n[value]++;
                Src.Add(n);
            }
        }

        private static List<char> BuildStringBase(SortedList sl)
        {
            var patternCount = sl.SortedElementArray.Length;
            var patternLength = sl.SortedElementArray[0].Chars.Length;

            var root = new Edge();
            for (int i = 0; i < patternCount; i++)
            {
                CreatePrefix(root, sl.SortedElementArray[i]);
            }
            var allPairsSuffixPrefixResule = AllPairsSuffixPrefix(sl.SortedElementArray, root);

            var list = CreateListWithVoting2(allPairsSuffixPrefixResule, sl, patternCount, patternLength);

            return list;
        }

        private static List<char> CreateListWithoutVoting(AllPairsSuffixPrefixResule allPairsSuffixPrefixResule, SortedList sl, int patternCount, int patternLength)
        {
            var list = new List<char>();
            var overlap = allPairsSuffixPrefixResule.OverlapQueue[patternCount - 1];
            for (int i = 0; i < patternCount; i++)
            {
                var pattern = sl.SortedElementArray[allPairsSuffixPrefixResule.PatternQueue[i]];

                for (int j = overlap; j < patternLength; j++)
                {
                    list.Add(pattern.Chars[j]);
                }
                overlap = allPairsSuffixPrefixResule.OverlapQueue[i];
            }
            return list;
        }
        private static List<char> CreateListWithVoting2(AllPairsSuffixPrefixResule allPairsSuffixPrefixResule, SortedList sl, int patternCount, int patternLength)
        {
            var fullList = new FullList();
            var cursor = 0;
            var overlap = allPairsSuffixPrefixResule.OverlapQueue[patternCount - 1];
            for (int i = 0; i < patternCount; i++)
            {
                cursor = cursor - overlap;
                var pattern = sl.SortedElementArray[allPairsSuffixPrefixResule.PatternQueue[i]];
                for (int j = 0; j < overlap; j++)
                {
                    if (cursor > 0)
                    {
                        fullList.Update(cursor, pattern.CodeChars[j]);
                    }
                    cursor++;
                }
                for (int j = overlap; j < patternLength; j++)
                {
                    fullList.Add(pattern.CodeChars[j]);
                    cursor++;
                }
                overlap = allPairsSuffixPrefixResule.OverlapQueue[i];
            }
            overlap = allPairsSuffixPrefixResule.OverlapQueue[patternCount - 1];
            var localPattern = sl.SortedElementArray[allPairsSuffixPrefixResule.PatternQueue[0]];
            cursor = cursor - overlap;
            for (int i = 0; i < overlap; i++)
            {
                fullList.Update(cursor, localPattern.CodeChars[i]);
                cursor++;
            }
            var list = new List<char>();
            foreach (var e in fullList.Src)
            {
                var m = GetMax(e);
                list.Add(Decode(m));
            }
            return list;
        }
        private static List<char> CreateListWithVoting(AllPairsSuffixPrefixResule allPairsSuffixPrefixResule, SortedList sl, int patternCount, int patternLength)
        {
            var fullList = new FullList();
            var overlap = 0;
            var cursor = 0;
            var queuePattern = allPairsSuffixPrefixResule.PatternQueue;
            var queueOverlap = allPairsSuffixPrefixResule.OverlapQueue;

            for (int i = 0; i < patternCount - 1; i++)
            {
                var pattern = sl.SortedElementArray[queuePattern[i]];
                cursor = cursor - overlap;
                for (int j = 0; j < overlap; j++)
                {
                    fullList.Update(cursor, pattern.CodeChars[j]);
                    cursor++;
                }
                for (int j = overlap; j < patternLength; j++)
                {
                    cursor++;
                    fullList.Add(pattern.CodeChars[j]);
                }
                overlap = queueOverlap[i];
            }
            cursor = cursor - overlap;

            var patternLocal = sl.SortedElementArray[queuePattern[patternCount - 1]];
            for (int i = 0; i < overlap; i++)
            {
                fullList.Update(cursor, patternLocal.CodeChars[i]);
                cursor++;
            }
            var overlapLast = queueOverlap[patternCount - 1];
            for (int i = overlap; i < patternLength - overlapLast; i++)
            {
                fullList.Add(patternLocal.CodeChars[i]);
                cursor++;
            }
            for (int i = 0; i < overlapLast; i++)
            {
                fullList.Update(i, patternLocal.CodeChars[patternLength - overlapLast - 1 + i]);
            }

            var list = new List<char>();
            foreach (var e in fullList.Src)
            {
                var m = GetMax(e);
                list.Add(Decode(m));
            }

            return list;
        }
        class Edge
        {
            public int StartIndex;
            public int EndIndex;

            public int StartString;
            public int LengthString;
            public Edge[] Childs;

            public SortedElement Src;

            public string CurrSuffix
            {
                get { return new string(Src.Chars, StartString, LengthString); }
            }


            public Edge()
            {
                Childs = new Edge[4];
            }

            public void CleanChild()
            {
                for (int i = 0; i < 4; i++)
                {
                    Childs[i] = null;
                }
            }

            public void CopyChilds(Edge[] childs)
            {
                for (int i = 0; i < 4; i++)
                {
                    Childs[i] = childs[i];
                }
            }
        }

        private static void CreatePrefix(Edge root, SortedElement sortedElement)
        {
            var inputArr = sortedElement.Chars;
            var codes = sortedElement.CodeChars;
            var length = inputArr.Length;

            var curentEdge = root;

            for (int j = 0; j < length; j++)
            {
                Edge searchElement = null;
                var cCode = codes[j];
                if (curentEdge.Childs[cCode] != null)
                {
                    searchElement = curentEdge.Childs[cCode];
                }
                if (searchElement == null)
                {
                    var newEdge = new Edge();
                    newEdge.LengthString = length - j;
                    newEdge.StartString = j;
                    newEdge.StartIndex = sortedElement.PositionInList;
                    newEdge.EndIndex = sortedElement.PositionInList;

                    newEdge.Src = sortedElement;

                    curentEdge.Childs[codes[j]] = newEdge;
                    break;
                }
                else
                {
                    var minL = Math.Min(searchElement.LengthString, length - j);
                    var copyL = 1;

                    var charsInEdge = searchElement.Src.Chars;
                    var codesCharsInEdge = searchElement.Src.CodeChars;

                    for (int k = 1; k < minL; k++)
                    {
                        if (charsInEdge[searchElement.StartString + k] == inputArr[j + k])
                        {
                            copyL++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (copyL < searchElement.LengthString)
                    {
                        var newEdge = new Edge();
                        newEdge.StartString = searchElement.StartString + copyL;
                        newEdge.LengthString = searchElement.LengthString - copyL;
                        newEdge.CopyChilds(searchElement.Childs);
                        newEdge.EndIndex = searchElement.EndIndex;
                        newEdge.StartIndex = searchElement.StartIndex;

                        newEdge.Src = searchElement.Src;


                        searchElement.CleanChild();
                        searchElement.Childs[codesCharsInEdge[newEdge.StartString]] = newEdge;
                        searchElement.LengthString = copyL;

                    }
                    searchElement.EndIndex = sortedElement.PositionInList;
                    j = j + copyL - 1;
                    curentEdge = searchElement;
                }
            }

        }

        class SortedElement
        {
            public int[] CodeChars;
            public char[] Chars;
            public int Id;
            public int PositionInList;
        }

        class SortedList
        {
            public List<SortedElement> SortedElementsList = new List<SortedElement>();

            public SortedElement[] SortedElementArray;

            private BWTComparer BwtComparer = new BWTComparer();

            public void Add(SortedElement s)
            {
                if (SortedElementsList.Count == 0)
                {
                    SortedElementsList.Add(s);
                }
                else
                {
                    var start = 0;
                    var end = SortedElementsList.Count;
                    while (end > start)
                    {
                        var n = start + ((end - start) / 2);
                        if (BwtComparer.Compare(s.Chars, SortedElementsList[n].Chars) == 1)
                        {
                            end = n;
                        }
                        else
                        {
                            start = n + 1;
                        }
                    }
                    SortedElementsList.Insert(start, s);
                }
            }

            public void Remove(SortedElement s)
            {
                SortedElementsList.RemoveAt(s.PositionInList);
                UpdateIndexes();
            }

            public void Update(SortedElement s)
            {
                SortedElementsList.RemoveAt(s.PositionInList);
                Add(s);
                UpdateIndexes();
            }

            public void UpdateIndexes()
            {
                var c = 0;
                foreach (var e in SortedElementsList)
                {
                    e.PositionInList = c;
                    c++;
                }
                SortedElementArray = SortedElementsList.ToArray();
            }
        }

        class BWTComparer : IComparer<char[]>
        {
            public int Compare(char[] x, char[] y)
            {
                var x_arr = x;
                var x_arr_l = x_arr.Length;
                var y_arr = y;
                var y_arr_l = y_arr.Length;
                var min = Math.Min(x_arr_l, y_arr_l);

                for (int i = 0; i < min; i++)
                {
                    if (x_arr[i] == '$' && y_arr[i] == '$')
                    {
                        continue;
                    }
                    if (x_arr[i] == '$')
                    {
                        return 1;
                    }
                    if (y_arr[i] == '$')
                    {
                        return -1;
                    }
                    if (x_arr[i] > y_arr[i])
                    {
                        return -1;
                    }
                    if (x_arr[i] < y_arr[i])
                    {
                        return 1;
                    }
                }
                if (y_arr_l == x_arr_l)
                {
                    return 0;
                }
                if (y_arr_l == min)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        }

        private static void EditErrors(SortedList sl)
        {
            var patternCount = sl.SortedElementArray.Length;
            var patternLength = sl.SortedElementArray[0].Chars.Length;

            //var overlapMatrix = new int[patternCount, patternCount];
            var queue = new Queue<SortedElement>();
            var overlapVector = new int[patternCount];
            var kmerMax = 3;

            foreach (var e in sl.SortedElementsList)
            {
                queue.Enqueue(e);
            }

            for (int i = 0; i < patternCount; i++)
            {
                var root = new Edge();
                for (int j = 0; j < patternCount; j++)
                {
                    CreatePrefix(root, sl.SortedElementArray[j]);
                }
                var currentElement = queue.Dequeue();
                var kmer = 0;

                for (int j = 0; j < patternLength; j++)
                {
                    var sufStart = j;
                    var sufLength = patternLength - j;
                    var missmatch = 0;
                    var misslimit = (int)(sufLength * 0.05);
                    //var misslimit = 1;
                    FindAllPairs(currentElement, sufStart, sufLength, 0, 0, root, missmatch, misslimit, overlapVector, currentElement.PositionInList, ref kmer);
                    if (kmer >= kmerMax)
                    {
                        break;
                    }
                }
                EditErrorsInSortedElement(overlapVector, sl.SortedElementArray, currentElement);
                sl.Update(currentElement);

                for (int j = 0; j < patternLength; j++)
                {
                    overlapVector[j] = 0;
                }
            }
        }

        private static void EditErrorsInSortedElement(int[] overlapVector, SortedElement[] sortedElements,
            SortedElement currentElement)
        {
            var length = currentElement.Chars.Length;
            var sortderList = Sort(overlapVector);

            var minIndex = sortderList[0];
            var minValue = overlapVector[minIndex];

            for (int j = 0; j < minValue; j++)
            {
                if (currentElement.Chars[length - 1 - j] != sortedElements[minIndex].Chars[minValue - 1 - j])
                {
                    currentElement.Chars[length - 1 - j] = sortedElements[minIndex].Chars[minValue - 1 - j];
                    currentElement.CodeChars[length - 1 - j] = sortedElements[minIndex].CodeChars[minValue - 1 - j];
                    break;
                }
            }
        }

        private static int GetMax(int[] ints)
        {
            var maxIndex = -1;
            var maxValues = -1;
            for (int i = 0; i < ints.Length; i++)
            {
                if (ints[i] > maxValues)
                {
                    maxIndex = i;
                    maxValues = ints[i];
                }
            }
            return maxIndex;
        }

        private static void Code(int[] p0, char c0)
        {
            if (c0 == 'A')
            {
                p0[0] = p0[0] + 1;
                return;
            }
            if (c0 == 'C')
            {
                p0[1] = p0[1] + 1;
                return;
            }
            if (c0 == 'G')
            {
                p0[2] = p0[2] + 1;
                return;
            }
            p0[3] = p0[3] + 1;
        }
        private static char Decode(int c0)
        {
            if (c0 == 0)
            {
                return 'A';
            }
            if (c0 == 1)
            {
                return 'C';
            }
            if (c0 == 2)
            {
                return 'G';
            }
            return 'T';
        }
        private static int[] Sort(int[] overlapVector)
        {
            var length = overlapVector.Length;
            var returnArr = new int[length];
            var retLists = new List<int>[length + 1];
            for (int j = 0; j < length + 1; j++)
            {
                retLists[j] = new List<int>();
            }
            for (int j = 0; j < length; j++)
            {
                retLists[overlapVector[j]].Add(j);
            }
            var c = length;
            for (int j = 0; j < length + 1; j++)
            {
                foreach (var e in retLists[j])
                {
                    c--;
                    returnArr[c] = e;
                }
            }
            return returnArr;
        }

        private static int[] Sort(int[,] overlapMatrix, int i)
        {
            var length = overlapMatrix.GetLength(0);
            var returnArr = new int[length];
            var retLists = new List<int>[length + 1];
            for (int j = 0; j < length + 1; j++)
            {
                retLists[j] = new List<int>();
            }
            for (int j = 0; j < length; j++)
            {
                retLists[overlapMatrix[i, j]].Add(j);
            }
            var c = length;
            for (int j = 0; j < length + 1; j++)
            {
                foreach (var e in retLists[j])
                {
                    c--;
                    returnArr[c] = e;
                }
            }
            return returnArr;
        }

        private static void FindAllPairs(SortedElement src, int sufStart, int sufLength, int v, int localPosition, Edge currentNode,
            int missmatch, int misslimit, int[] overlapVector, int indexOverlap, ref int kmer)
        {
            while (true)
            {
                if (sufLength == v)
                {
                    for (int i = currentNode.StartIndex; i < currentNode.EndIndex + 1; i++)
                    {
                        if (overlapVector[i] < v)
                        {
                            if (i != indexOverlap)
                            {
                                overlapVector[i] = v;
                                kmer++;
                            }
                        }
                    }
                    return;
                }

                if (localPosition <= currentNode.LengthString - 1)
                {
                    if (src.Chars[v + sufStart] != currentNode.Src.Chars[currentNode.StartString + localPosition])
                    {
                        missmatch++;
                    }

                    if (missmatch > misslimit)
                    {
                        return;
                    }

                    localPosition++;
                    v++;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var mistemp = missmatch;
                        if (src.CodeChars[sufStart + v] != j)
                        {
                            mistemp++;
                        }
                        if (currentNode.Childs[j] != null && mistemp <= misslimit)
                        {
                            var v1 = v + 1;
                            FindAllPairs(src, sufStart, sufLength, v1, 1, currentNode.Childs[j], mistemp, misslimit, overlapVector,
                                indexOverlap, ref kmer);
                        }
                    }
                    return;
                }
            }
        }

        private static void FindAllPairs(SortedElement src, int sufStart, int sufLength, int v, int localPosition, Edge currentNode, int missmatch, int misslimit,
            int[,] overlapMatrix, int indexOverlap, ref int kmer, bool[] addedPatterns)
        {
            while (true)
            {
                if (sufLength == v)
                {
                    for (int i = currentNode.StartIndex; i < currentNode.EndIndex + 1; i++)
                    {
                        if (overlapMatrix[indexOverlap, i] < v)
                        {
                            if (i != indexOverlap && !addedPatterns[i])
                            {
                                overlapMatrix[indexOverlap, i] = v;
                                kmer++;
                            }
                        }
                    }
                    return;
                }

                if (localPosition <= currentNode.LengthString - 1)
                {
                    if (src.Chars[v + sufStart] != currentNode.Src.Chars[currentNode.StartString + localPosition])
                    {
                        missmatch++;
                    }

                    if (missmatch > misslimit)
                    {
                        return;
                    }

                    localPosition++;
                    v++;
                }
                else
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var mistemp = missmatch;
                        if (src.CodeChars[sufStart + v] != j)
                        {
                            mistemp++;
                        }
                        if (currentNode.Childs[j] != null && mistemp <= misslimit)
                        {
                            var v1 = v + 1;
                            FindAllPairs(src, sufStart, sufLength, v1, 1, currentNode.Childs[j], mistemp, misslimit, overlapMatrix,
                                indexOverlap, ref kmer, addedPatterns);
                        }
                    }
                    return;
                }
            }
        }

        public class AllPairsSuffixPrefixResule
        {
            public int[,] OverlapMatrix;
            public int[] PatternQueue;
            public int[] OverlapQueue;
        }
        private static AllPairsSuffixPrefixResule AllPairsSuffixPrefix(SortedElement[] sortedElements, Edge root)
        {

            var patternsCount = sortedElements.Length;
            var overlapMatrix = new int[patternsCount, patternsCount];


            var addedPatterns = new bool[sortedElements.Length];
            var startIndex = 0;
            var currentIndex = startIndex;
            addedPatterns[startIndex] = true;

            var patternQueue = new int[patternsCount];
            var overlapQueue = new int[patternsCount];
            var kmer = 0;
            var kmerMax = 1;
            var localValue = -1;
            var localIndex = -1;

            for (int i = 0; i < sortedElements.Length - 1; i++)
            {
                patternQueue[i] = currentIndex;
                kmer = 0;
                for (int j = 0; j < sortedElements[0].Chars.Length; j++)
                {
                    var sufStart = j;
                    var sufLength = sortedElements[0].Chars.Length - j;
                    //var misslimit = (int)(sufLength * 0.05);

                    FindAllPairs(sortedElements[currentIndex], sufStart, sufLength, 0, 0, root, 0, 0, overlapMatrix, currentIndex, ref kmer, addedPatterns);
                    if (kmer >= kmerMax)
                    {
                        break;
                    }
                }

                localValue = -1;
                localIndex = -1;
                for (int j = 0; j < patternsCount; j++)
                {
                    if (overlapMatrix[currentIndex, j] > localValue && !addedPatterns[j])
                    {
                        localIndex = j;
                        localValue = overlapMatrix[currentIndex, j];
                    }
                }
                overlapQueue[i] = localValue;
                currentIndex = localIndex;
                addedPatterns[currentIndex] = true;
            }

            addedPatterns[startIndex] = false;
            localValue = -1;
            kmer = 0;
            for (int j = 0; j < sortedElements[0].Chars.Length; j++)
            {
                var sufStart = j;
                var sufLength = sortedElements[0].Chars.Length - j;
                //var misslimit = (int)(sufLength * 0.05);
                FindAllPairs(sortedElements[currentIndex], sufStart, sufLength, 0, 0, root, 0, 0, overlapMatrix, currentIndex, ref kmer, addedPatterns);
                if (kmer >= kmerMax)
                {
                    break;
                }
            }

            for (int j = 0; j < patternsCount; j++)
            {
                if (overlapMatrix[currentIndex, j] > localValue && !addedPatterns[j])
                {
                    localValue = overlapMatrix[currentIndex, j];
                }
            }
            patternQueue[patternsCount - 1] = currentIndex;
            overlapQueue[patternsCount - 1] = localValue;
            overlapMatrix[currentIndex, startIndex] = localValue;
            return new AllPairsSuffixPrefixResule() { OverlapMatrix = overlapMatrix, OverlapQueue = overlapQueue, PatternQueue = patternQueue };
        }

    }
}