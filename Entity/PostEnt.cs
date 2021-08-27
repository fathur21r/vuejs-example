using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PostEnt : Base
    {
        string categoryId;
        string content;
        string title;

        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string Content { get => content; set => content = value; }
        public string Title { get => title; set => title = value; }
    }
}
