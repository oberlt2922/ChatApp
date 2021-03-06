"use strict";

$(document).ready(function () {
    //VARIABLES AND CODE THAT RUNS AS SOON AS THE DOM IS READY///////////////////////////////////////////////////////////////////////////////////////////
    //current username and user id
    var currentUserId = $('#active_user_id').val();
    var currentUsername = $('#active_username').val();
    var mcsContainerHeight;
    var activeChatroomId;
    var startPos;
    var attached;


    //add custom scrollbar to chatroom list when page is loaded
    $('.contacts_body').mCustomScrollbar();

    //SIGNALR CODE////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //create signalr connection and disable send button until connection starts and the dom is ready
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    $('.send_btn').prop('disabled', true);

    //This function is called on all members of a newly created chatroom
    //It then calls AddCurrentUserToNewGroup() to add the current user to the chatroom group.
    //This is because in order to add a user to a group, you have to add their ConnectionId to the group.
    //Hub methods can only access the connection id of the current client.
    connection.on('AddToNewGroup', function (chatroomId) {
        connection.invoke('AddCurrentUserToNewGroup', chatroomId, currentUserId).catch(function (err) {
            return console.error(err.toString());
        });
    });

    //After the current user is added to a newly created chatroom,
    //the chatroom is fetched from the database and added to the chatroom list.
    //The chatroom is set to active inside addChatroomToList() ONLY IF THE CURRENT USER IS THE CHATROOM ADMIN
    connection.on("AddNewChatroomToList", function (chatroomId) {
        getChatroom(chatroomId).then(result => addChatroomToList(result, false));
    });

    //Receive a message and display it in the chatroom
    connection.on("ReceiveMessage", function (messageJson) {
        var message = $.parseJSON(messageJson);
        displayMessage(message);
        if (activeChatroomId == message.chatroomId) {
            var messageCount = parseInt($('.message_count').text());
            $('.message_count').text(++messageCount);
            if (message.userId == null || message.userId == '') {
                var membersCount = parseInt($('.members_count').text());
                if (message.text.includes('joined')) {
                    $('.members_count').text(++membersCount);
                }
                else if (message.text.includes('left') || message.text.includes('blocked')) {
                    $('.members_count').text(--membersCount);
                }
            }
        }
    });

    //Grants admin priveleges to the newly appointed admin by adding action icons in the action menu.
    //This only happens if the admin is currently in the chatroom where they have been appointed as the admin.
    connection.on("GrantAdminPrivileges", function (chatroomId) {
        if (chatroomId == activeChatroomId.toString()) {
            displayActionIcons(currentUserId, "");
        }
    });

    //Called on all clients in a group when the admin deletes the chatroom
    //Removes the current user from the SignalR group
    //Alerts teh users that the chatroom has been deleted if the chatroom is currently active.
    //Calls removeChatroom()
    connection.on('RemoveChatroom', function (chatroomId, adminId) {
        connection.invoke('RemoveCurrentUserFromGroup', chatroomId).catch(function (err) {
            return console.error(err.toString());
        });
        if (activeChatroomId == chatroomId && currentUserId != adminId) {
            $.alert({
                title: 'Uh-Oh!',
                content: 'The admin deleted this chatroom!',
            });
        }
        removeChatroom(chatroomId);
    });

    //alert user that they have been blocked if the chatroom is active
    //remove chatroom from list and clear chatroom panel
    //remove user from signalr group
    connection.on('BlockUser', function (chatroomId) {
        if (activeChatroomId == chatroomId)
            $.alert('You have been blocked from this chatroom.');
        removeChatroom(chatroomId);
        connection.invoke('RemoveCurrentUserFromGroup', chatroomId).catch(function (err) {
            console.error(err.toString());
        })
    });

    //Called when an exception is thrown in a chathub method.
    //Displays the exception message in the console.
    connection.on('DisplayError', function (errorMessage) {
        console.error(errorMessage);
    });

    //DOM FUNCTIONS/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //First clears the action menu, 
    //then adds action menu items depending on whether the chatroom is public and if the current user is the admin.
    function displayActionIcons(adminId, isPublic) {
        $('#action_menu_list').empty();
        if (adminId == currentUserId) {
            var deleteChatroomLi = $('<li id="delete_chatroom_li"><i class="fas fa-trash"></i> Delete chatroom</li>');
            var blockUserLi = $('<li id="block_user_li" data-toggle="modal" data-target="#blockUserModal"><i class="fas fa-ban"></i> Block user</li>');
            $('#action_menu_list').append(deleteChatroomLi).append(blockUserLi);
        }
        if (adminId == currentUserId || isPublic == true) {
            var inviteUsersLi = $('<li id="invite_user_li" data-toggle="modal" data-target="#addMembersModal"><i class="fas fa-users"></i> Invite users</li>');
            $('#action_menu_list').append(inviteUsersLi);
        }
        var leaveChatroomLi = $('<li id="leave_chatroom_li"><i class="fas fa-sign-out-alt"></i> Leave chatroom</li>');
        $('#action_menu_list').append(leaveChatroomLi);
    }

    //Destroys the custom scrollbar.
    //Displays the chatroom in the chatroom panel.
    //Adds the custom scrollbar to the newly selected chatroom.
    //When a new message is added to the chatroom the scrollbar is updated.
    //If the user is already scrolled to the bottom of the chatroom when a new message is received,
    //then the chatroom automatically scrolls to the bottom.
    //Calls displayMessage for every message in the chatroom.
    //Automatically scrolls down to the bottom of the chatroom when initially displayed.
    function displayChatroom(chatroom) {
        attached = true;
        $('.msg_card_body').mCustomScrollbar("destroy");
        displayActionIcons(chatroom.adminId, chatroom.isPublic);
        activeChatroomId = chatroom.chatroomId;
        $('.chat_chatroom_name').text(chatroom.chatroomName);
        $('.count').show();
        $('.message_count').text(chatroom.messages.length);
        $('.members_count').text(chatroom.members.length);
        $('.msg_card_body').empty();
        $('.msg_card_body').mCustomScrollbar({
            callbacks: {
                onInit: function () {
                    $(this).mCustomScrollbar("scrollTo", "bottom");
                },
                onUpdate: function () {
                    if (this.mcs) {
                        if (attached) {
                            $(this).mCustomScrollbar("scrollTo", "bottom");
                        }
                    }
                },
                whileScrolling: function () {
                    if (this.mcs.top > startPos && startPos != "") {
                        attached = false;
                        $('#scroll_down_btn').show();
                    }
                },
                onScrollStart: function () {
                    startPos = this.mcs.top;
                }
            }
        });
        $.each(chatroom.messages, function (index, message) {
            displayMessage(message);
        });
        $('.msg_card_body').mCustomScrollbar("scrollTo", "bottom");
    }

    //Displays a message with the correct classes depending on the message's userId.
    //Updates the chatroom's custom scrollbar.
    //Updates the message text preview and message sent time in the chatroom list.
    //Increases the message count
    function displayMessage(message) {
        if (message.chatroomId == activeChatroomId) {
            var div;
            if (message.userId == null || message.userId == "") {
                div = $('<div class="d-flex mb-4 justify-content-center bg-dark bg-transparent"></div>');
                $(div).append('<p class="text-white">' + message.text + '</p>');
            }
            else {
                div = $('<div class="d-flex mb-4"></div>');
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
                $(div).append(msgContainer);
            }
            mcsContainerHeight = $('.msg_card_body .mCSB_container').height();
            $('.msg_card_body .mCSB_container').append(div);
            $('.msg_card_body').mCustomScrollbar("update");
        }
        $('#msg-preview-txt-' + message.chatroomId).text(message.text);
        $('#msg-preview-sent-' + message.chatroomId).text(moment(message.sent).calendar());
    }

    //Adds a newly created or joined chatroom to the chatroom list.
    //If the admin id equals the current user id,
    //then that means the new chatroom was created by the current user and the new chatroom is set to active.
    //If active is set to true, that means the current user chose to join the chatroom after searching for it,
    //then the chatroom is set to active
    //Updates the chatroom list's custom scrollbar which was created when the page loaded.
    function addChatroomToList(chatroom, active) {
        var listItem = $('<li id="chatroom-list-item-' + chatroom.chatroomId + '" class="chatroomListItem"></li>');
        if (chatroom.adminId == currentUserId || active == true) {
            $('.active').removeClass('active');
            $(listItem).addClass('active');
            activeChatroomId = chatroom.chatroomId;
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
        var messageText = $('<p id="msg-preview-txt-' + chatroom.chatroomId + '" class="message-preview"></p>');
        var messageSent = $('<p id="msg-preview-sent-' + chatroom.chatroomId + '" class="message-preview"></p>');
        $(div2).append(messageText).append(messageSent);
        if (!$.isEmptyObject(chatroom.messages)) {
            $(messageText).text(chatroom.messages[chatroom.messages.length - 1].text);
            $(messageSent).text(moment(chatroom.messages[chatroom.messages.length - 1].sent).calendar());
        }
        $('ui.contacts').prepend(listItem);
        $('.contacts_body').mCustomScrollbar("update");
    }

    //Clears the chatroom panel if the chatroom being removed is active
    //Emptys the action menu
    //Removes the chatroom list item
    function removeChatroom(chatroomId) {
        $('li#chatroom-list-item-' + chatroomId).remove();
        if (activeChatroomId == chatroomId) {
            $('#action_menu_list').empty();
            $('.chat_chatroom_name').text('');
            $('.count').hide();
            $('.message_count').text('');
            $('.members_count').text('');
            $('.msg_card_body').mCustomScrollbar("destroy");
            $('.msg_card_body').empty();
            activeChatroomId = null;
        }
    }

    //AJAX FUNCTIONS////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Gets the chatroom and displays it in the chatroom panel.
    function getChatroom(id) {
        return $.ajax({
            type: 'GET',
            url: '../Home/GetChatroom',
            data: { 'chatroomId': id },
            dataType: 'json'
        });
    }

    //Creates a new chatroom, adds the members to the chatroom group, and displays the chatroom.
    function createChatroom(isPublic, chatroomName, usernames) {
        $.ajax({
            type: 'POST',
            url: '../Home/CreateChatroom',
            data: { 'isPublic': isPublic, 'chatroomName': chatroomName, 'usernames': [...new Set(usernames)] },
            datatype: 'json'
        }).done(function (result) {
            connection.invoke('AddMembersToGroup', result.chatroomId.toString(), JSON.stringify(result.members)).catch(function (err) {
                return console.error(err.toString());
            });
            displayChatroom(result);
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //Adds new members to the chatroom
    function addMembers(chatroomId, usernames) {
        $.ajax({
            type: 'POST',
            url: '../Home/AddMembers',
            data: { 'chatroomId': chatroomId, 'usernames': [...new Set(usernames)] },
            datatype: 'json'
        }).done(function (result) {
            $.each(result.members, function (index, member) {
                var text = member.userName + ' has joined the chatroom.';
                connection.invoke('SendNonUserMessage', text, chatroomId.toString(), currentUserId, false).catch(function () {
                    return console.error(err.toString());
                })
            });
            connection.invoke('AddMembersToGroup', result.chatroomId.toString(), JSON.stringify(result.members)).catch(function (err) {
                return console.error(err.toString());
            });
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //Adds the current user to the chatroom.
    //If display is true then the current user chose to join the chatroom, and the chatroom is displayed.
    //If display is false then the current user was added to the chatroom by someone else
    //and the chatroom is not displayed so that the user's current activity is not disrupted.
    //Adds the joined chatroom to the chatroom list
    function joinChatroom(chatroomId, display) {
        return $.ajax({
            type: 'POST',
            url: '../Home/JoinChatroom',
            data: { 'chatroomId': chatroomId },
            datatype: 'json'
        }).done(function (result) {
            var text = currentUsername + ' has joined the chatroom.';
            connection.invoke('AddCurrentUserToGroup', result.chatroomId.toString()).then(function () {
                connection.invoke('SendNonUserMessage', text, chatroomId.toString(), currentUserId, false);
            }).catch(function (err) {
                $.alert({ title: 'error', content: 'An error occured while trying to connect to the group, try refreshing the page.', type: 'red' });
            });
            if (display == true) {
                $('.active').removeClass('active');
                addChatroomToList(result, true);
                displayChatroom(result);
            }
            else {
                addChatroomToList(result, false);
            }
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //Clears the chatroom panel
    //Removes the chatroom list item
    //Removes the current user from the chatroom members.
    //Removes the current user from the SignalR group
    //Alerts the other chatroom members that the user left the chatroom.
    //If the user is the chatroom admin, alerts the other members of the new admin.
    function leaveChatroom() {
        $.ajax({
            type: 'POST',
            url: '../Home/LeaveChatroom',
            data: { 'chatroomId': activeChatroomId },
            dataType: 'json'
        }).done(function (result) {
            connection.invoke('RemoveCurrentUserFromGroup', activeChatroomId.toString()).catch(function (err) {
                return console.error(err.toString());
            });
            if (!result.deleted) {
                if (result.adminChanged == false) {
                    var text = currentUsername + ' left the chatroom.';
                    connection.invoke('SendNonUserMessage', text, activeChatroomId.toString(), currentUserId, false).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
                if (result.adminChanged == true) {
                    var text = currentUsername + ' left, ' + result.adminUsername + ' is now the admin.';
                    connection.invoke('SendNonUserMessage', text, activeChatroomId.toString(), currentUserId, true).catch(function (err) {
                        return console.error(err.toString());
                    });
                }
            }
            removeChatroom(activeChatroomId);
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //Deletes the chatroom from the database
    //Calls function on all group members to remove chatroom
    function deleteChatroom(chatroomId) {
        $.ajax({
            type: 'POST',
            url: '../Home/DeleteChatroom',
            data: { 'chatroomId': chatroomId }
        }).done(function () {
            connection.invoke('RemoveChatroom', chatroomId.toString(), currentUserId).catch(function (err) {
                return console.error(err.toString());
            });
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //checks if user exista
    function userExists(username) {
        return$.ajax({
            type: 'GET',
            url: '../Home/UserExists',
            data: { 'username': username },
            dataType: 'json'
        }).fail(function (jqXHR, textStatus) {
            $.alert({ title: textStatus, content: jqXHR.status + ' Error!', type: 'red' });
        });
    }

    //AUTOCOMPLETE FUNCTIONS///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Autocomplete that runs when the user types in the search chatroom text box
    //Displays a confirm popup that asks the user if they would like to join the selected chatroom.
    //If the user says yes, the user is added to the chatroom
    //and the other chatroom members are alerted of the user joining the chatroom.
    $("#txtSearchChatrooms").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: 'GET',
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
            $.confirm({
                title: 'Join this chatroom?',
                content: 'You are about to to join the chatroom \"' + ui.item.data.chatroomName + '\".',
                buttons: {
                    confirm: function () {
                        joinChatroom(ui.item.data.chatroomId, true);
                    },
                    cancel: function () {
                        $.alert('Canceled!');
                    }
                }
            });
        },
        close: function () {
            $('#txtSearchChatrooms').val('');
            $('#txtSearchChatrooms').data().uiAutocomplete.term = null;
        }
    });


    //EVENT LISTENERS////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Toggles the action menu
    $('#action_menu_btn').click(function (event) {
        event.stopPropagation();
        if (activeChatroomId) {
            $('.action_menu').toggle();
        }
    });

    //Hides the action menu if it is visible and the click occurs outside the action menu
    $(document).click(function (event) {
        var $target = $(event.target);
        if (!$target.closest('.action_menu').length && $('.action_menu').is(":visible")) {
            $('.action_menu').toggle();
        }
    });

    //Creates extra text box with x button and inserts it before the element that fired the event
    $('.addNewTextBoxBtn').click(function () {
        $('<div class="row justify-content-center"></div>').insertBefore($(this).parent()).append('<input type="text" id="txtSearchUsers" name="username" placeholder="Chatroom Member" class="bg-dark text-white"></input>').append('<button type="button" class="close"><span class="text-danger">x</span></button>');
    });

    //Removes extra text boxes when x button is clicked
    $('form').on('click', 'button.close', function (event) {
        $(this).parent().remove();
    });

    //When a chatroom list item is clicked, it is set to active and the chatroom is fetched and displayed.
    $('ui.contacts').on('click', 'li.chatroomListItem', function (event) {
        if (!$(this).hasClass('active')) {
            var chatroomId = $(this).find('.chatroom_id');
            $('.active').removeClass('active');
            $(this).addClass('active');
            getChatroom(chatroomId.val()).then(result => displayChatroom(result));
        }
    });

    //Calls create chatroom and clears the form in the create chatroom modal
    $('#createChatroomBtn').on('click', function (event) {
        event.preventDefault();
        var isPublic = $('input[name="isPublic"]:checked', '#createChatroomForm').val();
        var chatroomName = $('input[name="chatroomName"]').val();
        var members = new Array();
        $('#createChatroomForm input[name="username"]').each(function () {
            members.push($(this).val());
        });
        createChatroom(isPublic, chatroomName, members);
        $('input[name="isPublic"]:checked', '#createChatroomForm').prop('checked', false);
        $('input[name="chatroomName"]').val('');
        var closeButtons = $('#createChatroomForm').find('button.close');
        $.each(closeButtons, function (index, button) {
            $(button).parent().remove();
        });
        $('#createChatroomForm input[name="username"]').val('');
    });

    //Calls add members and clears the form in the add members modal
    $('#addMembersBtn').on('click', function (event) {
        event.preventDefault();
        var usernames = new Array();
        $('#addMembersForm input[name="username"]').each(function () {
            usernames.push($(this).val());
        });
        addMembers(activeChatroomId, usernames);
        var closeButtons = $('#addMembersForm').find('button.close');
        $.each(closeButtons, function (index, button) {
            $(button).parent().remove();
        });
        $('#addMembersForm input[name="username"]').val('');
    });

    //Block user btn
    $('#blockUserBtn').on('click', function (event) {
        event.preventDefault();
        var username = $('#blockUserForm input[name="username"]');
        connection.invoke('BlockUser', $(username).val(), activeChatroomId.toString(), currentUserId).catch(function (err) {
            console.error(err.toString());
        });
        $(username).val('');
    });

    //Sends a message to the chatroom.
    $('#send-msg-btn').on('click', function (event) {
        event.preventDefault();
        var messageText = $('textarea[name="new-message"]').val();
        if (activeChatroomId && messageText) {
            connection.invoke('SendMessage', messageText, currentUserId.toString(), activeChatroomId.toString()).catch(function (err) {
                console.error(err.toString());
            });
            $('textarea[name="new-message"]').val('');
        }
    });

    //clicks the send message button when the enter key is pressed
    $(document).keypress(function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $('#send-msg-btn').click();
        }
    });

    //Scrolls to the bottom of the chatroom and enables auto scrolling
    $('#scroll_down_btn').on('click', function (event) {
        attached = true;
        $('.msg_card_body').mCustomScrollbar("scrollTo", "bottom");
        $('#scroll_down_btn').hide();
    })

    //delete chatroom
    $('#action_menu_list').on('click', '#delete_chatroom_li', function (event) {
        event.stopPropagation();
        $.confirm({
            title: 'Wanna delete the chatroom?',
            content: 'Are you sure you want to delete this chatroom?',
            buttons: {
                confirm: function () {
                    $('.action_menu').toggle();
                    deleteChatroom(activeChatroomId);
                },
                cancel: function () {
                    $.alert('Canceled!');
                }
            }
        });
    });

    //leave chatroom
    $('#action_menu_list').on('click', '#leave_chatroom_li', function (event) {
        event.stopPropagation();
        $.confirm({
            title: 'Are you leaving? :(',
            content: 'Are you sure you want to leave this chatroom?',
            buttons: {
                confirm: function () {
                    $('.action_menu').toggle();
                    leaveChatroom();
                },
                cancel: function () {
                    $.alert('Canceled!');
                }
            }
        });
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