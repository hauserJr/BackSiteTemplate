#pragma checksum "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "42174538d57f962294cf45a4bc4287eda98cdcc0"
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
#line 1 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\_ViewImports.cshtml"
using BackSiteTemplate;

#line default
#line hidden
#line 2 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\_ViewImports.cshtml"
using BackSiteTemplate.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42174538d57f962294cf45a4bc4287eda98cdcc0", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03cf4a3eef6bb23fd8bd938fae26896866a48696", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("LoginData"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Login", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 477, true);
            WriteLiteral(@"<div style=""width: 100%;margin-left: auto;margin-right: auto;height:20px;"">
    <div class=""line_head"" style=""width:100%;margin: 30px 0px 0px 0px;""></div>
</div>
<div class=""modal-dialog modal-dialog-centered"" role=""document"">
    <div class=""modal-content"">
        <div class=""modal-header"">
            <h5 class=""modal-title"" id=""exampleModalLongTitle"">後台系統</h5>
        </div>
        <div class=""modal-body"">
            <div class=""container"">
                ");
            EndContext();
            BeginContext(477, 876, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "411099c9e50c4984be7380952335b5b4", async() => {
                BeginContext(539, 22, true);
                WriteLiteral("\r\n                    ");
                EndContext();
                BeginContext(562, 23, false);
#line 12 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\Home\Index.cshtml"
               Write(Html.AntiForgeryToken());

#line default
#line hidden
                EndContext();
                BeginContext(585, 168, true);
                WriteLiteral("\r\n                    <div class=\"input-group form-group\">\r\n                        <input type=\"text\" class=\"form-control\" name=\"Account\" id=\"Account\" placeholder=\"帳號\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 753, "\"", 781, 1);
#line 14 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\Home\Index.cshtml"
WriteAttributeValue("", 761, TempData["Account"], 761, 20, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(782, 212, true);
                WriteLiteral(" required autofocus>\r\n                    </div>\r\n                    <div class=\"input-group form-group\">\r\n                        <input type=\"password\" class=\"form-control\" name=\"Pwd\" id=\"Pwd\" placeholder=\"密碼\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 994, "\"", 1018, 1);
#line 17 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\Home\Index.cshtml"
WriteAttributeValue("", 1002, TempData["Pwd"], 1002, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1019, 102, true);
                WriteLiteral(" required>\r\n                    </div>\r\n                    <small id=\"ErrorCode\" class=\"text-danger\">");
                EndContext();
                BeginContext(1122, 20, false);
#line 19 "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\Home\Index.cshtml"
                                                         Write(TempData["LoginMeg"]);

#line default
#line hidden
                EndContext();
                BeginContext(1142, 204, true);
                WriteLiteral("</small>\r\n                    <div class=\"modal-footer d-flex\">\r\n                        <button type=\"submit\" class=\"btn btn-lg btn-primary mt-1\">登入</button>\r\n                    </div>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1353, 78, true);
            WriteLiteral("\r\n            </div> <!-- /container -->\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            EndContext();
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
