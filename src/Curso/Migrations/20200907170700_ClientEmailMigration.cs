using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoEFCore.Migrations
{
    public partial class ClientEmailMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SELECT *
                into #TMP_Pedido
                from Pedido
                GO

                SELECT *
                into #TMP_PedidoItem
                from PedidoItem
                GO

                SELECT *, '' as Email
                INTO #TMP_Cliente
                FROM Cliente
                GO

                DELETE PedidoItem
                DELETE Pedido
                DELETE Cliente
                GO");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Cliente",
                type: "VARCHAR(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Cliente ON;
                GO 

                INSERT INTO Cliente(Id, Nome, Telefone, CEP, Estado, Cidade, Email)
                SELECT a.Id, a.Nome, a.Telefone, a.CEP, a.Estado, a.Cidade, a.Email
                FROM #TMP_Cliente a
                GO

                SET IDENTITY_INSERT Cliente OFF;
                GO 

                SET IDENTITY_INSERT Pedido ON;
                GO 

                INSERT INTO Pedido(Id, ClienteId, IniciadoEm, FinalizadoEm, TipoFrete, Status, Observacao)
                SELECT a.Id, a.ClienteId, a.IniciadoEm, a.FinalizadoEm, a.TipoFrete, a.Status, a.Observacao
                FROM #TMP_Pedido a
                GO

                SET IDENTITY_INSERT Pedido OFF;
                GO 

                SET IDENTITY_INSERT PedidoItem ON;
                GO 

                INSERT INTO PedidoItem(Id, PedidoId, ProdutoId, Quantidade, Valor, Desconto)
                SELECT a.Id, a.PedidoId, a.ProdutoId, a.Quantidade, a.Valor, a.Desconto
                FROM #TMP_PedidoItem a
                GO

                SET IDENTITY_INSERT PedidoItem OFF;
                GO 

                DROP TABLE #TMP_Cliente
                GO
                DROP TABLE #TMP_Pedido
                GO
                DROP TABLE #TMP_PedidoItem
                GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Cliente");
        }
    }
}
