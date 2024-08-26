$(function () {
    var l = abp.localization.getResource('GerEventos');
    var createModal = new abp.ModalManager(abp.appPath + 'TipoEvento/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'TipoEvento/EditModal');

    // Initialize DataTable
    var dataTable = $('#TipoEventoTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true, 
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(gerEventos.services.tipoEventos.tipoEvento.getList),
            columnDefs: [
                {
                    title: l('Grid:Acoes'),
                    rowAction: {
                        items: [
                            {
                                text: l('Editar'),
                                visible: abp.auth.isGranted('GerEventos.TipoEvento.Edit'),
                                action: function (data) {
                                    editModal.open({ id: data.record.id });
                                }
                            },
               

                            {
                                text: l('Desativar'),
                                visible: abp.auth.isGranted('GerEventos.TipoEvento.Desativar') ,
                                confirmMessage: function (data) {
                                    return l('Deseja realmente desativar?');
                                },
                                action: function (data) {
                                    gerEventos.services.tipoEventos.tipoEvento
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
                                visible: abp.auth.isGranted('GerEventos.TipoEvento.Ativar') , 
                                confirmMessage: function (data) {
                                    return l('Deseja realmente ativar?');
                                },
                                action: function (data) {
                                    gerEventos.services.tipoEventos.tipoEvento
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
    $('#NewTipoEventoButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
