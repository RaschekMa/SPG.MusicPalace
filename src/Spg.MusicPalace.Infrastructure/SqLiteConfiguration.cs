using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MusicPalace.Infrastructure
{
    public static class SqlLiteConfiguration
    {
        public static void ConfigureSqLite(this IServiceCollection services)
        {
            services
                .AddDbContext<MusicPalaceDbContext>(options =>
                {
                    if (!options.IsConfigured)
                    {
                        options.UseSqlite($"Data Source=MusicPalace.db");
                    }
                });
        }
    }
}
