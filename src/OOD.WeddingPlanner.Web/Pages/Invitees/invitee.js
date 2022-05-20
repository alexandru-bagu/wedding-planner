var abp = abp || {};

abp.modals.inviteeModal = function () {
    var initModal = function (publicApi, args) {
        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            var service = oOD.weddingPlanner.invitations.invitation;
            var wedding = modal.find('[name="ViewModel.WeddingId"]');
            var invitation = modal.find('[name="ViewModel.InvitationId"]');
            invitation.data('value', invitation.val());
            wedding.change(async function (evt) {
                var value = invitation.data('value');
                invitation.empty();
                if (wedding.val()) {
                    var lookup = await service.getLookupList({ weddingId: wedding.val() });
                    var data = $.map(lookup.items, function (obj) {
                        obj.text = obj.displayName;
                        return obj;
                    });
                    data.splice(0, 0, { text: '-----', id: '' });
                    invitation.select2({ data });
                    invitation.val(value).trigger('change');
                }
            }).trigger('change');
        });

    };

    return {
        initModal: initModal
    };
};

