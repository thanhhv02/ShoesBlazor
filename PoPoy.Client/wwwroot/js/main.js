$(function() {

  $('.custom-dropdown').on('show.bs.dropdown', function() {
    // $(this).find('.dropdown-menu').first().stop(false, false).slideDown();
     // $(this).find('.dropdown-menu').addClass('active');
     var that = $(this);
    setTimeout(function(){
        that.find('.dropdown-menu').addClass('active');
    }, 100);
    

  });
  $('.custom-dropdown').on('hide.bs.dropdown', function() {
    $(this).find('.dropdown-menu').removeClass('active');
  });

});

// chat

$(document).ready(function () {
    window.scrollToBottom = function (id) {
        $(id).animate({ scrollTop: $(id).prop("scrollHeight") }, "slow");
    }
    window.sendChat = function (message, time, srcAvt, data) {
        var srcData = message;
        if (message == "{{html}}") {
            srcData = data;
        }
        var html = `
            <div class="d-flex flex-row justify-content-end animate__backInUp">
              <div>
                <p class="p-2 me-3 mb-1 text-white rounded-3 bg-primary">${srcData}
                </p>
                <p class="me-3 mb-3 rounded-3 text-muted d-flex justify-content-end">${time}</p>
              </div>
                 <img src="${srcAvt}"
                    alt="avatar 1" style="width: 45px; height: 100%;">
            </div>`
     

        $("#chat-user").append(html);
        scrollToBottom("#chat-user");
    }
    window.receiveChat = function (message, time, srcAvt, data) {
        var srcData = message;

        if (message == "{{html}}") {
            srcData = data;
        }
        var  html = `<div class="d-flex flex-row justify-content-start mb-1">
              <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3-bg.webp"
                alt="avatar 1" style="width: 45px; height: 100%;">
              <div>
                <p class=" p-2 ms-3 mb-1 rounded-3" style="background-color: #f5f6f7;">${srcData}</p>
                <p class=" ms-3 mb-3 rounded-3 text-muted">${time}</p>
              </div>
            </div>`;
        $("#chat-user").append(html);
        scrollToBottom("#chat-user");
    }


})

// navbar 