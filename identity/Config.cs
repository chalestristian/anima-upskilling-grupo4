// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace identity
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "clientApi",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("tePONsUlaZErDINglOntrOcTriJo".Sha256())
                },
                AllowedScopes = { "api1" }
            },
            new Client
            {
                ClientId = "clientConsumerConsole",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("TfulATErsoDIRTiNeaTrecOUstasTORM".Sha256())
                },
                AllowedScopes = { "consumerConsole1" }
            }
        };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("api1", "API Principal para gestão de cursos e alunos"),
                new ApiResource("consumerConsole1", "Consumer console da fila de RabbitMQ")
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "API Principal para gestão de cursos e alunos"),
                new ApiScope("consumerConsole1", "Consumer console da fila de RabbitMQ"),
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
            };
    }
}