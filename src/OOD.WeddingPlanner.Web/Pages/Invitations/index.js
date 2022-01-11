$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitations.invitation;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitations/CreateModal',
        scriptUrl: "/Pages/select2modal.js",
        modalClass: "select2modal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitations/EditModal',
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
                                text: l('Preview'),
                                action: function (data) {
                                    var a = $('<a href="' + abp.appPath + "Print/" + data.record.invitation.id + '" target="_blank"></a>');
                                    a.appendTo(document.body);
                                    a[0].click();
                                    a.remove();
                                }
                            },
                            {
                                text: l('Print'),
                                action: function (data) {
                                    var form = $('#download-invitation');
                                    form.attr('action', abp.appPath + "Print/" + data.record.invitation.id);
                                    form[0].submit();
                                }
                            },
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
            {
                title: l('InvitationDesign'),
                data: "design.name"
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
