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
    public class PlanoContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a descrição")]
        public string Descricao { get; set; }

        public string Tipo { get; set; }
        public int Usuario_Id { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; } 

        public PlanoContaModel()
        {

        }

        public PlanoContaModel(IHttpContextAccessor httpContext)
        {
            HttpContextAccessor = httpContext;
        }

        public List<PlanoContaModel> ListaPlanoContas()
        {
            List<PlanoContaModel> lista = new List<PlanoContaModel>();

            string sql = $"SELECT Id, Descricao, Tipo, Usuario_Id FROM PLANO_CONTAS WHERE Usuario_Id={UsuarioModel.IdUsuarioLogado(HttpContextAccessor)}";
            DataTable dt = new DAL().RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PlanoContaModel planoConta = new PlanoContaModel();
                planoConta.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                planoConta.Descricao = dt.Rows[i]["DESCRICAO"].ToString();
                planoConta.Tipo = dt.Rows[i]["TIPO"].ToString();
                planoConta.Usuario_Id = int.Parse(dt.Rows[i]["Usuario_Id"].ToString());
                lista.Add(planoConta);
            }

            return lista;
        }

        public PlanoContaModel CarregarRegistro(int? id)
        {
            string sql = $"SELECT Id, Descricao, Tipo, Usuario_Id FROM PLANO_CONTAS WHERE Usuario_Id={UsuarioModel.IdUsuarioLogado(HttpContextAccessor)} AND Id = {id}";
            DataTable dt = new DAL().RetDataTable(sql);

            PlanoContaModel item = new PlanoContaModel();
            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Descricao = dt.Rows[0]["DESCRICAO"].ToString();
            item.Tipo = dt.Rows[0]["TIPO"].ToString();
            item.Usuario_Id = int.Parse(dt.Rows[0]["Usuario_Id"].ToString());

            return item;
        }

        public void Inserir(int? id)
        {
            string sql = "";
            if(id == null)
            {
                sql = $"INSERT INTO PLANO_CONTAS (DESCRICAO, TIPO, USUARIO_ID) VALUES ('{Descricao}','{Tipo}', {Usuario_Id})";
            }
            else
            {
                sql = $"UPDATE PLANO_CONTAS SET DESCRICAO = '{Descricao}', TIPO = '{Tipo}' WHERE USUARIO_ID = '{Usuario_Id}' AND ID = {Id}";
            }
            
            new DAL().ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM PLANO_CONTAS WHERE ID = {id}";
            new DAL().ExecutarComandoSQL(sql);
        }
    }
}
