$(function () {
    var l = abp.localization.getResource('GerEventos');
    var createModal = new abp.ModalManager(abp.appPath + 'Eventos/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Eventos/EditModal');

    // Exibir dados retornados pela API para depuração

    // Initialize DataTable
    var dataTable = $('#EventosTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(gerEventos.services.eventos.evento.getList),
            columnDefs: [
                {
                    title: l('Ação'),
                    rowAction: {
                        items: [
                            {
                                text: l('Editar'),
                                visible: abp.auth.isGranted('GerEventos.Evento.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
                            {
                                text: l('Deletar'),
                                visible: abp.auth.isGranted('GerEventos.Evento.Delete'),
                                confirmMessage: function (data) {
                                    return l('EventoDeletionConfirmationMessage', data.record.nome);
                                },
                                action: function (data) {
                                    gerEventos.services.eventos.evento
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
                    title: l('Data Início'),
                    data: "dataInicio",
                 
                },
                {
                    title: l('Data Fim'),
                    data: "dataFim",
                   
                },
                {
                    title: l('Produtor'),
                    data: "nomeProdutor"
                },
                {
                    title: l('Tipo de Evento'),
                    data: "nomeTipoEvento"
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
    $('#NewEventoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
