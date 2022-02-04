var abp = abp || {};

abp.modals.select2modal = function () {
    var initModal = function (publicApi, args) {

        if(publicApi.__init__) return;
        publicApi.__init__ = true;
        
        publicApi.onOpen(function () {
            var modal = publicApi.getModal();
            modal.find('select').select2();
        });

    };

    return {
        initModal: initModal
    };
};

