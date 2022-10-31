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