$(function () {

  var l = abp.localization.getResource('WeddingPlanner');

  var service = oOD.weddingPlanner.events.event;
  var createModal = new abp.ModalManager(abp.appPath + 'Events/Event/CreateModal');
  var editModal = new abp.ModalManager(abp.appPath + 'Events/Event/EditModal');

  var dataTable = $('#EventTable').DataTable(abp.libs.datatables.normalizeConfiguration({
    processing: true,
    serverSide: true,
    paging: true,
    searching: false,
    autoWidth: false,
    scrollCollapse: true,
    order: [[1, "asc"]],
    ajax: abp.libs.datatables.createAjax(service.getListWithNavigation),
    columnDefs: [
      {
        rowAction: {
          items:
            [
              {
                text: l('Edit'),
                visible: abp.auth.isGranted('WeddingPlanner.Event.Update'),
                action: function (data) {
                  editModal.open({ id: data.record.event.id });
                }
              },
              {
                text: l('Delete'),
                visible: abp.auth.isGranted('WeddingPlanner.Event.Delete'),
                confirmMessage: function (data) {
                  return l('EventDeletionConfirmationMessage', data.record.event.id);
                },
                action: function (data) {
                  service.delete(data.record.event.id)
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
        title: l('EventName'),
        data: "event.name"
      },
      {
        title: l('EventTime'),
        data: "event.time"
      },
      {
        title: l('Location'),
        data: "location.name"
      },
      {
        title: l('Wedding'),
        data: "wedding.name"
      }
    ]
  }));

  createModal.onResult(function () {
    dataTable.ajax.reload();
  });

  editModal.onResult(function () {
    dataTable.ajax.reload();
  });

  $('#NewEventButton').click(function (e) {
    e.preventDefault();
    createModal.open();
  });
});
