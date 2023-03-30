namespace TwoProcessorsZad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    struct Material
    {
        public int TimeForProcessingOnFirstProcessor {get;private set;}
        public int TimeForProcessingOnSecondProcessor {get;private set;}
        public Material(int TimeForProcessingOnFirstProcessor, int TimeForProcessingOnSecondProcessor)
        {
            this.TimeForProcessingOnFirstProcessor = TimeForProcessingOnFirstProcessor;
            this.TimeForProcessingOnSecondProcessor = TimeForProcessingOnSecondProcessor;
        }
        public int FindMinSpeed()
        {
            if (TimeForProcessingOnFirstProcessor < TimeForProcessingOnSecondProcessor)
            {
                return TimeForProcessingOnFirstProcessor;
            }
            else return TimeForProcessingOnSecondProcessor;
        }
    }
    class TwoProcessorZad
    {
        Material[] StartRasp;
        Material[] EndRasp;
        int CountOfMaterial;
        public TwoProcessorZad()
        {
            Console.WriteLine("Введите количество матриалов для обработки");
            CountOfMaterial = Convert.ToInt32(Console.ReadLine());
            StartRasp = new Material[CountOfMaterial];
            EndRasp = new Material[CountOfMaterial];
            for(int i = 0; i < CountOfMaterial; i++)
            {
                Console.WriteLine($"Введите скорость обработки материала №{i+1} на процессоре 1 и на процессоре 2 соответвенно");
                string SpeedOfProcessing = Console.ReadLine();
                StartRasp[i] = new Material(Convert.ToInt32(SpeedOfProcessing.Split(" ")[0]), Convert.ToInt32(SpeedOfProcessing.Split(" ")[1]));
            }
        }
        public void CreateOptimalRasp()
        {
            List<Material> ListOfNotRaspred = new List<Material>(StartRasp);
            List<Material> BottomList = new List<Material>();
            List<Material> TopList = new List<Material>();
            while(ListOfNotRaspred.Count != 0)
            {
                foreach(var ChoseMaterial in ListOfNotRaspred)
                {
                    Material MaterialWithMinSpeedOfProcessing = ListOfNotRaspred[0];
                    if (MaterialWithMinSpeedOfProcessing.FindMinSpeed() > ChoseMaterial.FindMinSpeed()) MaterialWithMinSpeedOfProcessing = ChoseMaterial;
                    else if(MaterialWithMinSpeedOfProcessing.FindMinSpeed() == ChoseMaterial.FindMinSpeed())
                }
            }
        }
    }
}