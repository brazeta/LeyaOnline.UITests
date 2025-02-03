using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnector.TableModels;

namespace DatabaseConnector
{
    public class DBConnector
    {
        #region Internal Variables

        internal BookTable _bookTable;

        #endregion

        #region Public Properties

        public BookTable bookTable
        {
            get
            {
                if (_bookTable == null)
                    _bookTable = new BookTable();
                return _bookTable;
            }
        }

        #endregion
    }
}
