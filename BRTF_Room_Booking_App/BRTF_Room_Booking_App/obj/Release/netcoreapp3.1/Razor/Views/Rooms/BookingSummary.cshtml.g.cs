#pragma checksum "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fcff69effd26217baef6cb1548bb17659f5244a5"
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
#line 1 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\_ViewImports.cshtml"
using BRTF_Room_Booking_App.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcff69effd26217baef6cb1548bb17659f5244a5", @"/Views/Rooms/BookingSummary.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1700b735bbb69e861f7c7304dfafa8172d213b0e", @"/Views/_ViewImports.cshtml")]
    public class Views_Rooms_BookingSummary : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BRTF_Room_Booking_App.ViewModels.BookingSummary>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DownloadBookings", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-info mb-3"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "BookingSummary", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
  
    ViewData["Title"] = "Admin Reports";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<br />\r\n<h1 class=\"mb-4 text-center\">Admin Reports</h1>\r\n<br />\r\n<p class=\"text-danger\">");
#nullable restore
#line 10 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                  Write(TempData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<div class=\"container\">\r\n<section style=\"background-color: #FFF;display: block; border-radius: 8px; box-shadow: 0 15px 25px rgba(0,0,50,0.2);\" class=\"px-5 py-2 my-3\">\r\n    <br />\r\n    <h4 class=\"text-center\">Booking Summary</h4>\r\n    <br />\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcff69effd26217baef6cb1548bb17659f5244a56135", async() => {
                WriteLiteral("\r\n        <div class=\"row\">\r\n");
#nullable restore
#line 18 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
             if (User.IsInRole("Top-level Admin"))
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div class=\"col-12\">\r\n                    <div class=\"form-horizontal align-items-start text-left justify-content-start my-3\">\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcff69effd26217baef6cb1548bb17659f5244a56905", async() => {
                    WriteLiteral("Download Bookings Report");
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
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 25 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"col-12\">\r\n                <div class=\"form-horizontal align-items-start text-left justify-content-start my-3\">\r\n                    <button");
                BeginWriteAttribute("class", " class=\"", 1112, "\"", 1145, 2);
                WriteAttributeValue("", 1120, "btn", 1120, 3, true);
#nullable restore
#line 28 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
WriteAttributeValue(" ", 1123, ViewData["Filter"], 1124, 21, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"button\" data-toggle=\"collapse\" id=\"filterToggle\" data-target=\"#collapseFilter\" aria-expanded=\"false\" aria-controls=\"collapseFilter\">\r\n                        Filter/Search\r\n                    </button>\r\n                    <div");
                BeginWriteAttribute("class", " class=\"", 1381, "\"", 1422, 2);
                WriteAttributeValue("", 1389, "collapse", 1389, 8, true);
#nullable restore
#line 31 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
WriteAttributeValue(" ", 1397, ViewData["Filtering"], 1398, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" id=""collapseFilter"">
                        <div class=""card card-body bg-light"">
                            <div class=""row"">
                                <div class=""form-group col-md-4"">
                                    <label class=""control-label"">Bookings From:</label>
                                    ");
#nullable restore
#line 36 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                               Write(Html.TextBox("start", null, new { @class = "form-control", @type = "date" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </div>\r\n                                <div class=\"form-group col-md-4\">\r\n                                    <label class=\"control-label\">Bookings Before:</label>\r\n                                    ");
#nullable restore
#line 40 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                               Write(Html.TextBox("end", null, new { @class = "form-control", @type = "date" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </div>\r\n                                <div class=\"form-group col-md-4\">\r\n                                    <label class=\"control-label\">Search Room:</label>\r\n                                    ");
#nullable restore
#line 44 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                               Write(Html.TextBox("SearchRoom", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                </div>\r\n                                <div class=\"form-group col-md-4\">\r\n                                    <label class=\"control-label\">Search by Room Group:</label>\r\n                                    ");
#nullable restore
#line 48 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                               Write(Html.DropDownList("RoomGroupID", null, "All Room Groups", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                                </div>
                            </div>
                            <div class=""row"">
                                <div class=""form-group col-md-4 align-self-end"">
                                    <input type=""submit"" name=""actionButton"" value=""Filter"" class=""btn btn-outline-primary"" />
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fcff69effd26217baef6cb1548bb17659f5244a512866", async() => {
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
            </div>
        </div>
        <div id=""chartHolder"">
            <!-- HTML -->
            <div id=""chartdiv""></div>
        </div>


    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <table class=\"table\">\r\n        <thead>\r\n            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 73 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayNameFor(model => model.RoomName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 76 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayNameFor(model => model.RoomGroup));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 79 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayNameFor(model => model.NumberOfAppointments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n                <th>\r\n                    ");
#nullable restore
#line 82 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
               Write(Html.DisplayNameFor(model => model.TotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 88 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 92 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                   Write(Html.DisplayFor(modelItem => item.RoomName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 95 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                   Write(Html.DisplayFor(modelItem => item.RoomGroup));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 98 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                   Write(Html.DisplayFor(modelItem => item.NumberOfAppointments));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 101 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
                   Write(Html.DisplayFor(modelItem => item.TotalHours));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 105 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </tbody>
    </table>
</section>
</div>
<style>
    #chartHolder {
        height: 500px;
        overflow: scroll;
    }

    #chartdiv {
        width: 100%;
    }
</style>

<!-- Resources -->
<script src=""https://cdn.amcharts.com/lib/5/index.js""></script>
<script src=""https://cdn.amcharts.com/lib/5/xy.js""></script>
<script src=""https://cdn.amcharts.com/lib/5/themes/Animated.js""></script>

<!-- Chart code -->
<script>
    am5.ready(function() {

    // Create root element
    // https://www.amcharts.com/docs/v5/getting-started/#Root_element
    var root = am5.Root.new(""chartdiv"");


    // Set themes
    // https://www.amcharts.com/docs/v5/concepts/themes/
    root.setThemes([
      am5themes_Animated.new(root)
    ]);


    // Create chart
    // https://www.amcharts.com/docs/v5/charts/xy-chart/
    var chart = root.container.children.push(am5xy.XYChart.new(root, {
      panX: false,
      panY: false,
      wheelX: ""panX"",
      wheelY: ""zoomX"",
      lay");
            WriteLiteral(@"out: root.verticalLayout
    }));


    // Add legend
    // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
    var legend = chart.children.push(am5.Legend.new(root, {
      centerX: am5.p50,
      x: am5.p50
    }))

    var data = ");
#nullable restore
#line 160 "C:\Users\emmaa\Desktop\PROG1440-Prototype_3\BRTF_Room_Booking_App\BRTF_Room_Booking_App\Views\Rooms\BookingSummary.cshtml"
          Write(Html.Raw(ViewBag.RoomList));

#line default
#line hidden
#nullable disable
            WriteLiteral(@";

    var height = Math.round(Object.keys(data).length*80);

    if(height < 500)
    height = 500;

    document.getElementById(""chartdiv"").style.height = height+""px"";

    // Create axes
    // https://www.amcharts.com/docs/v5/charts/xy-chart/axes/
    var yAxis = chart.yAxes.push(am5xy.CategoryAxis.new(root, {
      categoryField: ""room"",
      renderer: am5xy.AxisRendererY.new(root, {
        inversed: true,
        cellStartLocation: 0.1,
        cellEndLocation: 0.9
      })
    }));

    yAxis.data.setAll(data);

    var xAxis = chart.xAxes.push(am5xy.ValueAxis.new(root, {
      renderer: am5xy.AxisRendererX.new(root, {}),
      min: 0
    }));


    // Add series
    // https://www.amcharts.com/docs/v5/charts/xy-chart/series/
    function createSeries(field, name) {
      var series = chart.series.push(am5xy.ColumnSeries.new(root, {
        name: name,
        xAxis: xAxis,
        yAxis: yAxis,
        valueXField: field,
        categoryYField: ""room"",
        s");
            WriteLiteral(@"equencedInterpolation: true,
        tooltip: am5.Tooltip.new(root, {
          pointerOrientation: ""horizontal"",
          labelText: ""[bold]{name}[/]\n{categoryY}: {valueX}""
        })
      }));

      series.columns.template.setAll({
        height: am5.p100
      });


      series.bullets.push(function() {
        return am5.Bullet.new(root, {
          locationX: 1,
          locationY: 0.5,
          sprite: am5.Label.new(root, {
            centerY: am5.p50,
            text: ""{valueX}"",
            populateText: true
          })
        });
      });

      series.bullets.push(function() {
        return am5.Bullet.new(root, {
          locationX: 1,
          locationY: 0.5,
          sprite: am5.Label.new(root, {
            centerX: am5.p100,
            centerY: am5.p50,
            fill: am5.color(0xffffff),
            populateText: true
          })
        });
      });

      series.data.setAll(data);
      series.appear();

      return series;
   ");
            WriteLiteral(@" }

    createSeries(""numBookings"", ""Number of Bookings"");
    createSeries(""hrsBookings"", ""Hours of Bookings"");


    // Add legend
    // https://www.amcharts.com/docs/v5/charts/xy-chart/legend-xy-series/
    var legend = chart.children.push(am5.Legend.new(root, {
      centerX: am5.p50,
      x: am5.p50
    }));

    legend.data.setAll(chart.series.values);


    // Add cursor
    // https://www.amcharts.com/docs/v5/charts/xy-chart/cursor/
    var cursor = chart.set(""cursor"", am5xy.XYCursor.new(root, {
      behavior: ""zoomY""
    }));
    cursor.lineY.set(""forceHidden"", true);
    cursor.lineX.set(""forceHidden"", true);


    // Make stuff animate on load
    // https://www.amcharts.com/docs/v5/concepts/animations/
    chart.appear(1000, 100);

    }); // end am5.ready()
</script>
");
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
