using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppPredicate
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = new Robo1();
            var r2 = new Robo2();
            var r3 = new Robo3();

            var coordenador = new Coordenador();
            coordenador.ExecutaRobo1 = r1.MinhaTarefa;
            coordenador.ExecutaRobo2 = r2.OutraTarefa;
            coordenador.ExecutaRobo3 = r3.TarefaDoRobo3;

            Thread t1 = new Thread(new ThreadStart(coordenador.ExecutarTarefas));
            t1.Start();

            Console.WriteLine("Foi na Thead......");

            Console.ReadKey();
        }

       
    }

    class Robo1
    {
        public bool MinhaTarefa(Coordenador c)
        {
            //string targetArchiveName = "archive.rar", targetFile = "testFile.txt";
            //ProcessStartInfo startInfo = new ProcessStartInfo("WinRAR.exe");
            //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //startInfo.Arguments = string.Format("a \"{0}\" \"{1}\"",targetArchiveName, targetFile);
            //try
            //{
            //    // Start the process with the info we specified.
            //    using (Process exeProcess = Process.Start(startInfo))
            //    {
            //        exeProcess.WaitForExit();
            //    }
            //}
            //catch
            //{
            //    {
            //        Console.WriteLine("Error Open");
            //    }
            //}

            var fileName = Directory.GetFiles(@"C:\Teste");
            if (fileName.Length.Equals(0))
                return false;


            File.Move(fileName[0], Path.Combine(@"C:\Teste\PROCESSADO\", Path.GetFileName(fileName[0])));

            Console.WriteLine("Robo 1 executando a tarefa....");
            System.Threading.Thread.Sleep(2000);
            return true;
        }

    }

    class Robo2
    {
        public bool OutraTarefa(Coordenador c)
        {
            Console.WriteLine("Robo 2 executando a tarefa....");
            var fName = Directory.GetFiles(@"C:\Teste\PROCESSADO")[0];
            File.Delete(fName);
            System.Threading.Thread.Sleep(2000);
            return true;
        }

    }

    class Robo3
    {
        public bool TarefaDoRobo3(Coordenador c)
        {
            Console.WriteLine("Robo 3 executando a tarefa....");
            System.Threading.Thread.Sleep(2000);
            c.ExecutarTarefas();
            return true;
        }
    }

    class Coordenador
    {
        public Predicate<Coordenador> ExecutaRobo1 { get; set; }
        public Predicate<Coordenador> ExecutaRobo2 { get; set; }
        public Predicate<Coordenador> ExecutaRobo3 { get; set; }

        public void ExecutarTarefas()
        {
            Console.WriteLine("Coordenador chamando...");
            bool e1 = ExecutaRobo1.Invoke(this);
            if (e1)
            {
                bool e2 = ExecutaRobo2.Invoke(this);
                if (e2)
                {
                    bool e3 = ExecutaRobo3.Invoke(this);
                }

            }
            else
            {
                Console.WriteLine("Coordenador Encerrou...");
            }

            

        }

    }
}
