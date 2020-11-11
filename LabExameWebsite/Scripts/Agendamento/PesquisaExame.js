function OnclickPesquisarExame() {

    event.preventDefault();

    var item = document.getElementById("TipoExameIDAgendamento").value;

    if (item == null)
        item = "";

    $.get("/Agendamento/AgendamentorListaExamesJR?idTipoExame=" + item, function (data) {

        $("#ExameID").empty();
        $("#dropdownListarExames").css({ "display": "block" });

        for (var i = 0; i < data.length; i++) {

            $('#ExameID').append('<option value="' + data[i].ExameID + '">' + data[i].NomeExame + '</option>');

        }

        /*
        for (var i = 0; i < data.length; i++) {
            $('#ExameID').append($('<option>', {
                value: data[i].ExameID,
                text: data[i].NomeExame
            }));
        }
        */

    });
}