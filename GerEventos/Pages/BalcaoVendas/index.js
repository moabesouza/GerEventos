$(function () {
    var l = abp.localization.getResource('GerEventos');
    var createModal = new abp.ModalManager(abp.appPath + 'BalcaoVendas/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'BalcaoVendas/EditModal');

    // Initialize DataTable
    var dataTable = $('#BalcaoVendasTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true, // Enable global search
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(gerEventos.services.balcaoVendas.balcaoVendas.getList),
            columnDefs: [
                {
                    title: l('Ação'),
                    rowAction: {
                        items: [
                            {
                                text: l('Editar'),
                                visible: abp.auth.isGranted('GerEventos.BalcaoVendas.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Deletar'),
                                visible: abp.auth.isGranted('GerEventos.BalcaoVendas.Delete'),
                                confirmMessage: function (data) {
                                    return l('BalcaoVendasDeletionConfirmationMessage', data.record.name);
                                },
                                action: function (data) {
                                    gerEventos.services.balcaoVendas.balcaoVendas
                                        .delete(data.record.id)
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
                    title: l('Nome'),
                    data: "nome"
                },
                {
                    title: l('Localização'),
                    data: "localizacao"
                },

            ]
        })
    );

    // Handle modal results
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    // Open create modal on button click
    $('#NewBalcaoVendasButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
