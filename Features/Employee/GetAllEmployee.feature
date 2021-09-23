Funcionalidade: Obter todos os funcionários

Cenario: Obter os funcionários sem registros na base
	Dado que a base de dados esteja limpa
	E que o usuário esteja autenticado
	E seja feita uma chamada do tipo 'GET' para o endpoint 'v1/employees'
	Então o código de retorno será '204'

Cenario: Obter os funcionários com registros na tabela
	Dado que a base de dados esteja limpa
	E que a tabela 'Employee' tenha os registros
		| Name            | Email                      | Active |
		| 'Funcionário 1' | 'funcionario1@empresa.com' | True   |
		| 'Funcionário 2' | 'funcionario2@empresa.com' | False  |
	E que o usuário esteja autenticado
	E seja feita uma chamada do tipo 'GET' para o endpoint 'v1/employees'
	Então o código de retorno será '200'
	E vou receber um json com a response
	"""
	[
	  {
		"id": 1,
		"name": "Funcionário 1",
		"email": "funcionario1@empresa.com",
		"active": true
	  },
	  {
		"id": 2,
		"name": "Funcionário 2",
		"email": "funcionario2@empresa.com",
		"active": false
	  }
	]
	"""