<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Head.ascx.cs" Inherits="OpenSignals.Frontend.Admin.Includes.Head" %>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.10/jquery-ui.min.js"></script>
<link href="/css/flick/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />
<script src="/js/jquery/plugins/superfish/supersubs.js" type="text/javascript"></script>
<script src="/js/jquery/plugins/superfish/superfish.js" type="text/javascript"></script>
<link href="/js/jquery/plugins/superfish/superfish.css" rel="stylesheet" type="text/css" />
<link href="/Admin/css/style.css" rel="stylesheet" type="text/css" />
<os:StaticFileManager ID="adminCommonScripts" ContextKey="adminCommonScripts" runat="server">
    <os:StaticFile Url="/js/functions.js" Type="Javascript" />
    <os:StaticFile Url="/js/validation.js" Type="Javascript" />
    <os:StaticFile Url="/js/json.js" Type="Javascript" />
</os:StaticFileManager>
