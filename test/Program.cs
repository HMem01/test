using Microsoft.EntityFrameworkCore;
using test.Models;
using test;

var builder = WebApplication.CreateBuilder(args);

// Получение строки подключения к базе данных из конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Добавление DbContext (ApplicationContext) с использованием SQLite в качестве провайдера базы данных
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

// Регистрация сервисов, связанных с контроллерами и представлениями
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфигурация приложения в зависимости от окружения
if (!app.Environment.IsDevelopment())
{
    // В случае, если приложение не находится в режиме разработки, настройка обработчика ошибок и включение HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Редирект HTTP-запросов на HTTPS и обслуживание статических файлов
app.UseHttpsRedirection();
app.UseStaticFiles();

// Включение маршрутизации и авторизации
app.UseRouting();
app.UseAuthorization();

// Настройка маршрута контроллеров по умолчанию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запуск приложения
app.Run();
