$(function () {
    var l = abp.localization.getResource('GerEventos');
    var createModal = new abp.ModalManager(abp.appPath + 'Eventos/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Eventos/EditModal');

    // Initialize DataTable
    var dataTable = $('#EventosTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            responsive: true, // Habilita a cascata responsiva no DataTable para dispositivos móveis
            ajax: function (data, callback, settings) {
                var dataRange = $('#FiltroEvento_DataRange').data('daterangepicker');
                var filter = {
                    nome: $('#FiltroEvento_Nome').val(),
                    produtorId: $('#FiltroEvento_Produtor').val(),
                    dataInicio: dataRange.startDate ? dataRange.startDate.toISOString() : null,
                    dataFim: dataRange.endDate ? dataRange.endDate.endOf('day').toISOString() : null,
                    skipCount: data.start,
                    maxResultCount: data.length,
                    sorting: data.columns[data.order[0].column].data + " " + data.order[0].dir
                };

                gerEventos.services.eventos.evento.getListFilter(filter).done(function (result) {
                    callback({
                        recordsTotal: result.totalCount,
                        recordsFiltered: result.totalCount,
                        data: result.items
                    });
                }).fail(function (xhr, status, error) {
                    abp.notify.error(l('Erro ao carregar os eventos.'));
                    console.error(xhr, status, error);
                });
            },
            columnDefs: [
                {
                    title: l('Grid:Acoes'),
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
                    title: l('Grid:DataInicio'),
                    data: "dataInicio"
                },
                {
                    title: l('Data Fim'),
                    data: "dataFim"
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

    // Apply filter on button click
    $('#ApplyFilterButton').click(function () {
        dataTable.ajax.reload();
    });

    // Clear filters and reload table
    $('#LimpaFiltroButton').click(function () {
        $('#FiltroEvento_Nome').val('');
        $('#FiltroEvento_Produtor').val(null).trigger('change');

        $('#FiltroEvento_DataRange').data('daterangepicker').setStartDate('');
        $('#FiltroEvento_DataRange').data('daterangepicker').setEndDate('');

        $('#FiltroEvento_DataRange').val('');

        dataTable.ajax.reload();
    });

    // Open create modal on button click
    $('#NewEventoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    // Initialize Select2 with all producers initially loaded
    $('#FiltroEvento_Produtor').select2({
        placeholder: "Selecione um produtor",
        allowClear: true,
        ajax: {
            url: '/api/app/produtor/',
            dataType: 'json',
            delay: 250,
            data: function (params) {
                return {
                    filter: params.term || '' // Envia uma string vazia para buscar todos os produtores se o termo estiver vazio
                };
            },
            processResults: function (data, params) {
                var filteredData = data.items;

                // Aplica o filtro somente se o usuário tiver digitado algo
                if (params.term && params.term.length > 0) {
                    filteredData = data.items.filter(function (item) {
                        return item.nome.toLowerCase().includes(params.term.toLowerCase());
                    });
                }

                return {
                    results: $.map(filteredData, function (item) {
                        return { id: item.id, text: item.nome };
                    })
                };
            },
            cache: true
        },
        minimumInputLength: 0 // Permite exibir todos os produtores ao abrir o dropdown sem necessidade de digitar
    });
});
