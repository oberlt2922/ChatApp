$(document).ready(function () {
    //toggles the chatroom menu
	$('#action_menu_btn').click(function () {
		$('.action_menu').toggle();
	});
});

//Adds an extra text box for adding members to a chatroom
function AddNewTextBox() {
    var form = document.getElementById("createChatroomForm");
    var button = document.getElementById("addMemberBtn");
    var closeButton = document.createElement("button");
    var div = document.createElement("div");
    var input = document.createElement("input");
    var span = document.createElement("span");

    input.type = "text";
    input.id = "txtSearchUsers";
    input.name = "username";
    input.placeholder = "Chatroom member";
    input.style = "border-radius: 10px; margin-top: 10px;"
    input.className = "bg-dark text-white";

    closeButton.type = "button";
    closeButton.className = "close";
    closeButton.style = "margin-right: -23px; margin-left: 10px;"
    closeButton.ariaLabel = "Close";
    closeButton.onclick = function () {
        var parentElement = this.parentElement;
        parentElement.remove();
    }

    span.ariaHidden = "true";
    span.textContent = "x";
    span.className = "text-danger";
    closeButton.appendChild(span);

    div.className = "row justify-content-center";
    div.appendChild(input);
    div.appendChild(closeButton);

    form.insertBefore(div, button);
}

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
});
*/