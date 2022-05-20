var abp = abp || {};

abp.modals.weddingModal = function () {
    var initModal = function (publicApi, args) {

        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            var noteInput = modal.find('[name="ViewModel.InvitationNoteHtml"]');
            noteInput.hide();
            var noteMonacoContainer = $('<div id="editor" style="height: 200px;"></div>');
            noteMonacoContainer.insertAfter(noteInput);

            var headerInput = modal.find('[name="ViewModel.InvitationHeaderHtml"]');
            headerInput.hide();
            var headerMonacoContainer = $('<div id="editor" style="height: 200px;"></div>');
            headerMonacoContainer.insertAfter(headerInput);

            var footerInput = modal.find('[name="ViewModel.InvitationFooterHtml"]');
            footerInput.hide();
            var footerMonacoContainer = $('<div id="editor" style="height: 200px;"></div>');
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

    };

    return {
        initModal: initModal
    };
};

