using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    [Serializable]
    public class Test
    {
        public string Subject { get; set; }
        public string Theme { get; set; }
        public string Author { get; set; }
        public List<Question> Questions { get; set; } = null;
        public Test() { Questions = new List<Question>(); }
        public Test(string subject = "", string theme = "", string author = "")
        {
            Questions = new List<Question>();
            Subject = subject;
            Theme = theme;
            Author = author;
        }
    }
}
