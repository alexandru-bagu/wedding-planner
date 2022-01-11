$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.events.event;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Events/CreateModal',
        scriptUrl: "/Pages/select2modal.js",
        modalClass: "select2modal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Events/EditModal',
        scriptUrl: "/Pages/select2modal.js",
        modalClass: "select2modal"
    });

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
                width: "100px",
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
                render: function (_, type, record) {
                    if (record.wedding) return record.wedding.name;
                    return "";
                }
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
