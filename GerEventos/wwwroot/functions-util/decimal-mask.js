$(document).ready(function () {
    $('[data-decimal="true"]').mask('#.##0,00', {
        placeholder: '0,00',
        reverse: true,
    });

    $('[data-decimal="true"]').blur(function () {
        const value = $(this).val();
        if (value === '') {
            $(this).val('0,00');
        } else if (value.slice(-3) !== ',00' && value.indexOf(',') === -1) {
            const formattedValue = parseFloat(value.replace('.', '')).toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
            $(this).val(formattedValue);
        }
    });
});