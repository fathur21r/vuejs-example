using Entity;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Router : Base
    {
        public static IEnumerable<ViewRouter> Get(bool IsShowMenu = false)
        {
            var connection = Create();
            string sql = "SELECT * FROM Router";

            if(IsShowMenu)
            {
                sql += " WHERE MenuName IS NOT NULL";
            }

            return connection.ExecuteQuery<ViewRouter>(sql);
        }
    }
}
