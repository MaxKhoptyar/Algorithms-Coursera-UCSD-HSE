using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructureToolbox
{
    class Program
    {
        class O
        {
            public char type;
            public int index;
        }


        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var inputArr = input.ToCharArray();
            var stack = new Stack<O>();

            var success = true;
            var index = 0;
            for (int i = 0; i < inputArr.Length; i++)
            {
                if (inputArr[i] == '[')
                {
                    var o = new O();
                    o.type = '[';
                    o.index = i + 1;
                    stack.Push(o);
                    continue;
                }
                if (inputArr[i] == '{')
                {
                    var o = new O();
                    o.type = '{';
                    o.index = i + 1;
                    stack.Push(o);
                    continue;
                }
                if (inputArr[i] == '(')
                {
                    var o = new O();
                    o.type = '(';
                    o.index = i + 1;
                    stack.Push(o);
                    continue;
                }

                if (inputArr[i] == ']')
                {
                    if (stack.Count > 0)
                    {
                        var o = stack.Pop();
                        if (o.type != '[')
                        {
                            var o2 = new O();
                            o2.type = inputArr[i];
                            o2.index = i + 1;
                            stack.Push(o2);
                            break;
                        }
                    }
                    else
                    {
                        var o2 = new O();
                        o2.type = inputArr[i];
                        o2.index = i + 1;
                        stack.Push(o2);
                        break;
                    }

                    continue;
                }
                if (inputArr[i] == '}')
                {
                    if (stack.Count > 0)
                    {
                        var o = stack.Pop();
                        if (o.type != '{')
                        {
                            var o2 = new O();
                            o2.type = inputArr[i];
                            o2.index = i + 1;
                            stack.Push(o2);
                            break;
                        }
                    }
                    else
                    {
                        var o2 = new O();
                        o2.type = inputArr[i];
                        o2.index = i + 1;
                        stack.Push(o2);
                        break;
                    }
                    continue;
                }
                if (inputArr[i] == ')')
                {
                    if (stack.Count > 0)
                    {
                        var o = stack.Pop();
                        if (o.type != '(')
                        {
                            var o2 = new O();
                            o2.type = inputArr[i];
                            o2.index = i + 1;
                            stack.Push(o2);
                            break;
                        }
                    }
                    else
                    {
                        var o2 = new O();
                        o2.type = inputArr[i];
                        o2.index = i + 1;
                        stack.Push(o2);
                        break;
                    }
                    continue;
                }
            }
            if (stack.Count > 0)
            {
                var o = stack.Pop();
                success = false;
                index = o.index;
            }
            if (success)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine(index);
            }
        }
    }
}
