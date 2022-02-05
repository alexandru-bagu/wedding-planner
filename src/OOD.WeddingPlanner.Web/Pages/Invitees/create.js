var abp = abp || {};

abp.modals.inviteeCreateModal = function () {
    var initModal = function (publicApi, args) {
        if(publicApi.__init__) return;
        publicApi.__init__ = true;
        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();

            function toggle(name, others, callback, { autoToggle, ensureOneChecked})
            {
                var check = modal.find('[id="' + name + '"]');
                var toggle = modal.find('[for="' + name + '"]');
                check.off('change').on("change", function() {
                    toggle.button("toggle");
                    if (check.prop("checked")) {
                        for(var i in others) {
                            var t = modal.find('[id="' + others[i] + '"]');
                            if(t.prop("checked")) {
                                t.trigger('click');
                            }
                        }
                    } else if(ensureOneChecked) {
                        var any_checked = false;
                        for(var i in others) {
                            var t = modal.find('[id="' + others[i] + '"]');
                            if(t.prop("checked")) {
                                any_checked = true;
                            }
                        }
                        if(!any_checked) {
                            check.prop('checked', true).trigger('change');
                        }
                    }
                    callback(check.prop("checked"));
                });
                if (autoToggle) check.prop('checked', true).trigger('change');
            }
            
            $('[name="ViewModel.Child"]').val('False');
            $('[name="ViewModel.Male"]').val('True');

            var child = "child-toggle", male = "male-toggle", female = "female-toggle"
            toggle(child, [], function(value) {
                if (value) { $('[name="ViewModel.Child"]').val('True'); }
                else { $('[name="ViewModel.Child"]').val('False'); }
            }, { });
            toggle(male, [female], function(value) {
                if (value) { $('[name="ViewModel.Male"]').val('True'); }
            }, { autoToggle: true, ensureOneChecked: true });
            toggle(female, [male], function(value) {
                if (value) { $('[name="ViewModel.Male"]').val('False'); }
            }, { ensureOneChecked: true });
            $(".show-invitee-details").off('click').on('click', function() { $('.invitee-details').toggleClass('d-none'); });

            var service = oOD.weddingPlanner.invitations.invitation;
            var wedding = modal.find('[name="ViewModel.WeddingId"]');
            var invitation = modal.find('[name="ViewModel.InvitationId"]');
            invitation.data('value', invitation.val());
            wedding.off('change').change(async function (evt) {
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

