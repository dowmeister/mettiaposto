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
    window.location.href = '/' + $('#ddlCities').text() + '/' + encodeURI($('#txtSearch').val()) + '/invia.aspx';
}