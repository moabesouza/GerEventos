$(function () {
    $('#Evento_ProdutorId').change(function () {
        var produtorId = $(this).val();

        if (produtorId) {
            abp.ajax({
                url: abp.appPath + 'api/app/produtor/' + produtorId,
                type: 'GET',
                success: function (result) {
                    $('#produtorEndereco').text('Endereço: ' + (result.endereco ? result.endereco : 'Não disponível'));
                    $('#produtorSite').text('Site: ' + (result.site ? result.site : 'Não disponível'));
                    $('#produtorInfo').removeClass('d-none');
                },
                error: function (e) {
                    abp.notify.error('Erro ao buscar as informações do produtor.');
                    console.error(e);
                }
            });
        } else {
            $('#produtorInfo').addClass('d-none');
        }
    });

});
