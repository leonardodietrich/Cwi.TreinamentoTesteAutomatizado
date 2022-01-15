@GetAllCompanies
Funcionalidade: Obter empresas cadastradas na base de dados
	Como usuário
	Eu quero listar as empresas
	Para visualizar a lista de empresas presentes na base de dados

Cenario: Validando busca de empresas para usuário não autenticado
	Dado que o usuário não esteja autenticado
	Quando o usuário solicitar um 'GET' do endpoint 'v1/companies'
	Entao o código de retorno será '401'

Cenario: Validando busca de empresas sem registros na base de dados
	Dado que o usuário esteja autenticado
	E que a base de dados esteja limpa
	Quando o usuário solicitar um 'GET' do endpoint 'v1/companies'
	Entao o código de retorno será '204'

Cenario: Validando busca de empresas com registros na base de dados
	Dado que o usuário esteja autenticado
	E que a base de dados esteja limpa
	E que a tabela 'company' tenha os registros
		| code  | name        | maxemployeesnumber | active |
		| '001' | 'Empresa 1' | 100                | true   |
		| '002' | 'Empresa 2' | 150                | false  |
		| '003' | 'Empresa 3' | 80                 | true   |
	Quando o usuário solicitar um 'GET' do endpoint 'v1/companies'
	Entao o código de retorno será '200'
	E vou receber um json com a response
		"""
		[
		  {
			"id": 1,
			"name": "Empresa 1",
			"code": "001",
			"active": true
		  },
		  {
		    "id": 2,
			"name": "Empresa 2",
			"code": "002",
			"active": false
		  },
		  {
		    "id": 3,
			"name": "Empresa 3",
			"code": "003",
			"active": true
		  }
		]
		"""