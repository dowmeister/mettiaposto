$(document).ready(function () {
    $('#txtSearch').click(function () { $('#txtSearch').val(''); });
    $('#txtSearch').focus();
    $('#txtSearch').keypress(function (event) {
        if (event.which == 13) {
            search();
        }
    });
});

function search() {
    window.location.href = '/Submit.aspx?address=' + $('#txtSearch').val();
}