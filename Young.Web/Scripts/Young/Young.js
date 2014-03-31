$(function () {
    $('.young-richbox').each(function (index, item) {
        UM.getEditor(item.id, {
            toolbar: [
            'source | bold italic underline strikethrough |  forecolor backcolor | removeformat |',
            'insertorderedlist insertunorderedlist  | fontfamily fontsize',
            '| justifyleft justifycenter justifyright justifyjustify'
            ]
        });
    });
})