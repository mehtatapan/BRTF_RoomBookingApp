#pragma checksum "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b57910106e0e7f881d05499f5207cf3edbecd38f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Users_Delete), @"mvc.1.0.view", @"/Views/Users/Delete.cshtml")]
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
#line 1 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b57910106e0e7f881d05499f5207cf3edbecd38f", @"/Views/Users/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_Users_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BRTF_Room_Booking_App.Models.User>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color:#E4E6E6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("justify-content-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b57910106e0e7f881d05499f5207cf3edbecd38f6718", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b57910106e0e7f881d05499f5207cf3edbecd38f6980", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b57910106e0e7f881d05499f5207cf3edbecd38f8862", async() => {
                WriteLiteral(@"
    <br/>
    <h1 class=""mb-4"">Delete User</h1>
    <br/>

    <section style=""background-color: #FFF;display: block; border-radius: 8px; box-shadow: 0 15px 25px rgba(0,0,50,0.2);"" class=""px-5 py-2 my-3"">
        <div>
            <br />
            <h4>Are you sure you want to delete this User?</h4>
            <br />
        </div>
        <div>
            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b57910106e0e7f881d05499f5207cf3edbecd38f9498", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 22 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                <dl class=\"row\">\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 25 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.Username));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 28 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.Username));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 31 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 34 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.FirstName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 37 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.MiddleName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 40 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.MiddleName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 43 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.LastName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 46 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.LastName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 49 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 52 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.Email));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 55 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.EmailBookingNotifications));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 58 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.EmailBookingNotifications));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 61 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.EmailCancelNotifications));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 64 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.EmailCancelNotifications));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        ");
#nullable restore
#line 67 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayNameFor(model => model.TermAndProgram));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 70 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(Html.DisplayFor(model => model.TermAndProgram.TermAndProgramSummary));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                    <dt class=\"col-sm-5\">\r\n                        Role\r\n                    </dt>\r\n                    <dd class=\"col-sm-7\">\r\n                        ");
#nullable restore
#line 76 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
                   Write(ViewBag.Role);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </dd>\r\n                </dl>\r\n        </div>\r\n            <br/>\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b57910106e0e7f881d05499f5207cf3edbecd38f18304", async() => {
                    WriteLiteral("\r\n                ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b57910106e0e7f881d05499f5207cf3edbecd38f18587", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 82 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ID);

#line default
#line hidden
#nullable disable
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n                <input type=\"submit\" value=\"Delete\" class=\"btn btn-danger\" style=\"width:100px;\" />\r\n                <a");
                    BeginWriteAttribute("href", " href=\'", 3507, "\'", 3536, 1);
#nullable restore
#line 84 "C:\Users\mehta\Downloads\Prototype_2_v6\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Users\Delete.cshtml"
WriteAttributeValue("", 3514, ViewData["returnURL"], 3514, 22, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" role=\"button\" class=\"btn btn-secondary mx-2\">Back to Users</a>\r\n            ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            <br/>\r\n    </section>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BRTF_Room_Booking_App.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
