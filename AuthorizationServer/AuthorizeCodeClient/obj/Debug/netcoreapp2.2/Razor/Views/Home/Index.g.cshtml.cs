#pragma checksum "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9ab36c57ed39cc9c33b70cfc56d2ef8d21851c31"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\_ViewImports.cshtml"
using AuthorizeCodeClient;

#line default
#line hidden
#line 2 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\_ViewImports.cshtml"
using AuthorizeCodeClient.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9ab36c57ed39cc9c33b70cfc56d2ef8d21851c31", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e3632d6d85bda845a59e2e5cce0e1c9d9e08e443", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 19, true);
            WriteLiteral("\r\n<h2>Claims</h2>\r\n");
            EndContext();
#line 6 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
 if (User.Identity.IsAuthenticated)
{

#line default
#line hidden
            BeginContext(104, 23, true);
            WriteLiteral("    <p>\r\n        <dl>\r\n");
            EndContext();
#line 10 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
             foreach (var claim in this.Context.Request.HttpContext..(""))
            {

#line default
#line hidden
            BeginContext(218, 20, true);
            WriteLiteral("                <dt>");
            EndContext();
            BeginContext(239, 10, false);
#line 12 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
               Write(claim.Type);

#line default
#line hidden
            EndContext();
            BeginContext(249, 27, true);
            WriteLiteral("</dt>\r\n                <dd>");
            EndContext();
            BeginContext(277, 11, false);
#line 13 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
               Write(claim.Value);

#line default
#line hidden
            EndContext();
            BeginContext(288, 7, true);
            WriteLiteral("</dd>\r\n");
            EndContext();
#line 14 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(310, 25, true);
            WriteLiteral("        </dl>\r\n    </p>\r\n");
            EndContext();
#line 17 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
}
else
{

#line default
#line hidden
            BeginContext(347, 23, true);
            WriteLiteral("    <p>anonymous.</p>\r\n");
            EndContext();
#line 21 "E:\Code\ID4AndOcelot\AuthorizationServer\AuthorizeCodeClient\Views\Home\Index.cshtml"
}

#line default
#line hidden
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
