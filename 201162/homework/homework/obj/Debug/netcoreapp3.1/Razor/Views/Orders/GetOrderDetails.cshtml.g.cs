#pragma checksum "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6e129295d377a8a3074ff0ff915ec966d5eee7c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Orders_GetOrderDetails), @"mvc.1.0.view", @"/Views/Orders/GetOrderDetails.cshtml")]
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
#line 1 "C:\Users\milos\source\repos\homework\homework\Views\_ViewImports.cshtml"
using homework;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\milos\source\repos\homework\homework\Views\_ViewImports.cshtml"
using homework.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6e129295d377a8a3074ff0ff915ec966d5eee7c0", @"/Views/Orders/GetOrderDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6f364cc8f2887035c5624ab65f9272a5b123c710", @"/Views/_ViewImports.cshtml")]
    public class Views_Orders_GetOrderDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<eBoxOffice.Domain.Domain_models.Order>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"container\">\r\n\r\n   \r\n");
#nullable restore
#line 12 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
     for (int i = 0; i < Model.TicketsInOrder.ToList().Count; i++)
    {
        var ticketsInOrder = Model.TicketsInOrder.ToList();
        var item = ticketsInOrder[i];

        if (i % 3 == 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            WriteLiteral("<div class=\"row\">\r\n");
#nullable restore
#line 20 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
            }


#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card m-2\" style=\"width: 22rem;\">\r\n\r\n                <div class=\"card-body\">\r\n\r\n                    <h3 class=\"card-subtitle\">");
#nullable restore
#line 26 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
                                         Write(item.Ticket.DateTime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n                    <p class=\"card-text\">");
#nullable restore
#line 27 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
                                    Write(item.Ticket.TicketPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p class=\"card-text\">");
#nullable restore
#line 28 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
                                    Write(item.Quantity);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 31 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"


            if (i % 3 == 2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            WriteLiteral("</div>\r\n");
#nullable restore
#line 36 "C:\Users\milos\source\repos\homework\homework\Views\Orders\GetOrderDetails.cshtml"
        }
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<eBoxOffice.Domain.Domain_models.Order> Html { get; private set; }
    }
}
#pragma warning restore 1591
