#pragma checksum "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\Transacao\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "de1f893c11b9808ce790cbfdd84410d4b65f9939"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Transacao_Dashboard), @"mvc.1.0.view", @"/Views/Transacao/Dashboard.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Transacao/Dashboard.cshtml", typeof(AspNetCore.Views_Transacao_Dashboard))]
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
#line 1 "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\_ViewImports.cshtml"
using MyFinance;

#line default
#line hidden
#line 2 "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\_ViewImports.cshtml"
using MyFinance.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"de1f893c11b9808ce790cbfdd84410d4b65f9939", @"/Views/Transacao/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d05dd6abef5a8ff60f9a555c67ee727241a6c480", @"/Views/_ViewImports.cshtml")]
    public class Views_Transacao_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 444, true);
            WriteLiteral(@"<h3>Dashboard</h3>
<br /><br />
<script src=""https://www.chartjs.org/dist/2.9.3/Chart.min.js""></script>

<div id=""canvas-holder"" style=""width:50%"">
    <canvas id=""chart-area""></canvas>
</div>
<script>
    var randomScalingFactor = function () {
        return Math.round(Math.random() * 100);
    };

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: [
                    ");
            EndContext();
            BeginContext(445, 25, false);
#line 18 "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\Transacao\Dashboard.cshtml"
               Write(Html.Raw(ViewBag.valores));

#line default
#line hidden
            EndContext();
            BeginContext(470, 98, true);
            WriteLiteral("                    \r\n                ],\r\n                backgroundColor: [\r\n                    ");
            EndContext();
            BeginContext(569, 23, false);
#line 21 "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\Transacao\Dashboard.cshtml"
               Write(Html.Raw(ViewBag.cores));

#line default
#line hidden
            EndContext();
            BeginContext(592, 114, true);
            WriteLiteral("\r\n                ],\r\n                label: \'Dataset 1\'\r\n            }],\r\n            labels: [\r\n                ");
            EndContext();
            BeginContext(707, 24, false);
#line 26 "C:\Estudos\ASPNETCOREMVC\MyFinance\MyFinance\Views\Transacao\Dashboard.cshtml"
           Write(Html.Raw(ViewBag.labels));

#line default
#line hidden
            EndContext();
            BeginContext(731, 279, true);
            WriteLiteral(@"
            ]
        },
        options: {
            responsive: true
        }
    };

    window.onload = function () {
        var ctx = document.getElementById('chart-area').getContext('2d');
        window.myPie = new Chart(ctx, config);
    };

</script>
");
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
