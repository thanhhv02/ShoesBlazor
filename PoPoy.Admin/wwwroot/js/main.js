

$(document).ready(function () {

    window.OpenNav = function (isOpen) {
        if (!isOpen) {
            $('body').addClass('toggle-sidebar');
        }
        else {
            $('body').removeClass('toggle-sidebar');
        }
        
    }


})


$(document).ready(function () {
    window.currentChatUserId = '#chat-user'; 
    window.setId = function (userId) {
        currentChatUserId = "#chat-user_" + userId;
    }
    window.scrollToBottom = function (id) {
        $(id).animate({ scrollTop: $(id).prop("scrollHeight") }, { queue: false, duration: 0 });
    }
    window.sendChatmini = function (message, id) {
        if (message == "{{html}}") {
            message = "Đã gửi một thông tin đơn hàng !";
        }
        $("#usermessage_" + id).text(message);
        var countNow = parseInt($("#usercount_" + id).text());
        $("#usercount_" + id).text(countNow + 1);
        $("#usercount_" + id).css("display", "block");
    }
    window.sendChatmini2 = function (message, id) {
        if (message == "{{html}}") {
            message = "Đã gửi một thông tin đơn hàng !";
        }
        $("#usermessage_" + id).text(message);

    }
    window.hideCount = function (id) {
        $("#usercount_" + id).text(0);
        $("#usercount_" + id).css("display", "none");

    }

    window.clearChat = function () {
        $("#chat-user_sub").empty();
    }

    window.sendChat = function (message, time, srcAvt , data) {
        if (!srcAvt) {
            srcAvt = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3-bg.webp";
        }
      
        html = `
                                            <div class="chat-message-right pb-4">
												<div>
													<img src="${srcAvt}" class="rounded-circle mr-1" alt="Sharon Lessman" width="40" height="40">
												</div>
												<div class="flex-shrink-1 bg-light rounded py-1 px-3 ml-3">
															${message}
													<div class="text-muted small text-nowrap mt-2 text-end">${time}</div>

												</div>
											</div>
                `
        $("#chat-user_sub").append(html);
        scrollToBottom("#chat-user");
    }
    window.receiveChat = function (message, time, srcAvt, data) {
        debugger
        if (!srcAvt) {
            srcAvt = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3-bg.webp";
        }
        var srcData = message;
        if (srcData == "{{html}}") {
            srcData = data;
        }
        var html = `<div class="chat-message-left pb-4">
											<div>
												<img src="${srcAvt}" class="rounded-circle mr-1" alt="Sharon Lessman" width="40" height="40"/>
											</div>
											<div class="flex-shrink-1 bg-light rounded py-2 px-3 ml-3 text_message">
												${srcData}
												<div class="text-muted small text-nowrap mt-2">${time}</div>

											</div>
										</div>`;
        $("#chat-user_sub").append(html);
        scrollToBottom("#chat-user");
    }

})