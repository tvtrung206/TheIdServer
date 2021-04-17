﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2021 @Olivier Lefebvre
using Aguacongas.TheIdServer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtentsions
    {
        public static IServiceCollection AddTheIdServerStores(this IServiceCollection services, Type userType, Type roleType, Func<IServiceProvider, Task<HttpClient>> getHttpClient)
        {
            services.AddTheIdServerStores(userType, roleType);
            services.AddIdentityServer4AdminHttpStores(getHttpClient);

            return services;
        }

        public static void AddTheIdServerStores(this IServiceCollection services, Type userType, Type roleType)
        {
            var userOnlyStoreType = typeof(UserOnlyStore<>).MakeGenericType(userType);

            if (roleType != null)
            {
                var userStoreType = typeof(UserStore<,>).MakeGenericType(userType, roleType);
                var roleStoreType = typeof(RoleStore<>).MakeGenericType(roleType);

                services.TryAddScoped(typeof(UserOnlyStore<>)
                    .MakeGenericType(userType),
                        provider => provider.CreateUserOnlyStore(userOnlyStoreType));
                services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType),
                    provider => provider.CreateUserStore(userOnlyStoreType, userStoreType));
                services.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType),
                        provider => provider.CreateRoleStore(roleStoreType));
            }
            else
            {   // No Roles
                services.TryAddScoped(typeof(IUserStore<>)
                    .MakeGenericType(userType), provider => provider.CreateUserOnlyStore(userOnlyStoreType));
            }
        }
    }
}
