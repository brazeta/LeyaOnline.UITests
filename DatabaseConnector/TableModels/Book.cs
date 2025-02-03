using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnector.ObjectModels;

namespace DatabaseConnector.TableModels
{
    /// <summary>
    /// This class provides public methods that simulate access to the Book table
    /// </summary>
    public class BookTable
    {
        public List<BookRecord> GetByNameAndAuthor(string BookName, string Author)
        {
            var matchingRecords = new List<BookRecord>();

            //Here we would place the code to query the database to find books by name and author
            //For demo purposes this method will only simulate a call to the database to retrieve this information

            if (BookName.ToLower().Equals("O Triunfo dos Porcos".ToLower()) && Author.ToLower().Equals("GEORGE ORWELL".ToLower()))
                matchingRecords.Add(new BookRecord()
                {
                    BookId = "83457",
                    Image250px = "https://www.leyaonline.com/fotos/produtos/250_9789722071581_o_triunfo_dos_porcos.jpg",
                    Image500px = "https://www.leyaonline.com/fotos/produtos/500_9789722071581_o_triunfo_dos_porcos.jpg",
                    BookTitle = "O Triunfo dos Porcos",
                    BookAuthor = "GEORGE ORWELL",
                    BookDecimalPrice = 9.9m,
                    AuthorURL = "https://www.leyaonline.com/pt/autores/george-orwell/",
                    BookURL = "https://www.leyaonline.com/pt/livros/romance/o-triunfo-dos-porcos/"
                });

            if (BookName.ToLower().Equals("1984".ToLower()) && Author.ToLower().Equals("GEORGE ORWELL".ToLower()))
                matchingRecords.Add(new BookRecord()
                {
                    BookId = "74653",
                    Image250px = "https://www.leyaonline.com/fotos/produtos/250_9789722071550_1984_george_orwell.jpg",
                    Image500px = "https://www.leyaonline.com/fotos/produtos/500_9789722071550_1984_george_orwell.jpg",
                    BookTitle = "1984",
                    BookAuthor = "GEORGE ORWELL",
                    BookDecimalPrice = 12.5m,
                    AuthorURL = "https://www.leyaonline.com/pt/autores/george-orwell/",
                    BookURL = "https://www.leyaonline.com/pt/livros/romance/1984/"
                });

            if (BookName.ToLower().Equals("Meio Sol Amarelo".ToLower()) && Author.ToLower().Equals("CHIMAMANDA NGOZI ADICHIE".ToLower()))
                matchingRecords.Add(new BookRecord()
                {
                    BookId = "128652",
                    Image250px = "https://www.leyaonline.com/fotos/produtos/250_9789722084468_meio_sol_amarelo.jpg",
                    Image500px = "https://www.leyaonline.com/fotos/produtos/500_9789722084468_meio_sol_amarelo.jpg",
                    BookTitle = "Meio Sol Amarelo",
                    BookAuthor = "CHIMAMANDA NGOZI ADICHIE",
                    BookDecimalPrice = 24.93m,
                    AuthorURL = "https://www.leyaonline.com/pt/autores/chimamanda-ngozi-adichie-2/",
                    BookURL = "https://www.leyaonline.com/pt/livros/romance/meio-sol-amarelo-2-2/"
                });


            return matchingRecords;

        }
    }
}
