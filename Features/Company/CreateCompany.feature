Funcionalidade: Criar empresa

Cenario: Criação de empresa com sucesso
	Dado que a base de dados esteja limpa
	E que o usuário esteja autenticado
	E seja feita uma chamada do tipo 'POST' para o endpoint 'v1/companies' com o corpo da requisição
		"""
		{
		  "name": "<Name>",
		  "code": "001",
		  "maxEmployeesNumber": 15
		}
		"""
	Então o código de retorno será '201'
	E o registro estará disponível na tabela 'Company' da base de dados
		| Id | Code  | Name     | MaxEmployeesNumber | Active |
		| 1  | '001' | '<Name>' | 15                 | True   |

	Exemplos:
		| Name      |
		| Empresa 1 |