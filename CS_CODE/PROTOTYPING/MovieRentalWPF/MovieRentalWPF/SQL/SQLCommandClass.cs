using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalWPF.SQL
{
    public abstract class SQLCommandClass
    {
        public string ViewAvaialbeMovies { get; } = "SELECT * FROM VIEW_SELECT_AVAILABLE_MOVIES";
        public string ViewAllCustomers { get; } = "SELECT * FROM VIEW_ALL_CUSTOMERS";

        public string DefaultConnectionName { get;  } =
            System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    }
}
