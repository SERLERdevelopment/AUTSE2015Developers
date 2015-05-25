/// <reference path="jquery-1.10.1-vsdoc.js" />
/// 
$.lazyload = function (options) {
    var outerContainer = $('.lazyload-container');
    var searchPanel = $('#' + outerContainer.attr('data-search-panel'));
    var container = $('#' + outerContainer.attr('data-lazyload-id'));
    var searchBox = $('#' + outerContainer.attr('data-search-id'));
    var searchButton = $('#' + outerContainer.attr('data-search-btn'));
    var searchCount = $('#' + outerContainer.attr('data-search-count'));
    if (searchPanel.length > 0)
    {
        var searchPanelBottom = searchPanel.offset().top + searchPanel.outerHeight();
        var mockSearchPanel = $('<div></div>').hide();
        mockSearchPanel.insertAfter(searchPanel);
        mockSearchPanel.outerHeight(searchPanel.outerHeight());
    }
    
    var searchText = '';
    var url = outerContainer.attr('data-url');

    searchBox.keypress(function (e) {
        if (e.keyCode == 13) {
            searchText = $(this).val();
            container.empty();
            load();
        }
    });

    searchButton.click(function (e) {
        e.preventDefault();
        searchText = searchBox.val();
        container.empty();
        load();
    });

    if (!options.type)
    {
        options.type = 'GET';
    }

    var repositionSearchPanel = function () {
        if (searchPanel.length > 0)
        {
            var windowOffset = options.scrollContainer.scrollTop();
            if (windowOffset >= searchPanelBottom) {
                mockSearchPanel.show();
                searchPanel.css('position', 'fixed')
                    .css('z-index', 999999)
                    .css('top', 0)
                    .css('left', 0)
                    .css('background-color', 'rgba(255,255,255,0.9)')
                    .css('margin', '2px 0')
                    .css('width', '100%')

            }
            else {
                mockSearchPanel.hide();
                searchPanel.css('position', '')
                    .css('z-index', '')
                    .css('top', '')
                    .css('left', '')
                    .css('background-color', '')
                    .css('margin', '')
                    .css('width', '');
            }
        }
    };

    var displayCount = function (total) {

        if (searchCount.length > 0) {
            if (total === undefined)
                total = 0;
            var displaying = $('.lazyload-item', container).length;
            searchCount.text('Displayed ' + displaying + " of " + total);
        }

    };

    var process = function (more) {
        options.token = more.attr('data-token');
        if (more.offset().top < options.scrollContainer.scrollTop() + options.scrollContainer.height()) {
            more.remove();
            load();
            return true;
        }
        else
            return false;
    }
    

    var load = function () {
        options.scrollContainer.off('scroll');
        var json = {
            skip: $('.lazyload-item', container).length,
            take: options.take,
            search: searchText,
            filter: options.filter,
            searchToken: options.token
        };

        var postdata = options.type == 'GET' ? json : JSON.stringify(json);

        $.ajax({
            url: url,
            type: options.type,
            contentType: 'application/json, charset=utf-8',
            data: postdata,
            success: function (data) {
                $data = $(data)
                container.append($data);
                // do we want to put any animation effect here?
                var more = $('.lazyload-more', container);
                displayCount(more.attr('data-total'));
                if(more.length == 1){
                    if (!process(more)) {
                        options.scrollContainer.scroll(function () {
                            repositionSearchPanel();
                            process(more);
                        });
                    }
                } else {
                    options.scrollContainer.scroll(function () {
                        repositionSearchPanel();
                    });
                }
                if (options.ready) {
                    options.ready($data);
                }
            }
        });
    };

    return {
        init: function () {
            load();
        }
    };
    
}