using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class HttpRequestSteps
    {
        private readonly HttpRequestController HttpRequestController;
        public HttpRequestSteps (HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }


        [When(@"o usuário solicitar um '(.*)' do endpoint '(.*)'")]
        [Given(@"seja feita uma chamada do tipo '(.*)' para o endpoint '(.*)'")]
        public async Task DadoSejaFeitaUmaChamadaDoTipoParaOEndpoint(string httpMethodName, string endpoint)
        {
            await HttpRequestController.SendAsync(endpoint, httpMethodName);
        }


        [Given(@"seja feita uma chamada do tipo '(.*)' para o endpoint '(.*)' com o corpo da requisição")]
        public async Task DadoSejaFeitaUmaChamadaDoTipoParaOEndpointComOCorpoDaRequisicao(string httpMethodName, string endpoint, string body)
        {
            HttpRequestController.AddJsonBody(body);

            await HttpRequestController.SendAsync(endpoint, httpMethodName);
        }

        [Then(@"o código de retorno será '(.*)'")]
        public void EntaoOCodigoDeRetornoSera(int httpStatusCode)
        {
            Assert.AreEqual(httpStatusCode, (int)HttpRequestController.GetResponseHttpStatusCode());
        }

        [Then(@"vou receber um json com a response")]
        public async Task EntaoVouReceberUmJsonComAResponse(string responseContent)
        {
            var responseCurrent = JValue.Parse(await HttpRequestController.GetResponseBodyContent()).ToString(Formatting.Indented);
            var expectedResponseContent = JValue.Parse(responseContent).ToString(Formatting.Indented);

            Assert.IsTrue(JToken.DeepEquals(JToken.Parse(responseCurrent), JToken.Parse(expectedResponseContent)), $"Conteúdo atual do retorno \n{responseCurrent} diferente do esperado \n{expectedResponseContent}.");
        }
    }
}
