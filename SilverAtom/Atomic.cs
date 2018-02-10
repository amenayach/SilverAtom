using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SilverAtom
{
    public class Atomic
    {
        private Dictionary<string, Atom> dictionary;
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

        public Atomic()
        {
            this.dictionary = new Dictionary<string, Atom>();
        }

        public async Task<T> Enqueue<T>(string key, Func<T> operation)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, new Atom());
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return await dictionary[key].Enqueue<T>(operation);
        }
    }
}
