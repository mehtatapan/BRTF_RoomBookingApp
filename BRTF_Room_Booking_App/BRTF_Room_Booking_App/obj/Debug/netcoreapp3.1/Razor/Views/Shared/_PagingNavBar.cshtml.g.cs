#pragma checksum "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8f298431fa2bc1f97ed70b7d0aff9d9cbb344f3e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PagingNavBar), @"mvc.1.0.view", @"/Views/Shared/_PagingNavBar.cshtml")]
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
#line 1 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8f298431fa2bc1f97ed70b7d0aff9d9cbb344f3e", @"/Views/Shared/_PagingNavBar.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PagingNavBar : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PageSizeModal", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "8f298431fa2bc1f97ed70b7d0aff9d9cbb344f3e3484", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
  
    if (Model.TotalPages == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <ul class=""pagination justify-content-center"" style=""margin:20px 0"">
            <li class=""page-item"">
                <button type=""button"" title=""Click to change page size."" data-toggle=""modal"" data-target=""#pageSizeModal"" class=""btn btn-primary"">
                    No Data to Show
                </button>
            </li>
        </ul>
");
#nullable restore
#line 12 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
    }
    else if (Model.TotalPages == 1)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <ul class=""pagination justify-content-center"" style=""margin:20px 0"">
            <li class=""page-item"">
                <button type=""button"" title=""Click to change page size."" data-toggle=""modal"" data-target=""#pageSizeModal"" class=""btn btn-primary"">
                    Page ");
#nullable restore
#line 18 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                    Write(Model.PageIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral(" of ");
#nullable restore
#line 18 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                        Write(Model.TotalPages);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </button>\r\n            </li>\r\n        </ul>\r\n");
#nullable restore
#line 22 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
    }
    else
    {
        var jumpAmount = (Model.TotalPages > 25) ? 10 : 5;
        var prevDisabled = !Model.HasPreviousPage ? "disabled='disabled'" : "";
        var nextDisabled = !Model.HasNextPage ? "disabled='disabled'" : "";
        var stepBack = (Model.PageIndex <= jumpAmount) ? 1 : Model.PageIndex - jumpAmount;
        var stepForward = (Model.PageIndex + jumpAmount > Model.TotalPages) ? Model.TotalPages : Model.PageIndex + jumpAmount;

#line default
#line hidden
#nullable disable
            WriteLiteral("        <ul class=\"pagination justify-content-center\" style=\"margin:20px 0\">\r\n            <li class=\"page-item\">\r\n                <button type=\"submit\" name=\"page\" value=\"1\" ");
#nullable restore
#line 32 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                       Write(prevDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"btn page-link\">\r\n                    <span aria-hidden=\"true\">&lArr;</span>&nbsp;First\r\n                </button>\r\n            </li>\r\n            <li class=\"page-item d-none d-md-inline\">\r\n                <button type=\"submit\" name=\"page\"");
            BeginWriteAttribute("value", " value=\"", 1771, "\"", 1790, 1);
#nullable restore
#line 37 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
WriteAttributeValue("", 1779, stepBack, 1779, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 37 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                 Write(prevDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        data-toggle=\"tooltip\" title=\"Jump Back ");
#nullable restore
#line 38 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                           Write(Model.PageIndex-stepBack);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Pages\" class=\"btn page-link\">\r\n                    <span aria-hidden=\"true\">&lArr;</span>\r\n                </button>\r\n            </li>\r\n            <li class=\"page-item\">\r\n                <button type=\"submit\" name=\"page\" ");
#nullable restore
#line 43 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                             Write(prevDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" value=\"");
#nullable restore
#line 43 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                   Write(Model.PageIndex - 1);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" class=""btn page-link"">
                    &nbsp;<span aria-hidden=""true"">&larr;</span>&nbsp;<span class=""d-none d-md-inline"">Previous</span>
                </button>
            </li>
            <li class=""page-item"">
                <button type=""button"" title=""Click to change page size."" data-toggle=""modal"" data-target=""#pageSizeModal"" class=""btn btn-primary"">
                    <span class=""d-none d-md-inline"">Pg. </span>");
#nullable restore
#line 49 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                           Write(Model.PageIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral(" of ");
#nullable restore
#line 49 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                               Write(Model.TotalPages);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </button>\r\n            </li>\r\n            <li class=\"page-item\">\r\n                <button type=\"submit\" name=\"page\" ");
#nullable restore
#line 53 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                             Write(nextDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" value=\"");
#nullable restore
#line 53 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                   Write(Model.PageIndex + 1);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""" class=""btn page-link"">
                    <span class=""d-none d-md-inline"">Next</span>&nbsp;<span aria-hidden=""true"">&rarr;</span>&nbsp;
                </button>
            </li>
            <li class=""page-item d-none d-md-inline"">
                <button type=""submit"" name=""page"" ");
#nullable restore
#line 58 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                             Write(nextDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" value=\"");
#nullable restore
#line 58 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                   Write(stepForward);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" title=\"Jump Forward ");
#nullable restore
#line 58 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                                                       Write(stepForward-Model.PageIndex);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Pages\" class=\"btn page-link\">\r\n                    <span aria-hidden=\"true\">&rArr;</span>\r\n                </button>\r\n            </li>\r\n            <li class=\"page-item\">\r\n                <button type=\"submit\" name=\"page\" ");
#nullable restore
#line 63 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                             Write(nextDisabled);

#line default
#line hidden
#nullable disable
            WriteLiteral(" value=\"");
#nullable restore
#line 63 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
                                                                   Write(Model.TotalPages);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"btn page-link\">\r\n                    Last&nbsp;<span aria-hidden=\"true\">&rArr;</span>\r\n                </button>\r\n            </li>\r\n        </ul>\r\n");
#nullable restore
#line 68 "C:\Users\mehta\Downloads\Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Shared\_PagingNavBar.cshtml"
    }

#line default
#line hidden
#nullable disable
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
