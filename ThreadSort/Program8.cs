using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ThreadSort {
    public class Program8 {
        private static int[] data, data2;
        private static Form6 form1, form2;
        private static Thread thread1; private static Thread thread2;
        private static bool end1 = false;
        private static bool end2 = false;
        static EventWaitHandle th1Ready = new AutoResetEvent(false);
        static EventWaitHandle th2Ready = new AutoResetEvent(false);
        static EventWaitHandle th1Go= new AutoResetEvent(false);
        static EventWaitHandle th2Go = new AutoResetEvent(false);

        private static int index1a;
        private static int index1b;
        private static int index2a;
        private static int index2b;

        [STAThread]
        static void Main() {
            int velikost = 15;
            data = new int[velikost]; data2 = new int[velikost];
            Random value = new Random();
            for (int i = 0; i < velikost; i++) {
                data[i] = value.Next(0, 101);
                data2[i] = data[i];
            }
            form1 = new Form6(data);
            form2 = new Form6(data2);
            form1.Show();
            form2.Show();
            form1.Location = new System.Drawing.Point(100, 150);
            form2.Location = new System.Drawing.Point(350, 150);
            form1.Text = "BubbleSortSimple";
            form2.Text = "BubbleSortOptim";
            form1.showData(0, 0);
            form2.showData(0, 0);
            thread1 = new Thread(bubbleSortSimple); thread2 = new Thread(bubbleSortOptim);
            thread1.Start(); thread2.Start();
            Thread.Sleep(100);
            
            while ((!end1) || (!end2 )) {
                form1.showData(index1a, index1b);
                form2.showData(index2a, index2b);

                if (!end1 ) th1Ready.WaitOne();
                if (!end2 ) th2Ready.WaitOne();
                th1Go.Set();th2Go.Set();
                Thread.Sleep(1);
            }
            Thread.Sleep(30000);

        }
        private static void bubbleSortSimple() {
            int pom, pocetPruchodu = 0;
            //Thread.Sleep(10);

            for (int i = 0; i < data.Length - 1; i++) {
                for (int j = 0; j < data.Length - 1; j++) {
                    //ready1 = false;
                    if (data[j] > data[j + 1]) {
                        pom = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = pom;
                        //f6.showData(j, j + 1);
                        index1a = j;
                        index1b = j + 1;
                    }
                    //ready1 = true;
                    //thread1.Suspend();
                    th1Ready.Set();
                    th1Go.WaitOne();
                }
                pocetPruchodu++;
            }
            end1 = true;
            Console.WriteLine("Pocet pruchodu " + pocetPruchodu + " bubbleSortSimple");
            
        }
        private static void bubbleSortOptim() {
            int pom, pocetPruchodu = 0;
            int lastSwapIndex = data2.Length;
            int currentSwapIndex;
            do {
                currentSwapIndex = 0;
                for (int j = 0; j < lastSwapIndex - 1; j++) {
                    //ready2 = false;
                    if (data2[j] > data2[j + 1]) {
                        pom = data2[j];
                        data2[j] = data2[j + 1];
                        data2[j + 1] = pom;
                        currentSwapIndex = j + 1;
                        //f7.showData(j, j + 1);
                        index2a = j;
                        index2b = j + 1;
                    }
                    //ready2 = true; 
                    //thread2.Suspend();
                    Thread.Sleep(30);
                    th2Ready.Set();
                    th2Go.WaitOne();
                }
                pocetPruchodu++;
                lastSwapIndex = currentSwapIndex;
            } while (lastSwapIndex > 0);
            end2 = true;
            Console.WriteLine("Pocet pruchodu " + pocetPruchodu + " BubbleSortOptim");


        }
    }

}

