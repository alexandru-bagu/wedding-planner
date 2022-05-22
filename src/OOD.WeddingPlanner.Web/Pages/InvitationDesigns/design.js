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

                design.measurementUnit.subscribe(updateIframe);
                design.paperWidth.subscribe(updateIframe);
                design.paperHeight.subscribe(updateIframe);
                design.paperDpi.subscribe(updateIframe);

                function updateIframe() {
                    var canvas = document.getElementById('preview');
                    canvas.innerHTML = "";
                    var content = editor.getValue();

                    var jsonStr = ko.toJSON(window.bogusInvitation);
                    var json = JSON.parse(jsonStr);
                    json.design.paperWidth = Globalize.parseNumber(json.design.paperWidth);
                    json.design.paperHeight = Globalize.parseNumber(json.design.paperHeight);
                    json.design.paperDpi = Globalize.parseNumber(json.design.paperDpi);

                    content = content.replace('/*INVITATION DATA*/', "var invitation = " + JSON.stringify(json));
                    
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Design/Preview");

                    xhr.setRequestHeader("Accept", "application/json");
                    xhr.setRequestHeader("Content-Type", "application/json");

                    xhr.onreadystatechange = function () {
                        if (xhr.readyState === 4) {
                            var arrayBuffer = xhr.response;
                            var byteArray = new Uint8Array(arrayBuffer);

                            PDFJS.getDocument(byteArray).then(function (pdf) {
                                pdf.getPage(1).then(function (page) {
                                  var scale = 1;
                                  var viewport = page.getViewport(scale);
                                  var context = canvas.getContext('2d');
                                  canvas.height = viewport.height;
                                  canvas.width = viewport.width;
                                  page.render({ canvasContext: context, viewport: viewport });
                                });
                            });
                        }
                    };
                    xhr.send(JSON.stringify({ body: content, invitation: json }));
                }
                updateIframe();
                modal.find('[data-bs-toggle="pill"]').on('click', updateIframe);

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

