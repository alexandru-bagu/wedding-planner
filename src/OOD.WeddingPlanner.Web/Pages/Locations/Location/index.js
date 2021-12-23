$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.locations.location;
    var createModal = new abp.ModalManager(abp.appPath + 'Locations/Location/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Locations/Location/EditModal');

    var dataTable = $('#LocationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('WeddingPlanner.Location.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('WeddingPlanner.Location.Delete'),
                                confirmMessage: function (data) {
                                    return l('LocationDeletionConfirmationMessage', data.record.id);
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
                title: l('LocationName'),
                data: "name"
            },
            {
                title: l('LocationDescription'),
                data: "description"
            },
            {
                title: l('LocationLongitude'),
                data: "longitude"
            },
            {
                title: l('LocationLatitude'),
                data: "latitude"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewLocationButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
