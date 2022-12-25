using Microsoft.AspNetCore.Mvc.ApplicationParts;
using PiszczekSzpotek.BookCatalogue.API.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Reflection;
using System.Configuration;
using System.Text.Json;

namespace API
{
    public class Program
    {
        private static Assembly? assembly = null;
        private static IDAO? dao = null;
        private static Type? authorType = null;
        private static Type? bookType = null;
        private static Type? reviewType = null;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        }

        public static IDAO GetDAO()
        {
            if (dao == null)
            {
                Type daoObjectType = GetTypeFromAssembly(typeof(IDAO));
                dao = Activator.CreateInstance(daoObjectType) as IDAO ?? throw new InvalidAssemblyException();
            }
            return dao;
        }

        public static Type GetAuthorType()
        {
            if (authorType == null)
            {
                authorType = GetTypeFromAssembly(typeof(IAuthor));
            }
            return authorType;
        }

        public static Type GetBookType()
        {
            if (bookType == null)
            {
                bookType = GetTypeFromAssembly(typeof(IBook));
            }
            return bookType;
        }

        public static Type GetReviewType()
        {
            if (reviewType == null)
            {
                reviewType = GetTypeFromAssembly(typeof(IReview));
            }
            return reviewType;
        }

        public static Type GetTypeFromAssembly(Type typeName)
        {
            if (assembly == null)
            {
                LoadAssembly();
            }
            var types = assembly.GetTypes();
            foreach (var type in types.Where(t => t.GetInterfaces().Contains(typeName)))
            {
                return type;
            }
            throw new InvalidAssemblyException();
        }

        private static void LoadAssembly()
        {
            if (assembly == null)
            {
                string assemblyName = System.Configuration.ConfigurationManager.AppSettings["dbName"] ?? throw new AssemblyNameNotSetException("Set dbName property in app.config file.");
                assembly = Assembly.UnsafeLoadFrom(assemblyName) ?? throw new InvalidAssemblyException();
            }
        }
    }
}