using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crp10
{
    public class DatabaseConnection
    {
        public string ConnectionString { get; private set; }

        public DatabaseConnection()
        {
            ConnectionString = @"Data Source=DESKTOP-CFBSDT0\SQLEXPRESS;Initial Catalog=АВТОПРОКАТ;Integrated Security=True;MultipleActiveResultSets=True";
        }
    }
}
