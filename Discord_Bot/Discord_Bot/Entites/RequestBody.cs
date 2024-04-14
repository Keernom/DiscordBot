using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Entites
{
    public class RequestBody
    {
        public string modelUri { get; set; }
        public CompletionOptions completionOptions { get; set; }
        public List<Message> messages { get; set; }
    }
}
