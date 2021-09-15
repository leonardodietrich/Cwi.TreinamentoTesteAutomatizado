using Cwi.TreinamentoTesteAutomatizado.Controllers;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.EmployeeSteps
{
    [Binding]
    [Scope(Tag = "CreateEmployee")]
    public class CreateEmployeeSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public CreateEmployeeSteps(HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }

        [Given(@"que seja solicitado a criação de um novo funcionário")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionario()
        {
            HttpRequestController.AddJsonBody(new { Name = "Funcionário 1", Email = "funcionario1@empresa.com" });

            await HttpRequestController.SendAsync("v1/employees", "POST");

        }

        [Then(@"o funcionário não será cadastrado")]
        public void EntaoOFuncionarioNaoSeraCadastrado()
        {
            Assert.AreNotEqual(HttpStatusCode.Created, HttpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"o funcionário será cadastrado")]
        public void EntaoOFuncionarioSeraCadastrado()
        {
            Assert.AreEqual(HttpStatusCode.Created, HttpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"será retornado uma mensagem de falha de autenticação")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDeAutenticacao()
        {
            Assert.AreEqual(HttpStatusCode.Unauthorized, HttpRequestController.GetResponseHttpStatusCode());
        }

    }
}
