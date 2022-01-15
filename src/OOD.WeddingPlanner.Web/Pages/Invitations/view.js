$(function () {
    var l = abp.localization.getResource('WeddingPlanner');

    $.each($('[id="disabled-tab"]'), function (_, el) {
        el.dataset.bsToggle = null;
        el.href = '#';
        $(el).on('click', function (evt) {
            evt.preventDefault();
            evt.stopPropagation();
        });
    });

    $(document).on('change', '[data-rsvp]', async function (evt) {
        var el = $("[name='" + evt.target.name + "']:checked");
        await abp.ajax({ url: abp.appPath + 'RSVP/' + el.data('invitee-id') + '/' + (abp.currentTenant.name ?? ""), data: JSON.stringify(el.val()) });
        abp.notify.info(l('SuccessfullyUpdated'));
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

    var map = L.map('map');
    var markerLayer = L.layerGroup().addTo(map);
    L.tileLayer.provider('MapBox', {
        id: 'mapbox/streets-v11',
        accessToken: 'pk.eyJ1IjoiYWxleGFuZHJ1LWJhZ3UiLCJhIjoiY2t5ZXg4NXN5MWRqbDJ1bjAydjU4MWNpNCJ9.MNTm1-zd2fCY1NlPDByjvA'
    }).addTo(map);

    for (var i = 0; i < model.wedding.events.length; i++) {
        var evt = model.wedding.events[i];
        var when = new Date(evt.time);
        if (model.wedding.events.length == 1 || new Date() >= when) {
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