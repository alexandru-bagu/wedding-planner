$(function () {
    function ViewModel(data) {
        var vm = this;
        vm.invitation = data.invitation;
        vm.invitees = vm.invitation.invitees;
        vm.design = data.design;
        vm.wedding = data.wedding;

        setPaperConstraints();
        generateQRCode();

        function setPaperConstraints() {
            var unit = vm.design.measurementUnit,
                width = vm.design.paperWidth,
                height = vm.design.paperHeight,
                dpi = vm.design.paperDpi,
                debug = data.debug;
            var ratio = 1;
            if (unit === 'mm') ratio = 25.4;
            if (unit === 'cm') ratio = 2.54;

            vm.width = width * dpi / ratio;
            vm.height = height * dpi / ratio;
            vm.border = debug ? '1px solid silver' : '';
        }

        function generateQRCode() {
            var div = document.createElement("div");
            div.style.visibility = 'collapse';
            document.body.appendChild(div);
            new QRCode(div, {
                text: location.protocol + "//" + location.host + "/v/" + vm.invitation.id,
                width: 256,
                height: 256,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.H
            });
            var canvas = $(div).find('canvas')[0];
            vm.qrCode = canvas.toDataURL();
            setTimeout(function () { document.removeChild(div); });
        }
    }

    var view = new ViewModel(invitation);
    ko.applyBindings(view, document.body.parentElement);
});