using Microsoft.EntityFrameworkCore;
using test.Models;


// Класс контекста базы данных. Он наследуется от DbContext, предоставляемого Entity Framework Core
public class ApplicationContext : DbContext
{
    // DbSet представляет собой коллекции сущностей, связанных с таблицами в базе данных

    // DbSet для сущности Employee, представляющей сотрудника
    public DbSet<Employee> Employers { get; set; } = null!;

    // DbSet для сущности Company, представляющей компанию
    public DbSet<Company> Companys { get; set; } = null!;

    // DbSet для сущности Project, представляющей проект
    public DbSet<Project> Projects { get; set; } = null!;

    // DbSet для сущности EmpProj, представляющей отношение многие ко многим между сотрудниками и проектами
    public DbSet<EmpProj> EmpProjs { get; set; } = null!;

    // DbSet для сущности Task, представляющей задачу
    public DbSet<test.Models.Task> Tasks { get; set; } = null!;

    // Конструктор по умолчанию. Используется для создания контекста без параметров
    public ApplicationContext() : base() { }

    // Конструктор, принимающий параметры, такие как опции, которые можно использовать для настройки контекста
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        // При использовании опций, контекст пытается создать базу данных, если она ещё не создана
        Database.EnsureCreated();   

    }

    // Метод для настройки модели данных при создании контекста
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка таблицы Project с использованием ограничения CHECK для даты
        modelBuilder.Entity<Project>()
            .ToTable(t => t.HasCheckConstraint("ValidDate", "StartDate <= EndDate"));

        // Настройка составного ключа для сущности EmpProj (многие ко многим)

        modelBuilder.Entity<EmpProj>().HasKey(u => new { u.EmployeeID, u.ProjectID });

        // Настройка внешних ключей для задачи (Task) относительно сотрудников (Worker) и авторов (Author)

        modelBuilder.Entity<test.Models.Task>().HasOne(p => p.Worker).WithMany(t => t.PerformingTasks).HasForeignKey(p => p.WorkerID);

        modelBuilder.Entity<test.Models.Task>().HasOne(p => p.Author).WithMany(t => t.AuthorizedTasks).HasForeignKey(p => p.AuthorID);
    }

    // DbSet для сущности Task. Примечание: Здесь указано Task в единственном числе, следует обратить внимание на согласование с именами в других частях приложения
    public DbSet<test.Models.Task> Task { get; set; } = default!;

}
