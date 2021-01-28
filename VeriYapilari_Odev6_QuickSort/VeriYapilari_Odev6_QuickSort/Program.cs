using System;
using System.Diagnostics;

namespace VeriYapilari_Odev6_QuickSort
{
    class CDizi
    {
        public long[] dizi;
        public int boyut;
        public static int i = 0;
        public CDizi(int boyut)
        {
            this.boyut = boyut;
            dizi = new long[boyut];
        }
        public void Ekle(long sayi)
        {
            dizi[i] = sayi;
            i++;
        }
        public void Yazdir()
        {
            for (long j = 0; j < i; j++)
            {
                Console.Write(dizi[j] + "-");
            }
        }
        public void Sırala()
        {
            InsertluQuickli(0, i - 1);
        }
        public void QuickSort(int sol, int sag)
        {
            if (sag - sol <= 0)
            { return; }
            else
            {
                long pivot = dizi[sag];
                int partition = Partition(sol, sag, pivot);
                QuickSort(sol, partition - 1);
                QuickSort(partition + 1, sag);
            }
        }
        public int Partition(int sol, int sag, long pivot)
        {
            int left = sol - 1;
            int right = sag;
            while (true)
            {
                while (dizi[++left] < pivot)
                    ;
                while (dizi[--right] > 0 && dizi[--right] > pivot)
                    ;
                if (left >= right)
                    break;
                else
                {
                    Degistir(left, right);
                }
            }
            Degistir(left, sag);
            return left;
        }
        public void InsertluQuickli(int sol, int sag)
        {
            int size = sag - sol + 1;
            if (size < 100)
                insertionSort(sol, sag);
            else
            {
                long orta = Orta(sol, sag);
                int bol = Bol(sol, sag, orta);
                InsertluQuickli(sol, bol - 1);
                InsertluQuickli(bol + 1, sag);
            }
        }
        public long Orta(int sol, int sag)
        {
            int orta = (sol + sag) / 2;
            if (dizi[sol] > dizi[orta])
                Degistir(sol, orta);
            if (dizi[sol] > dizi[sag])
                Degistir(sol, sag);
            if (dizi[orta] > dizi[sag])
                Degistir(orta, sag);
            Degistir(orta, sag - 1);
            return dizi[sag - 1];
        }
        public void Degistir(int a, int b)
        {
            long gecici = dizi[a];
            dizi[a] = dizi[b];
            dizi[b] = gecici;
        }
        public int Bol(int sol, int sag, long orta)
        {
            int left = sol;
            int right = sag - 1;
            while (true)
            {
                while (dizi[++left] < orta)
                    ;
                while (dizi[--right] > orta)
                    ;
                if (left >= right)
                    break;
                else
                {
                    Degistir(left, right);
                }
            }
            Degistir(left, sag - 1);
            return left;
        }
        public void insertionSort(int sol, int sag)
        {
            int ilk, son;
            for (son = sol + 1; son <= sag; son++)
            {
                long gecici = dizi[son];
                ilk = son;
                while ((ilk > sol) && (dizi[ilk - 1] >= gecici))
                {
                    dizi[ilk] = dizi[ilk - 1];
                    --ilk;
                }
                dizi[ilk] = gecici;
            }
        }
        class Program
        {
            private static long nanoTime()
            {
                long nano = 10000L * Stopwatch.GetTimestamp();
                nano /= TimeSpan.TicksPerMillisecond;
                nano *= 100L;
                return nano;
            }
            static void Main(string[] args)
            {
                Random rnd = new Random();
                CDizi dizi = new CDizi(10000);
                int j;
                long sure1, sure2, basla, bitir;
                for (j = 0; j < 10000; j++)
                {
                    long n = rnd.Next(0,100);
                    dizi.Ekle(n);
                }
                Console.WriteLine("Dizinin sıralanmamış hali:");
                dizi.Yazdir();
                basla = nanoTime();
                dizi.Sırala();
                bitir = nanoTime();
                sure1 = bitir - basla;

                basla = nanoTime();
                dizi.QuickSort(0, j - 1);
                bitir = nanoTime();
                sure2 = bitir - basla;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Dizinin sıralanmış hali:");
                dizi.Yazdir();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Quick ve insertion birlikte kullanılan sıralamanın toplam süresi=" + sure1.ToString() + "ns");
                Console.WriteLine("Sadece quick sort kullanılan sıralamanın toplam süresi=" + sure2.ToString() + "ns");
            }
        }
    }
}
