using NUnit.Framework;
using Shouldly;
using static Leeetcode.Problems.Medium.FizzBuzz;


namespace Leeetcode.Problems.Medium;

/// <summary>
/// 1195
/// </summary>

public class FizzBuzz
{
    public const string FIZZ = "fizz";
    public const string BUZZ = "buzz";
    public const string FIZZBUZZ = "fizzbuzz";
    public const string NUMBER = "number";

    private readonly Dictionary<string, SemaphoreSlim> _sempahoreRegistry = new()
    {
        [FIZZ] = new(0),
        [BUZZ] = new(0),
        [FIZZBUZZ] = new(0),
        [NUMBER] = new(1),
    };

    private int n;
    private int number = 1;
    public bool InRange => number <= n;
    public FizzBuzz(int n)
    {
        this.n = n;
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) => RunInLoop(() => Process(FIZZ, printFizz));

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) => RunInLoop(() => Process(BUZZ, printBuzz));

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) => RunInLoop(() => Process(FIZZBUZZ, printFizzBuzz));

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) => RunInLoop(() =>
    {
        Wait(NUMBER);
        var _ = number switch
        {
            var x when number % 3 == 0 && number % 5 == 0 => Release(FIZZBUZZ),
            var x when number % 3 == 0 => Release(FIZZ),
            var x when number % 5 == 0 => Release(BUZZ),
            _ => ExecuteOrReleaseAll(() => printNumber(number))
        };
    }).Then(ReleaseAll);

    private void ReleaseAll()
    {
        foreach (var semaphore in _sempahoreRegistry.Values) semaphore.Release();
    }

    private Unit RunInLoop(Action action)
    {
        while (InRange) action();
        return Unit.Default;
    }

    private ExecutionResult ExecuteIf(bool condition, Action action)
    {
        if (condition)
        {
            action();
            return ExecutionResult.Success();
        }
        return ExecutionResult.Failure();
    }

    private Unit ExecuteOrReleaseAll(Action action) => ExecuteIf(InRange, () =>
    {
        action();
        number++;
        Release(NUMBER);
    }).Else(ReleaseAll);

    private Unit Wait(string key)
    {
        _sempahoreRegistry[key].Wait();
        return Unit.Default;
    }

    private Unit Release(string key)
    {
        _sempahoreRegistry[key].Release();
        return Unit.Default;
    }
    private void Process(string key, Action action)
    {
        Wait(key);
        ExecuteOrReleaseAll(action);
    }

    public record ExecutionResult
    {
        public required bool IsSuccess { get; init; }
        private ExecutionResult() { }
        public static ExecutionResult Success() => new ExecutionResult { IsSuccess = true };
        public static ExecutionResult Failure() => new ExecutionResult { IsSuccess = false };

        public Unit Else(Action action)
        {
            if (!IsSuccess)
                action();
            return Unit.Default;
        }
    };

    public record struct Unit
    {
        public static Unit Default => new();
    }

}

public static class Extensions
{
    public static Unit Then(this Unit unit, Action action)
    {
        action();
        return unit;
    }
}

public class Tests
{
    [TestCase(7, new object[] { 1, 2, "fizz", 4, "buzz", "fizz", 7 })]
    [TestCase(1, new object[] { 1 })]
    [TestCase(15, new object[] { 1, 2, "fizz", 4, "buzz", "fizz", 7, 8, "fizz", "buzz", 11, "fizz", 13, 14, "fizzbuzz" })]
    public void Test(int n, object[] expectedResult)
    {
        var resultList = new List<object>();
        var fizzBuzz = new FizzBuzz(n);
        var threads = new List<Thread>()
        {
            new Thread(() => fizzBuzz.Fizz(() => resultList.Add(FIZZ))),
            new Thread(() => fizzBuzz.Buzz(() => resultList.Add(BUZZ))),
            new Thread(() => fizzBuzz.Fizzbuzz(() => resultList.Add(FIZZBUZZ))),
            new Thread(() => fizzBuzz.Number((i) => resultList.Add(i)))
        };

        threads.ForEach(t => t.Start());
        threads.ForEach(t => t.Join());
        resultList.ShouldBeEquivalentTo(expectedResult.ToList());
    }
}
