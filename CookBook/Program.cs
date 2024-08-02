using CookBook.Services;
using CookBook.UI;
using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace CookBook
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            ServiceCollection services = ConfigureServices();
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var startForm = serviceProvider.GetRequiredService<HomeForm>();
            Application.Run(startForm);
        }

        static ServiceCollection ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

                services.AddTransient<IIngredientsRepository>(_ => new IngredientsRepository());
            services.AddTransient<IRecipeTypesRepository>(_ => new RecipeTypesRepository());
            services.AddTransient<IRecipesRepository>(_ => new RecipesRepository());
            services.AddTransient<IRecipeIngredientsRepository>(_ => new RecipeIngredientsRepository());


            services.AddTransient<IngredientsForm>();
            services.AddTransient<RecipesForm>();
            services.AddTransient<RecipeTypesForm>();
            services.AddTransient<RecipeIngredientsForm>();
            services.AddTransient<AmountForm>();
            services.AddTransient<FoodManagerForm>();
            services.AddTransient<HomeForm>();

            services.AddTransient<FoodManagerCache>();

            return services;

        }

    }
}