#pragma checksum "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dab21bede90d7ed2995f3fb24cfdb489a3412aa7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\_ViewImports.cshtml"
using ChatApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\_ViewImports.cshtml"
using ChatApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dab21bede90d7ed2995f3fb24cfdb489a3412aa7", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6cf30333ff74047b044a1c577267f3d6907496f9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChatApp.Models.AppUser>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/moment.js/moment.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("createChatroomForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("addMembersForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("blockUserForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery-confirm/jquery-confirm.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/site.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 5 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa76389", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 11 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
 if (!SignInManager.IsSignedIn(User))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""text-center"">
        <h1 class=""display-4"">Welcome to my chat application.</h1>
        <p>
            This website allows users to create an account and chat with other users in real time.<br />
            The register and login features use ASP.NET Identity<br />
            SignalR is used to allow users to send and recieve messages in real time.<br />
            Users can search for chatrooms to join, or create their own chatrooms and add disired users to the chatroom.<br />
            The creator of a chatroom is the admin and can perform various actions on the chatroom.
        </p>
        <h4>Please Register and/or Login to continue</h4>
    </div>
");
#nullable restore
#line 24 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <!--CREATE CHATROOM POPUP-->
    <div class=""modal fade bg-dark bg-transparent"" id=""newChatroomModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""newChatroomModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content bg-dark text-white"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">Create New Chatroom</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span class=""text-white"" aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa79355", async() => {
                WriteLiteral(@"
                        <div class=""row justify-content-center"">
                            <label>Public</label>
                            <input id=""isPublic-public"" name=""isPublic"" type=""radio"" value=""Public"">
                            <label>Private</label>
                            <input id=""isPublic-private"" name=""isPublic"" type=""radio"" value=""Private"">
                        </div>
                        <div class=""row justify-content-center"">
                            <input type=""text"" name=""chatroomName"" class=""bg-dark text-white"" placeholder=""Chatroom Name"" />
                        </div>
                        <div class=""row justify-content-center"">
                            <input type=""text"" id=""txtSearchUsers"" name=""username"" placeholder=""Chatroom member"" class=""bg-dark text-white"" />
                        </div>
                        <div class=""row justify-content-center"">
                            <button type=""button"" class=""btn btn-link addNewTextBox");
                WriteLiteral(@"Btn"">Add another member</button>
                        </div>
                        <div class=""row justify-content-center"">
                            <input id=""createChatroomBtn"" type=""submit"" class=""btn btn-primary"" data-dismiss=""modal"" value=""Create"" />
                            <button type=""button"" class=""btn btn-secondary offset-1"" data-dismiss=""modal"">Close</button>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
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
    <!--ADD USERS POPUP-->
    <div class=""modal fade bg-dark bg-transparent"" id=""addMembersModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""addMembersModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content bg-dark text-white"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">Add new chatroom members</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span class=""text-white"" aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa713082", async() => {
                WriteLiteral(@"
                        <div class=""row justify-content-center"">
                            <input type=""text"" id=""txtSearchUsers"" name=""username"" placeholder=""Chatroom member"" class=""bg-dark text-white"" />
                        </div>
                        <div class=""row justify-content-center"">
                            <button type=""button"" class=""btn btn-link addNewTextBoxBtn"">Add another member</button>
                        </div>
                        <div class=""row justify-content-center"">
                            <input id=""addMembersBtn"" type=""submit"" class=""btn btn-primary"" data-dismiss=""modal"" value=""Add Members"" />
                            <button type=""button"" class=""btn btn-secondary offset-1"" data-dismiss=""modal"">Close</button>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
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
    <!--BLOCK USER POPUP-->
    <div class=""modal fade bg-dark bg-transparent"" id=""blockUserModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""blockUserModalLabel"" aria-hidden=""true"">
        <div class=""modal-dialog"" role=""document"">
            <div class=""modal-content bg-dark text-white"">
                <div class=""modal-header"">
                    <h5 class=""modal-title"" id=""exampleModalLabel"">Block a user from the chatroom</h5>
                    <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                        <span class=""text-white"" aria-hidden=""true"">&times;</span>
                    </button>
                </div>
                <div class=""modal-body"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa716121", async() => {
                WriteLiteral(@"
                        <div class=""row justify-content-center"">
                            <input type=""text"" id=""txtSearchUsers"" name=""username"" placeholder=""Username"" class=""bg-dark text-white"" />
                        </div>
                        <div class=""row justify-content-center"" style=""margin-top: 10px;"">
                            <input id=""blockUserBtn"" type=""submit"" class=""btn btn-primary"" data-dismiss=""modal"" value=""Block User"" />
                            <button type=""button"" class=""btn btn-secondary offset-1"" data-dismiss=""modal"">Close</button>
                        </div>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
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
    <!--CHATROOM LIST-->
    <div class=""container-fluid h-100"">
        <div class=""row justify-content-center h-100"">
            <div class=""col-md-4 col-xl-3 chat"">
                <div class=""card mb-sm-3 mb-md-0 contacts_card"">
                    <div class=""card-header"">
                        <div class=""input-group"">
                            <input id=""txtSearchChatrooms"" type=""text"" placeholder=""Search Chatrooms...""");
            BeginWriteAttribute("name", " name=\"", 6944, "\"", 6951, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control search\">\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"card-body contacts_body\">\r\n                        <ui class=\"contacts\">\r\n");
#nullable restore
#line 126 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                             foreach (Chatroom room in Model.Chatrooms)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li");
            BeginWriteAttribute("id", " id=\"", 7288, "\"", 7328, 2);
            WriteAttributeValue("", 7293, "chatroom-list-item-", 7293, 19, true);
#nullable restore
#line 128 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 7312, room.ChatroomId, 7312, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"chatroomListItem\">\r\n                                    <div class=\"d-flex bd-highlight\">\r\n                                        <div class=\"user_info col-11\">\r\n                                            <input type=\"hidden\" class=\"chatroom_id\"");
            BeginWriteAttribute("value", " value=\"", 7584, "\"", 7608, 1);
#nullable restore
#line 131 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 7592, room.ChatroomId, 7592, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                            <div class=\"row col-12\">\r\n                                                <span id=\"chatroom-name\">");
#nullable restore
#line 133 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                                                    Write(room.ChatroomName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            </div>\r\n");
#nullable restore
#line 135 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                             if (room.Messages.Count > 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <p");
            BeginWriteAttribute("id", " id=\"", 8008, "\"", 8045, 2);
            WriteAttributeValue("", 8013, "msg-preview-txt-", 8013, 16, true);
#nullable restore
#line 137 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 8029, room.ChatroomId, 8029, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"message-preview\">");
#nullable restore
#line 137 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                                                                                            Write(room.Messages[room.Messages.Count - 1].Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                <p");
            BeginWriteAttribute("id", " id=\"", 8171, "\"", 8209, 2);
            WriteAttributeValue("", 8176, "msg-preview-sent-", 8176, 17, true);
#nullable restore
#line 138 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 8193, room.ChatroomId, 8193, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"message-preview\">\r\n                                                    <script>\r\n                                                        document.write(moment(\"");
#nullable restore
#line 140 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                                                          Write(room.Messages[room.Messages.Count - 1].Sent);

#line default
#line hidden
#nullable disable
            WriteLiteral("\").calendar());\r\n                                                    </script>\r\n                                                </p>\r\n");
#nullable restore
#line 143 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                            }
                                            else
                                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <p");
            BeginWriteAttribute("id", " id=\"", 8752, "\"", 8789, 2);
            WriteAttributeValue("", 8757, "msg-preview-txt-", 8757, 16, true);
#nullable restore
#line 147 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 8773, room.ChatroomId, 8773, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"message-preview\"></p>\r\n                                                <p");
            BeginWriteAttribute("id", " id=\"", 8871, "\"", 8909, 2);
            WriteAttributeValue("", 8876, "msg-preview-sent-", 8876, 17, true);
#nullable restore
#line 148 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 8893, room.ChatroomId, 8893, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"message-preview\"></p>\r\n");
#nullable restore
#line 149 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </div>\r\n                                    </div>\r\n                                </li>\r\n");
#nullable restore
#line 153 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </ui>
                    </div>
                    <div class=""card-footer text-center"">
                        <button type=""button"" class=""btn btn-primary rounded-pill"" data-toggle=""modal"" data-target=""#newChatroomModal"">Create new chatroom</button>
                    </div>
                </div>
            </div>
            <!--CHATROOM-->
            <div class=""col-md-8 col-xl-6 chat"">
                <div class=""card"">
                    <div class=""card-header msg_head"">
                        <div class=""d-flex bd-highlight"">
                            <div class=""user_info"">
                                <span class=""chat_chatroom_name""></span>
                                <div class=""count"">
                                    <p class=""message_count d-inline""></p><p id=""messages_count_static"" class=""d-inline"">&nbsp;Messages</p>
                                </div>
                                <div class=""count"">
                         ");
            WriteLiteral(@"           <p class=""members_count d-inline""></p><p id=""members_count_static"" class=""d-inline"">&nbsp;Members</p>
                                </div>
                            </div>
                        </div>
                        <span id=""action_menu_btn""><i class=""fas fa-ellipsis-v""></i></span>
                        <div class=""action_menu"">
                            <ul id=""action_menu_list""></ul>
                        </div>
                        <button id=""scroll_down_btn"" class=""btn btn-primary rounded-pill"">Scroll to bottom</button>
                    </div>
                    <div class=""card-body msg_card_body"">
                        <!--messages go here-->
                    </div>
                    <div class=""card-footer"">
                        <div class=""input-group"">
                            <textarea name=""new-message"" class=""form-control type_msg"" placeholder=""Type your message...""></textarea>
                            <div class=""input-group");
            WriteLiteral(@"-append"">
                                <span id=""send-msg-btn"" class=""input-group-text send_btn""><i class=""fas fa-location-arrow""></i></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type=""hidden"" id=""active_user_id""");
            BeginWriteAttribute("value", " value=\"", 11557, "\"", 11574, 1);
#nullable restore
#line 197 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 11565, Model.Id, 11565, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <input type=\"hidden\" id=\"active_username\"");
            BeginWriteAttribute("value", " value=\"", 11625, "\"", 11648, 1);
#nullable restore
#line 198 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 11633, Model.UserName, 11633, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa728693", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa729797", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dab21bede90d7ed2995f3fb24cfdb489a3412aa730901", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
#nullable restore
#line 203 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 204 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
     
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<AppUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<AppUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ChatApp.Models.AppUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
