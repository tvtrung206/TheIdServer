﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2022 @Olivier Lefebvre
using Aguacongas.IdentityServer.Store;
using IdentityModel;

namespace Microsoft.AspNetCore.Authorization
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddIdentityServerPolicies(this AuthorizationOptions options, bool checkAdminsScope = false, bool showSettings = false)
        {
            options.AddPolicy(SharedConstants.WRITERPOLICY, policy =>
                   policy.RequireAuthenticatedUser()
                    .RequireRole(SharedConstants.WRITERPOLICY)
                    .RequireAssertion(context => !checkAdminsScope || context.User.HasClaim(c => c.Type == JwtClaimTypes.Scope && c.Value == SharedConstants.ADMINSCOPE)));
            options.AddPolicy(SharedConstants.READERPOLICY, policy =>
                   policy.RequireAuthenticatedUser()
                   .RequireRole(SharedConstants.READERPOLICY)
                   .RequireAssertion(context => !checkAdminsScope || context.User.HasClaim(c => c.Type == JwtClaimTypes.Scope && c.Value == SharedConstants.ADMINSCOPE)));
            options.AddPolicy(SharedConstants.REGISTRATIONPOLICY, policy =>
                   policy.RequireAuthenticatedUser()
                    .RequireRole(SharedConstants.REGISTRATIONPOLICY));
            options.AddPolicy(SharedConstants.TOKENPOLICY, policy =>
                   policy.RequireAuthenticatedUser()
                    .RequireClaim(JwtClaimTypes.ClientId)
                    .RequireClaim(JwtClaimTypes.Scope, SharedConstants.TOKENSCOPES));
            AddSettingsPolicies(options, checkAdminsScope, showSettings);
        }

        private static void AddSettingsPolicies(AuthorizationOptions options, bool checkAdminsScope, bool showSettings)
        {
            options.AddPolicy(SharedConstants.DYNAMIC_CONFIGURATION_WRITTER_POLICY, policy => policy.RequireAuthenticatedUser()
                    .RequireRole(SharedConstants.WRITERPOLICY)
                    .RequireAssertion(context => !checkAdminsScope || context.User.HasClaim(c => c.Type == JwtClaimTypes.Scope && c.Value == SharedConstants.ADMINSCOPE)));
            options.AddPolicy(SharedConstants.DYNAMIC_CONFIGURATION_READER_POLICY, policy =>
                   policy.RequireAuthenticatedUser()
                   .RequireRole(SharedConstants.READERPOLICY)
                   .RequireAssertion(context => !checkAdminsScope || context.User.HasClaim(c => c.Type == JwtClaimTypes.Scope && c.Value == SharedConstants.ADMINSCOPE)));
        }
    }
}
