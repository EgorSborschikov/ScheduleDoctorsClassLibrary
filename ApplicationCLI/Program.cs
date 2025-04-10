using System;
using System.Collections.Generic;
using SF2022User12Lib;

/// <summary>
/// Консольное приложение с примером использованием разработанной библиотеки классов
/// </summary>
public class Program
{
    public static void Main()
    {
        // Создается экземпляр класса Calculation для упращения обращения к методу этого класса
        Calculation calculation = new Calculation();

        // Вывод шаблона ввода данных пользователю на консоль терминала
        Console.WriteLine("Пример ввода данных:");
        Console.WriteLine("Ввод:");
        Console.WriteLine("Время\t\t| Длительность");
        Console.WriteLine("08:00-08:30\t| 30");
        Console.WriteLine("10:00-10:30\t| 30");
        Console.WriteLine("11:00-11:30\t| 30");
        Console.WriteLine("15:00-15:10\t| 10");
        Console.WriteLine("15:30-15:40\t| 10");
        Console.WriteLine("16:30-16:40\t| 10");
        Console.WriteLine("Время работы: 08:00-18:00");
        Console.WriteLine("Время консультации: 30 минут");
        Console.WriteLine();

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
        Console.WriteLine("Время\t\t| Длительность");
        foreach (var interval in freeIntervals)
        {
            Console.WriteLine(interval.Replace(" - ", "\t| "));
        }
    }
}