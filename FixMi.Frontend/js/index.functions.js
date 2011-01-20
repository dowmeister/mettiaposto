var initial = 'CAP, indirizzo, quartiere...';
$(document).ready(function ()
{
    $('#txtSearch').val(initial);
    $('#txtSearch').click(function () { $('#txtSearch').val(''); });
    $('#txtSearch').focus();
    $(document).keypress(function (event)
    {
        if (event.which == 13)
        {
            search();
            return false;
        }
    });
});

function search()
{
    if ($('#txtSearch').val() == '' || $('#txtSearch').val() == initial)
        alert('Inserire un indirizzo valido');
    else
        window.location.href = '/' + $('#ddlCities').text() + '/' + encodeURI($('#txtSearch').val()) + '/invia.aspx';
}