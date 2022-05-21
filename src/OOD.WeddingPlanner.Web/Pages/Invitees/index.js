$(async function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitees.invitee;
    var weddingService = oOD.weddingPlanner.weddings.wedding;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitees/CreateModal',
        scriptUrl: "/Pages/Invitees/create.js",
        modalClass: "inviteeCreateModal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitees/EditModal',
        scriptUrl: "/Pages/Invitees/invitee.js",
        modalClass: "inviteeModal"
    });

    var getFilter = function () {
        return {
            filter: $("#FilterText").val(),
            name: $("#NameFilter").val(),
            surname: $("#SurnameFilter").val(),
            confirmed: (function () {
                var value = $("#ConfirmedFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value.toLowerCase() === 'true';
            })(),
            child: (function () {
                var value = $("#ChildFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value.toLowerCase() === 'true';
            })(),
            hasInvitation: (function () {
                var value = $("#HasInvitationFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value.toLowerCase() === 'true';
            })()
        };
    };
    var weddingList = await weddingService.getList({ maxCountResult: 0 });
    var dataTable = $('#InviteeTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(service.getListWithNavigation, getFilter),
        columnDefs: [
            {
                width: "100px",
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('WeddingPlanner.Invitee.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.invitee.id });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: abp.auth.isGranted('WeddingPlanner.Invitee.Delete'),
                                confirmMessage: function (data) {
                                    return l('InviteeDeletionConfirmationMessage', data.record.invitee.name);
                                },
                                action: function (data) {
                                    service.delete(data.record.invitee.id)
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
                title: l('InviteeSurname'),
                data: "invitee.surname"
            },
            {
                title: l('InviteeName'),
                data: "invitee.name"
            },
            {
                title: l('InviteeRSVP'),
                data: "invitee.rsvp",
                render: function (_, type, record) {
                    if(record.invitee.rsvp) {
                        var date = new Date(record.invitee.rsvp);
                        return date.toLocaleTimeString() + " " + date.toLocaleDateString();
                    } else {
                        return '';
                    }
                }
            },
            {
                title: l('InviteeConfirmed'),
                data: "invitee.confirmed"
            },
            {
                title: l('InviteePersonType'),
                data: "invitee.child",
                render: function (_, type, record) {
                    var age = l("Adult");
                    var gender = l("Female");
                    var plusOne = "";
                    if (record.invitee.child) age = l("Child");
                    if (record.invitee.male) gender = l("Male");
                    if (record.invitee.plusOne) plusOne = "(+1)";
                    return gender + " <span style='color: red'>" + age + "</strong> " + plusOne;
                }
            },
            {
                title: l('InviteeMenu'),
                data: "invitee.menu"
            },
            {
                title: l('Invitation'),
                data: "invitee.invitationId",
                render: function (_, type, record) {
                    if (record.invitation)
                        return record.invitation.destination;
                    return "";
                }
            },
            {
                title: l('Wedding'),
                visible: weddingList.totalCount > 1,
                data: "invitation.weddingId",
                render: function (_, type, record) {
                    if (record.wedding)
                        return record.wedding.name;
                    return "";
                }
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewInviteeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('input, select').on('blur change', function () {
        dataTable.ajax.reload();
    });
});
