/*
 * 
 * Analista: Jacques de Lassau
 * Data: 04/08/2022 23:17h
 * Modificações: Incluídas constantes para textos soltos em validações dentro dos controllers.
 * 
 */

using System;

namespace LabExameWebsite
{
    public class Constantes
    {
        public const string MensagemAlerta = "AlertaController";
        public const string MensagemErro = "Houve um problema durante a solicitação. Por favor, contate o suporte especializado. Erro original: {0}";
        public const string CpfInvalido = "Desculpe, mas o Cpf digitado é inválido ou não existe. Por favor, tente novamente.";
        public const string CpfExiste = "Desculpe, mas o Cpf digitado já foi cadastrado. Por favor, verifique o cadastro e tente novamente.";
        public static string ProtocoloAleatorio = Guid.NewGuid().ToString();
    }
}