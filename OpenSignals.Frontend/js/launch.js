$(document).ready(function ()
{
    LR.lrInstance = new LrInstance('container', {

        tagLine: "Segnala, condividi, aiuta a risolvere i problemi della tua città!",
        description: "Mettiaposto ti da la possibilità di segnalare un problema che hai trovato nella tua città (una strada dissestata, una buca, un murales non opportuno, spazzatura all'aperto), restarne informato, condividerlo e commentarlo con gli altri cittadini",
        refCodeUrl: "http://mettiaposto.it/?ref=",
        lrDomain: "mettiaposto.it",
        apiKey: "add78c72bc7d90af8a1a044a4b44bcf5",
        inviteList: "E' quasi pronto, lascia la tua email per partecipare alla beta pubblica"
    });

    // Handles events related to signup form, form validations
    // and submitting the form to the server:
    LR.signupForm = new SignupForm({
        secondaryPostLocation: ""
    });


    // Handles rendering the post submit content:
    LR.postSubmit = new PostSignupForm('pagesubmit', {
        twitterHandle: "mettiaposto",
        twitterMessage: "Mettiaposto è quasi pronto! #launch",
        newUserHeaderText: "Vuoi vederlo subito?",
        newUserParagraphText: "Invita almeno 3 amici! Piu' amici coinvolgi prima potrai provare il servizio in anteprima.<br/><br/>Clicca su Share o Tweet per condividere!",
        newUserParagraphText3: "O copia e incolla questo link",
        returningUserHeaderText: "Welcome Back!",
        returningUserParagraphText: "Invita almeno 3 amici! Piu' amici coinvolgi prima potrai provare il servizio in anteprima.<br/><br/>Clicca su Share o Tweet per condividere!",
        returningUserParagraphText3: "O copia e incolla questo link",
        statsPreText: "Your live stats: ",
        footerLinks: "<a href='http://twitter.com/mettiaposto'>Follow Us on Twitter</a> | <a href='http://www.facebook.com/pages/Mettiaposto/179851262071721'>Like Us on Facebook</a>"
, showDescription: true,
        showTagLine: true,
        showHeaderText: true,
        showParagraphText: true,
        showStats: true,
        showShareButtons: true,
        showFooterLinks: true
    });


});

function doPoll()
{
    var dialog = $('<div id="pollDialog"></div>');
    dialog.html('<iframe src="https://spreadsheets.google.com/spreadsheet/embeddedform?formkey=dGlWa2hFQ1BPRGVCWE1wc05wNGIzb2c6MQ" width="760" height="2520" frameborder="0" marginheight="0" marginwidth="0">Caricamento in corso...</iframe>');
    dialog.dialog({ width: 810, height: 500, title: 'Partecipa al sondaggio' });
}