$(function () {

  var l = abp.localization.getResource('WeddingPlanner');

  var service = oOD.weddingPlanner.invitations.invitation;
  var createModal = new abp.ModalManager(abp.appPath + 'Invitations/Invitation/CreateModal');
  var editModal = new abp.ModalManager(abp.appPath + 'Invitations/Invitation/EditModal');

  var dataTable = $('#InvitationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
    processing: true,
    serverSide: true,
    paging: true,
    searching: false,
    autoWidth: false,
    scrollCollapse: true,
    order: [[0, "asc"]],
    ajax: abp.libs.datatables.createAjax(service.getListWithNavigation),
    columnDefs: [
      {
        rowAction: {
          items:
            [
              {
                text: l('Edit'),
                visible: abp.auth.isGranted('WeddingPlanner.Invitation.Update'),
                action: function (data) {
                  editModal.open({ id: data.record.invitation.id });
                }
              },
              {
                text: l('Delete'),
                visible: abp.auth.isGranted('WeddingPlanner.Invitation.Delete'),
                confirmMessage: function (data) {
                  return l('InvitationDeletionConfirmationMessage', data.record.invitation.id);
                },
                action: function (data) {
                  service.delete(data.record.invitation.id)
                    .then(function () {
                      abp.notify.info(l('SuccessfullyDeleted'));
                      dataTable.ajax.reload();
                    });
                }
              }
            ]
        }
      },
      {
        title: l('Wedding'),
        data: "wedding.name"
      },
      {
        title: l('InvitationDestination'),
        data: "invitation.destination"
      },
    ]
  }));

  createModal.onResult(function () {
    dataTable.ajax.reload();
  });

  editModal.onResult(function () {
    dataTable.ajax.reload();
  });

  $('#NewInvitationButton').click(function (e) {
    e.preventDefault();
    createModal.open();
  });
});
