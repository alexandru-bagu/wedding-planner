$(function () {

  var l = abp.localization.getResource('WeddingPlanner');

  var service = oOD.weddingPlanner.weddings.wedding;
  var createModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Weddings/Wedding/CreateModal',
    scriptUrl: "/Pages/select2modal.js",
    modalClass: "select2modal"
  });
  var editModal = new abp.ModalManager({
    viewUrl: abp.appPath + 'Weddings/Wedding/EditModal',
    scriptUrl: "/Pages/select2modal.js",
    modalClass: "select2modal"
  });

  var dataTable = $('#WeddingTable').DataTable(abp.libs.datatables.normalizeConfiguration({
    processing: true,
    serverSide: true,
    paging: true,
    searching: false,
    autoWidth: false,
    scrollCollapse: true,
    order: [[0, "asc"]],
    ajax: abp.libs.datatables.createAjax(service.getList),
    columnDefs: [
      {
        rowAction: {
          items:
            [
              {
                text: l('Edit'),
                visible: abp.auth.isGranted('WeddingPlanner.Wedding.Update'),
                action: function (data) {
                  editModal.open({ id: data.record.id });
                }
              },
              {
                text: l('Delete'),
                visible: abp.auth.isGranted('WeddingPlanner.Wedding.Delete'),
                confirmMessage: function (data) {
                  return l('WeddingDeletionConfirmationMessage', data.record.name);
                },
                action: function (data) {
                  service.delete(data.record.id)
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
        title: l('WeddingGroomName'),
        data: "groomName"
      },
      {
        title: l('WeddingBrideName'),
        data: "brideName"
      },
      {
        title: l('WeddingName'),
        data: "name"
      },
      {
        title: l('WeddingContactPhoneNumber'),
        data: "contactPhoneNumber"
      },
    ]
  }));

  createModal.onResult(function () {
    dataTable.ajax.reload();
  });

  editModal.onResult(function () {
    dataTable.ajax.reload();
  });

  $('#NewWeddingButton').click(function (e) {
    e.preventDefault();
    createModal.open();
  });
});
