using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnector.ObjectModels
{
    public class BookRecord
    {
        public string BookId { get; set; }
        public string Image250px { get; set; }
        public string Image500px { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookPrice
        {
            get
            {
                return $"€{BookDecimalPrice.ToString("F2").Replace(".", ",")}";
            }
        }
        public decimal BookDecimalPrice { get; set; }
        public string AuthorURL { get; set; }
        public string BookURL { get; set; }

    }
}
