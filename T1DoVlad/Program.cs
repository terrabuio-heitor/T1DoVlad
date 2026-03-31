/*
Alunos
[
 Nome: Heitor Terrabuio
 RA: 252407
 E-mail: heitorterrabuio@gmail.com
],
[
 Nome: Gustavo Campos
 RA: 241734
 E-mail: gustavo.campos.ribeiro@hotmail.com
]
[
 Nome: Luiz Henrique da Silva
 RA: 256732
 E-mail: luiz60828@gmail.com
]
[
 Nome: Murilo Soares Bezerra
 RA: 257013 
 E-mail: muri31102006@gmail.com
]

*/


using Spectre.Console; //extensão para melhorar o CLI, deixando ele mais bonito e interativo. --Heitor
using System;
using System.Threading;
using T1DoVlad.Servicos;

//ja criei e configurei o banco de dados usando o SQLite no DatabaseConfig.cs.
//optei por antes de inserir os dados na tabela do banco, fazer as funcoes de leitura e criacao de item de acordo com os parametros do professor.--Murilo


//Diminui o código ao máximo, deixando ele mais organizado e legível, e também criei uma classe de interface do
//usuário para lidar com toda a parte de exibição e interação, deixando a classe Program apenas para rodar o sistema,
//e a classe LojaServico para lidar com as funções de negócio, como criar itens, ler itens, etc. --Heitor

namespace projeto_denovo_tp_vladmir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool loop = true;
            while (loop)
            {
                try
                {
                    InterfaceUsuario UI = new InterfaceUsuario();//Puxo a UI--Heitor
                    LojaServico LJ = new LojaServico();//puxo a classe--Heitor
                    LJ.CarregarDados();//Carrego os dados do JSON--Heitor
                    UI.ExibirCabecalho("A Doninha Encantada", false);
                    AnsiConsole.Write(new Panel("Bem vindo ao sistema novo da loja...")
                                           .Header("[bold yellow]Status[/]")
                                           .Border(BoxBorder.Rounded)
                                           .BorderStyle(new Style(Color.Blue)) 
                                       );
                    Thread.Sleep(500);
                    Console.WriteLine("O que deseja fazer? ");
                    Console.WriteLine($"Deseja VENDER um item, GERENCIAR os itens, ou checar os RELATÓRIOS?");
                    Thread.Sleep(1000);
                    UI.ExibirMenu();
                    loop = UI.ExibirSair();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        
    }
}