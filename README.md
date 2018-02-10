### SilverAtom
C# multiple task queuing using SemaphoreSlim

#### Creating queue and task
```csharp
var atomic = new Atomic();
var task = atomic.Enqueue("myKey", () =>
{
    // Replace with your logic
    Task.Delay(1000).Wait();

    return 0;
});
```
#### How it works? Let's Test it
The below code sample is running 2 queues of tasks "abc1" and "abc-1" where they are filling the list "list" with [positive and even] numbers and [negatives and odd] for the second, to throttle the process in **some points** we've adding a "conditional" Task.Delay.

```csharp
var atomic = new Atomic();
var list = new List<int>();
var tasks = new List<Task<int>>();
var range = Enumerable.Range(0, 100);

foreach (var number in range)
{
    var key = "abc" + (number % 2 == 0 ? "1" : "-1");

    var task = atomic.Enqueue(key, () =>
    {
        if (number % 19 == 1)
        {
            Task.Delay(1000).Wait();

            list.Add(number * (number % 2 == 0 ? 1 : -1));
        }
        else
        {
            list.Add(number * (number % 2 == 0 ? 1 : -1));
        }

        return number;
    });

    tasks.Add(task);
}

Task.WaitAll(tasks.ToArray());

foreach (var item in list)
{
    Console.WriteLine(item);
}
```

So the abc tasks will run in a consecutive manner but it will not wait for any of abc-1 tasks (vice versa).
In other words the output list can have 0 2 -1 even -1 came from 1 x -1 so its task was generated before 2, but you cannot have 0 2 -3 -1 because -3 and -1 belong to the abc-1 queue so should be -1 -3.

An output sample could be:
```
0 ,2 ,4 ,6 ,8 ,10 ,12 ,14 ,16 ,18 ,-1 ,-3 ,-5 ,-7 ,-9 ,-11 ,-13 ,-15 ,-17 ,-19 ,-21 ,-23 ,-25 ,-27 ,-29 ,-31 ,
-33 ,-35 ,-37 ,20 ,22 ,24 ,26 ,28 ,30 ,32 ,34 ,36 ,38...
```
