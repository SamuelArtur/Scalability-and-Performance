using System;
using System.Linq;
using System.Text;
using static System.Console;
using MonitoringLib;

namespace MonitoringApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //              MONITORANDO PERFORMANCE E USO DE MEMÓRIA:   
            /*
            WriteLine("Processando. Por favor aguarde...");
            Recorder.Start();

            int[] largeArrayOfInts = Enumerable.Range(1, 10_000).ToArray();

            System.Threading.Thread.Sleep(new Random().Next(5, 10) * 1000);

            Recorder.Stop();
            */

            //           MEDINDO A EFICIÊNCIA DO PROCESSAMENTO DE STRINGS:
            int[] numbers = Enumerable.Range(1, 50_000).ToArray();

            WriteLine("Usano string com +");
            Recorder.Start();
            string s = "";
            foreach (var item in numbers) s += item + ", ";
            Recorder.Stop();

            WriteLine("Usando StringBuilder");
            Recorder.Start();
            StringBuilder builder = new StringBuilder();
            foreach (var item in numbers) builder.Append(item); builder.Append(", ");
            Recorder.Stop();
        }
    }
}
