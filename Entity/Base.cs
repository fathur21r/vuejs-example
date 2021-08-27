using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity
{
    public class Base
    {
        String id;
        DataTable data;
        int rowsAffected;
        String isActive;

        public Base()
        {
            data = new DataTable();
        }

        public string Id { get => id; set => id = value; }
        public DataTable Data { get => data; set => data = value; }
        public int RowsAffected { get => rowsAffected; set => rowsAffected = value; }
        public string IsActive { get => isActive; set => isActive = value; }
    }
}
