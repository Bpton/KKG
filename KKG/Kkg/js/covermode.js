$(document).ready(function () {

    // global vars
    var leftMenu = $('#leftMenu');
    var icon = leftMenu.find('.fa');
    var menu = leftMenu.find('.vert');

    // get the actual doc height when everything has loaded
    //    chHeight = function () {
    //       var fullHeight = Math.max($(document).height());
    //       leftMenu.css('min-height', fullHeight);
    //    };

    // pushmenu options
    leftMenu.multilevelpushmenu({
        containersToPush: [$('')],
        collapsed: true,        
        preventItemClick: false,
        backText: '',
        backItemIcon: 'fa fa-reply',
        //menuHeight: '100%',
        menuHeight: 555,
        onExpandMenuStart: function (options) {
            console.log(options);
            var level = leftMenu.multilevelpushmenu('activemenu').data('level');
            console.log('expanding start.. level=' + level);
            icon.addClass('fa-list-ul').removeClass('fa-reorder');
            menu.fadeOut();
        },
        onCollapseMenuEnd: function (options) {
            var level = leftMenu.multilevelpushmenu('activemenu').data('level');
          
            console.log('collapsing start.. level=' + level);
            if (level === null) {
                console.log('inactive');
                icon.addClass('fa-reorder').removeClass('fa-list-ul');
                menu.fadeIn()
            }
        },
        onMenuReady: function () {
                        icon.addClass('fa-reorder').removeClass('fa-list-ul');
                        menu.fadeIn();
        },
        onItemClick: function () {
            // First argument is original event object
            var event = arguments[0],
            // Second argument is menu level object containing clicked item (<div> element)
                $menuLevelHolder = arguments[1],
            // Third argument is clicked item (<li> element)
                $item = arguments[2],
            // Fourth argument is instance settings/options object
                options = arguments[3];

            //$('#leftMenu').multilevelpushmenu('collapse', 1);
           $('#leftMenu').multilevelpushmenu('collapse');

            // You can do some cool stuff here before
            // redirecting to href location
            // like logging the event or even
            // adding some parameters to href, etc...

            $('li').removeClass('active'); // reset any active element
            $item.addClass('active'); // set active class
            // Anchor href
            //var itemHref = $item.find( 'a:first' ).attr( 'href' );
            // Redirecting the page
            //location.href = itemHref;
        }
    });

    //    $(document).bind('DOMNodeInserted ajaxComplete', function () {
    //        chHeight();
    //    });

});