using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace NestedAndChildTasks
{
    class Program
    {
        static void InnerMethod()
        {
            WriteLine("Método interior iniciando...");
            Thread.Sleep(2000);
            WriteLine("Método interior finalizado.");
        }
        
        static void OuterMethod()
        {
            WriteLine("Método exterior iniciando...");
            var inner = Task.Factory.StartNew(InnerMethod, TaskCreationOptions.AttachedToParent);
            WriteLine("Método exterior finalizado.");
        }
        
        static void Main(string[] args)
        {
            var outer = Task.Factory.StartNew(OuterMethod);
            outer.Wait();
            WriteLine("A aplicação está encerrando.");
        }
    }
}
