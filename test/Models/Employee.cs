using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Класс, представляющий модель данных для сотрудника
namespace test.Models
{
    public class Employee : IComparable<Employee>
    {
        // Атрибут Key указывает, что свойство Id является первичным ключом в таблице базы данных
        [Key]
        public int Id { get; set; }

        // Свойство Name представляет имя сотрудника
        public string Name { get; set; }

        // Свойство Email представляет электронную почту сотрудника
        public string Email { get; set; }

        // Свойство Projects представляет список проектов, в которых участвует сотрудник
        [ValidateNever]
        public List<Project> Projects { get; set; } = new();

        // Свойство ManagingProjects представляет список проектов, которыми управляет сотрудник
        [ValidateNever]
        public List<EmpProj> ManagingProjects { get; set; } = new();

        // Свойство AuthorizedTasks представляет список задач, для которых сотрудник является автором
        [ValidateNever]
        public List<Task> AuthorizedTasks { get; set; }

        // Свойство PerformingTasks представляет список задач, которые сотрудник выполняет
        [ValidateNever]
        public List<Task> PerformingTasks { get; set; }

        // Реализация интерфейса IComparable<Employee> для сравнения сотрудников
        public int CompareTo(Employee? other)
        {
            // Если other равен null, то текущий сотрудник считается меньше
            if (other == null) return -1;

            // Сравниваем Id текущего сотрудника с Id другого сотрудника
            if (Id == other.Id) return 0;
            else return 1;
        }
    }

    // Перечисление UserRole представляет роли сотрудников
    public enum UserRole
    {
        Admin,
        Manager,
        Employer
    }
}
