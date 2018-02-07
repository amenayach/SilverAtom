using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilverAtom
{
    public class Atomic
    {
        Dictionary<string, Atom> dictionary;

        public Atomic()
        {
            this.dictionary = new Dictionary<string, Atom>();
        }

        public async Task<T> Enqueue<T>(string key, Func<T> operation)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new Atom());
            }

            return await dictionary[key].Enqueue<T>(operation);
        }
    }
}
