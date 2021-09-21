"use strict";

$(document).ready(function () {
    //VARIABLES AND CODE THAT RUNS AS SOON AS THE DOM IS READY////////////////////////////////////////////////////
    //current username and user id
    var currentUserId = $('#active_user_id').val();
    var currentUsername = $('#active_username').val();
    var activeChatroomId;
    var mcsContainerHeight;

    //add custom scrollbar to chatroom list when page is loaded
    $('.contacts_body').mCustomScrollbar();

    //SIGNALR CODE///////////////////////////////////////////////////////////////////////////////////////////////
    //create signalr connection and disable send button until connection starts and the dom is ready
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    $('.send_btn').prop('disabled', true);

    //when a new chatroom is created each member will be added to the group
    connection.on('AddToNewGroup', function (chatroomId) {
        connection.invoke('AddCurrentUserToNewGroup', chatroomId, currentUserId).catch(function (err) {
            return console.error(err.toString());
        });
    });

    //after the current user is added to a newly created chatroom
    //the chatroom will be displayed in the chatroom list
    connection.on("AddNewChatroomToList", function (chatroomId) {
        $.ajax({
            type: 'POST',
            url: '../Home/GetChatroom',
            data: { 'chatroomId': chatroomId },
            dataType: 'json'
        }).done(function (result) {
            addChatroomToList(result, false);
        });
    });

    //Receive a message and add it to the dom
    connection.on("ReceiveMessage", function (messageJson) {
        var message = $.parseJSON(messageJson);
        displayMessage(message);
    });

    //Receive message that alerts group of a user leaving chatroom
    connection.on("ReceiveLeftChatroomMessage", function (message, chatroomId) {
        if (chatroomId == activeChatroomId.toString()) {
            var div = $('<div class="d-flex mb-4 justify-content-center bg-dark bg-transparent"></div>');
            $(div).append('<p class="text-white">' + message + '</p>');
        }
    });

    //Receive message that alerts group on new admin and adds action menu items if current user is new admin
    connection.on("ReceiveNewAdminMessage", function (message, chatroomId, adminId, isPublic) {
        if (chatroomId == activeChatroomId.toString()) {
            var div = $('<div class="d-flex mb-4 justify-content-center bg-dark bg-transparent"></div>');
            $(div).append('<p class="text-white">' + message + '</p>');
            if (currentUserId == adminId) {
                displayActionIcons(adminId, isPublic);
            }
        }
    });

    connection.on('DisplayError', function (errorMessage) {
        console.log(errorMessage);
    });

    //DOM FUNCTIONS////////////////////////////////////////////////////////////////////////////////////////////////display action menu items
    function displayActionIcons(adminId, isPublic) {
        $('#action_menu_list').empty();
        if (adminId == currentUserId) {
            var deleteChatroomLi = $('<li id="delete_chatroom_li"><i class="fas fa-trash"></i> Delete chatroom</li>');
            var blockUserLi = $('<li id="block_user_li"><i class="fas fa-ban"></i> Block user</li>');
            $('#action_menu_list').append(deleteChatroomLi).append(blockUserLi);
        }
        if (adminId == currentUserId || isPublic == true) {
            var inviteUsersLi = $('<li id="invite_user_li"><i class="fas fa-users"></i> Invite users</li>');
            $('#action_menu_list').append(inviteUsersLi);
        }
        var leaveChatroomLi = $('<li id="leave_chatroom_li"><i class="fas fa-sign-out-alt"></i> Leave chatroom</li>');
        $('#action_menu_list').append(leaveChatroomLi);
    }

    //display chatroom function to be called when chatroom is clicked or created
    function displayChatroom(chatroom) {
        $('.msg_card_body').mCustomScrollbar("destroy");
        displayActionIcons(chatroom.adminId, chatroom.isPublic);
        $('#active_chatroom_id').val(chatroom.chatroomId);
        activeChatroomId = chatroom.chatroomId;
        $('.chat_chatroom_name').text(chatroom.chatroomName);
        $('.message_count').text(chatroom.messages.length + ' Messages');
        $('.members_count').text(chatroom.members.length + ' Members');
        $('.msg_card_body').empty();
        $('.msg_card_body').mCustomScrollbar({
            callbacks: {
                onInit: function () {
                    $(this).mCustomScrollbar("scrollTo", "bottom");
                },
                onUpdate: function () {
                    if (this.mcs) {
                        if (this.mcs.top - $('.msg_card_body').height() === mcsContainerHeight * -1) {
                            $(this).mCustomScrollbar("scrollTo", "bottom");
                        }
                    }
                },
                whileScrolling: function () {
                }
            }
        });
        $.each(chatroom.messages, function (index, message) {
            displayMessage(message);
        });
        $('.msg_card_body').mCustomScrollbar("scrollTo", "bottom");
    }

    //displays a message with the correct classes depending on the current user and the message's sender
    function displayMessage(message) {
        if (message.chatroomId == activeChatroomId) {
            var div = $('<div class="d-flex mb-4"></div>');
            var msgContainer = $('<div></div>');
            if (message.userId == currentUserId) {
                $(div).addClass('justify-content-end');
                $(msgContainer).addClass('msg_cotainer_send');
                $(msgContainer).html(message.text + '<span class="msg_time_send">' + moment(message.sent).calendar() + '</span>');
            }
            else {
                $(div).addClass('justify-content-start');
                $(msgContainer).addClass('msg_cotainer');
                $(msgContainer).html(message.text + '<span class="msg_time">' + message.username + ' ' + moment(message.sent).calendar() + '</span>');
            }
            mcsContainerHeight = $('.msg_card_body .mCSB_container').height();
            $(div).append(msgContainer);
            $('.msg_card_body .mCSB_container').append(div);
            $('.msg_card_body').mCustomScrollbar("update");
        }
        $('#msg-preview-txt-' + message.chatroomId).text(message.text);
        $('#msg-preview-sent-' + message.chatroomId).text(moment(message.sent).calendar());
    }

    //add chatroom to list
    function addChatroomToList(chatroom, active) {
        var listItem = $('<li class="chatroomListItem" style="margin-bottom: 0; border-bottom-style:solid; border-bottom-color: lightslategrey; border-bottom-width: 1px;"></li>');
        if (chatroom.adminId == currentUserId || active == true) {
            $('.active').removeClass('active');
            $(listItem).addClass('active');
        }
        var div1 = $('<div class="d-flex bd-highlight"></div>');
        var div2 = $('<div class="user_info col-11"></div>');
        var chatIdInput = $('<input type="hidden" class="chatroom_id" value="' + chatroom.chatroomId + '" />');
        var div3 = $('<div class="row col-12"></div>');
        var span = $('<span id="chatroom-name">' + chatroom.chatroomName + '</span>');
        $(listItem).append(div1);
        $(div1).append(div2);
        $(div2).append(chatIdInput).append(div3);
        $(div3).append(span);
        var messageText = $('<p id="msg-preview-txt-' + chatroom.chatroomId + '" class="message-text-preview" style="margin-bottom: 0;"></p>');
        var messageSent = $('<p id="msg-preview-sent-' + chatroom.chatroomId + '" style="margin-bottom: 0;"></p>');
        $(div2).append(messageText).append(messageSent);
        if (!$.isEmptyObject(chatroom.messages)) {
            $(messageText).text(chatroom.messages[chatroom.messages.length - 1].text);
            $(messageSent).text(chatroom.messages[chatroom.messages.length - 1].sent);
        }
        $('ui.contacts').prepend(listItem);
        //$('.contacts_body .mCSB_container').prepend(listItem);
        $('.contacts_body').mCustomScrollbar("update");
    }

    //AJAX FUNCTIONS//////////////////////////////////////////////////////////////////////////////////////////////
    function getChatroom(id) {
        $.ajax({
            type: 'POST',
            url: '../Home/GetChatroom',
            data: { 'chatroomId': id },
            dataType: 'json'
        }).done(function (result) {
            displayChatroom(result);
        });
    }

    function createChatroom(isPublic, chatroomName, username) {
        $.ajax({
            type: 'POST',
            url: '../Home/CreateChatroom',
            data: { 'isPublic': isPublic, 'chatroomName': chatroomName, 'username': username },
            datatype: 'json'
        }).done(function (result) {
            var members = JSON.stringify(result.members);
            connection.invoke('AddMembersToGroup', result.chatroomId.toString(), members).catch(function (err) {
                return console.error(err.toString());
            });
            displayChatroom(result);
        });
    }

    function joinChatroom(chatroomId, display) {
        $.ajax({
            type: 'POST',
            url: '../Home/JoinChatroom',
            data: { 'chatroomId': chatroomId },
            datatype: 'json'
        }).done(function (result) {
            connection.invoke('AddCurrentUserToGroup', result.chatroomId.toString()).catch(function (err) {
                return console.error(err.toString());
            });
            if (display == true) {
                $('.active').removeClass('active');
                addChatroomToList(result, true);
                displayChatroom(result);
            }
            else {
                addChatroomToList(result, false);
            }
        });
    }

    function leaveChatroom() {
        $.ajax({
            type: 'POST',
            url: '../Home/LeaveChatroom',
            data: { 'chatroomId': activeChatroomId },
            dataType: 'json'
        }).done(function (result) {
            $('li.active').remove();
            $('#action_menu_list').empty();
            $('.chat_chatroom_name').text('');
            $('.message_count').text('');
            $('.members_count').text('');
            $('.msg_card_body').mCustomScrollbar("destroy");
            $('.msg_card_body').empty();
            connection.invoke('LeftChatroomMessage', activeChatroomId.toString(), currentUsername.toString()).catch(function (err) {
                return console.error(err.toString());
            });
            if (result.adminChanged == true) {
                connection.invoke('NewAdminMessage', activeChatroomId.toString(), result.adminId.toString()).catch(function (err) {
                    return console.error(err.toString());
                })
            }
            activeChatroomId = null;
        });
    }

    //AUTOCOMPLETE FUNCTIONS//////////////////////////////////////////////////////////////////////////////////////
    //autocomplete that runs when the user types in the search chatroom text box
    $("#txtSearchChatrooms").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: 'POST',
                url: '../Home/AutoCompleteChatroom',
                cache: false,
                data: { 'prefix': request.term, 'userId': currentUserId },
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            chatroomId: item.chatroomId,
                            chatroomName: item.chatroomName,
                            label: item.chatroomName,
                            value: request.term,
                            data: item
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var result = confirm('Would you like to join the chatroom \"' + ui.item.data.chatroomName + '\"\?');
            if (result == true) {
                joinChatroom(ui.item.data.chatroomId, true);
            }
        },
        close: function () {
            $('#txtSearchChatrooms').val('');
        }
    });


    //EVENT LISTENERS/////////////////////////////////////////////////////////////////////////////////////////////
    //toggles the chatroom menu
    $('#action_menu_btn').click(function () {
        if (activeChatroomId) {
            $('.action_menu').toggle();
        }
    });

    //create extra text box for adding members to chatroom
    $('#addMemberBtn').click(function () {
        $('<div class="row justify-content-center"></div>').insertBefore('#addMemberBtn').append('<input type="text" id="txtSearchUsers" name="username" placeholder="Chatroom Member" style="border-radius: 10px; margin-top: 10px;" class="bg-dark text-white"></input>').append('<button type="button" class="close" style="margin-right: -23px; margin-left: 10px;"><span class="text-danger">x</span></button>');
    });

    //remove extra text boxes when x button is clicked
    $('#createChatroomForm').on('click', 'button.close', function (event) {
        $(this).parent().remove();
    });

    //calls getchatroom which calls display chatroom
    $('ui.contacts').on('click', 'li.chatroomListItem', function (event) {
        var chatroomId = $(this).find('.chatroom_id');
        $('.active').removeClass('active');
        $(this).addClass('active');
        getChatroom(chatroomId.val());
    });

    //calls create chatroom and clears the form in the popup
    $('#createChatroomBtn').on('click', function (event) {
        event.preventDefault();
        var isPublic = $('input[name="isPublic"]:checked', '#createChatroomForm').val();
        var chatroomName = $('input[name="chatroomName"]').val();
        var members = new Array();
        $('input[name="username"]').each(function () {
            members.push($(this).val());
        });
        createChatroom(isPublic, chatroomName, members);
        $('input[name="isPublic"]:checked', '#createChatroomForm').prop('checked', false);
        $('input[name="chatroomName"]').val('');
        var closeButtons = $('#createChatroomForm').find('button.close');
        $.each(closeButtons, function (index, button) {
            $(button).parent().remove();
        });
        $('input[name="username"]').val('');
    });

    //calls send message
    $('#send-msg-btn').on('click', function (event) {
        event.preventDefault();
        var messageText = $('textarea[name="new-message"]').val();
        activeChatroomId = $('#active_chatroom_id').val();
        if (activeChatroomId && messageText) {
            connection.invoke('SendMessage', messageText, currentUserId.toString(), activeChatroomId.toString()).catch(function (err) {
                return console.error(err.toString());
            });
            $('textarea[name="new-message"]').val('');
        }
    });

    //delete chatroom
    $('#action_menu_list').on('click', '#delete_chatroom_li', function (event) {

    });

    //block user
    $('#action_menu_list').on('click', '#block_user_li', function (event) {

    });

    //invite user
    $('#action_menu_list').on('click', '#invite_user_li', function (event) {

    });

    //leave chatroom
    $('#action_menu_list').on('click', '#leave_chatroom_li', function (event) {
        $('.action_menu').toggle();
        leaveChatroom();
    });

    //START SIGNALR CONNECTION/////////////////////////////////////////////////////////////////////////////////////
    //the connection must started after the connection.on event listeners are defined
    //enable send button when connection is started
    connection.start().then(function () {
        //add user's connection to each chatroom group
        $('.chatroom_id').each(function (index) {
            connection.invoke('AddCurrentUserToGroup', $(this).val()).catch(function (err) {
                return console.error(err.toString());
            });
        });
        $('.send_btn').prop('disabled', false);
    }).catch(function (err) {
        return console.error(err.toString());
    });
});