using Microsoft.AspNetCore.Mvc.ApplicationParts;
using PiszczekSzpotek.BookCatalogue.API.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Reflection;
using System.Configuration;

namespace API
{
    public class Program
    {
        private static IDAO? dao = null;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.AddControllers();
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
                string assemblyPath = System.Configuration.ConfigurationManager.AppSettings["dbName"];

                Type daoObjectType = LoadAssembly(assemblyPath, typeof(IDAO)) ?? throw new InvalidAssemblyException();
                //Console.WriteLine(daoObjectType);
                dao = Activator.CreateInstance(daoObjectType) as IDAO;
            }
            return dao;
        }

        public static Type GetTypeFromAssembly(Type interfaceType)
        {
            string assemblyPath = System.Configuration.ConfigurationManager.AppSettings["dbName"];
            return LoadAssembly(assemblyPath, interfaceType);
        }

        private static Type LoadAssembly(string assemblyName, Type typeName)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(assemblyName);
            var types = assembly.GetTypes();
            foreach (var type in types.Where(t => t.GetInterfaces().Contains(typeName)))
            {
                return type;
            }
            throw new InvalidAssemblyException();
        }
    }
}