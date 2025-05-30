﻿#if NET

namespace TUnit.Assertions.AssertConditions.Chronology;

public class DateOnlyEqualsExpectedValueAssertCondition(DateOnly expected) : ExpectedValueAssertCondition<DateOnly, DateOnly>(expected) 
{
    private int? _tolerance;

    internal protected override string GetExpectation()
    {
        if (_tolerance is null or 0)
        {
            return $"to be equal to {expected}";
        }

        return $"to be equal to {expected} +-{_tolerance}";
    }

    protected override ValueTask<AssertionResult> GetResult(DateOnly actualValue, DateOnly expectedValue)
    {
        if (_tolerance is not null)
        {
            var min = expected.AddDays(-_tolerance.Value);
            var max = expected.AddDays(_tolerance.Value);

            return AssertionResult
                .FailIf(actualValue < min || actualValue > max,
                    $"the received value {actualValue} is outside the tolerances");
        }

        return AssertionResult
            .FailIf(actualValue != expected,
                $"the received value {actualValue} is different");
    }

    public void SetTolerance(int toleranceDays)
    {
        _tolerance = toleranceDays;
    }
}

#endif
