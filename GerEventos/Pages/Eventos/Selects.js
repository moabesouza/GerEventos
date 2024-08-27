
$(function () {

    $('#Evento_ProdutorId').select2({
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