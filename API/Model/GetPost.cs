using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class GetPost
    {
        string categoryId;
        string isActive;

        public string CategoryId { get => categoryId; set => categoryId = value; }
        public string IsActive { get => isActive; set => isActive = value; }
    }
}
