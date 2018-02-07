using System;
using System.Threading;
using System.Threading.Tasks;

namespace SilverAtom
{
    public class Atom
    {
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public async Task<T> Enqueue<T>(Func<T> operation)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                return await Task.Run(operation);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }

}
