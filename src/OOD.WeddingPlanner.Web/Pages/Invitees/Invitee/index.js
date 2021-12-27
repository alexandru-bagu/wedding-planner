$(function () {

  var l = abp.localization.getResource('WeddingPlanner');

  var service = oOD.weddingPlanner.invitees.invitee;
  var createModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Invitees/Invitee/CreateModal',
    scriptUrl: "/Pages/select2modal.js",
    modalClass: "select2modal"
  });
  var editModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Invitees/Invitee/EditModal',
    scriptUrl: "/Pages/select2modal.js",
    modalClass: "select2modal"
  });

  var dataTable = $('#InviteeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                visible: abp.auth.isGranted('WeddingPlanner.Invitee.Update'),
                action: function (data) {
                  editModal.open({ id: data.record.invitee.id });
                }
              },
              {
                text: l('Delete'),
                visible: abp.auth.isGranted('WeddingPlanner.Invitee.Delete'),
                confirmMessage: function (data) {
                  return l('InviteeDeletionConfirmationMessage', data.record.invitee.id);
                },
                action: function (data) {
                  service.delete(data.record.invitee.id)
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
        title: l('InviteeSurname'),
        data: "invitee.surname"
      },
      {
        title: l('InviteeGivenName'),
        data: "invitee.givenName"
      },
      {
        title: l('InviteeRSVP'),
        data: "invitee.rsvp"
      },
      {
        title: l('InviteeConfirmed'),
        data: "invitee.confirmed"
      },
      {
        title: l('InviteeChild'),
        data: "invitee.child"
      },
      {
        title: l('Invitation'),
        render: function(_, type, record) {
          if(record.invitation)
            return record.invitation.destination;
          return "";
        }
      },
      {
        title: l('Wedding'),
        render: function(_, type, record) {
          if(record.wedding)
            return record.wedding.name;
          return "";
        }
      },
    ]
  }));

  createModal.onResult(function () {
    dataTable.ajax.reload();
  });

  editModal.onResult(function () {
    dataTable.ajax.reload();
  });

  $('#NewInviteeButton').click(function (e) {
    e.preventDefault();
    createModal.open();
  });
});
