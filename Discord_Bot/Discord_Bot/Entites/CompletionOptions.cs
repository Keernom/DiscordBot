using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Entites
{
    public class CompletionOptions
    {
        public bool stream;
        public float temperature;
        public int maxTokens;
    }
}
