var abp = abp || {};

abp.modals.select2modal = function () {
    var initModal = function (publicApi, args) {

        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2({
                dropdownParent: modal
            });
        });
    };

    return {
        initModal: initModal
    };
};

