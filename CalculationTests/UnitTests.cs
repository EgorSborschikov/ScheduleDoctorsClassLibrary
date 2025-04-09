using System;
using System.Collections.Generic;
using NUnit.Framework;
using SF2022User12Lib;

namespace CalculationTests;

/// <summary>
/// Модульные тесты для различных ситуаций использования библиотеки классов
/// </summary>

[TestFixture]
public class Tests
{
    // 1. Проверка ситуации, когда нет занятых интервалов
    [Test]
    public void TestNoBusyIntervals()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan>(),
            new List<int>(),
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Is.EqualTo(new List<string> { "08:00 - 18:00" }));
    }
    
    // 2. Проверка наличия одного занятого интервала
    [Test]
    public void TestOneBusyInterval()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0) },
            new List<int> { 60 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Is.EquivalentTo(new List<string> { "08:00 - 10:00", "11:00 - 18:00" }));
    }
    
    // 3. Проверка наличия нескольких занятых интервалов
    [Test]
    public void TestMultipleBusyIntervals()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0), new TimeSpan(12, 0, 0) },
            new List<int> { 60, 30 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );

        Assert.That(result, Is.EquivalentTo(new List<string> { "08:00 - 10:00", "11:00 - 12:00", "12:30 - 18:00" }));
    }
    
    // 4. Проверка ситуации, когда нет свободного времени
    [Test]
    public void TestNoFreeTime()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(8, 0, 0) },
            new List<int> { 600 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Is.Empty);
    }
    
    // 5. Проверка наличия свободного времени в начале рабочего дня
    [Test]
    public void TestFreeTimeAtStartOfDay()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0) },
            new List<int> { 60 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Does.Contain("08:00 - 10:00"));
    }
    
    // 6. Проверка наличия свободного времени в конце рабочего дня
    [Test]
    public void TestFreeTimeAtEndOfDay()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0) },
            new List<int> { 60 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Does.Contain("11:00 - 18:00"));
    }
    
    // 7. Проверка короткого минимального времени консультации
    [Test]
    public void TestShortConsultationTime()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0) },
            new List<int> { 60 },
            10,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );

        Assert.That(result, Is.EquivalentTo(new List<string> { "08:00 - 10:00", "11:00 - 18:00" }));
    }
    
    // 8. Проверка длинного минимального времени консультации
    [Test]
    public void TestLongConsultationTime()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0) },
            new List<int> { 60 },
            90,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Is.EquivalentTo(new List<string> { "08:00 - 10:00", "11:00 - 18:00" }));
    }
    
    // 9. Проверка ситуации с перекрывающимися интервалами
    [Test]
    public void TestIverlappingIntervals()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(10, 0, 0), new TimeSpan(10, 30, 0) },
            new List<int> { 60, 60 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );
        
        Assert.That(result, Is.EquivalentTo(new List<string> { "08:00 - 10:00", "11:30 - 18:00" }));
    }
    
    // 10. Проверка граничного случая, когда занятый интервал начинается в начале рабочего дня
    [Test]
    public void TestEdgeCaseAtStart()
    {
        var calculation = new Calculation();
        var result = calculation.AvaliablePeriods(
            new List<TimeSpan> { new TimeSpan(8, 0, 0) },
            new List<int> { 30 },
            30,
            new TimeSpan(8, 0, 0),
            new TimeSpan(18, 0, 0)
        );

        Assert.That(result, Is.EquivalentTo(new List<string> { "08:30 - 18:00" }));
    }
}