$(function () {
    $.each($('[data-toggle]'), function (_, el) {
        el.dataset['bsToggle'] = el.dataset.toggle;
    });
});