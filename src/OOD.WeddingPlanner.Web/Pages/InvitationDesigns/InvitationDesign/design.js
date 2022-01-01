var abp = abp || {};

abp.modals.designModal = function () {
  var initModal = function (publicApi, args) {

    publicApi.onOpen(function () {
      var modal = publicApi.getModal();
      modal.find('select').select2();
    });

  };

  return {
    initModal: initModal
  };
};

