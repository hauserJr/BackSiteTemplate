#pragma checksum "D:\.personal\BackSiteTemplate\BackSiteTemplate\Views\WhiteList\WhiteListManager.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a661a628d526aed53c22090c3867cfddcb43e196"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_WhiteList_WhiteListManager), @"mvc.1.0.view", @"/Views/WhiteList/WhiteListManager.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/WhiteList/WhiteListManager.cshtml", typeof(AspNetCore.Views_WhiteList_WhiteListManager))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a661a628d526aed53c22090c3867cfddcb43e196", @"/Views/WhiteList/WhiteListManager.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"03cf4a3eef6bb23fd8bd938fae26896866a48696", @"/Views/_ViewImports.cshtml")]
    public class Views_WhiteList_WhiteListManager : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("WhiteListForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(0, 1427, true);
            WriteLiteral(@"
<button type=""button"" class=""btn btn-md btn-primary"" onclick=""UpModal(this)"" data-test=""1"">新增</button>
<div class=""alert alert-danger mt-1"" role=""alert"">
    可新增、修改、刪除，但目前已暫時關閉此過濾功能。
</div>
<div id=""Dts"" class=""container bg-white text-dark mt-3"" style=""font-size:15px"">
    <table id=""example"" class=""table table-striped table-bordered display"" style=""width: 100%;"">
        <thead>
            <tr>
                <th style=""width: 20%;"">IP</th>
                <th style=""width: 40%;"">備註</th>
                <th style=""width: 20%;"">動作</th>
            </tr>
        </thead>
        <tfoot>
            <tr>
                <th>IP</th>
                <th>備註</th>
                <th>動作</th>
            </tr>
        </tfoot>
    </table>
</div>

<!-- Modal -->
<div class=""modal fade"" id=""WhiteListModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""WhiteListModalCenterTitle"" aria-hidden=""true"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
         ");
            WriteLiteral(@"   <div class=""modal-header"">
                <h5 class=""modal-title"" id=""WhiteListModalTitle""></h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""col-auto"">
                    ");
            EndContext();
            BeginContext(1427, 136, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae48375d298547e89bf6dd16547a07a2", async() => {
                BeginContext(1452, 104, true);
                WriteLiteral("\r\n                        <div id=\"WhiteListArea\">\r\n                        </div>\r\n                    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1563, 4679, true);
            WriteLiteral(@"
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-secondary"" data-dismiss=""modal"">關閉</button>
                <button type=""button"" class=""btn btn-primary SaveData"">儲存</button>
            </div>
        </div>
    </div>
</div>


<script>
    $(document).ready(function () {
        InitDataTables();
    });
    function InitDataTables() {
        var Table = $('#example');
        Table.dataTable().fnDestroy();
        Table.DataTable({
            ""processing"": false,
            ""serverSide"": true,
            ""ajax"": {
                ""url"": ""/Ajax_WhiteList/GetDtsWhiteList"",
                ""type"": ""GET""
            },
            ""language"": {
                ""processing"": ""處理中..."",
                ""lengthMenu"": ""顯示 _MENU_ 筆"",
                ""zeroRecords"": ""未找到任何資料"",
                ""info"": ""顯示第 _START_ 至 _END_ 項結果，共 _TOTAL_ 項"",
                ""infoEmpty"": ""沒有任何資料"",
                ""inf");
            WriteLiteral(@"oFiltered"": ""<p>(從 _MAX_ 項結果過濾)"",
                ""search"": ""關鍵字"",
                ""paginate"": {
                    ""sFirst"": ""首頁"",
                    ""sPrevious"": ""上一頁"",
                    ""sNext"": ""下一頁"",
                    ""sLast"": ""尾頁""
                }
            },
            ""columns"": [
                {
                    ""data"": ""Ip""
                },
                {
                    ""data"": ""Note""
                },
                {
                    ""data"": ""Id"",
                    orderable: false, // 禁用排序
                    render: function (data, type, row, meta) {
                        var DeleteBtn = ""<button class='btn btn-danger mr-1' onclick='DelData(this)' data-myid='"" + data + ""'>刪除</button>"";
                        var EditBtn = ""<button class='btn btn-primary mr-1' onclick='EditData(this)' data-myid='"" + data + ""'>編輯</button>"";
                        return EditBtn + DeleteBtn;
                    }
                }
            ]
        ");
            WriteLiteral(@"});
    }
    function UpModal(e) {
        $('#WhiteListModalTitle').text($(e).text());
        $.ajax({
            url: '/Ajax_WhiteList/GetPageWhiteList',
            type: 'POST',
            success: function (data) {
                $('#WhiteListArea').empty();
                $('#WhiteListArea').html(data);
                $('#WhiteListModal').modal('show');
            }
        });
    }
    function DelData(e) {
        swal({
            title: '確認嗎?',
            text: ""你即將刪除該筆資料!"",
            type: 'warning',
            showCancelButton: true,
            confirmButtonText: '確認!',
            cancelButtonText: '取消',
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33'

        }).then(function (result){
            if (result.value) {
                $.ajax({
                    url: '/Ajax_WhiteList/DeleteWhiteList',
                    data: { Id: $(e).data('myid') },
                    type: 'POST',
                    success: fun");
            WriteLiteral(@"ction (data) {
                        swal({
                            title: ""刪除!"",
                            text: data,
                            type: ""success"",
                            buttons: true
                        }).then(function (result){
                            location.reload();
                        });
                    }
                });
            }
        });
    }


    function EditData(e) {
        $('#WhiteListModalTitle').text($(e).text());
        $.ajax({
            url: '/Ajax_WhiteList/GetPageWhiteList',
            data: {
                Id : $(e).data('myid')
            },
            type: 'POST',
            success: function (data) {
                $('#WhiteListArea').empty();
                $('#WhiteListArea').html(data);
                $('#WhiteListModal').modal('show');
            }
        });
    }

    $("".SaveData"").click(function (event) {
        if (CheckNumber()) {
            $.ajax({
           ");
            WriteLiteral(@"     url: '/Ajax_WhiteList/SaveWhiteList',
                data: $('#WhiteListForm').serialize(),
                type: 'POST',
                success: function (r) {
                    if (r.result == false) {
                        swal(
                            '錯誤!',
                            '資料已存在或錯誤，請重新檢查。',
                            'warning'
                        );
                    }
                    else {
                        location.reload();
                    }
                }
            });
        }
    });
</script>");
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
