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
