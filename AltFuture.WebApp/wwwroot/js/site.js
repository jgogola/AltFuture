

// Set the active class to the current page's menu item and dropdown menu
$(function () {
    let currentPath = window.location.pathname;

    const pathParts = currentPath.split("/");
    currentPath = pathParts.slice(0, 3).join("/"); //* Only use the first 3 parts of the path

    $(`nav a[href="${currentPath}"]`).addClass('active');
    $(`nav a[href="${currentPath}"]`).parents('.nav-item.dropdown').find('.dropdown-toggle:first').addClass('active');
});


// Horizontal grab scrolling
$(document).ready(function () {
    const container = $('.horizontal-scrolling-container');
    let isMouseDown = false;
    let startX;
    let scrollLeft;

    container.on('mousedown', function (e) {
        isMouseDown = true;
        container.addClass('grabbing');
        startX = e.pageX - container.offset().left;
        scrollLeft = container.scrollLeft();
    });

    container.on('mouseleave', function () {
        isMouseDown = false;
        container.removeClass('grabbing');
    });

    container.on('mouseup', function () {
        isMouseDown = false;
        container.removeClass('grabbing');
    });

    container.on('mousemove', function (e) {
        if (!isMouseDown) return;
        e.preventDefault();
        const x = e.pageX - container.offset().left;
        const walk = (x - startX) * 2;
        container.scrollLeft(scrollLeft - walk);
    });
});


