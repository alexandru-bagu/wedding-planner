$(function () {
    var l = abp.localization.getResource('WeddingPlanner');
    var map = L.map('map');
    var markerLayer = L.layerGroup().addTo(map);

    $('#add-plus-one-modal').detach().appendTo(document.body);
    $.each($('[id="disabled-tab"]'), function (_, el) {
        el.dataset.bsToggle = null;
        el.href = '#';
        $(el).on('click', function (evt) {
            evt.preventDefault();
            evt.stopPropagation();
        });
    });

    $(document).on('change', '[data-rsvp]', async function (evt) {
        try {
            var el = $("[name='" + evt.target.name + "']:checked");
            var menu = $('[data-menu][data-invitee-id="' + el.data('invitee-id') + '"]');
            if (el.val() === "false") { menu.closest(".invitee-menu").addClass("d-none"); }
            else { menu.closest(".invitee-menu").removeClass("d-none"); }
            await abp.ajax({ url: abp.appPath + 'RSVP/' + el.data('invitee-id') + '/' + encodeURIComponent(window.app_tenant_name || ""), data: JSON.stringify(el.val()) });
            if (el.val() !== "false") { menu.trigger('change'); }
            abp.notify.success(l('SuccessfullyUpdated'),"", {timeOut: 100});
        } catch (ex) {
            abp.notify.error(l('UpdateFailed'),"", {timeOut: 1000});
        }
    });

    $(document).on('change', '[data-menu]', async function (evt) {
        try {
            var el = $(evt.target);
            await abp.ajax({ url: abp.appPath + 'RSVPMenu/' + el.data('invitee-id') + '/' + encodeURIComponent(window.app_tenant_name || ""), data: JSON.stringify(el.val()) });
            if(evt.originalEvent) abp.notify.success(l('SuccessfullyUpdated'),"", {timeOut: 100});
        } catch (ex) {
            if(evt.originalEvent) abp.notify.error(l('UpdateFailed'),"", {timeOut: 1000});
        }
    });

    $(document).on('click keypress', '[data-events] li a', function (evt) {
        var $tab = $(evt.target);
        var area_id = $tab.attr('aria-controls');
        var area = $('#' + area_id);
        var container = area.find('[data-event-id]');
        var event_id = container.data('event-id');
        $.each(model.wedding.events, function (_, evt) {
            if (evt.id === event_id) {
                updateLocation(evt);
            }
        });
    });

    $('#add-plus-one-modal').find('[data-type="save"]').on('click', async function (evt) {
        function getFormData($form) {
            var indexed_array = {};
            $.map($form.serializeArray(), function (n, i) { indexed_array[n['name']] = n['value']; });
            return indexed_array;
        }

        evt.preventDefault();
        evt.stopPropagation();

        var form = $('#add-plus-one-modal').find('[data-type="plus-one-form"]');
        var validation = form.validate();
        if (!validation.errorList.length) {
            var obj = getFormData(form);
            var data = { name: obj['PlusOne.Name'], surname: obj['PlusOne.Surname'], male: obj['PlusOne.Male'] };
            abp.ui.block({
                elm: '#add-plus-one-modal',
                busy: true
            });
            try {
                await abp.ajax({ url: abp.appPath + 'PlusOne/' + model.invitation.id + '/' + encodeURIComponent(window.app_tenant_name || ""), data: JSON.stringify(data) });
                abp.notify.info(l('SuccessfullyUpdated'));
                setTimeout(function () { location.reload(); }, 1000);
            } catch {
                abp.notify.info(l('UpdateFailed'));
                abp.ui.unblock('[data-type="plus-one-form"]');
            }
        }
    });

    L.tileLayer.provider('MapBox', {
        id: 'mapbox/streets-v11',
        accessToken: 'pk.eyJ1IjoiYWxleGFuZHJ1LWJhZ3UiLCJhIjoiY2t5ZXg4NXN5MWRqbDJ1bjAydjU4MWNpNCJ9.MNTm1-zd2fCY1NlPDByjvA'
    }).addTo(map);

    updateLocation(model.wedding.events[0]);
    for (var i = 0; i < model.wedding.events.length; i++) {
        var evt = model.wedding.events[i];
        var when = new Date(evt.time);
        if (new Date() >= when) {
            var container = $('[data-events]').find('[data-event-id="' + evt.id + '"]');
            var tabpanel = container.closest('[role="tabpanel"]');
            var aria_labelled_by = tabpanel.attr('aria-labelledby');
            var tab = $('#' + aria_labelled_by);
            tab.tab('show').trigger('click');
            break;
        }
    }

    function updateLocation(event) {
        var location = event.location;

        markerLayer.clearLayers();

        map.setView([location.latitude, location.longitude], 16);
        var marker = L.marker([location.latitude, location.longitude]).addTo(markerLayer);
        marker.bindPopup("<b>" + location.name + "</b><br>" + location.address).openPopup();
    }
});