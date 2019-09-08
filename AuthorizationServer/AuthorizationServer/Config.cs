using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new[]
            {
                new ApiResource("ClientCredentialsApi", "客户凭据模式"),
                new ApiResource("PasswordApi", "密码模式", new List<string>(){ "role" }),
                new ApiResource("ImplicitApi", "简约模式")
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "ClientCredentialsDemo",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new [] { "ClientCredentialsApi" } // 对应 ApiResource
                },
                new Client
                {
                    ClientId = "PasswordDemo",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new [] {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "PasswordApi"
                    }
                },
                new Client
                {
                    ClientId = "ImplicitClient",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ImplicitApi"
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static List<TestUser> Users()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "ryan",
                    Password = "password",
                    Claims = new List<Claim>{
                        new Claim("email", "1234567890@qq.com"),
                        new Claim("role", "superadmin")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "people",
                    Password = "123456",
                    Claims = new List<Claim>{
                        new Claim("role", "admin")
                    }
                }
            };
        }
    }
}
