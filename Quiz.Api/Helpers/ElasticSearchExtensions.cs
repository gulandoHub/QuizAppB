using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;


namespace QuizApi.Helpers
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url)).DefaultIndex(defaultIndex);
          
            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}