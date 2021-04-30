using System;
using System.Linq;
using HartmannPractical.Data;
using Microsoft.Extensions.DependencyInjection;

namespace HartmannPractical
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            /*IByteSerializer serializer = null; 
            IPracticalTest test = new PracticalTest(serializer);*/

            IPracticalTest test = serviceProvider.GetRequiredService<PracticalTest>();

            if (test.Validate())
                Console.WriteLine("IByteSerializer implementation is correct");
            else
                Console.WriteLine("IByteSerializer implemetnation is not correct");

            Console.Read();
        }
        private static IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IByteSerializer, ByteSerializer>();
            services.AddTransient<PracticalTest>();  

            return services.BuildServiceProvider();
        }
    }
}
