using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a data")]
        public string Data { get; set; }


        public string DataFinal { get; set; }

        public string Tipo { get; set; }

        public double Valor { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]

        public string Descricao { get; set; }

        public int Usuario_Id { get; set; }

        public int Conta_Id { get; set; }

        public string DescricaoConta { get; set; }

        public int PlanoConta_Id { get; set; }

        public string DescricaoPlanoConta { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TransacaoModel()
        {

        }

        public TransacaoModel(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public List<TransacaoModel> ListaTransacao()
        {
            List<TransacaoModel> lista = new List<TransacaoModel>();

            // Utilizado pela view Extrato
            string filtro = "";

            if (Data != null && DataFinal != null)
                filtro += $" AND T.DATA >= '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' AND T.DATA <= '{DateTime.Parse(DataFinal).ToString("yyyy/MM/dd")}' ";

            if (Tipo != null && Tipo != "A")
                filtro += $" AND T.TIPO = '{Tipo}' ";

            if (Conta_Id != 0)
                filtro += $" AND T.CONTA_ID = {Conta_Id} ";
            
            string sql = $"SELECT " +
                            $"T.ID,  T.DESCRICAO AS HISTORICO, T.DATA, T.TIPO, T.VALOR, T.CONTA_ID, C.NOME, " +
                            $"T.PLANO_CONTAS_ID, P.DESCRICAO AS PLANO_CONTA " +
                        $"FROM " +
                            $"TRANSACAO AS T INNER JOIN CONTA C ON C.ID = T.CONTA_ID INNER JOIN PLANO_CONTAS P ON P.ID = T.PLANO_CONTAS_ID " +
                        $"WHERE " +
                            $"T.USUARIO_ID = {UsuarioModel.IdUsuarioLogado(HttpContextAccessor)} {filtro} ORDER BY T.DATA DESC LIMIT 5";

            DataTable dt = new DAL().RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TransacaoModel transacao = new TransacaoModel();
                transacao.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                transacao.Descricao = dt.Rows[i]["HISTORICO"].ToString();
                transacao.Data = DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy");
                transacao.Tipo = dt.Rows[i]["TIPO"].ToString();
                transacao.Conta_Id = int.Parse(dt.Rows[i]["CONTA_ID"].ToString());
                transacao.DescricaoConta = dt.Rows[i]["NOME"].ToString();
                transacao.PlanoConta_Id = int.Parse(dt.Rows[i]["PLANO_CONTAS_ID"].ToString());
                transacao.DescricaoPlanoConta = dt.Rows[i]["PLANO_CONTA"].ToString();
                transacao.Valor = double.Parse(dt.Rows[i]["VALOR"].ToString());
                lista.Add(transacao);
            }

            return lista;
        }

        public TransacaoModel CarregarRegistro(int? id)
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");

            string sql = $"SELECT " +
                            $"T.ID,  T.DESCRICAO AS HISTORICO, T.DATA, T.TIPO, T.VALOR, T.CONTA_ID, C.NOME, " +
                            $"T.PLANO_CONTAS_ID, P.DESCRICAO AS PLANO_CONTA " +
                        $"FROM " +
                            $"TRANSACAO AS T INNER JOIN CONTA C ON C.ID = T.CONTA_ID INNER JOIN PLANO_CONTAS P ON P.ID = T.PLANO_CONTAS_ID " +
                        $"WHERE " +
                            $"T.ID = {id}";

            DataTable dt = new DAL().RetDataTable(sql);

            TransacaoModel transacao = new TransacaoModel();
            transacao.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            transacao.Descricao = dt.Rows[0]["HISTORICO"].ToString();
            transacao.Data = DateTime.Parse(dt.Rows[0]["DATA"].ToString()).ToString("dd/MM/yyyy");
            transacao.Tipo = dt.Rows[0]["TIPO"].ToString();
            transacao.Conta_Id = int.Parse(dt.Rows[0]["CONTA_ID"].ToString());
            transacao.DescricaoConta = dt.Rows[0]["NOME"].ToString();
            transacao.PlanoConta_Id = int.Parse(dt.Rows[0]["PLANO_CONTAS_ID"].ToString());
            transacao.DescricaoPlanoConta = dt.Rows[0]["PLANO_CONTA"].ToString();
            transacao.Valor = double.Parse(dt.Rows[0]["VALOR"].ToString());

            return transacao;
        }

        public void Inserir(int? id)
        {
            string sql = "";
            if (id == null)
            {
                sql = $"INSERT INTO TRANSACAO (DATA, TIPO, VALOR, DESCRICAO, CONTA_ID, PLANO_CONTAS_ID, USUARIO_ID) VALUES " +
                    $"('{DateTime.Parse(Data).ToString("yyyy/MM/dd")}','{Tipo}', '{Valor}', '{Descricao}', {Conta_Id}, {PlanoConta_Id}, {Usuario_Id})";
            }
            else
            {
                sql = $"UPDATE TRANSACAO " +
                    $"SET DATA = '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}', " +
                    $"TIPO = '{Tipo}', " +
                    $"VALOR = '{Valor}', " +
                    $"DESCRICAO = '{Descricao}', " +
                    $"CONTA_ID = '{Conta_Id}', " +
                    $"PLANO_CONTAS_ID = '{PlanoConta_Id}', " +
                    $"USUARIO_ID = '{Usuario_Id}' " +
                    $"WHERE USUARIO_ID = '{Usuario_Id}' AND ID = {Id}";
            }

            new DAL().ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM TRANSACAO WHERE ID = {id}";
            new DAL().ExecutarComandoSQL(sql);
        }
    }

    public class DashBoard
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public DashBoard()
        {

        }

        public DashBoard(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public double Total { get; set; }

        public string Descricao { get; set; }

        public List<DashBoard> retornarDados()
        {
            List<DashBoard> lista = new List<DashBoard>();

            string sql = "SELECT " +
                            "SUM(T.VALOR) AS TOTAL, P.DESCRICAO " +
                        "FROM    " +
                            "TRANSACAO AS T    INNER JOIN PLANO_CONTAS AS P ON T.PLANO_CONTAS_ID = P.ID " +
                        "WHERE    " +
                            $"T.TIPO = 'D' AND T.USUARIO_ID = {UsuarioModel.IdUsuarioLogado(HttpContextAccessor)}" +
                        "group by P.DESCRICAO; ";

            DataTable dt = new DAL().RetDataTable(sql);

            foreach (var item in dt.Rows)
            {
                DashBoard dashboard = new DashBoard();
                dashboard.Descricao = ((System.Data.DataRow)item).ItemArray[1].ToString();
                dashboard.Total = Double.Parse(((System.Data.DataRow)item).ItemArray[0].ToString());
                lista.Add(dashboard);
            }

            return lista;
        }
    }
}
