using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly PostgreDatabaseController PostgreDatabaseController;

        public DatabaseSteps(PostgreDatabaseController postgreDatabaseController)
        {
            PostgreDatabaseController = postgreDatabaseController;
        }

        [Given(@"que a base de dados esteja limpa")]
        public async Task DadoQueABaseDeDadosEstejaLimpa()
        {
            await PostgreDatabaseController.ClearDatabase();
        }

        [Then(@"o registro estará disponível na tabela '(.*)' da base de dados")]
        public async Task EntaoORegistroEstaraDisponivelNaTabelaDaBaseDeDados(string tableName, Table table)
        {
            var currentItens = await PostgreDatabaseController.SelectFrom(tableName, table);

            Assert.NotZero(currentItens.Count(), $"Não foram escontrados registros na tabela {tableName}.");

            var actualJsonResponse = JsonConvert.SerializeObject(currentItens);
            var expectedJsonResponse = JsonConvert.SerializeObject(table.CreateDynamicSet()).Replace("'", "");


            Assert.IsTrue(JToken.DeepEquals(JToken.Parse(actualJsonResponse), JToken.Parse(expectedJsonResponse)), $"Conteúdo atual do retorno \n{actualJsonResponse} diferente do esperado \n{expectedJsonResponse}." );

        }

        [Given(@"que a tabela '(.*)' tenha os registros")]
        public async Task DadoQueATabelaTenhaOsRegistros(string tableName, Table table)
        {
            await PostgreDatabaseController.InsertInto(tableName, table);

            var currentItens = await PostgreDatabaseController.SelectFrom(tableName, table);

            Assert.NotZero(currentItens.Count(), $"Os registros não foram inseridos na tabela {tableName}.");
        }



    }
}
