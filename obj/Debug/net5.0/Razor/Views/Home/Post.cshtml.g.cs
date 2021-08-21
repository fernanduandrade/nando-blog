#pragma checksum "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "09c847911eff3ce36e8940106d6cb3d8ec44ce46"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Post), @"mvc.1.0.view", @"/Views/Home/Post.cshtml")]
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
#nullable restore
#line 1 "/home/fernando/Área de Trabalho/nando-blog/Views/_ViewImports.cshtml"
using my_blog.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/fernando/Área de Trabalho/nando-blog/Views/_ViewImports.cshtml"
using my_blog.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"09c847911eff3ce36e8940106d6cb3d8ec44ce46", @"/Views/Home/Post.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f654ebb0f6de6782c04a83b00e20e23952fbe503", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Post : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Post>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
  
    ViewBag.Title = Model.Title;
    ViewBag.Description = Model.Description;
    ViewBag.Keywords = $"{Model.Tags?.Replace(",", " ")} {Model.Category}";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            WriteLiteral("\n<div class=\"container\">\n    <div class=\"post no-shadow\">\n");
#nullable restore
#line 21 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
         if (!String.IsNullOrEmpty(Model.Image))
        {
            var imgPath = $"/Image/{Model.Image}";

#line default
#line hidden
#nullable disable
            WriteLiteral("            <img");
            BeginWriteAttribute("src", " src=\"", 532, "\"", 546, 1);
#nullable restore
#line 24 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
WriteAttributeValue("", 538, imgPath, 538, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\n            <span class=\"title\">");
#nullable restore
#line 25 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
                           Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\n");
#nullable restore
#line 26 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n    <div class=\"post-body\">\n        ");
#nullable restore
#line 29 "/home/fernando/Área de Trabalho/nando-blog/Views/Home/Post.cshtml"
   Write(Html.Raw(@Model.Body));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    </div>\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Post> Html { get; private set; }
    }
}
#pragma warning restore 1591
