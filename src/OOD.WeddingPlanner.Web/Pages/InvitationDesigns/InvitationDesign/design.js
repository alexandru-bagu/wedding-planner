var abp = abp || {};

abp.modals.designModal = function () {
  var initModal = function (publicApi, args) {

    publicApi.onOpen(function () {
      var modal = publicApi.getModal();

      // specify the fonts you would 
      var fonts = ['Arial', 'Courier', 'Garamond', 'Tahoma', 'Times New Roman', 'Verdana'];
      // generate code friendly names
      function getFontName(font) {
        return font.toLowerCase().replace(/\s/g, "-");
      }
      var fontNames = fonts.map(font => getFontName(font));
      // add fonts to style
      var fontStyles = "";
      fonts.forEach(function (font) {
        var fontName = getFontName(font);
        fontStyles += ".ql-snow .ql-picker.ql-font .ql-picker-label[data-value=" + fontName + "]::before, .ql-snow .ql-picker.ql-font .ql-picker-item[data-value=" + fontName + "]::before {" +
          "content: '" + font + "';" +
          "font-family: '" + font + "', sans-serif;" +
          "}" +
          ".ql-font-" + fontName + "{" +
          " font-family: '" + font + "', sans-serif;" +
          "}";
      });
      var node = document.createElement('style');
      node.innerHTML = fontStyles;
      document.body.appendChild(node);

      var toolbarOptions = [
        [{ 'header': [2, 3, 4, 5, false] }],
        [{ 'align': [] }],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'script': 'sub' }, { 'script': 'super' }],
        [{ 'indent': '-1' }, { 'indent': '+1' }],
        [{ 'color': [] }, { 'background': [] }],
        ['bold', 'italic', 'underline', 'strike', 'link'],
        [{ 'font': fontNames }]
      ]

      var Font = Quill.import('formats/font');
      Font.whitelist = fontNames;
      Quill.register(Font, true);

      var htmlBodyInput = modal.find('[name="ViewModel.Body"]');
      modal.find('#editor-container').remove();
      var mainContainer = $('<div id="editor-container"></div>');
      var bodyContainer = $('<div id="editor"></div>');
      htmlBodyInput.hide();
      mainContainer.insertAfter(htmlBodyInput);
      bodyContainer.appendTo(mainContainer);
      var bodyQuill = new Quill("#editor", {
        modules: {
          toolbar: toolbarOptions,
          imageResize: { displaySize: true },
          imageDrop: true
        },
        theme: 'snow'
      });

      bodyQuill.root.innerHTML = htmlBodyInput.val();

      var submit = modal.find('button:not([data-dismiss])');
      submit.on('click', function (e) {
        htmlBodyInput.val(bodyQuill.root.innerHTML);
      });

      var iframe = modal.find('iframe#preview')[0];
      bodyQuill.on('text-change', function () {
        if (iframe && iframe.contentWindow && iframe.contentWindow.document) {
          var content = bodyQuill.root.innerHTML;
          content = content + "<style>" + fontStyles + "</style>";
          iframe.contentWindow.document.open();
          iframe.contentWindow.document.write(content);
          iframe.contentWindow.document.close();
        }
      });
    });

  };

  return {
    initModal: initModal
  };
};

