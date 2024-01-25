// -------------------------------------------------------------------------------- 
// <copyright file="AuthorizeCheckOperationFilter.cs" company="Sacyr S.A."> 
//       Copyright (c) Sacyr S.A. 
// </copyright> 
// --------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TheFullStackTeam.API.Configurations;

/// <summary>
/// Auth filter
/// </summary>
public class AuthorizeCheckOperationFilter : IOperationFilter
{
    /// <summary>
    /// Apply filter
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Check for authorize attribute
        var hasAuthorize = context.MethodInfo.ReflectedType != null
                           && (context.MethodInfo.ReflectedType
                                   .GetCustomAttributes(true)
                                   .OfType<AuthorizeAttribute>()
                                   .Any()
                               || context.MethodInfo
                                   .GetCustomAttributes(true)
                                   .OfType<AuthorizeAttribute>().Any());
        if (!hasAuthorize)
        {
            return;
        }

        operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

        operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "aad-jwt"
                    },
                    UnresolvedReference = true
                },
                new[] { "testapi" }
            }
        });
    }
}