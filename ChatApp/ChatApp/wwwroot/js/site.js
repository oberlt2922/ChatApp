var userId;

$(document).ready(function () {
    //display chatroom
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
        //add hidden input with chatroom id to send message form
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

    //calls getchatroom with calls display chatroom
    $('ui.contacts').on('click', 'li.chatroomListItem', function (event) {
        var chatroomId = $(this).find('.chatroom_id');
        userId = $('.user_id');
        $('.active').removeClass('active');
        $(this).addClass('active');
        getChatroom(chatroomId.val());
    });
});