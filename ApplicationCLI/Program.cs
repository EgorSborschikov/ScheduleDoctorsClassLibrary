using System;
using System.Collections.Generic;
using SF2022User12Lib;

/// <summary>
/// Консольное приложение с использованием разработанной библиотеки классов
/// </summary>

public class Program
{
    public static void Main()
    {
        // Создаем экземпляр класса Calculation
        Calculation calculation = new Calculation();

        // Запрос у пользователя входных данных
        Console.WriteLine("Введите количество занятых интервалов:");
        int numberOfIntervals = int.Parse(Console.ReadLine());

        List<TimeSpan> startTimes = new List<TimeSpan>();
        List<int> durations = new List<int>();

        for (int i = 0; i < numberOfIntervals; i++)
        {
            Console.WriteLine($"Введите начало занятого интервала {i + 1} (в формате HH:mm):");
            string startTimeInput = Console.ReadLine();
            TimeSpan startTime;
            if (!TimeSpan.TryParse(startTimeInput, out startTime))
            {
                Console.WriteLine("Неверный формат времени. Повторите ввод.");
                i--;
                continue;
            }
            startTimes.Add(startTime);

            Console.WriteLine($"Введите длительность занятого интервала {i + 1} (в минутах):");
            int duration = int.Parse(Console.ReadLine());
            durations.Add(duration);
        }

        Console.WriteLine("Введите минимальное необходимое время для посетителя (в минутах):");
        int consultationTime = int.Parse(Console.ReadLine());

        Console.WriteLine("Введите начало рабочего дня (в формате HH:mm):");
        TimeSpan beginWorkingTime = TimeSpan.Parse(Console.ReadLine());

        Console.WriteLine("Введите конец рабочего дня (в формате HH:mm):");
        TimeSpan endWorkingTime = TimeSpan.Parse(Console.ReadLine());

        // Получение списка свободных интервалов
        List<string> freeIntervals = calculation.AvaliablePeriods(
            startTimes, durations, consultationTime, beginWorkingTime, endWorkingTime);

        // Вывод результата
        Console.WriteLine("Свободные временные интервалы:");
        foreach (var interval in freeIntervals)
        {
            Console.WriteLine(interval);
        }
    }
}
