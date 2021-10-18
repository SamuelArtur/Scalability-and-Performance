using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static int Counter;
        static object conch = new object();
        static Random r = new Random();
        static string Message; // O recurso compartilhado

        static void MethodA()
        {
            try
            {
                if (Monitor.TryEnter(conch, TimeSpan.FromSeconds(15)))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(r.Next(2000));
                        Message += "A";
                        Interlocked.Increment(ref Counter);
                        Write(".");
                    }
                } 
                else
                {
                    WriteLine("Método A falhou em entrar no bloqueio de monitor.");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }

        static void MethodB()
        {
            try
            {
                if (Monitor.TryEnter(conch, TimeSpan.FromSeconds(15)))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(r.Next(2000));
                        Message += "B";
                        Interlocked.Increment(ref Counter);
                       Write(".");
                    }
                } 
                else
                {
                    WriteLine("Método B falhou em entrar no bloqueio de monitor.");
                }
            }
            finally
            {
                Monitor.Exit(conch);
            }
        }

        static void Main(string[] args)
        {
            WriteLine("Por favor aguarde o término das tarefas.");
            Stopwatch watch = Stopwatch.StartNew();

            //Instanciando e aninhando as tarefas
            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);
            Task.WaitAll(new Task[] { a, b });

            //Mostrando os resultados
            WriteLine();
            WriteLine($"Resultados: {Message}.");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} milissegundos se passou.");
            WriteLine($"{Counter} modificações de string.");
        }
    }
}