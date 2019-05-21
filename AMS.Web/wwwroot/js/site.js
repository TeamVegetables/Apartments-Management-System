// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#change').on('show.bs.modal',
    function (e) {
        var userId = $(e.relatedTarget).data('user-id');
        $(e.currentTarget).find('input[name="UserId"]').val(userId);
    });

$('#change-role').on('show.bs.modal',
    function (e) {
        var userId = $(e.relatedTarget).data('user-id');
        $(e.currentTarget).find('input[name="UserId"]').val(userId);
        var oldRole = $(e.relatedTarget).data('user-role');
        $(e.currentTarget).find('input[name="OldRole"]').val(oldRole);
    });