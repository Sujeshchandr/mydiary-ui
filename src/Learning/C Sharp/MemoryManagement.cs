using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.C_Sharp
{
    public class MemoryManagement
    {

        public MemoryManagement()
        {
        }

        public void Start()
        {
            ///A value type stores its contents in memory allocated on the stack.

            int x = 42; //// in this case the value 42 is stored in an area of memory called the stack
            //// When the variable x goes out of scope because the method in which it was defined has finished executing, the value is discarded from the stack.
            //// Using the stack is efficient, but the limited lifetime of value types makes them less suited for sharing data between different classes.

            string a = "test";
            object o = a; //no boxing

            object dd = 10;

            double db = (double)dd;

            int di = (int)dd;

            int b = 10;
            object c = b; //boxing

            int d = 11;
            object e = b.ToString();//no boxing

            Console.Write(d.GetType());
            Console.Write(e.GetType());

            int f = 11;
            object g = (object)f;//boxing

            Console.Write(f.GetType());
            Console.Write(g.GetType());

            Queue<int> a1 = new Queue<int>();
            a1.Enqueue(10);
            
            System.Collections.ArrayList list = new System.Collections.ArrayList(); // list is a reference type                                                    
            int n = 67;          // n is a value type 
            list.Add(n);         // n is boxed 
            n = (int)list[0];   // list[0] is unboxed

            try
            {

                list.Add("sss");
                list.Add("sss");
                list.Add(1);
                list.Add(1.222);
                list.Add(new object());

            }
            catch (Exception ex)
            {
                ex.Data.Add("ArticleId", 100); ////not good way of assigning !!!!!!

                ex.Data["ArticleId"] = 100; ////good way

                throw;
            }

            IList<int> intList = new List<int>();

            intList.Add(1);
            intList.Add(1);
            intList.Add(2);

            Hashtable h = new Hashtable();
            

        }
    }
}
