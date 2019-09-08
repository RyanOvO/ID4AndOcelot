ID4 三种模式集成Ocelot

AuthorizationServer项目：授权服务器，集成了IdentityServer4，端口号 5000
OcelotDemo项目：网关层，集成了Ocelot，端口号 50005，将上游请求转发到下游
MvcClient项目：ClientCredentials模式的客户端，实现了客户端凭据认证
PasswordDemo项目：ResourceOwnerPassword模式的客户端，实现了密码模式的认证，==并支持基于Claim的角色验证==
ImplicitClient项目：Implicit模式的客户端，实现了简约模式的认证

