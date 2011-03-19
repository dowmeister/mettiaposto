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
        else
            $('.facebook-button').show();
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
    $('.facebook-button').hide();

    FB.api('/me', function (response)
    {
        socialUser = response;
        $('#txtName').val(response.name);
        $('#txtEmail').val(response.email);
        $('#userAvatar').attr('src', 'https://graph.facebook.com/' + response.id + '/picture?type=square');
        $('#userName').html(response.name);
        $('#loginStatus').show();
    });

    $('#nameContainer').hide();
    $('#emailContainer').hide();
}

function fbLogout()
{
    FB.logout(function (response)
    {
        socialUser = null;
        $('#nameContainer').show();
        $('#emailContainer').show();
        $('#loginStatus').hide();
        $('.facebook-button').show();
    });
}

function fbPostToFeed(params)
{
    FB.ui(
           {
               method: 'feed',
               name: params.name,
               link: params.link,
               picture: params.image,
               caption: params.caption,
               description: params.description,
               message: params.message
           },
           function (response)
           {
               if (response && response.post_id)
               {
                   alert('Post was published.');
               } else
               {
                   alert('Post was not published.');
               }
           }
         );
}