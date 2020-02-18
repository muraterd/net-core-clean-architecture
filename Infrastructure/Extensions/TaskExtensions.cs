using System;
using System.Threading.Tasks;

public static class TaskExtensions
{
    /// <summary>
    /// Usage 
    /// Test
    /// await A.AsyncFunc().AndThen(A.AsyncFunc2).AndThen(A.AsyncFunc3)
    /// </summary>
    public static async Task<TOut> AndThen<TIn, TOut>(this Task<TIn> inputTask, Func<TIn, Task<TOut>> mapping)
    {
        var input = await inputTask;
        return (await mapping(input));
    }
}
