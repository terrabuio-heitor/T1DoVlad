using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

// comecei usando o gpt pra me ajudar na criação do banco de dados, ainda lembro como usar o SQL Server e os comandos são os mesmos ou bem parecidos, usarei mais pra saber os comandos da biblioteca.

namespace projeto_denovo_tp_vladmir.Data {
    public static class DatabaseConfig {
        private static readonly string connectionString = "Data Source=loja.db";

        public static void InicializarBanco() {
            using (SqliteConnection connection = new SqliteConnection(connectionString)) {
                connection.Open();

                string sqlItens = @"
                    CREATE TABLE IF NOT EXISTS Itens (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nome TEXT NOT NULL,
                        Preco REAL NOT NULL,
                        Estoque INTEGER NOT NULL,
                        Tipo TEXT NOT NULL
                    );
                ";

                string sqlVendas = @"
                    CREATE TABLE IF NOT EXISTS Vendas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ItemId INTEGER NOT NULL,
                        NomeItem TEXT NOT NULL,
                        Quantidade INTEGER NOT NULL,
                        ValorUnitario REAL NOT NULL,
                        ValorTotal REAL NOT NULL,
                        DataVenda TEXT NOT NULL
                    );
                ";

                using (SqliteCommand command = connection.CreateCommand()) {
                    command.CommandText = sqlItens;
                    command.ExecuteNonQuery();

                    command.CommandText = sqlVendas;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}