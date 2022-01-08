$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitations.invitation;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitations/Invitation/CreateModal',
        scriptUrl: "/Pages/select2modal.js",
        modalClass: "select2modal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitations/Invitation/EditModal',
        scriptUrl: "/Pages/select2modal.js",
        modalClass: "select2modal"
    });

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
                width: "100px",
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
                                    return l('InvitationDeletionConfirmationMessage', data.record.invitation.destination);
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
                render: function (_, type, record) {
                    if (record.wedding) return record.wedding.name;
                    return "";
                }
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
