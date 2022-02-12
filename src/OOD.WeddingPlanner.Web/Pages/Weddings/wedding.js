var abp = abp || {};

abp.modals.weddingModal = function () {
    var initModal = function (publicApi, args) {

        if(publicApi.__init__) return;
        publicApi.__init__ = true;
        
        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            var input = modal.find('[name="ViewModel.InvitationNote"]');
            input.hide();
            
            var monacoContainer = $('<div id="editor" style="height: 200px;"></div>');
            monacoContainer.insertAfter(input);

            var quillContainer = $("<div></div>").insertAfter(input);
            quillContainer.insertAfter(monacoContainer);
            var quill = new Quill(quillContainer[0], {
                theme: 'snow',
                modules: { toolbar: false }
            });
            quill.enable(false);
            
            input.on('change', function() {
                quill.root.innerHTML = input.val();
            }).trigger('change');

            require(['vs/editor/editor.main'], function () {
                var editor = monaco.editor.create(monacoContainer[0], {
                    value: input.val(),
                    language: 'html',
                    automaticLayout: true
                });
                editor.onDidChangeModelContent(function () {
                    input.val(editor.getValue()).trigger('change');
                });
            });
        });

    };

    return {
        initModal: initModal
    };
};

