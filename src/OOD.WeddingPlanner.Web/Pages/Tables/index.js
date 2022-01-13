$(async function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.tables.table;
    var weddingService = oOD.weddingPlanner.weddings.wedding;
    var eventService = oOD.weddingPlanner.events.event;
    var inviteeService = oOD.weddingPlanner.invitees.invitee;
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Tables/CreateModal',
        scriptUrl: "/Pages/Tables/table.js",
        modalClass: "tableModal"
    });
    var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + 'Tables/EditModal',
        scriptUrl: "/Pages/Tables/table.js",
        modalClass: "tableModal"
    });

    ko.subscribable.fn.subscribeChanged = function (callback) {
        var oldValue;
        this.subscribe(function (_oldValue) {
            oldValue = _oldValue;
        }, this, 'beforeChange');

        this.subscribe(function (newValue) {
            callback(newValue, oldValue);
        });
    };

    ko.bindingHandlers.select2 = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var options = ko.unwrap(valueAccessor());
            ko.unwrap(allBindings.get('selectedOptions'));

            $(element).select2(options);
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            var options = ko.unwrap(valueAccessor());
            ko.unwrap(allBindings.get('selectedOptions'));

            $(element).select2(options);
        }
    };

    function Lookup(lkp) {
        var lookup = this;
        lookup.value = ko.observable(lkp.id);
        lookup.text = ko.observable(lkp.displayName);
    }

    function Invitee(invt) {
        var invitee = this;
        invitee.id = invt.id;
        invitee.surname = invt.surname;
        invitee.name = invt.name;
        invitee.tableId = ko.observable(invt.tableId);
        invitee.tableId.subscribe(async function () {
            var dbInvitee = await inviteeService.get(invitee.id);
            if (dbInvitee.tableId !== invitee.tableId()) {
                dbInvitee.tableId = invitee.tableId();
                await inviteeService.update(invitee.id, dbInvitee);
                abp.notify.info(l('SuccessfullyUpdated'));
            }
        });
    }

    function Guest(table) {
        var guest = this;
        guest.number = ko.observable();
        guest.invitee = ko.observable();

        guest.invitees = ko.computed(function () {
            var arrayFilter = vm.allInvitees().filter(function (p) { return !p.tableId() || p.id === guest.invitee(); });
            return arrayFilter.map(function (value) {
                value.displayName = (value.surname || '') + ' ' + (value.name || '');
                return new Lookup(value);
            });
        });

        guest.invitee.subscribeChanged(async function (current, previous) {
            if (previous) {
                var inv = vm.allInvitees().filter(function (p) { return p.id == previous })[0];
                if (inv) inv.tableId(null);
            }
            if (current) {
                var inv = vm.allInvitees()
                    .filter(function (p) { return p.id == current })[0];
                if (inv) inv.tableId(table.id);
            }
        });
    }

    function Table(tbl) {
        var table = this;
        table.__record = tbl;
        table.id = tbl.table.id;
        table.name = tbl.table.name;
        table.description = tbl.table.description;
        table.row = tbl.table.row;
        table.column = tbl.table.column;
        table.guests = ko.observableArray();

        table.canEdit = ko.computed(function () {
            return abp.auth.isGranted('WeddingPlanner.Table.Update');
        });
        table.canDelete = ko.computed(function () {
            return abp.auth.isGranted('WeddingPlanner.Table.Delete');
        });

        table.editTable = function () {
            editModal.open({ id: table.id });
        }

        table.deleteTable = function () {
            abp.message.confirm(l('TableDeletionConfirmationMessage', table.name), l('AreYouSure?'), (result) => {
                if (result) {
                    service.delete(table.id)
                        .then(function () {
                            abp.notify.info(l('SuccessfullyDeleted'));
                            vm.reload();
                        });
                }
            })
        }

        for (var i = 0; i < tbl.table.size; i++) {
            var guest = new Guest(table);
            guest.number(i + 1);
            if (tbl.invitees.length > i) {
                var invitee = tbl.invitees[i];
                guest.invitee(invitee.id);
            }
            table.guests.push(guest);
        }
    }

    function ViewModel() {
        var vm = this;
        vm.weddings = ko.observableArray();
        vm.wedding = ko.observable();
        vm.events = ko.observableArray();
        vm.event = ko.observable();
        vm.tables = ko.observableArray();
        vm.allInvitees = ko.observableArray();

        vm.event.subscribe(async function () {
            var tables = await service.getListWithNavigation({ eventId: vm.event(), sorting: 'table.creationTime ASC' });
            vm.tables(tables.items.map(function (value) { return new Table(value); }));
        });

        vm.wedding.subscribe(async function () {
            var invitees = await inviteeService.getList({ weddingId: vm.wedding() });
            invitees.items.splice(0, 0, { surname: "---------" });
            vm.allInvitees(invitees.items.map(function (value) { return new Invitee(value); }));

            var events = await eventService.getLookupList({ weddingId: vm.wedding() });
            vm.events(events.items.map(function (value) { return new Lookup(value); }));
        });

        vm.init = async function () {
            var weddings = await weddingService.getLookupList({});
            vm.weddings(weddings.items.map(function (value) { return new Lookup(value); }));
        }

        vm.reload = function () {
            var id = vm.event();
            vm.event(null);
            vm.event(id);
        }

        vm.init();
    }

    var vm = new ViewModel();
    ko.applyBindings(vm, $('#table-card')[0]);

    createModal.onResult(function () {
        vm.reload();
    });

    editModal.onResult(function () {
        vm.reload();
    });

    $('#NewTableButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });


});
