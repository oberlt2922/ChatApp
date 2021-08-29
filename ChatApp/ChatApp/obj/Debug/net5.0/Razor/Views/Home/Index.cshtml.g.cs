#pragma checksum "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14f3c80fb2ec71631fd98e945a3e9797884b4432"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"14f3c80fb2ec71631fd98e945a3e9797884b4432", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6cf30333ff74047b044a1c577267f3d6907496f9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ChatApp.Models.AppUser>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("createChatroomForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/site.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
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
#nullable restore
#line 9 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
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
#line 22 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
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
                    <!--validate input
                        check chatroom name length
                        check if members exist
                        autocomplete member textbox
                            dont show current user
                        change submit functionality
  ");
            WriteLiteral(@"                          onclick
                                call ajax post function
                                    calls create chatroom json result controller method
                                        creates chatroom
                                        adds members to chatroom
                                        returns chatroom including members as json
                                        calls function to add chatroom to chatroom list
                                        calls function to display chatroom-->
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f3c80fb2ec71631fd98e945a3e9797884b44327756", async() => {
                WriteLiteral(@"
                        <div class=""row justify-content-center"">
                            <label>Public</label>
                            <input id=""isPublic"" name=""isPublic"" type=""radio"" style=""margin-right: 15px; margin-left: 5px;"" value=""Public"">
                            <label>Private</label>
                            <input id=""isPublic"" name=""isPublic"" type=""radio"" style=""margin-left: 5px;"" value=""Private"">
                        </div>
                        <div class=""row justify-content-center"">
                            <input type=""text"" name=""chatroomName"" class=""bg-dark text-white"" style=""border-radius: 10px;"" placeholder=""Chatroom Name"" />
                        </div>
                        <div class=""row justify-content-center"">
                            <input type=""text"" id=""txtSearchUsers"" name=""username"" class=""bg-dark text-white"" style=""border-radius: 10px; margin-top:10px;"" placeholder=""Chatroom member"" />
                        </div>
                 ");
                WriteLiteral(@"       <div id=""addMemberBtn"" class=""row justify-content-center"">
                            <button type=""button"" class=""btn btn-link"">Add another member</button>
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
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
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
    <!--CHATROOM LIST
        search/join chatrooms autocomplete
            only display chatroom if current user isn't already a member of chatroom or if they are blocked
            onclick ajax post function
                call add join chatroom json result controller method
                    gets chatroom including members messages and senders
                    adds current user to members
                    return chatroom as json
                    calls function to add chatroom to chatroom list
                    calls function to display chatroom
        add new messages tag
            add timestamp to AppUserChatroom table-->
    <div class=""container-fluid h-100"">
        <div class=""row justify-content-center h-100"">
            <div class=""col-md-4 col-xl-3 chat"">
                <div class=""card mb-sm-3 mb-md-0 contacts_card"">
                    <div class=""card-header"">
                        <");
            WriteLiteral("div class=\"input-group\">\r\n                            <input id=\"txtSearchChatrooms\" type=\"text\" placeholder=\"Search Chatrooms...\"");
            BeginWriteAttribute("name", " name=\"", 5346, "\"", 5353, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control search\">\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"card-body contacts_body\">\r\n                        <ui class=\"contacts\">\r\n");
#nullable restore
#line 98 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                             foreach (Chatroom room in Model.Chatrooms)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <li class=""chatroomListItem"" style=""border-bottom-style:solid; border-bottom-color: lightslategrey; border-bottom-width: 1px;"">
                                    <div class=""d-flex bd-highlight"">
                                        <div class=""user_info col-11"">
                                            <input type=""hidden"" class=""chatroom_id""");
            BeginWriteAttribute("value", " value=\"", 6043, "\"", 6067, 1);
#nullable restore
#line 103 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 6051, room.ChatroomId, 6051, 16, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n                                            <div class=\"row col-12\">\r\n                                                <span id=\"chatroom-name\">");
#nullable restore
#line 105 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                                                    Write(room.ChatroomName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                            </div>\r\n");
#nullable restore
#line 107 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                             if (room.Messages.Count > 0)
                                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                <p id=\"message-text-preview\">");
#nullable restore
#line 109 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                                                        Write(room.Messages[room.Messages.Count - 1].Text);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                                                <p>");
#nullable restore
#line 110 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                              Write(room.Messages[room.Messages.Count - 1].Sent);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 111 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </div>\r\n                                    </div>\r\n                                </li>\r\n");
#nullable restore
#line 115 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                        </ui>
                    </div>
                    <div class=""card-footer text-center"">
                        <button type=""button"" class=""btn btn-primary rounded-pill"" style=""border-radius: 40px;"" data-toggle=""modal"" data-target=""#newChatroomModal"">Create new chatroom</button>
                    </div>
                </div>
            </div>
            <!--CHATROOM
                invite users should only show up if chatroom is public or if user is admin and chatroom is private
                    onclick display modal to enter usernames
                    autocomplete usernames
                        dont show current user or any users who are already members or blocked users
                block user is only available to admin
                    create blocked field in AppUserChatroom table
                unblock user only available to admin
                delete chatroom is only available to admin
                    deleter chatroom and all messages ");
            WriteLiteral(@"(test for cascade delete)
                leave chatroom is available to everyone
                    remove current user from chatroom-->
            <div class=""col-md-8 col-xl-6 chat"">
                <div class=""card"">
                    <div class=""card-header msg_head"">
                        <div class=""d-flex bd-highlight"">
                            <div class=""user_info"">
                                <span class=""chat_chatroom_name"">Chatroom name</span>
                                <p class=""message_count"" style=""margin-bottom: 0;"">1767 Messages</p>
                                <p class=""members_count"">10 Members</p>
                            </div>
                        </div>
                        <span id=""action_menu_btn""><i class=""fas fa-ellipsis-v""></i></span>
                        <div class=""action_menu"">
                            <ul>
                                <li><i class=""fas fa-users""></i> Invite users</li>
                                <li>");
            WriteLiteral(@"<i class=""fas fa-ban""></i> Block user</li>
                                <li><i class=""fas fa-trash""></i> Delete chatroom</li>
                                <li><i class=""fas fa-sign-out-alt""></i> Leave chatroom</li>
                            </ul>
                        </div>
                    </div>
                    <div class=""card-body msg_card_body"">
                        <div class=""d-flex justify-content-start mb-4"">
                            <div class=""msg_cotainer"">
                                Hi, how are you samim?
                                <span class=""msg_time"">Sender Name 8:40 AM, Today</span>
                            </div>
                        </div>
                        <div class=""d-flex justify-content-end mb-4"">
                            <div class=""msg_cotainer_send"">
                                Hi Khalid i am good tnx how about you?
                                <span class=""msg_time_send"">8:55 AM, Today</span>
                ");
            WriteLiteral("            </div>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"card-footer\">\r\n                        <div class=\"input-group\">\r\n                            <textarea");
            BeginWriteAttribute("name", " name=\"", 10141, "\"", 10148, 0);
            EndWriteAttribute();
            WriteLiteral(@" class=""form-control type_msg"" placeholder=""Type your message...""></textarea>
                            <div class=""input-group-append"">
                                <span class=""input-group-text send_btn""><i class=""fas fa-location-arrow""></i></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type=""hidden"" id=""active_user_id""");
            BeginWriteAttribute("value", " value=\"", 10620, "\"", 10637, 1);
#nullable restore
#line 181 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 10628, Model.Id, 10628, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <input type=\"hidden\" id=\"active_username\"");
            BeginWriteAttribute("value", " value=\"", 10688, "\"", 10711, 1);
#nullable restore
#line 182 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
WriteAttributeValue("", 10696, Model.UserName, 10696, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <input type=\"hidden\" id=\"active_chatroom_id\" />\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f3c80fb2ec71631fd98e945a3e9797884b443220625", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "14f3c80fb2ec71631fd98e945a3e9797884b443221729", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
#nullable restore
#line 187 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
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
#line 188 "C:\Users\oberl\Documents\GitHub\ChatApp\ChatApp\ChatApp\Views\Home\Index.cshtml"
     
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
