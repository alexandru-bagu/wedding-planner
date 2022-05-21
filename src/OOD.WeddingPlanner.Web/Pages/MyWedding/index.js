$(async function () {
    function sleep(ms) { return new Promise(function (resolve) { setTimeout(resolve, ms) }); }
    while (!window.require) await sleep(100);
    var l = abp.localization.getResource('WeddingPlanner');
    var container = $('body');
    var noteInput = container.find('[name="Wedding.InvitationNoteHtml"]');
    noteInput.hide();
    var noteMonacoContainer = $('<div style="height: 316px;"></div>');
    noteMonacoContainer.insertAfter(noteInput);

    var headerInput = container.find('[name="Wedding.InvitationHeaderHtml"]');
    headerInput.hide();
    var headerMonacoContainer = $('<div style="height: 316px;"></div>');
    headerMonacoContainer.insertAfter(headerInput);

    var footerInput = container.find('[name="Wedding.InvitationFooterHtml"]');
    footerInput.hide();
    var footerMonacoContainer = $('<div style="height: 316px;"></div>');
    footerMonacoContainer.insertAfter(footerInput);

    require(['vs/editor/editor.main'], function () {
        var noteEditor = monaco.editor.create(noteMonacoContainer[0], {
            value: noteInput.val(),
            language: 'html',
            automaticLayout: true
        });
        noteEditor.onDidChangeModelContent(function () {
            noteInput.val(noteEditor.getValue()).trigger('change');
        });

        var headerEditor = monaco.editor.create(headerMonacoContainer[0], {
            value: headerInput.val(),
            language: 'html',
            automaticLayout: true
        });
        headerEditor.onDidChangeModelContent(function () {
            headerInput.val(headerEditor.getValue()).trigger('change');
        });

        var footerEditor = monaco.editor.create(footerMonacoContainer[0], {
            value: footerInput.val(),
            language: 'html',
            automaticLayout: true
        });
        footerEditor.onDidChangeModelContent(function () {
            footerInput.val(footerEditor.getValue()).trigger('change');
        });
    });

});
