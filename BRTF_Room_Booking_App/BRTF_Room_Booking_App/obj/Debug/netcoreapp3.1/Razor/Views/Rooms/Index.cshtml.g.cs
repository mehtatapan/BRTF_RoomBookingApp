#pragma checksum "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5d86ebbf6ad83e96a5526feb1e84d542e0316f4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Rooms_Index), @"mvc.1.0.view", @"/Views/Rooms/Index.cshtml")]
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
#line 1 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5d86ebbf6ad83e96a5526feb1e84d542e0316f4", @"/Views/Rooms/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_Rooms_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BRTF_Room_Booking_App.Models.Room>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn mx-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:75px; background:#2F7DBB; color:#E4E6E6; font-weight:400;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PagingNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
  
    ViewData["Title"] = "Rooms Index";

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
 if (TempData["AlertMessage"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n    <div class=\"alert alert-success\">\r\n        ");
#nullable restore
#line 11 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
   Write(Html.Raw(TempData["AlertMessage"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <br />\r\n");
#nullable restore
#line 14 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
 if (TempData["Message"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n    <div class=\"alert alert-success\">\r\n        <strong>");
#nullable restore
#line 19 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
           Write(Html.Raw(TempData["Message"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n    <br />\r\n");
#nullable restore
#line 22 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f49646", async() => {
                WriteLiteral("\r\n<br />\r\n<h1 class=\"mb-4 text-center\">Rooms</h1>\r\n<br />\r\n\r\n<section style=\"background-color: #FFF;display: block; border-radius: 8px; box-shadow: 0 15px 25px rgba(0,0,50,0.2);\" class=\"px-3 py-3 my-3\">\r\n    <p>\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f410141", async() => {
                    WriteLiteral("Create New");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </p>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f411468", async() => {
                    WriteLiteral("\r\n        <input type=\"hidden\" name=\"sortDirection\"");
                    BeginWriteAttribute("value", " value=\"", 840, "\"", 874, 1);
#nullable restore
#line 34 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
WriteAttributeValue("", 848, ViewData["sortDirection"], 848, 26, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n        <input type=\"hidden\" name=\"sortField\"");
                    BeginWriteAttribute("value", " value=\"", 925, "\"", 955, 1);
#nullable restore
#line 35 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
WriteAttributeValue("", 933, ViewData["sortField"], 933, 22, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n        <div class=\"form-horizontal\">\r\n            <button");
                    BeginWriteAttribute("class", " class=\"", 1019, "\"", 1052, 2);
                    WriteAttributeValue("", 1027, "btn", 1027, 3, true);
#nullable restore
#line 37 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
WriteAttributeValue(" ", 1030, ViewData["Filter"], 1031, 21, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" type=\"button\" data-toggle=\"collapse\" id=\"filterToggle\" data-target=\"#collapseFilter\" aria-expanded=\"false\" aria-controls=\"collapseFilter\">\r\n                Filter/Search\r\n            </button>\r\n            <div");
                    BeginWriteAttribute("class", " class=\"", 1264, "\"", 1305, 2);
                    WriteAttributeValue("", 1272, "collapse", 1272, 8, true);
#nullable restore
#line 40 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
WriteAttributeValue(" ", 1280, ViewData["Filtering"], 1281, 24, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(@" id=""collapseFilter"">
                <div class=""card card-body bg-light"">
                    <div class=""row"">
                        <div class=""form-group col-md-4"">
                            <label class=""control-label"">Filter by Is Enabled:</label>
                            ");
#nullable restore
#line 45 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.DropDownList("EnabledFilterString", new List<SelectListItem>
                       {   new SelectListItem { Text="Show All", Value="All"},
                           new SelectListItem { Text="Show Enabled", Value="True"},
                           new SelectListItem { Text="Show Disabled", Value="False"}
                       }, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        </div>\r\n                        <div class=\"form-group col-md-4\">\r\n                            <label class=\"control-label\">Filter by Room:</label>\r\n                            ");
#nullable restore
#line 53 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.TextBox("SearchRoom", null, "All Rooms", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        </div>\r\n                        <div class=\"form-group col-md-4\">\r\n                            <label class=\"control-label\">Filter by Area:</label>\r\n                            ");
#nullable restore
#line 57 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.DropDownList("RoomGroupID", null, "All Areas", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                    WriteLiteral(@"
                        </div>
                        <div class=""form-group col-md-4 align-self-end"">
                            <input type=""submit"" name=""actionButton"" value=""Filter"" class=""btn btn-outline-primary"" />
                            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f416374", async() => {
                        WriteLiteral("Clear");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral(@"
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <table class=""table"">
            <thead>
                <tr>
                    <th>
                        <input type=""submit"" name=""actionButton"" value=""Room"" class=""btn btn-link"" style=""font-weight:700""/>
                    </th>
                    <th>
                        <input type=""submit"" name=""actionButton"" value=""Area"" class=""btn btn-link"" style=""font-weight:700""/>
                    </th>
                    <th>
                        <input type=""submit"" name=""actionButton"" value=""Is Enabled"" class=""btn btn-link"" style=""font-weight:700""/>
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 84 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 88 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.RoomName));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 91 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.RoomGroup.AreaName));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 94 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Enabled));

#line default
#line hidden
#nullable disable
                    WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f420212", async() => {
                        WriteLiteral("Edit");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                    {
                        throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                    }
                    BeginWriteTagHelperAttribute();
#nullable restore
#line 97 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                                                   WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n                            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f422894", async() => {
                        WriteLiteral("Details");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_8.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
                    if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                    {
                        throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                    }
                    BeginWriteTagHelperAttribute();
#nullable restore
#line 98 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                                                      WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n                            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f425582", async() => {
                        WriteLiteral("Delete");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_9.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
                    if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                    {
                        throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                    }
                    BeginWriteTagHelperAttribute();
#nullable restore
#line 99 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                                                     WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
                    __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                    __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                    __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 102 "C:\Users\mehta\Downloads\Prototype_3_v2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
                    WriteLiteral("            </tbody>\r\n        </table>\r\n        ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b5d86ebbf6ad83e96a5526feb1e84d542e0316f428603", async() => {
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_10.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_11.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n</section>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BRTF_Room_Booking_App.Models.Room>> Html { get; private set; }
    }
}
#pragma warning restore 1591
