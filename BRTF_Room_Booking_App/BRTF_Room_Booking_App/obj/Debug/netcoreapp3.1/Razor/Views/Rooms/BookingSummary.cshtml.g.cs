#pragma checksum "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b1b3cd44e9887173065d991d7d5a9f33f3457f7b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Rooms_BookingSummary), @"mvc.1.0.view", @"/Views/Rooms/BookingSummary.cshtml")]
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
#line 1 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b1b3cd44e9887173065d991d7d5a9f33f3457f7b", @"/Views/Rooms/BookingSummary.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_Rooms_BookingSummary : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BRTF_Room_Booking_App.ViewModels.BookingSummary>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
  
    ViewData["Title"] = "BookingSummary";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>BookingSummary</h1>\r\n\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
           Write(Html.DisplayNameFor(model => model.RoomName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
           Write(Html.DisplayNameFor(model => model.NumberOfAppointments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
           Write(Html.DisplayNameFor(model => model.TotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 26 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 30 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayFor(modelItem => item.RoomName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 33 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayFor(modelItem => item.NumberOfAppointments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 36 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayFor(modelItem => item.TotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n            </tr>\r\n");
#nullable restore
#line 40 "C:\Users\emmaa\Desktop\PROG1440-Prototype_2\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BRTF_Room_Booking_App.ViewModels.BookingSummary>> Html { get; private set; }
    }
}
#pragma warning restore 1591
