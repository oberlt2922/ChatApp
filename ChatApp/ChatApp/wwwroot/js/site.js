var userId;

$(document).ready(function () {
    //get chatroom ajax function
    function getChatroom(id) {
        $.ajax({
            type: 'POST',
            url: '../Home/GetChatroom',
            data: { 'chatroomId': id },
            dataType: 'json'
        }).done(function (json) {
            console.log(json);
        });
    }

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

    $('ui.contacts').on('click', 'li.chatroomListItem', function (event) {
        var chatroomId = $(this).find('.chatroom_id');
        userId = $('.user_id');
        getChatroom(chatroomId.val());
    });
});

/*search chatrooms auto complete
 *  EVENT LISTENER WILL ONLY BE CREATED FOR TEXT BOXES THAT ALREADY EXIST
 *  MUST CREATE EVENT LISTENER FOR TEXT BOXES CREATED AFTER DOCUMENT IS READY
 *  RESEARCH EVENT DELEGATION SO THAT EVENT BEHAVIORS WILL BE EXTENDED TO NEW ELEMENTS WITHOUT HAVING
 *  TO REBIND THEM
$(document).ready(function () {
    $("#txtSearchChatrooms").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/ChatroomList/AutoCompleteChatrooms',
                type: 'POST',
                cache: false,
                data: { "prefix": request.term },
                dataType: 'json',
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            chatroomId: data.chatroomId,
                            chatroomName: data.chatroomName,
                            data: item
                        }
                    }))
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $.confirm({
                title: 'Join Chatroom',
                content: 'Would you like to join the chatroom \"' + ui.item.chatroomName + '\"\?',
                buttons: {
                    confirm: function () {
                        window.location.href = url.replace('id', ui.item.chatroomId);
                    }
                }
            });
        }
    });
});*/