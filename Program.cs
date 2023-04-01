namespace TwoProcessorsZad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TwoProcessorZad Zad = new TwoProcessorZad("InputData3.txt");
            Zad.CreateOptimalRasp();
            Zad.VivodResRasp();
            Console.WriteLine($"Время простоя до оптимизации: {Zad.FindMax(Zad.StartRasp)}\nВремя простоя после оптимизации: {Zad.FindMax(Zad.EndRasp)}");
        }
    }
    struct Material
    {
        public int TimeForProcessingOnFirstProcessor {get;private set; } //Время обработки на первом станке
        public int TimeForProcessingOnSecondProcessor {get;private set;} //Время обработки на втором станке
        public Material(int TimeForProcessingOnFirstProcessor, int TimeForProcessingOnSecondProcessor)
        {
            this.TimeForProcessingOnFirstProcessor = TimeForProcessingOnFirstProcessor;
            this.TimeForProcessingOnSecondProcessor = TimeForProcessingOnSecondProcessor;
        }
        public int FindMinSpeed() //Для нахождения минимальной скорости обработки детали
        {
            if (TimeForProcessingOnFirstProcessor < TimeForProcessingOnSecondProcessor)
            {
                return TimeForProcessingOnFirstProcessor;
            }
            else return TimeForProcessingOnSecondProcessor;
        }
        public int SumSpeed() => TimeForProcessingOnSecondProcessor + TimeForProcessingOnFirstProcessor; //Сумма скорости обработки на разных станках
        public int SpeedDifferent() => TimeForProcessingOnFirstProcessor - TimeForProcessingOnSecondProcessor; //Разница в скорости обработки на разных станках
        public enum StateOfSort //Для идентификации сортировки
        {
            SortOnFirstSpeed,
            SortOnSecondSpeed
        }
        public override string ToString()
        {
            return TimeForProcessingOnFirstProcessor + "\t" + TimeForProcessingOnSecondProcessor;
        }
        public static void SortListOfMatherial(List<Material> MatherialList, StateOfSort State = StateOfSort.SortOnFirstSpeed) //Метод сортировки списка материалов
        {
            bool flag = true;
            if(State == StateOfSort.SortOnFirstSpeed)
            {
                while (flag)
                {
                    flag = false;
                    for (int i = 0; i < MatherialList.Count - 1; i++)
                    {
                        if((MatherialList[i].FindMinSpeed() > MatherialList[i+1].FindMinSpeed()) 
                            || ((MatherialList[i].FindMinSpeed() == MatherialList[i + 1].FindMinSpeed()) && (MatherialList[i].SumSpeed() < MatherialList[i+1].SumSpeed())))
                        {
                            Material MathForReplace = MatherialList[i];
                            MatherialList[i] = MatherialList[i + 1];
                            MatherialList[i + 1] = MathForReplace;
                            flag = true;
                        }
                    }
                }
            }
            if (State == StateOfSort.SortOnSecondSpeed)
            {
                while (flag)
                {
                    flag = false;
                    for (int i = 0; i < MatherialList.Count - 1; i++)
                    {
                        if ((MatherialList[i].FindMinSpeed() < MatherialList[i + 1].FindMinSpeed())
                            || ((MatherialList[i].FindMinSpeed() == MatherialList[i + 1].FindMinSpeed()) && (MatherialList[i].SumSpeed() > MatherialList[i + 1].SumSpeed())))
                        {
                            Material MathForReplace = MatherialList[i];
                            MatherialList[i] = MatherialList[i + 1];
                            MatherialList[i + 1] = MathForReplace;
                            flag = true;
                        }
                    }
                }
            }
        }
    }
    class TwoProcessorZad
    {
        public Material[] StartRasp {get;protected set;} //Изначальный массив материалов
        public Material[] EndRasp {get;protected set;} //Массив материалов после сортировки
        int CountOfMaterial; //Количество материалов
        public TwoProcessorZad(string path)
        {
            string[] InputFromFile = File.ReadAllLines(path);
            StartRasp = new Material[InputFromFile.Length];
            EndRasp = new Material[InputFromFile.Length];
            for(int i = 0; i < InputFromFile.Length; i++)
            {
                StartRasp[i] = new Material(Convert.ToInt32(InputFromFile[i].Split(" ")[0]), Convert.ToInt32(InputFromFile[i].Split(" ")[1]));
            }
        }
        public void CreateOptimalRasp()
        {
            List<Material> MatherialThatProcessingFasterOnFirstProcessor = new List<Material>(); //Для хранения элементов скорость обработки которых выше на первом станке
            List<Material> MatherialThatProcessingFasterOnSecondProcessor = new List<Material>(); //Для хранения элементов скорость обработки которых выше на втором станке
            foreach (var i in StartRasp)
            {
                if(i.SpeedDifferent() < 0) MatherialThatProcessingFasterOnFirstProcessor.Add(i);
                else MatherialThatProcessingFasterOnSecondProcessor.Add(i);
            }
            Material.SortListOfMatherial(MatherialThatProcessingFasterOnFirstProcessor, Material.StateOfSort.SortOnFirstSpeed);
            Material.SortListOfMatherial(MatherialThatProcessingFasterOnSecondProcessor, Material.StateOfSort.SortOnSecondSpeed);
            EndRasp = MatherialThatProcessingFasterOnFirstProcessor.Concat(MatherialThatProcessingFasterOnSecondProcessor).ToArray(); //Два массива в один
        }
        public int FindMax(Material[] Rasp)
        {
            int max = Rasp[0].TimeForProcessingOnFirstProcessor;
            int NowSum = Rasp[0].TimeForProcessingOnFirstProcessor;
            for (int i = 1; i < Rasp.Length; i++)
            {
                NowSum += Rasp[i].TimeForProcessingOnFirstProcessor - Rasp[i - 1].TimeForProcessingOnSecondProcessor;
                if(max < NowSum) max = NowSum;
            }
            return max;
        }
        public void VivodResRasp()
        {
            Console.WriteLine("Исходная матрица:");
            foreach(var a in StartRasp) Console.WriteLine(a);
            Console.WriteLine("После распределения:");
            foreach(var a in EndRasp) Console.WriteLine(a);
        }
    }
}