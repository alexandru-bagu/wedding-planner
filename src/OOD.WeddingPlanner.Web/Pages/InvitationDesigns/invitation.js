$(function () {
    var design = invitation.design;

    function View(unit, width, height, dpi, debug) {
        var ratio = 1;
        if (unit === 'mm') ratio = 25.4;
        if (unit === 'cm') ratio = 2.54;

        var view = this;
        view.width = width *  dpi / ratio;
        view.height = height * dpi / ratio;
        view.border = debug ? '1px solid silver' : '';
    }
    invitation.view = new View(design.measurementUnit, design.paperWidth, design.paperHeight, design.paperDpi, invitation.debug);
    ko.applyBindings(invitation, document.body.parentElement);
});