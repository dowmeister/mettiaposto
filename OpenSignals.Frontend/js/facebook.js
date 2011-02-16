/*
* FACEBOOK INTEGRATIONS FUNCTIONS
*/

function fbInit()
{
    FB.init({ appId: '183751108307062', cookie: true, xfbml: true });
    FB.getLoginStatus(function (response)
    {
        if (response.session)
        {
            retrievePersonalData();
        }
    });
}

function fbLogin()
{
    FB.login(function (response)
    {
        if (response.session)
            retrievePersonalData();
    }, { perms: 'publish_stream,email' });
}

function retrievePersonalData()
{
    FB.api('/me', function (response)
    {
        $('#txtName').val(response.name);
        $('#txtEmail').val(response.email);
    });

    $('#btnFBLogin').hide();
}