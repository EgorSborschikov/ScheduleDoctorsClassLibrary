namespace SF2022User12Lib;

/// <summary>
/// Библиотека классов для вывода свободных временных интервалов в расписании врачей
/// </summary>

public class Calculation
{
    public List<String> AvaliablePeriods(
        List<TimeSpan> startTimes,
        List<int> durations,
        int consultationTime,
        TimeSpan beginWorkingTime,
        TimeSpan endWorkingTime
    )
    {
        // Список для хранения занятых интервалов
        List<(TimeSpan start, TimeSpan end)> busyIntervals = new List<(TimeSpan start, TimeSpan end)>();
        
        // Заполняем список занятых интервалов
        for (int i = 0; i < startTimes.Count; i++)
        {
            busyIntervals.Add((startTimes[i], startTimes[i].Add(TimeSpan.FromMinutes(durations[i]))));
        }
        
        // Сортировка занятых интервалы по времени начала
        busyIntervals.Sort((a, b) => a.start.CompareTo(b.start));
        
        // Инициализация времени начала и конца рабочего дня
        TimeSpan startOfDay = beginWorkingTime;
        TimeSpan endOfDay = endWorkingTime;
        
        // Список для хранения свободных интервалов
        List<string> freeIntervals = new List<string>();
        
        // Начальное свободное время равно началу рабочего дня
        TimeSpan currentStart = startOfDay;
        
        foreach (var busyInterval in busyIntervals)
        {
            // Если есть промежуток между текущим свободным временем и началом занятого интервала
            if (currentStart < busyInterval.start && busyInterval.start - currentStart >= TimeSpan.FromMinutes(consultationTime))
            {
                freeIntervals.Add($"{currentStart:hh\\:mm} - {busyInterval.start:hh\\:mm}");
            }

            // Обновление текущего свободного времени на конец текущего занятого интервала
            currentStart = busyInterval.end;
        }

        // Проверка последнего свободного интервала до конца рабочего дня
        if (currentStart < endOfDay && endOfDay - currentStart >= TimeSpan.FromMinutes(consultationTime))
        {
            freeIntervals.Add($"{currentStart:hh\\:mm} - {endOfDay:hh\\:mm}");
        }

        // Возврат выходных данных
        return freeIntervals;
    }
}