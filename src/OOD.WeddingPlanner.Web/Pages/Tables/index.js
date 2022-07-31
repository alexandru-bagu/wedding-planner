$(async function () {

    var l = abp.localization.getResource('WeddingPlanner');

    var service = oOD.weddingPlanner.tables.table;
    var weddingService = oOD.weddingPlanner.weddings.wedding;
    var eventService = oOD.weddingPlanner.events.event;
    var inviteeService = oOD.weddingPlanner.invitees.invitee;
    var tableInviteeService = oOD.weddingPlanner.tableInvitees.tableInvitee;

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
        if (!this.__hasOldSubscribed) {
            this.__hasOldSubscribed = true;
            this.subscribe(function (_oldValue) {
                oldValue = _oldValue;
            }, this, 'beforeChange');
        }

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
        invitee.tableId = ko.observable();
    }

    function Guest(table) {
        var guest = this;
        guest.number = ko.observable();
        guest.invitee = ko.observable();
        guest.plusOne = ko.observable(); 
        guest.pushUpdates = false;

        guest.invitees = ko.computed(function () {
            var arrayFilter = vm.allInvitees().filter(function (p) { return !p.tableId() || p.id === guest.invitee(); });
            return arrayFilter.map(function (value) {
                value.displayName = (value.surname || '') + ' ' + (value.name || '');
                return new Lookup(value);
            });
        });

        guest.invitee.subscribeChanged(async function (current, previous) {
            guest.plusOne(null);
            if (current) {
                var plusOne = await inviteeService.getPlusOneById(current);
                if (plusOne) {
                    guest.plusOne(plusOne);
                }
            }
            if (!guest.pushUpdates) return;
            if (previous) {
                var inv = vm.allInvitees().filter(function (p) { return p.id == previous })[0];
                if (inv) inv.tableId(null);
            }
            if (current) {
                var inv = vm.allInvitees().filter(function (p) { return p.id == current })[0];
                if (inv) inv.tableId(table.id);
            }
            if (pushUpdates) {
                if (previous) {
                    var tblInv = await tableInviteeService.getList({ tableId: table.id, inviteeId: previous, maxResultCount: 1000 });
                    if (tblInv.items) {
                        await tableInviteeService.delete(tblInv.items[0].id);
                        abp.notify.info(l("SuccessfullyUpdated"));
                    }
                }
                if (current) {
                    await tableInviteeService.create({ tableId: table.id, inviteeId: current });
                    abp.notify.info(l("SuccessfullyUpdated"));
                }
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
            if (tbl.assignments.length > i) {
                var assignment = tbl.assignments[i];
                guest.invitee(assignment.inviteeId);
                guest.pushUpdates = true;
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
            pushUpdates = false;
            var invitees = await inviteeService.getList({ confirmed: true, weddingId: vm.wedding(), maxResultCount: 1000 });
            invitees.items.splice(0, 0, { surname: "---------" });
            vm.allInvitees(invitees.items.map(function (value) { return new Invitee(value); }));

            var tables = await service.getListWithNavigation({ eventId: vm.event(), maxResultCount: 1000, sorting: 'table.creationTime ASC' });
            vm.tables(tables.items.map(function (value) { return new Table(value); }));
        });

        vm.wedding.subscribe(async function () {
            vm.tables([]);
            setTimeout(async function () {
                vm.allInvitees([]);
                var events = await eventService.getLookupList({ weddingId: vm.wedding(), maxResultCount: 1000 });
                if (!events.items.length) {
                    events.items.push({ id: '00000000-0000-0000-0000-000000000001', displayName: '-----' });
                }
                vm.events(events.items.map(function (value) { return new Lookup(value); }));
            });
        });

        vm.init = async function () {
            var weddings = await weddingService.getLookupList({ maxResultCount: 1000 });
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
        createModal.open({ weddingId: vm.wedding(), eventId: vm.event() });
    });

    $("#DownloadTablesButton").click(function () {
        window.open('/download/tables/' + vm.event(), '_blank');
    });
});
