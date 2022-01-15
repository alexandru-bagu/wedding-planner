$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.invitees.invitee;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Invitees/CreateModal',
        scriptUrl: "/Pages/Invitees/invitee.js",
        modalClass: "inviteeModal"
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
                return value === 'true';
            })()
        };
    };

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
                data: "invitee.rsvp"
            },
            {
                title: l('InviteeConfirmed'),
                data: "invitee.confirmed"
            },
            {
                title: l('InviteeChild'),
                data: "invitee.child"
            },
            {
                title: l('InviteeMale'),
                data: "invitee.male"
            },
            {
                title: l('Invitation'),
                render: function (_, type, record) {
                    if (record.invitation)
                        return record.invitation.destination;
                    return "";
                }
            },
            {
                title: l('Wedding'),
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
