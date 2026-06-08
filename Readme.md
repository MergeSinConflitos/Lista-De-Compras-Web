# Lista De Compras Web

![]()


Maria faz as compras da família toda semana, mas sempre esquece algum item ou compra coisas que já tem
em casa. Por isso, resolveu pedir ajuda para organizar melhor as compras.
Assim foi criado o projeto Lista de Compras, um sistema simples para cadastrar produtos, organizar listas e
registrar as compras realizadas. Os alunos da Academia do Programador foram contratados para desenvolver a
aplicação.

## 1. Módulo de Categorias

**Requisitos Funcionais:**
* O sistema deve permitir cadastrar novas categorias
* O sistema deve permitir editar categorias existentes
* O sistema deve permitir excluir categorias
* O sistema deve permitir visualizar todas as categorias

**Regras de Negócio:**
* Campos obrigatórios:
* Nome (texto único, máximo 50 caracteres)
* Cor (seleção de paleta ou hexadecimal)
* Não pode haver categorias com nomes duplicados
* Não permitir excluir uma categoria caso tenha produtos vinculados

## 2. Módulo de Produtos

**Requisitos Funcionais:**
* O sistema deve permitir cadastrar novos produtos
* O sistema deve permitir editar produtos existentes
* O sistema deve permitir excluir produtos
* O sistema deve permitir visualizar todos os produtos cadastrados

**Regras de Negócio:**
* Campos obrigatórios:
* Nome (2 a 100 caracteres)
* Categoria (seleção obrigatória)
* Unidade de medida (ex: kg, unidade, litro, caixa)
* Preço aproximado
* Não pode haver produtos com o mesmo nome na mesma categoria

## 3. Módulo de Listas de Compras

**Requisitos Funcionais:**
* O sistema deve permitir criar novas listas de compras
* O sistema deve permitir editar listas existentes
* O sistema deve permitir excluir listas
* O sistema deve permitir visualizar todas as listas

**Regras de Negócio:**
* Campos obrigatórios:
* Nome da lista (mínimo 3 caracteres, máximo 100)
* Data de criação (automática)
* Status possíveis: Aberta / Concluída
* Não permitir excluir uma lista que já tenha itens vinculados
* O sistema deve exibir o total de itens e o total estimado gasto de cada lista

## 4. Módulo de Itens da Lista

**Requisitos Funcionais:**
* O sistema deve permitir adicionar itens a uma lista de compras
* O sistema deve permitir remover itens de uma lista
* O sistema deve permitir visualizar todos os itens de uma lista
* O sistema deve exibir a categoria do produto ao selecionar um item para a lista

**Regras de Negócio:**
* Campos obrigatórios:
* Produto (seleção obrigatória)
* Quantidade (número positivo)
* Não pode adicionar o mesmo produto duas vezes na mesma lista
* O valor total da lista deve ser calculado automaticamente (soma dos preços estimados × quantidades)

### Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

   ```bash
   dotnet restore
   ```

4. Para executar o projeto compilando em tempo real

   ```bash
   dotnet run --project ListaDeCompras.ConsoleApp
   ```

## Requisitos

- .NET 10.0 SDK
