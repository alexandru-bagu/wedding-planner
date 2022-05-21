$(function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.languageTexts.languageText;
    var createModal = new abp.ModalManager(abp.appPath + 'LanguageTexts/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'LanguageTexts/EditModal');

    var getFilter = function () {
        return {
            filter: $("#FilterText").val(),
            cultureName: $("#CultureFilter").val(),
        };
    };

    var dataTable = $('#LanguageTextTable').DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        autoWidth: false,
        scrollCollapse: true,
        order: false,
        ajax: abp.libs.datatables.createAjax(service.getAllList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l('Edit'),
                                visible: abp.auth.isGranted('WeddingPlanner.LanguageText.Update'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id, name: data.record.name, value: data.record.value, cultureName: getFilter().cultureName });
                                }
                            },
                            {
                                text: l('Delete'),
                                visible: function (record) { return record.id !== "00000000-0000-0000-0000-000000000000" && abp.auth.isGranted('WeddingPlanner.LanguageText.Delete'); },
                                confirmMessage: function (data) {
                                    return l('LanguageTextDeletionConfirmationMessage', data.record.id);
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
                title: l('LanguageTextName'),
                data: "name"
            },
            {
                title: l('LanguageTextValue'),
                data: "value"
            },
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewLanguageTextButton').click(function (e) {
        e.preventDefault();
        createModal.open({ cultureName: getFilter().cultureName });
    });


    $("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $('input, select').on('blur change', function () {
        dataTable.ajax.reload();
    });
});
