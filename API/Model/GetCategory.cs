using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class GetCategory
    {
        string name;
        String isActive;

        public string Name { get => name; set => name = value; }
        public string IsActive { get => isActive; set => isActive = value; }
    }
}
