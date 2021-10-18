using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace WorkingWithTasks
{
    class Program
    {
        static void MethodA()
        {
            WriteLine("Começando Mètodo A...");
            Thread.Sleep(3000); 
            WriteLine("Método A finalizado.");
        }

        static void MethodB()
        {
            WriteLine("Começando Mètodo B...");
            Thread.Sleep(2000); 
            WriteLine("Método B finalizado.");
        }

        static void MethodC()
        {
            WriteLine("Começando Mètodo C...");
            Thread.Sleep(1000); 
            WriteLine("Método C finalizado.");
        }
        
        static decimal CallWebService()
        {
            WriteLine("Começando uma requisição ao servidor web...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Requisição ao servidor web concluída.");
            return 89.99M;
        }

        static string CallStoredProcedure(decimal amount)
        {
            WriteLine("Começando uma requisição ao serviço de armazenamento...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Requisição ao serviço de armazenamento concluída.");
            return $"12 produtos custam mais que {amount:C}.";
        }

        static void Main(string[] args)
        {
            var timer = Stopwatch.StartNew();
            //WriteLine("Executando métodos de forma síncrona em uma thread.");
            // MethodA();
            // MethodB();
            // MethodC();

            /*
            WriteLine("Executano métodos de forma asíncrona em multiplas threads.");
            Task taskA = new Task(MethodA);
            taskA.Start();
            Task taskB = Task.Factory.StartNew(MethodB);
            Task taskC = Task.Run(new Action(MethodC));
            Task[] tasks = {taskA, taskB, taskC};
            Task.WaitAll(tasks);
            */

            WriteLine("Passando o resultado de uma tarefa como argumento para a seguinte.");
            var taskCallWebServiceAndThenStoredProcedure = 
            Task.Factory.StartNew(CallWebService)
            .ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));
            
            WriteLine($"Resultado: {taskCallWebServiceAndThenStoredProcedure.Result}");
            WriteLine($"{timer.ElapsedMilliseconds}ms se passaram.");
        }
    }
}