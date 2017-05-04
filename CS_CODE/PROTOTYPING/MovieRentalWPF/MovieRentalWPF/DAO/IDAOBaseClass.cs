using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentalWPF.DAO
{
    interface IDAOBaseClass
    {
        // Create Operation
        void Create(DAOBaseClass dao);

        // Retreive Operation
        #region to be implemented in the future
        //DAOBaseClass Get(int ID);           
        #endregion
        IEnumerable<DAOBaseClass> Get();

        // Update Operation
        void Update(DAOBaseClass dao);

        // Deleted Operation
        void Delete(DAOBaseClass dao);
    }
}
