# Библиотека классов, предназначенная для работы с графиком врачей

## Техническое задание
Требуется разработать библиотеку классов, которая позволит вернуть список свободных временных интервалов (заданного размера) в графике врачей. Метод должен иметь модификатор public.
Вход: список занятых промежутокв времени (в двух массивах: startTime - начало, durations - длительность). Минимальное необходимое время посетителя (constitutionTime). Рабочий день сотрудника (начало - beginWorkingTime и завершение - endWorkingTime)
Выход: список подходящих свободных временных промежутков (в массив строк формата HH:mm - HH:mm)