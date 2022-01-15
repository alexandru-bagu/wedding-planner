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

        function getCookie(name) {
            const value = `; ${document.cookie}`;
            const parts = value.split(`; ${name}=`);
            if (parts.length === 2) return parts.pop().split(';').shift();
        }

        function generateQRCode() {
            var tenant_name = getCookie("tenant_name");
            var div = document.createElement("div");
            div.id = 'qr-code';
            div.style.visibility = 'collapse';
            document.body.appendChild(div);
            new QRCode(div, {
                text: location.protocol + "//" + location.host + "/" + vm.invitation.id + "/" + (tenant_name ?? ''),
                width: 256,
                height: 256,
                colorDark: "#000000",
                colorLight: "#ffffff",
                correctLevel: QRCode.CorrectLevel.M
            });
            var canvas = $(div).find('canvas')[0];
            vm.qrCode = canvas.toDataURL();
            setTimeout(function () { $('#qr-code').remove(); });
        }
    }

    var view = new ViewModel(invitation);
    ko.applyBindings(view, document.body.parentElement);
});