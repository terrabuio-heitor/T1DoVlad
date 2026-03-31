# 🦦 A Doninha Encantada - Sistema de Gestão de RPG

![Status](https://img.shields.io/badge/Status-Desenvolvimento-blue)
![C#](https://img.shields.io/badge/C%23-7.2-purple)
![Professor](https://img.shields.io/badge/Professor-Vladmir%20Cruz-gold)

Trabalho de conclusão do 1º Bimestre da disciplina de **Desenvolvimento de Sistemas** (ADS - FORTEC). O projeto consiste numa aplicação de consola interativa para modernizar a gestão de stock e vendas da lendária loja do Sr. Getúlio Setter.

## 👥 Equipe (Os Desenvolvedores)
* **Heitor Terrabuio** - RA: 252407
* **Gustavo Campos** - RA: 241734
* **Luiz Henrique da Silva** - RA: 256732
* **Murilo Soares Bezerra** - RA: 257013

---

## 🛠️ Tecnologias e Arquitetura
O sistema foi construído utilizando as melhores práticas de **Arquitetura Limpa** e **Programação Orientada a Objetos**:

* **Linguagem:** C# (.NET Framework 4.7.2).
* **Interface:** [Spectre.Console](https://spectreconsole.net/) para uma CLI rica e interativa.
* **Persistência:** JSON (Newtonsoft.Json) para armazenamento de stock e vendas.
* **POO:** Uso de classe abstrata `ItemRPG` com herança para as classes filhas `Arma` e `Pocao`.
* **Consultas:** Implementação extensiva de **LINQ** para filtragem, somatórios e ordenação de relatórios.

---

## 📦 Módulos do Sistema

### 1. Gestão de Stock (CRUD)
Permite ao administrador do sistema o controlo total dos itens:
* **Cadastrar:** Adição de novos itens ao catálogo (Armas ou Poções).
* **Atualizar:** Funcionalidade para alteração de preços e reposição de stock de itens existentes.
* **Remover:** Exclusão definitiva de itens do inventário.

### 2. Controlo de Vendas
Processamento de transações informando o item e a quantidade:
* **Cálculo Automático:** O sistema gera o valor total da venda instantaneamente.
* **Validação de Defesa:** Bloqueio de vendas acima do stock disponível com mensagens de erro amigáveis.
* **Registo de Histórico:** Cada venda bem-sucedida é guardada no arquivo `dadosVenda.json`.

### 3. Painel do Gerente (Relatórios)
Relatórios gerados obrigatoriamente através de **System.Linq**:
* **Relatório de Stock:** Lista itens com stock disponível, ordenados do mais caro para o mais barato.
* **Histórico de Vendas:** Listagem detalhada e cronológica de todas as transações realizadas.
* **Fecho de Caixa:** Exibição do valor total (R$) arrecadado com todas as vendas.

---

## 🛡️ Regras de Negócio (Defesa do Código)
O código foi desenvolvido com foco em estabilidade e segurança:
* **Encapsulamento:** Nenhuma classe permite a entrada de valores negativos para preço ou stock.
* **Integridade:** Uso de `try-catch` e `throw` em todas as interações críticas para garantir que o sistema não encerre inesperadamente durante o uso.

---

## 🚀 Como Executar
1. Certifique-se de ter o SDK do .NET Framework 4.7.2 instalado.
2. Clone o repositório.
3. Restaure os pacotes NuGet (`Spectre.Console` e `Newtonsoft.Json`).
4. Execute o projeto via Visual Studio ou através do comando `dotnet run`.
