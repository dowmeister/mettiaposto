$(document).ready(function ()
{
    LR.lrInstance = new LrInstance('container', {

        tagLine: "Segnala i problemi della tua citta'",
        description: "Il primo servizio di segnalazione dei disservizi e dei problemi della città",
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
        twitterHandle: "@mettiaposto",
        twitterMessage: "Mettiaposto è quasi pronto! #launch",
        newUserHeaderText: "Vuoi vederlo subito?",
        newUserParagraphText: "Invita almeno 3 amici! Piu' amici coinvolgi prima potrai provare il servizio in anteprima.<br/><br/>Clicca su Share o Tweet per condividere!",
        newUserParagraphText3: "O copia e incolla questo link",
        returningUserHeaderText: "Bentornato!",
        returningUserParagraphText: "Invita almeno 3 amici! Piu' amici coinvolgi prima potrai provare il servizio in anteprima.<br/><br/>Clicca su Share o Tweet per condividere!",
        returningUserParagraphText3: "O copia e incolla questo link",
        statsPreText: "Le tue statistiche: ",
        footerLinks: "<a href='http://twitter.com/mettiaposto'>Seguici su Twitter</a> | <a href='http://www.facebook.com/apps/application.php?id=183751108307062'>Diventa Fan su Facebook</a>"
, showDescription: true,
        showTagLine: true,
        showHeaderText: true,
        showParagraphText: true,
        showStats: true,
        showShareButtons: true,
        showFooterLinks: true
    });


});