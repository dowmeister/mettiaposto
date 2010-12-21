$(document).ready(function ()
{
    $('#tabs').tabs({
        show: function (event, ui)
        {
            if ($(ui.panel).attr('map'))
            {
                var mapDiv = $(ui.panel).attr('mapDiv');
                var map = getMap(mapDiv);

                if (!map)
                {
                    initializeMap(mapDiv, 45.4636889, 9.1881408);
                }
            }
        }
    });
});

function searchSignals()
{
        
}