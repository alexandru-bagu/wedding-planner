$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.tableMenus.tableMenu;
    var createModal = new abp.ModalManager(abp.appPath + 'TableMenus/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'TableMenus/EditModal');

    var dataTable = $('#TableMenuTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                                visible: abp.auth.isGranted('WeddingPlanner.TableMenu.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('WeddingPlanner.TableMenu.Delete'),
                                confirmMessage: function (data) {
                                    return l('TableMenuDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    service.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('SuccessfullyDeleted'));
                                            dataTable.ajax.reload(null, false);
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('TableMenuName'),
                data: "name"
            },
            {
                title: l('TableMenuAdult'),
                data: "adult"
            },
            {
                title: l('TableMenuOrder'),
                data: "order"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload(null, false);
    });

    editModal.onResult(function () {
        dataTable.ajax.reload(null, false);
    });

    $('#NewTableMenuButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
