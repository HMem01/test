using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// Класс, представляющий модель данных для отображения связи между проектом и сотрудником
namespace test.Models
{
    public class EmpProj : IComparable<EmpProj>
    {
        // Свойство ProjectID представляет идентификатор проекта
        public int ProjectID { get; set; }

        // Свойство EmployeeID представляет идентификатор сотрудника
        public int EmployeeID { get; set; }

        // Свойство Project представляет проект, связанный с данной связью проекта и сотрудника
        [ValidateNever]
        public Project Project { get; set; }

        // Свойство Employee представляет сотрудника, связанного с данной связью проекта и сотрудника
        [ValidateNever]
        public Employee Employee { get; set; }

        // Реализация интерфейса IComparable<EmpProj> для сравнения связей проекта и сотрудника
        public int CompareTo(EmpProj? other)
        {
            // Если other равен null, то текущая связь считается меньше
            if (other == null) return -1;

            // Сравниваем ProjectID и EmployeeID текущей связи с ProjectID и EmployeeID другой связи
            if (ProjectID == other.ProjectID && EmployeeID == other.EmployeeID) return 0;
            else return 1;
        }
    }

    // Перечисление RoleTypes представляет типы ролей
    public enum RoleTypes
    {
        Employee = 1,
        ProjectManager,
        Leader
    }
}
