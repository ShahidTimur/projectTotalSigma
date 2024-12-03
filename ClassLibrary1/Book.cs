using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookJurnalLibrary
{
    public class Book : AbstractItem //создание класса книги, наследуемого от абстрактного класса
    {
        public string? Summary { get; set; }

        public genre Genre { get; set; }

        public Book(string isbn, string name, string edition, int Quantity, string summary, genre genre, double Price)
                    : base(isbn, name, edition, Quantity, Price) //создание конструктора класса книги с указанием базовых параметров
        {
            Summary = summary;
            Genre = genre;
        }
        public Book() // создание пустого конструктора для возможного обновления параметров
        {

        }

        public override void IsFormValid() //проверка на валидность заполнения данных о книге
        {
            if (String.IsNullOrWhiteSpace(Isbn) ||
                String.IsNullOrWhiteSpace(Name) ||
                String.IsNullOrWhiteSpace(Edition) ||
                String.IsNullOrWhiteSpace(Quantity.ToString()) ||
                String.IsNullOrWhiteSpace(Price.ToString()) ||
                String.IsNullOrWhiteSpace(Summary) ||
                String.IsNullOrWhiteSpace(Genre.ToString()))
            {
                throw new ArgumentNullException("One or more fields are not set!");
            }
        }

        public override string ToString() //перезапись метода для вывода необходимой информации
        {
            return $"Book | ISBN: {Isbn} | Name: {Name} | Date of Print: {DateOfPrint} | Edition {Edition} |" +
                $" Quantity in Shop: {Quantity} | Price: {Price:C} | Summary: {Summary} | Genre: {Genre}";
        }
    }
}