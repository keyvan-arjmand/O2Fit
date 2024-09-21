﻿using Microsoft.AspNetCore.Mvc.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Track.Api.Filters
{
    public class UnauthorizedResponsesOperationFilter : IOperationFilter
    {
        private readonly bool _includeUnauthorizedAndForbiddenResponses;
        private readonly string _schemeName;

        public UnauthorizedResponsesOperationFilter(bool includeUnauthorizedAndForbiddenResponses, string schemeName = "Bearer")
        {
            _includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            _schemeName = schemeName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;
            var metaData = context.ApiDescription.ActionDescriptor.EndpointMetadata;

            var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter) || metaData.Any(p => p is AllowAnonymousAttribute);
            if (hasAnonymous) return;


            if (_includeUnauthorizedAndForbiddenResponses)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
            }

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Scheme = _schemeName,
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
                    },
                    Array.Empty<string>() //new[] { "readAccess", "writeAccess" }
                }
            });
        }
    }
}
