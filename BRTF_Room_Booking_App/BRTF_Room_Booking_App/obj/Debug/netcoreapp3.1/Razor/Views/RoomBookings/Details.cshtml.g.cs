#pragma checksum "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4043fa31dee0b1119815dfc60f20ebefe65a0d28"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_RoomBookings_Details), @"mvc.1.0.view", @"/Views/RoomBookings/Details.cshtml")]
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
#line 1 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4043fa31dee0b1119815dfc60f20ebefe65a0d28", @"/Views/RoomBookings/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_RoomBookings_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BRTF_Room_Booking_App.Models.RoomBooking>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/style.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("role", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mx-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color:#E4E6E6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("justify-content-left"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
  
    ViewData["Title"] = "Booking Details";

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
 if (TempData["AlertMessage"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n    <div class=\"alert alert-success\">\r\n        ");
#nullable restore
#line 10 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
   Write(Html.Raw(TempData["AlertMessage"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <br />\r\n");
#nullable restore
#line 13 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
}

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
 if (TempData["Message"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n    <div class=\"alert alert-success\">\r\n        <strong>");
#nullable restore
#line 18 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
           Write(Html.Raw(TempData["Message"]));

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>\r\n    </div>\r\n    <br />\r\n");
#nullable restore
#line 21 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d288984", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4043fa31dee0b1119815dfc60f20ebefe65a0d289246", async() => {
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
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2811132", async() => {
                WriteLiteral(@"

    <br />
    <h1 class=""mb-4 text-center"">Details</h1>
    <br />

    <div class=""container-fluid d-block d-lg-none"" style=""display:block;"">
        <div class=""card-deck-wrapper"">
            <div class=""card-deck"">
                <div class=""card mb-sm-3"" style=""min-width: 18rem;"">
                    <h5 class=""card-header text-center"">Booking</h5>
                    <div class=""card-body"">
                        <div class=""media-body ml-2"">
                            <div class=""card-text mb-3""><strong>Full Name: </strong> ");
#nullable restore
#line 40 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                                Write(Html.DisplayFor(model => model.User.FullName));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text mb-3\"><strong>Special Notes: </strong> ");
#nullable restore
#line 41 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                                    Write(Html.DisplayFor(model => model.SpecialNotes));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text mb-3\"><strong>Start: </strong> ");
#nullable restore
#line 42 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                            Write(Html.DisplayFor(model => model.StartDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text mb-3\"><strong>End: </strong> ");
#nullable restore
#line 43 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                          Write(Html.DisplayFor(model => model.EndDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text mb-3\"><strong>Area: </strong> ");
#nullable restore
#line 44 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                           Write(Html.DisplayFor(model => model.Room.RoomGroup.AreaName));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text mb-3\"><strong>Room: </strong> ");
#nullable restore
#line 45 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                           Write(Html.DisplayFor(model => model.Room.RoomName));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <div class=\"card-text\"><strong>Approval Status: </strong> ");
#nullable restore
#line 46 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                                                 Write(Html.DisplayFor(model => model.ApprovalStatus));

#line default
#line hidden
#nullable disable
                WriteLiteral("</div>\r\n                            <br />\r\n                        </div>\r\n                        <div class=\"card-footer d-flex justify-content-center\">\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2815324", async() => {
                    WriteLiteral("Edit");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 50 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                                   WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            <a");
                BeginWriteAttribute("href", " href=\'", 2340, "\'", 2369, 1);
#nullable restore
#line 51 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
WriteAttributeValue("", 2347, ViewData["returnURL"], 2347, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" role=""button"" class=""btn btn-secondary mx-2"">Back to Bookings</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <section style=""background-color: #FFF;display: block; border-radius: 8px; box-shadow: 0 15px 25px rgba(0,0,50,0.2); width: 60%; margin:auto"" class=""px-5 py-2 my-3 d-none d-lg-block"">
        <div>
            <br />
            <h4 class=""text-center"">Booking</h4>
            <br />
        </div>
        <div class=""row justify-content-center"">
            <div class=""col-lg-10"">
                <div class=""row"">
                    <div class=""col-6 text-right pt-2"">
                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2819073", async() => {
                    WriteLiteral("Full Name");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 68 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.UserID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n                        ");
#nullable restore
#line 71 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.User.FullName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2821342", async() => {
                    WriteLiteral("Special Notes");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 76 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.SpecialNotes);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n                        ");
#nullable restore
#line 79 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.SpecialNotes));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2823620", async() => {
                    WriteLiteral("Start");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 84 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.StartDate);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n                        ");
#nullable restore
#line 87 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.StartDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2825884", async() => {
                    WriteLiteral("End");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 92 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EndDate);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n                        ");
#nullable restore
#line 95 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.EndDate));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2828142", async() => {
                    WriteLiteral("Area");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 100 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Room.RoomGroup.AreaName);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n");
                WriteLiteral("                        ");
#nullable restore
#line 104 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Room.RoomGroup.AreaName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2830470", async() => {
                    WriteLiteral("Room");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 109 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Room.RoomName);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n");
                WriteLiteral("                        ");
#nullable restore
#line 113 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.Room.RoomName));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n                <div class=\"row\">\r\n                    <div class=\"col-6 text-right pt-2\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2832778", async() => {
                    WriteLiteral("Approval Status");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 118 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ApprovalStatus);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                    <div class=\"col-6 pt-2\">\r\n                        ");
#nullable restore
#line 121 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                   Write(Html.DisplayFor(model => model.ApprovalStatus));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <br />\r\n        <div class=\"d-flex justify-content-center\">\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4043fa31dee0b1119815dfc60f20ebefe65a0d2835067", async() => {
                    WriteLiteral("Edit");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 128 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
                                   WriteLiteral(Model.ID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n            <a");
                BeginWriteAttribute("href", " href=\'", 6104, "\'", 6133, 1);
#nullable restore
#line 129 "C:\Users\emmaa\Desktop\PROG1440-Prototype_4\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\RoomBookings\Details.cshtml"
WriteAttributeValue("", 6111, ViewData["returnURL"], 6111, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" role=\"button\" class=\"btn btn-secondary mx-2\">Back to Bookings</a>\r\n        </div>\r\n        <br />\r\n    </section>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script type=""text/javascript"">
        // Reload certain input fields when page loads
        $(document).ready(function () {
            // Write ""None."" if there are no Special Notes
            var specialNotes = $.trim($(""#SpecialNotes"").val());
            if (specialNotes == """") {
                $(""textarea#SpecialNotes"").val(""None."");
            }
        });
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BRTF_Room_Booking_App.Models.RoomBooking> Html { get; private set; }
    }
}
#pragma warning restore 1591
