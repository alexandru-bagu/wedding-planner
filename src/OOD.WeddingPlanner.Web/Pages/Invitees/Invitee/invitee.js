var abp = abp || {};

abp.modals.inviteeModal = function () {
  var initModal = function (publicApi, args) {

    publicApi.onOpen(function () {
      var modal = publicApi.getModal();
      modal.find('select').select2();

      var service = oOD.weddingPlanner.invitations.invitation;
      var wedding = modal.find('[name="ViewModel.WeddingId"]');
      var invitation = modal.find('[name="ViewModel.InvitationId"]');
      wedding.change(async function (evt) {
        var lookup = await service.getLookupList({ weddingId: wedding.val() });
        var data = $.map(lookup.items, function (obj) {
          obj.text = obj.displayName;
          return obj;
        });
        invitation.empty();
        invitation.select2({ data });
      });

    });

  };

  return {
    initModal: initModal
  };
};

