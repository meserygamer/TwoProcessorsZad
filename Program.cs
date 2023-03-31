namespace TwoProcessorsZad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TwoProcessorZad Zad = new TwoProcessorZad();
            Zad.CreateOptimalRasp();
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
        Material[] StartRasp; //Изначальный массив материалов
        Material[] EndRasp; //Массив материалов после сортировки
        int CountOfMaterial; //Количество материалов
        public TwoProcessorZad()
        {
            Console.WriteLine("Введите количество матриалов для обработки");
            CountOfMaterial = Convert.ToInt32(Console.ReadLine()); 
            StartRasp = new Material[CountOfMaterial]; 
            EndRasp = new Material[CountOfMaterial]; 
            for(int i = 0; i < CountOfMaterial; i++) //Заполнение массива материалов
            {
                Console.WriteLine($"Введите скорость обработки материала №{i+1} на процессоре 1 и на процессоре 2 соответвенно");
                string SpeedOfProcessing = Console.ReadLine();
                StartRasp[i] = new Material(Convert.ToInt32(SpeedOfProcessing.Split(" ")[0]), Convert.ToInt32(SpeedOfProcessing.Split(" ")[1]));
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
    }
}