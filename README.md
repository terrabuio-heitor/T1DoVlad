# 🦦 A Doninha Encantada - Sistema de Gestão de RPG

![Status](https://img.shields.io/badge/Status-Desenvolvimento-blue)
![C#](https://img.shields.io/badge/C%23-7.2-purple)
![Professor](https://img.shields.io/badge/Professor-Vladmir%20Cruz-gold)

Trabalho de conclusão do 1º Bimestre da disciplina de **Desenvolvimento de Sistemas** (ADS - FORTEC). [cite_start]O projeto consiste em uma aplicação de console interativa para modernizar a gestão de estoque e vendas da lendária loja do Sr. Getúlio Setter[cite: 21, 23].

## 👥 Equipe (Os Desenvolvedores)
* [cite_start]**Heitor Terrabuio** - RA: 252407 [cite: 59, 60]
* [cite_start]**Gustavo Campos** - RA: [Inserir Aqui] [cite: 65, 66]
* **Luiz Henrique da Silva** - RA: 256732
* **Murilo Soares Bezerra** - RA: 257013

---

## 🛠️ Tecnologias e Arquitetura
[cite_start]O sistema foi construído utilizando as melhores práticas de **Arquitetura Limpa** e **Programação Orientada a Objetos**[cite: 46]:

* **Linguagem:** C# (.NET Framework 4.7.2).
* **Interface:** [Spectre.Console](https://spectreconsole.net/) para uma CLI rica e interativa.
* **Persistência:** JSON (Newtonsoft.Json) para armazenamento de estoque e vendas.
* [cite_start]**POO:** Uso de classe abstrata `ItemRPG` com herança para `Arma` e `Pocao`[cite: 42, 89].
* [cite_start]**Consultas:** Implementação extensiva de **LINQ** para filtragem e ordenação de relatórios[cite: 48, 89].

---

## 📦 Módulos do Sistema

### 1. Gestão de Estoque (CRUD)
[cite_start]Permite ao administrador do sistema[cite: 25]:
* [cite_start]**Cadastrar** novos itens (Armas ou Poções)[cite: 26].
* [cite_start]**Atualizar** preços de itens existentes[cite: 28].
* [cite_start]**Repor** a quantidade de itens no estoque[cite: 29].
* **Remover** itens do catálogo.

### 2. Controle de Vendas
[cite_start]Processamento de transações informando o item e a quantidade[cite: 31, 32]:
* [cite_start]**Cálculo Automático:** O sistema gera o valor total da venda instantaneamente[cite: 33].
* [cite_start]**Validação de Defesa:** Bloqueio de vendas acima do estoque disponível com mensagens de erro amigáveis[cite: 34].
* [cite_start]**Registro de Histórico:** Cada venda bem-sucedida é salva no arquivo `dadosVenda.json`[cite: 35, 89].

### 3. Painel do Gerente (Relatórios)
[cite_start]Relatórios gerados puramente através de **LINQ**[cite: 36, 48]:
* [cite_start]**Estoque:** Lista itens com estoque > 0, ordenados do mais caro para o mais barato[cite: 37, 89].
* [cite_start]**Histórico de Vendas:** Listagem detalhada de todas as transações realizadas[cite: 38, 89].
* [cite_start]**Fechamento de Caixa:** Exibição do valor total arrecadado em tempo real[cite: 39, 89].

---

## 🛡️ Regras de Negócio (Defesa do Código)
[cite_start]O código foi "blindado" contra falhas lógicas[cite: 44, 45]:
* **Preço/Estoque:** Nenhuma operação permite valores negativos.
* [cite_start]**Integridade:** Uso de `try-catch` em todas as interações críticas para garantir que o sistema não "quebre" durante o uso do Sr. Getúlio[cite: 45, 89].

---

## 🚀 Como Executar
1. Certifique-se de ter o SDK do .NET Framework 4.7.2 instalado.
2. Clone o repositório.
3. Restaure os pacotes NuGet (Spectre.Console e Newtonsoft.Json).
4. Execute o projeto via Visual Studio ou `dotnet run`.
