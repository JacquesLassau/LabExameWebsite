function OnclickPesquisarExame() {

    event.preventDefault();

    var item = document.getElementById("TipoExameIDAgendamento").value;
    if (item == undefined)
        item = "";

    $.get("/Agendamento/AgendamentorListaExamesJR?idTipoExame=" + item, function (data) {

        $("#ExameID").empty();
        $("#dropdownListarExames").css({ "display": "block" });
        $("#btnPesquisaExame").attr("disabled", "disabled");

        for (var i = 0; i < data.length; i++) {
            $('#ExameID').append('<option value="' + data[i].ExameID + '">' + data[i].NomeExame + '</option>');
        }

    });
}