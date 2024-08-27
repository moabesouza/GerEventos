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
            searching: false, 
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(gerEventos.services.balcaoVendas.balcaoVendas.getList),
            columnDefs: [
                {
                    title: l('Grid:Acoes'),
                    rowAction: {
                        items: [
                            {
                                text: l('Editar'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Desativar'),
                                action: function (data) {
                                    gerEventos.services.balcaoVendas.balcaoVendas
                                        .deactivate(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('Desativado com sucesso!'));
                                            dataTable.ajax.reload();
                                        })
                                        .catch(function (error) {
                                            abp.notify.error(l('Erro ao desativar!'));
                                            console.error(error);
                                        });
                                }
                            },
                            {
                                text: l('Ativar'),
                                action: function (data) {
                                    gerEventos.services.balcaoVendas.balcaoVendas
                                        .activate(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l('Ativado com sucesso!'));
                                            dataTable.ajax.reload();
                                        })
                                        .catch(function (error) {
                                            abp.notify.error(l('Erro ao ativar!'));
                                            console.error(error);
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
                    title: l('Grid:Localizacao'),
                    data: "localizacao"
                },

                {
                    title: l('Status'),
                    data: "status",
                    render: function (data) {
                        return data === 1 ? '<span class="badge rounded-pill bg-success">Ativado</span>' : '<span class="badge rounded-pill bg-info">Desativado</span>';
                    }
                }
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

    // Update dropdown actions based on permissions and status
    $('#BalcaoVendasTable').on('draw.dt', function () {
        $('#BalcaoVendasTable tbody tr').each(function () {
            var $row = $(this);
            var data = dataTable.row($row).data();
            var status = data.status;

            // Find dropdown for this row
            var $dropdown = $row.find('.dropdown-menu');

            // Hide all actions initially
            $dropdown.find('a.dropdown-item').hide();

            // Show relevant actions based on status and permissions
            if (abp.auth.isGranted('GerEventos.BalcaoVendas.Edit')) {
                $dropdown.find('a:contains("Editar")').show();
            }

            if (abp.auth.isGranted('GerEventos.BalcaoVendas.Desativar') && status === 1) {
                $dropdown.find('a:contains("Desativar")').show();
            }

            if (abp.auth.isGranted('GerEventos.BalcaoVendas.Ativar') && status === 2) {
                $dropdown.find('a:contains("Ativar")').show();
            }
        });
    });
});



