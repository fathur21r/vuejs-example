using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class SavePost : BaseModel
    {
        string categoryId;
        string content;
        string title;
        string isActive;

        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string Content { get => content; set => content = value; }
        public string Title { get => title; set => title = value; }
        public string IsActive { get => isActive; set => isActive = value; }
    }
}
