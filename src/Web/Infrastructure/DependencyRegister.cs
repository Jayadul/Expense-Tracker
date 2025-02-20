﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Common;
using Repositories.Helper;
using Repositories.UnitOfWorks;
using Services.ExpenseCategoryManager;
using Services.ExpenseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Infrastructure
{
    public static class DependencyRegister
    {
        public static void DependencyRegisterLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IExpenseCategoryService, ExpenseCategoryService>();
            services.AddTransient<IExpenseService, ExpenseService>();          
            services.AddTransient<IAuthenticatedUser, AuthenticatedUser>();          
            services.AddTransient<IDateTimeService, DateTimeService>();          
        }
    }
}
