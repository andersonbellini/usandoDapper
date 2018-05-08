using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Usando_Dapper
{
    class Program
    {
        static string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;

        static void Main(string[] args)
        {
            //Exemplo1(); //Usando Mysql
            Exemplo2(); //Usando SQL Server  Northwind
            //Exemplo3();
            //Exemplo4();
            //Exemplo5();
            //Exemplo6();
            Console.ReadKey();
        }

        private static void Exemplo6()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var atualizarBD = @"Update Products Set UnitPrice = @UnitPrice
                                  Where ProductID = @ProductId";

                conexaoBD.Execute(atualizarBD, new
                {
                    UnitPrice = 99.9m,
                    ProductId = 50
                });

                Console.WriteLine("Informações do fornecedor atualizadas com sucesso.");
            }
        }

        private static void Exemplo5()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var fornecedor = new Fornecedor()
                {
                    Address = "Rua das flores 1000",
                    CompanyName = "Bellini SA"
                };

                conexaoBD.Execute(@"insert Suppliers(CompanyName, Address)
                                  values (@CompanyName, @Address)", fornecedor);

                Console.WriteLine("Informações do fornecedor incluídas com sucesso.");
            }
        }

        private static void Exemplo4()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
               var consulta = @"SELECT * FROM dbo.Suppliers WHERE SupplierID = @Id
                              SELECT * FROM dbo.Products WHERE SupplierID = @Id";
                
                using (var resultado = conexaoBD.QueryMultiple(consulta, new { Id = 1 }))
                {
                    var fornecedor = resultado.Read().Single();
                    var produtos = resultado.Read().ToList();

                    Console.WriteLine("Fornecedor  - {0} ", fornecedor.CompanyName);

                    Console.WriteLine(string.Format("Total de Produtos {0}", produtos.Count));
                    foreach (dynamic produto in produtos)
                    {
                        Console.WriteLine("{0} - {1} - {2}", produto.ProductID, produto.ProductName, produto.UnitPrice);
                    }
                }
            }
        }

        private static void Exemplo3()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                IEnumerable produtos = conexaoBD.Query<Produto>
                    (
                      "Select ProductID, ProductName, UnitPrice from Products Where UnitPrice > @Preco", new { Preco = 40 }
                    );

                Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Produto", "Preço do Produto");
                foreach (Produto produto in produtos)
                {
                    Console.WriteLine("{0} - {1} - {2}", produto.ProductID, produto.ProductName, produto.UnitPrice);
                }
            }
        }
       
        private static void Exemplo2()
        {
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                IEnumerable produtos = conexaoBD.Query<Produto>("Select * from Products");

                Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Produto", "Preço do Produto");
                foreach (Produto produto in produtos)
                {
                    Console.WriteLine("{0} - {1} - {2}", produto.ProductID, produto.ProductName, produto.UnitPrice);
                }
            }
       }

        /// <summary>
        /// Usando Mysql
        /// </summary>
        private static void Exemplo1()
        {
            MySqlConnection conexaoBD = new MySqlConnection(ConfigurationManager.ConnectionStrings["bd_gdaConnectionString"].ConnectionString);
            conexaoBD.Open();

            var resultado = conexaoBD.Query("Select * from ckf_gda_cockpit_centro");

            Console.WriteLine("{0} - {1} - {2} ", "num_id", "str_centro", "str_descr");
            foreach (var item in resultado)
            {
                Console.WriteLine("{0} - {1} - {2} ", item.num_id, item.str_centro, item.str_descr);
            }
            conexaoBD.Close();
        }

    }
}
