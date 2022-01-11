var abp = abp || {};

abp.modals.designModal = function () {
    var initModal = function (publicApi, args) {

        publicApi.onOpen(function () {
            var modal = publicApi.getModal();

            var htmlBodyInput = modal.find('[name="ViewModel.Body"]');

            modal.find('#editor').remove();
            var bodyContainer = $('<div id="editor" style="height: 400px;"></div>');
            htmlBodyInput.hide();
            bodyContainer.insertAfter(htmlBodyInput);

            var design = window.bogusInvitation.design;
            var mu = $("[name='ViewModel.MeasurementUnit']");
            var w = $("[name='ViewModel.PaperWidth']");
            var h = $("[name='ViewModel.PaperHeight']");
            var dpi = $("[name='ViewModel.PaperDpi']");
            design.measurementUnit = ko.observable(mu.val());
            design.paperWidth = ko.observable(w.val());
            design.paperHeight = ko.observable(h.val());
            design.paperDpi = ko.observable(dpi.val());
            window.bogusInvitation.debug = true;

            mu.attr('data-bind', 'value: measurementUnit');
            w.attr('data-bind', 'value: paperWidth');
            h.attr('data-bind', 'value: paperHeight');
            dpi.attr('data-bind', 'value: paperDpi');

            ko.cleanNode(modal[0]);
            ko.applyBindings(design, modal[0]);

            require(['vs/editor/editor.main'], function () {
                var editor = monaco.editor.create(bodyContainer[0], {
                    value: htmlBodyInput.val(),
                    language: 'html',
                    automaticLayout: true
                });

                var iframe = modal.find('iframe#preview')[0];
                editor.getModel().onDidChangeContent(() => {
                    updateIframe();
                });

                design.measurementUnit.subscribe(updateIframe);
                design.paperWidth.subscribe(updateIframe);
                design.paperHeight.subscribe(updateIframe);
                design.paperDpi.subscribe(updateIframe);

                function updateIframe() {
                    if (iframe && iframe.contentWindow && iframe.contentWindow.document) {
                        var content = editor.getValue();
                        content = content.replace('/*INVITATION DATA*/', "var invitation = " + ko.toJSON(window.bogusInvitation));
                        iframe.contentWindow.document.open();
                        iframe.contentWindow.document.write('clean');
                        iframe.contentWindow.document.close();

                        iframe.contentWindow.document.open();
                        iframe.contentWindow.document.write(content);
                        iframe.contentWindow.document.close();
                    }
                }
                updateIframe();

                var submit = modal.find('button:not([data-dismiss])');
                submit.on('click', function () {
                    htmlBodyInput.val(editor.getValue());
                });
            });
        });

    };

    return {
        initModal: initModal
    };
};

