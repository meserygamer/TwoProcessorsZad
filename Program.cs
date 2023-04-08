namespace TwoProcessorMethod
{
    /// <summary>
    /// Основной класс программы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        static void Main(string[] args)
        {
            TwoProcessorMethod Zad = new TwoProcessorMethod("InputData3.txt");
            Zad.CreateOptimalRasp();
            Zad.VivodResursovRaspred();
            Console.WriteLine($"Время простоя до оптимизации: {Zad.FindMax(Zad.StartRaspred)}" +
                $"\nВремя простоя после оптимизации: {Zad.FindMax(Zad.EndRaspred)}");
        }
    }
    /// <summary>
    /// Структура описывающая скорость обработки материала на двух процессорах
    /// </summary>
    struct Material
    {
        /// <summary>
        /// Время обработки на первом процессоре
        /// </summary>
        public int TimeForProcessingOnFirstProcessor { get; private set; }
        /// <summary>
        /// Время обработки на втором процессоре
        /// </summary>
        public int TimeForProcessingOnSecondProcessor { get; private set; }
        /// <summary>
        /// Конструктор струкутуры Material
        /// </summary>
        /// <param name="timeForProcessingOnFirstProcessor">
        /// Время обработки на первом процессоре </param>
        /// <param name="timeForProcessingOnSecondProcessor">
        /// Время обработки на втором процессоре </param>
        public Material(int timeForProcessingOnFirstProcessor,
            int timeForProcessingOnSecondProcessor)
        {
            this.TimeForProcessingOnFirstProcessor = timeForProcessingOnFirstProcessor;
            this.TimeForProcessingOnSecondProcessor = timeForProcessingOnSecondProcessor;
        }
        /// <summary>
        /// Функция для нахождения минимального времени обработки
        /// детали на любом из процессоров
        /// </summary>
        /// <returns>Минимальное время обработки детали</returns>
        public int FindMinSpeed()
        {
            if (TimeForProcessingOnFirstProcessor < TimeForProcessingOnSecondProcessor)
            {
                return TimeForProcessingOnFirstProcessor;
            }
            else
            {
                return TimeForProcessingOnSecondProcessor;
            }
        }
        /// <summary>
        /// Функция для суммарного времени обработки детали на двух процессорах
        /// </summary>
        /// <returns>Суммарное время обработки</returns>
        public int SumSpeed() =>
            TimeForProcessingOnSecondProcessor + TimeForProcessingOnFirstProcessor;
        /// <summary>
        /// Функция для нахождения разницы между временем обработки детали
        /// на первом процессоре и временем обработки детали на втором процессоре
        /// </summary>
        /// <returns>Разница времени обработки</returns>
        public int SpeedDifferent() =>
            TimeForProcessingOnFirstProcessor - TimeForProcessingOnSecondProcessor;
        /// <summary>
        /// Переречисление для идентификации метода сортировки
        /// </summary>
        public enum StateOfSort
        {
            /// <summary>
            /// Сортировка выполняется по времени обработки на первом станке
            /// </summary>
            SortOnFirstSpeed,
            /// <summary>
            /// Сортировка выполняется по времени обработки на втором станке
            /// </summary>
            SortOnSecondSpeed
        }
        /// <summary>
        /// Функция для представления объекта класса Material как строки
        /// </summary>
        /// <returns>Строка где через табуляцию записана скорость обработки
        /// на первом процессоре и скорость обработки на втором процессоре</returns>
        public override string ToString()
        {
            return TimeForProcessingOnFirstProcessor + "\t"
                + TimeForProcessingOnSecondProcessor;
        }
        /// <summary>
        /// Функция для сортировки метериалов в массиве
        /// </summary>
        /// <param name="matherialList">Сортируемый массив </param>
        /// <param name="flagOfSort">Метод сортировки
        /// (опционально, по умолчанию сортировка выполняется
        /// по времени обработки на первом процессоре) </param>
        public static void SortListOfMatherial(List<Material> matherialList,
            StateOfSort flagOfSort = StateOfSort.SortOnFirstSpeed)
        {
            bool flagOfEndSorting = true; //Флаг конца сортировки
            if (flagOfSort == StateOfSort.SortOnFirstSpeed)
            {
                while (flagOfEndSorting)
                {
                    flagOfEndSorting = false; //Меняется на false, если все дальнейшие элементы
                    //будут отсортированы, то так и останется false и сортировка закончится
                    for (int i = 0; i < matherialList.Count - 1; i++)
                    {
                        if ((matherialList[i].FindMinSpeed() > matherialList[i + 1].FindMinSpeed())
                            || ((matherialList[i].FindMinSpeed() == matherialList[i + 1].FindMinSpeed())
                            && (matherialList[i].SumSpeed() < matherialList[i + 1].SumSpeed())))
                        {
                            Material mathForReplace = matherialList[i]; //Смена местами элементов 
                            matherialList[i] = matherialList[i + 1];
                            matherialList[i + 1] = mathForReplace;
                            flagOfEndSorting = true; 
                            //Меняется на true, указывая на необходимость ещё одного круга
                        }
                    }
                }
            }
            if (flagOfSort == StateOfSort.SortOnSecondSpeed)
            {
                while (flagOfEndSorting)
                {
                    flagOfEndSorting = false;//Меняется на false, если все дальнейшие элементы
                    //будут отсортированы, то так и останется false и сортировка закончится
                    for (int i = 0; i < matherialList.Count - 1; i++)
                    {
                        if ((matherialList[i].FindMinSpeed() < matherialList[i + 1].FindMinSpeed())
                            || ((matherialList[i].FindMinSpeed() == matherialList[i + 1].FindMinSpeed())
                            && (matherialList[i].SumSpeed() > matherialList[i + 1].SumSpeed())))
                        {
                            Material mathForReplace = matherialList[i];//Смена местами элементов 
                            matherialList[i] = matherialList[i + 1];
                            matherialList[i + 1] = mathForReplace;
                            flagOfEndSorting = true;
                            //Меняется на true, указывая на необходимость ещё одного круга
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// Основной класс с функционалом Двух-процессорного метода
    /// </summary>
    class TwoProcessorMethod
    {
        /// <summary>
        /// Изначальное распределение материалов
        /// </summary>
        public Material[] StartRaspred { get; protected set; }
        /// <summary>
        /// Оптимальное распределение после обработки алгоритмом
        /// </summary>
        public Material[] EndRaspred { get; protected set; }
        /// <summary>
        /// Конструктор экземляра TwoProcessorMethod
        /// </summary>
        /// <remarks>Формирует массив StartRaspred,
        /// заполняя его Material созданными по данным из файла</remarks>
        /// <param name="path">
        /// Путь к файлу с изначальным распределением </param>
        public TwoProcessorMethod(string path)
        {
            string[] inputFromFile = File.ReadAllLines(path);
            StartRaspred = new Material[inputFromFile.Length];
            EndRaspred = new Material[inputFromFile.Length];
            for (int i = 0; i < inputFromFile.Length; i++)
            {
                StartRaspred[i] = new Material(Convert.ToInt32(inputFromFile[i].Split(" ")[0]),
                    Convert.ToInt32(inputFromFile[i].Split(" ")[1]));
            }
        }
        /// <summary>
        /// Функция для формирования оптимального распределения
        /// </summary>
        /// <remarks>Создает два массива,
        /// первый берет в себя все Material из начального распределения,
        /// где время обработки на первом процессоре меньше чем на втором,
        /// а второй массив наоборот,
        /// те Material, где время обработки на втором меньше чем на первом.
        /// Полученые массивы сортируются и второй массив объединяется с первым,
        /// вставая в конец, формируя финальное распределение</remarks>
        public void CreateOptimalRasp()
        {
            List<Material> matherialThatProcessingFasterOnFirstProcessor = new List<Material>();
            //Для хранения элементов скорость обработки которых выше на первом станке
            List<Material> matherialThatProcessingFasterOnSecondProcessor = new List<Material>();
            //Для хранения элементов скорость обработки которых выше на втором станке
            foreach (var material in StartRaspred)
            {
                if (material.SpeedDifferent() < 0)
                {
                    matherialThatProcessingFasterOnFirstProcessor.Add(material);
                }
                else
                {
                    matherialThatProcessingFasterOnSecondProcessor.Add(material);
                }
            }
            Material.SortListOfMatherial(matherialThatProcessingFasterOnFirstProcessor,
                Material.StateOfSort.SortOnFirstSpeed);
            Material.SortListOfMatherial(matherialThatProcessingFasterOnSecondProcessor,
                Material.StateOfSort.SortOnSecondSpeed);
            EndRaspred = matherialThatProcessingFasterOnFirstProcessor.
                Concat(matherialThatProcessingFasterOnSecondProcessor).ToArray();
            //Два массива в один
        }
        /// <summary>
        /// Функция для нахождения времени простоя первого процессора
        /// </summary>
        /// <returns>Время простоя первого процессора</returns>
        public int FindMax(Material[] raspred)
        {
            int max = raspred[0].TimeForProcessingOnFirstProcessor;
            int nowSum = raspred[0].TimeForProcessingOnFirstProcessor;
            for (int i = 1; i < raspred.Length; i++)
            {
                nowSum += raspred[i].TimeForProcessingOnFirstProcessor -
                    raspred[i - 1].TimeForProcessingOnSecondProcessor;
                if (max < nowSum)
                {
                    max = nowSum;
                }
            }
            return max;
        }
        /// <summary>
        /// Функция для вывода в консоль результата работы алгоритма
        /// </summary>
        /// <remarks> Выводит в консоль начальное распределение,
        /// а затем и получившееся в результате работы алгоритма</remarks>
        public void VivodResursovRaspred()
        {
            Console.WriteLine("Исходная матрица:");
            foreach (var material in StartRaspred)
            {
                Console.WriteLine(material);
            }
            Console.WriteLine("После распределения:");
            foreach (var material in EndRaspred)
            {
                Console.WriteLine(material);
            }
        }
    }
}