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

    var getFilter = function () {
        return {
            filter: $("#FilterText").val(),
            destination: $("#DestinationFilter").val(),
            weddingId: $("#WeddingIdFilter").val()
        };
    };

    var dataTable = $('#InvitationTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[0, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getListWithNavigation, getFilter),
        columnDefs: [
            {
                width: "100px",
                rowAction: {
                    items:
                        [
                            {
                                text: l('Preview Print'),
                                action: function (data) {
                                    var a = $('<a href="' + abp.appPath + "Invitation/Print/" + data.record.invitation.id + '/' + (abp.currentTenant.name ?? "") + '" target="_blank"></a>');
                                    a.appendTo(document.body);
                                    a[0].click();
                                    a.remove();
                                }
                            },
                            {
                                text: l('Print'),
                                action: function (data) {
                                    var form = $('#download-invitation');
                                    form.attr('action', abp.appPath + "Invitation/Print/" + data.record.invitation.id);
                                    form[0].submit();
                                }
                            },
                            {
                                text: l('View'),
                                action: function (data) {
                                    var a = $('<a href="' + abp.appPath + data.record.invitation.id + '/' + (abp.currentTenant.name ?? "")+ '" target="_blank"></a>');
                                    a.appendTo(document.body);
                                    a[0].click();
                                    a.remove();
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
            {
                title: l('InvitationPlusOne'),
                data: "invitation.plusOne"
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

    function delay(s) {
        return new Promise((r) => setTimeout(r, s));;
    }

    $('#DownloadInvitationButton').click(async function (e) {
        abp.notify.info(l('Invitations build is enqueued.'));
        const { id } = await abp.ajax({ url: '/download/begin', data: getFilter() });
        var status = null;
        while (status == null || !status.total) {
            status = await abp.ajax({ url: '/download/status/' + id, method: 'get' });
            await delay(1000);
        }
        abp.notify.info(l('Invitations are being built; progress: 0%'));
        var prev = status.complete;
        while (status.total != status.complete) {
            status = await abp.ajax({ url: '/download/status/' + id, method: 'get' });
            if (prev !== status.complete) {
                prev = status.complete;
                var progress = parseInt(status.complete / status.total * 10000) / 100;
                abp.notify.info(l('Invitations are being built; progress: ' + progress + '%'));
            }
            await delay(1000);
        }
        abp.notify.info(l('Invitations are built!'));
        var a = $('<a href="/download/' + id + '"></a>');
        a.appendTo(document.body);
        a[0].click();
        a.remove();
    });

    $('input, select').on('blur change', function () {
        dataTable.ajax.reload();
    });
});
