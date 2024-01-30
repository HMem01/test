using Microsoft.EntityFrameworkCore;
using test.Models;
using test;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ ����������� � ���� ������ �� ������������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ���������� DbContext (ApplicationContext) � �������������� SQLite � �������� ���������� ���� ������
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

// ����������� ��������, ��������� � ������������� � ���������������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ������������ ���������� � ����������� �� ���������
if (!app.Environment.IsDevelopment())
{
    // � ������, ���� ���������� �� ��������� � ������ ����������, ��������� ����������� ������ � ��������� HSTS
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// �������� HTTP-�������� �� HTTPS � ������������ ����������� ������
app.UseHttpsRedirection();
app.UseStaticFiles();

// ��������� ������������� � �����������
app.UseRouting();
app.UseAuthorization();

// ��������� �������� ������������ �� ���������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ������ ����������
app.Run();
