using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    // Класс, представляющий модель данных для отображения информации о проекте
    public class Project : IComparable<Project>
    {
        // Свойство Id представляет идентификатор проекта
        [Key]
        public int Id { get; set; }

        // Свойство Name представляет имя проекта
        public string Name { get; set; }

        // Свойство Description представляет описание проекта
        public string Description { get; set; }

        // Свойство StartDate представляет дату начала проекта
        public DateOnly StartDate { get; set; }

        // Свойство EndDate представляет дату завершения проекта
        public DateOnly EndDate { get; set;}

        // Свойство Priority представляет приоритет проекта
        public int Priority { get; set; }

        // Свойство ManagerID представляет идентификатор менеджера проекта
        public int? ManagerID { get; set; }

        // Свойство CustomerID представляет идентификатор заказчика проекта
        public int? CustomerID { get; set; }

        // Свойство PerfomerID представляет идентификатор исполнителя проекта
        public int? PerfomerID { get; set; }

        // Свойство Manager представляет менеджера проекта
        public Employee? Manager { get; set; }

        // Свойство Customer представляет заказчика проекта
        public Company? Customer { get; set; }

        // Свойство Perfomer представляет исполнителя проекта
        public Company? Perfomer { get; set; }

        // Свойство Tasks представляет список задач, связанных с проектом
        public List<Task> Tasks { get; set; } = new();

        // Свойство Employers представляет список связей проекта и сотрудников
        public List<EmpProj> Employers { get; set; } = new();

        // Реализация интерфейса IComparable<Project> для сравнения проектов
        public int CompareTo(Project? other)
        {
            // Если other равен null, то текущий проект считается меньше
            if (other == null) return -1;

            // Сравниваем Id текущего проекта с Id другого проекта
            if (Id == other.Id) return 0;
            else return 1;
        }

    }
}
