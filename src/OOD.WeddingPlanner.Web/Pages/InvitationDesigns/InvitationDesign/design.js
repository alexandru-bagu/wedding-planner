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
      
      require(['vs/editor/editor.main'], function () {
				var editor = monaco.editor.create(bodyContainer[0], {
					value: htmlBodyInput.val(),
					language: 'html'
				});

        var iframe = modal.find('iframe#preview')[0];
        editor.getModel().onDidChangeContent(() => {
          updateIframe();
        });

        function updateIframe() {
          if (iframe && iframe.contentWindow && iframe.contentWindow.document) {
            var content = editor.getValue();
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

