var abp = abp || {};

abp.modals.tableModal = function () {
    var initModal = function (publicApi, args) {

        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            var service = oOD.weddingPlanner.events.event;
            var wedding = modal.find('[name="ViewModel.WeddingId"]');
            var event = modal.find('[name="ViewModel.EventId"]');
            wedding.change(async function (evt) {
                var val = event.val();
                var lookup = await service.getLookupList({ weddingId: wedding.val() });
                var data = $.map(lookup.items, function (obj) {
                    obj.text = obj.displayName;
                    return obj;
                });
                event.empty();
                event.select2({ data });
                setTimeout(function () {
                    event.val(val).trigger('change');
                });
            }).trigger('change');

        });

    };

    return {
        initModal: initModal
    };
};

