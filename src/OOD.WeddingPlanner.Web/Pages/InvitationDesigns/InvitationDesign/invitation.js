$(function () {
    var vm = new ViewModel(invitation);
    ko.applyBindings(vm);

    function ViewModel(invitation) {
        var vm = this;
        vm.invitation = invitation;
    }
});