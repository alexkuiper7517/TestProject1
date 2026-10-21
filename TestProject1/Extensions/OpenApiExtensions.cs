
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;

namespace TestProject1.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddApiDocumentation(
        this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            // Define the Bearer authentication scheme
            options.AddDocumentTransformer(
                (document, context, cancellationToken) =>
                {
                    document.Components ??= new OpenApiComponents();

                    document.Components.SecuritySchemes ??=
                        new Dictionary<string, IOpenApiSecurityScheme>();

                    document.Components.SecuritySchemes["Bearer"] =
                        new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT",
                            Description = "Enter your JWT access token"
                        };

                    return Task.CompletedTask;
                });

            // Apply authentication to protected endpoints
            options.AddOperationTransformer(
                (operation, context, cancellationToken) =>
                {
                    var metadata =
                        context.Description.ActionDescriptor.EndpointMetadata;

                    var requiresAuth =
                        metadata.OfType<IAuthorizeData>().Any();

                    var allowsAnonymous =
                        metadata.OfType<IAllowAnonymous>().Any();

                    if (requiresAuth && !allowsAnonymous)
                    {
                        operation.Security ??= [];

                        operation.Security.Add(
                            new OpenApiSecurityRequirement
                            {
                                [new OpenApiSecuritySchemeReference(
                                    "Bearer", context.Document!)] = []
                            });
                    }

                    return Task.CompletedTask;
                });
        });

        return services;
    }
}
