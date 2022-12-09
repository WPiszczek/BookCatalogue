using PiszczekSzpotek.BookCatalogue.API.Exceptions;
using PiszczekSzpotek.BookCatalogue.Interfaces;
using System.Reflection;

namespace API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Specify DAO assembly path as parameter");
                return;
            }
            string assemblyPath = args[0];
            Type daoObjectType = loadAssembly(assemblyPath) ?? throw new InvalidAssemblyException();
            //Type DaoObjectType = loadAssembly("bin/Debug/net6.0/MockDatabase.dll") ?? throw new InvalidAssemblyException();
            //Type DaoObjectType = loadAssembly("bin/Debug/net6.0/SQLiteDatabase.dll") ?? throw new InvalidAssemblyException();

            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton(typeof(IDAO), daoObjectType);

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

        private static Type loadAssembly(string assemblyName)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyName);
            var types = assembly.GetTypes();
            foreach (var type in types.Where(t => t.GetInterfaces().Contains(typeof(IDAO))))
            {
                return type;
            }
            throw new InvalidAssemblyException();
        }
    }
}