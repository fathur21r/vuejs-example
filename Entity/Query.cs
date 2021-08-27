using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ViewRouter
    {
        int id;
        string routeName;
        string url;
        string menuName;
        string component;

        public int Id { get => id; set => id = value; }
        public string RouteName { get => routeName; set => routeName = value; }
        public string Url { get => url; set => url = value; }
        public string MenuName { get => menuName; set => menuName = value; }
        public string Component { get => component; set => component = value; }
    }
}
