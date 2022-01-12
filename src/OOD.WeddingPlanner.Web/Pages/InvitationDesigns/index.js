$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitationDesigns.invitationDesign;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'InvitationDesigns/CreateModal',
        scriptUrl: "/Pages/InvitationDesigns/design.js",
        modalClass: "designModal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'InvitationDesigns/EditModal',
        scriptUrl: "/Pages/InvitationDesigns/design.js",
        modalClass: "designModal"
    });

    var dataTable = $('#InvitationDesignTable').DataTable(abp.libs.datatables.normalizeConfiguration({
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
                width: "100px",
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('WeddingPlanner.InvitationDesign.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('WeddingPlanner.InvitationDesign.Delete'),
                                confirmMessage: function (data) {
                                    return l('InvitationDesignDeletionConfirmationMessage', data.record.name);
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
                title: l('InvitationDesignName'),
                data: "name"
            },
            {
                title: l('InvitationDesignPaperWidth'),
                render: function (_, type, record) {
                    return record.paperWidth + " " + record.measurementUnit;
                }
            },
            {
                title: l('InvitationDesignPaperHeight'),
                render: function (_, type, record) {
                    return record.paperHeight + " " + record.measurementUnit;
                }
            },
            {
                title: l('InvitationDesignDpi'),
                data: "paperDpi"
            }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewInvitationDesignButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});