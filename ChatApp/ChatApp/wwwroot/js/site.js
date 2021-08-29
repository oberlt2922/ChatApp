"use strict";

//current username and user id
var userId = $('#active_user_id').val();
var username = $('#active_username').val();

$(document).ready(function () {  
    //SIGNALR CODE
    //create signalr connection and disable send button until connection starts and the dom is ready
    var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    $('.send_btn').prop('disabled', true);

    //when a new chatroom is created each member will be added to the group
    connection.on('AddToNewGroup', function (chatroomId) {
        connection.invoke('AddCurrentUserToNewGroup', chatroomId, userId).catch(function (err) {
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
            addChatroomToList(result);
        });
    });

    //DOM FUNCTIONS
    //display chatroom function to be called when chatroom is clicked or created
    function displayChatroom(chatroom) {
        $('.chat_chatroom_name').text(chatroom.chatroomName);
        $('.message_count').text(chatroom.messages.length + ' Messages');
        $('.members_count').text(chatroom.members.length + ' Members');
        $('.msg_card_body').empty();
        $.each(chatroom.messages, function (index, message) {
            var div = '<div class="d-flex mb-4"></div>';
            var msgContainer = '<div></div>';
            if (message.Sender.id == userId) {
                div.addClass('justify-content-end');
                msgContainer.addClass('msg_cotainer_send');
            }
            else {
                div.addClass('justify-content-start');
                msgContainer.addClass('msg_cotainer');
            }
            $(msgContainer).html(message.text + '<span class="msg_time">' + message.Sender.userName + ' ' + message.sent + '</span>')
            div.append(msgContainer);
            $('.msg_card_body').append(div);
        });
        $('#active_chatroom_id').val(chatroom.chatroomId);
    }

    //add chatroom to list
    function addChatroomToList(chatroom) {
        var listItem = $('<li class="chatroomListItem" style="border-bottom-style:solid; border-bottom-color: lightslategrey; border-bottom-width: 1px;"></li>');
        if (chatroom.adminId == userId) {
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
        if (!$.isEmptyObject(chatroom.Messages)) {
            var messageText = $('<p id="message-text-preview">' + chatroom.Messages[chatroom.Messages.length - 1].text + '</p>');
            var messageSent = $('<p>' + chatroom.Messages[chatroom.Messages.length - 1].sent + '</p>');
            $(div2).append(messageText).append(messageSent);
        }
        $('ui.contacts').prepend(listItem);
    }

    //AJAX FUNCTIONS
    function getChatroom(id) {
        $.ajax({
            type: 'POST',
            url: '../Home/GetChatroom',
            data: { 'chatroomId': id },
            dataType: 'json'
        }).done(function (result) {
            console.log(result);
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


    //EVENT LISTENERS
    //toggles the chatroom menu
	$('#action_menu_btn').click(function () {
		$('.action_menu').toggle();
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
        var isPublic = $('input[name="isPublic"]:checked', '#createChatroomForm').val()
        var chatroomName = $('input[name="chatroomName"]').val();
        var members = $('#createChatroomForm').find('input[name="username"]').val();
        createChatroom(isPublic, chatroomName, members);
        $('input[name="isPublic"]:checked', '#createChatroomForm').prop('checked', false);
        $('input[name="chatroomName"]').val('');
        var closeButtons = $('#createChatroomForm').find('button.close');
        $.each(closeButtons, function (index, button) {
            $(button).parent().remove();
        });
        $('input[name="username"]').val('');
    });

    //START SIGNALR CONNECTION
    //the connection must started after the connection.on event listeners are defined
    //enable send button when connection is started
    connection.start().then(function () {
        console.log("connection started!!!!!!!");
        //add user's connection to each chatroom group
        $('.chatroom_id').each(function (index) {
            connection.invoke('AddCurrentUserToGroup', $(this).val()).catch(function (err) {
                return console.error(err.toString());
            });
            console.log('added user to chatroom ' + $(this).val());
        });
        $('.send_btn').prop('disabled', false);
    }).catch(function (err) {
        return console.error(err.toString());
    });
});