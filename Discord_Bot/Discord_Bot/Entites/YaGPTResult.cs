using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Entites
{
    public class YaGPTResult
    {
        public Result Result { get; set; }
    }

    public class Result
    {
        public List<Alternatives> Alternatives { get; set; }
        public Usage Usage { get; set; }
        public string ModelVersion { get; set; }
    }

    public class Alternatives
    {
        public Message Message { get; set; }
        public string Status { get; set; }
    }

    public class Usage
    {
        public string InputTokens { get; set; }
        public string CompletionTokens { get; set; }
        public string totalTokens { get; set; }
    }
}
