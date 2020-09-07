using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            // using var db = new ApplicationContext();
            // Dangerous executing on prod
            // db.Database.Migrate();

            // var hasPendingMigration = db.Database.GetPendingMigrations().Any();
            // if (hasPendingMigration)
            // {
            //     // logic..
            // }

            // InsertProduct();
            // ProductBulkInsert();
            // GetClientes();
            // InsertPedido();
            // GetPedidoEagerLoading();
            // UpdateClient();
            // DeleteClient();
            // InsertClients();
        }

        private static void DeleteClient()
        {
            using var db = new Data.ApplicationContext();
            // var cliente = db.Clientes.Find(3);

            var cliente = new Cliente { Id = 4 };

            // db.Clientes.Remove(cliente);
            // db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void UpdateClient()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);
            // cliente.Nome = "Cliente Alterado Passo 4";
            cliente.Cidade = "SBO";

            // var cliente = new Cliente { Id = 2 };
            // var clienteDesconectado = new
            // {
            //     Nome = "Cliente Desconectado Passo 3",
            //     Telefone = "19999999966"
            // };

            // db.Attach(cliente);
            // db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            // Atualiza todas as propriedades
            // db.Clientes.Update(cliente);
            // db.Entry(cliente).State = EntityState.Modified;

            // Se apenas chamar SaveChanges(), irá fazer update só nas propriedades que mudaram
            db.SaveChanges();
        }

        // Carregamento Adiantado
        private static void GetPedidoEagerLoading()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(pedido => pedido.Itens)
                .ThenInclude(item => item.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void InsertPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void GetClientes()
        {
            using var db = new Data.ApplicationContext();
            // var queryByLINQ = (from cliente in db.Clientes where cliente.Id > 0 select cliente)
            //     .ToList();
            var queryByExtesion = db.Clientes
                // .AsNoTracking()
                .Where(cliente => cliente.Id > 0)
                .OrderBy(cliente => cliente.Id)
                .ToList();

            foreach (var cliente in queryByExtesion)
            {
                Console.WriteLine($"Consultando cliente: {cliente.Id}");
                // Apenas Find que é o responsável por consultar se o objeto está em memória ou não
                // db.Clientes.Find(cliente.Id);
                // Vai consultar direto na base
                db.Clientes.FirstOrDefault(cli => cli.Id == cliente.Id);
            }

        }

        private static void ProductBulkInsert()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "01234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MarcadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Vinicius",
                CEP = "99999999",
                Cidade = "Americana",
                Estado = "SP",
                Telefone = "19999999999",
                // Email = "vi@email.com.br"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registers = db.SaveChanges();
            Console.WriteLine($"Total: {registers}");
        }

        private static void InsertProduct()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "01234567891234",
                Valor = 10m,
                TipoProduto = TipoProduto.MarcadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto);

            var registers = db.SaveChanges();
            Console.WriteLine($"Registros alterados: {registers}");
        }

        private static void InsertClients()
        {
            using var db = new Data.ApplicationContext();
            var clients = new List<Cliente>();

            for (int i = 1; i <= 10; i++)
            {
                clients.Add(new Cliente
                {
                    Nome = $"Teste {i}",
                    CEP = "99999999",
                    Cidade = "Americana",
                    Estado = "SP",
                    Telefone = "19999999999",
                    // Email = $"vi_{i}@email.com.br"
                });
            }

            db.Clientes.AddRange(clients);
            db.SaveChanges();
        }
    }
}
