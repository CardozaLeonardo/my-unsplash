using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOPersistence.DAL
{
    public class BaseManager
    {
        protected DatabaseAccess _data;

        public BaseManager()
        {
            try
            {
                _data = new DatabaseAccess();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
