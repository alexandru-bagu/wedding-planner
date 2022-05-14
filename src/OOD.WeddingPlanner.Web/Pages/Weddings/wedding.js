var abp = abp || {};

abp.modals.weddingModal = function () {
    var initModal = function (publicApi, args) {

        if(publicApi.__init__) return;
        publicApi.__init__ = true;
        
        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            var noteInput = modal.find('[name="ViewModel.InvitationNote"]');
            noteInput.hide();
            var noteMonacoContainer = $('<div id="editor" style="height: 200px;"></div>');
            noteMonacoContainer.insertAfter(noteInput);

            var styleInput = modal.find('[name="ViewModel.InvitationStyle"]');
            styleInput.hide();
            var styleMonacoContainer = $('<div id="editor" style="height: 200px;"></div>');
            styleMonacoContainer.insertAfter(styleInput);

            require(['vs/editor/editor.main'], function () {
                var noteEditor = monaco.editor.create(noteMonacoContainer[0], {
                    value: noteInput.val(),
                    language: 'html',
                    automaticLayout: true
                });
                noteEditor.onDidChangeModelContent(function () {
                    noteInput.val(noteEditor.getValue()).trigger('change');
                });

                var styleEditor = monaco.editor.create(styleMonacoContainer[0], {
                    value: styleInput.val(),
                    language: 'css',
                    automaticLayout: true
                });
                styleEditor.onDidChangeModelContent(function () {
                    styleInput.val(styleEditor.getValue()).trigger('change');
                });
            });
        });

    };

    return {
        initModal: initModal
    };
};

