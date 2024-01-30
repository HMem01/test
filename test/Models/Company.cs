using System.ComponentModel.DataAnnotations;

// Класс представляющий модель данных для компании
namespace test.Models
{
    // Атрибут Key указывает, что свойство Id является первичным ключом в таблице базы данных
    public class Company : IComparable<Company>
    {
        [Key]
        public int Id { get; set; }

        // Свойство Name представляет название компании
        public string Name { get; set; }

        // Реализация интерфейса IComparable<Company> для сравнения компаний
        public int CompareTo(Company? other)
        {
            // Если other равен null, то текущая компания считается меньше
            if (other == null) return -1;

            // Сравниваем Id текущей компании с Id другой компании
            if (Id == other.Id) return 0;
            else return 1;
        }
    }
}
