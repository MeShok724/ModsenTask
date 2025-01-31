using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ModsenTask.Application.Mapping;
using ModsenTask.Application.Services;
using ModsenTask.Infrastructure.Persistence;
using ModsenTask.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using ModsenTask.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// ����������� dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ������������ ������������
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// ����������� ��������
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

// ����������� auto mapper
builder.Services.AddAutoMapper(typeof(AuthorProfile), typeof(BookProfile));

builder.Services.AddControllers();

// ����������� fluent validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<BookRequestValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
