$(async function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitations.invitation;
    var weddingService = oOD.weddingPlanner.weddings.wedding;
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

    var prevFilter = '';
    var getFilter = function () {
        return {
            filter: $("#FilterText").val(),
            destination: $("#DestinationFilter").val(),
            weddingId: $("#WeddingIdFilter").val(),
            groomSide: (function () {
                var value = $("#GroomSideFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value.toLowerCase() === 'true';
            })(),
            brideSide: (function () {
                var value = $("#BrideSideFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value.toLowerCase() === 'true';
            })(),
        };
    };
    function filterChanged(newFilter) {
        var json = JSON.stringify(newFilter);
        var ret = json !== prevFilter;
        prevFilter = json;
        return ret;
    }


    var weddingList = await weddingService.getList({ maxCountResult: 0 });
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
                                    var a = $('<a href="' + abp.appPath + "Invitation/Print/" + data.record.invitation.id + '/' + encodeURIComponent(abp.currentTenant.name || "") + '" target="_blank"></a>');
                                    a.appendTo(document.body);
                                    a[0].click();
                                    a.remove();
                                }
                            },
                            {
                                text: l('Print PDF'),
                                action: function (data) {
                                    var form = $('#download-invitation');
                                    form.attr('action', abp.appPath + "Invitation/Print/PDF/" + data.record.invitation.id);
                                    form[0].submit();
                                }
                            },
                            {
                                text: l('Print Image'),
                                action: function (data) {
                                    var form = $('#download-invitation');
                                    form.attr('action', abp.appPath + "Invitation/Print/Image/" + data.record.invitation.id);
                                    form[0].submit();
                                }
                            },
                            {
                                text: l('View'),
                                action: function (data) {
                                    var a = $('<a href="' + abp.appPath + data.record.invitation.id + '/' + encodeURIComponent(abp.currentTenant.name || "") + '" target="_blank"></a>');
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
                                            dataTable.ajax.reload(null, false);
                                        });
                                }
                            }
                        ]
                }
            },
            {
                title: l('Wedding'),
                visible: weddingList.totalCount > 1,
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
            {
                title: l('InvitationNotes'),
                data: "invitation.notes"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload(null, false);
    });

    editModal.onResult(function () {
        dataTable.ajax.reload(null, false);
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
        const { id } = await abp.ajax({ url: '/download/begin', contentType : 'application/json', data: JSON.stringify(getFilter()) });
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
        if(filterChanged(getFilter()))
            dataTable.ajax.reload(null, false);
    });
});
