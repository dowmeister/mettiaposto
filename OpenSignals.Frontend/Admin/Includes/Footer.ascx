<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="OpenSignals.Frontend.Admin.Includes.Footer" %>
<div id="footer">
</div>
<script type="text/javascript" src="/js/StaticFileHandler.ashx?key=adminCommonScripts"></script>
<script>
    $(document).ready(function ()
    {
        $('#menu').supersubs().superfish({ dropShadows: false, delay: 400 });
    });
</script>
</div> 