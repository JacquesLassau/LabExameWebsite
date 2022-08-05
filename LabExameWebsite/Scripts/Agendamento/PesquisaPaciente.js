function OnclickPesquisarCpfPaciente() {

    event.preventDefault();
    var item = document.getElementById("CpfPacienteAgendamento").value;

    if (item == undefined)
        item = "";

    $.get("/Agendamento/AgendamentorPacienteCpfJR?cpfPesquisaPaciente=" + item, function (data) {

        if (data.CpfPaciente != null) {
            $(function () {
                $("#PacienteID").val(data.PacienteID);
                $("#NomePacienteSelecionado").val(data.NomePaciente);
            });
        }

    });
}