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
    /*FB.Event.subscribe('auth.sessionChange', function (response)
    {
        if (response.session)
        {
            retrievePersonalData();
        }
    });*/
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
        socialUser = response;
        $('#txtName').val(response.name);
        $('#txtEmail').val(response.email);
        $('#userAvatar').attr('src', 'https://graph.facebook.com/' + response.id + '/picture?type=small&access_token=' + FB.getSession().access_token);
        $('#userName').html(response.name);
        $('#loginStatus').show();
    });

    $('#nameContainer').hide();
    $('#emailContainer').hide();
    $('#btnFBLogin').hide();
}

function fbLogout()
{
    FB.logout(function (response)
    {
        $('#nameContainer').show();
        $('#emailContainer').show();
        $('#btnFBLogin').show();
        socialUser = null;
        $('#loginStatus').hide();
    });
}