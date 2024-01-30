using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models
{
    // Класс, представляющий модель данных для отображения информации о задаче.
    public class Task : IComparable<Task>
    {
        // Свойство Id представляет идентификатор задачи.
        [Key]
        public int Id { get; set; }

        // Свойство Name представляет имя задачи.
        public string Name { get; set; }

        // Свойство AuthorID представляет идентификатор автора задачи.
        public int AuthorID { get; set; }

        // Свойство WorkerID представляет идентификатор исполнителя задачи.
        public int WorkerID { get; set; }

        // Свойство ProjectID представляет идентификатор проекта, к которому относится задача.
        public int ProjectID { get; set; }

        // Свойство Author представляет автора задачи.
        [ValidateNever]
        public Employee Author { get; set; }

        // Свойство Worker представляет исполнителя задачи.
        [ValidateNever]
        public Employee Worker { get; set; }

        // Свойство Project представляет проект, к которому относится задача.
        [ValidateNever]
        public Project Project { get; set; }

        // Свойство Status представляет статус задачи (используется перечисление TaskStatus).
        public int Status { get; set; }

        // Свойство Description представляет описание задачи.
        public string Description { get; set; }

        // Свойство Priority представляет приоритет задачи.
        public int Priority { get; set; }

        // Метод CompareTo реализует интерфейс IComparable<Task> для сравнения задач по их идентификаторам.
        public int CompareTo(Task? other)
        {
            // Если other равен null, то текущая задача считается меньше.
            if (other == null) return -1;

            // Сравниваем Id текущей задачи с Id другой задачи.
            if (Id == other.Id) return 0;
            else return 1;
        }
    }

    // Перечисление TaskStatus представляет возможные статусы задачи.
    public enum TaskStatus : int
    {
        ToDo = 0,
        InProgress = 1,
        Done = 2,
    }
}
