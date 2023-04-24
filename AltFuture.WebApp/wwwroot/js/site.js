// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {
    // Get the current page's URL
    var currentPath = window.location.pathname;

    // Set the active class to the current page's menu item and dropdown menu
    $('nav a[href="' + currentPath + '"]').addClass('active');
    $('nav a[href="' + currentPath + '"]').parents('.nav-item.dropdown').find('.dropdown-toggle:first').addClass('active');
});


