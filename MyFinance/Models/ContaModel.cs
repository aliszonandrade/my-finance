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
    public class ContaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por gentileza, meu amor! Preencha o nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por gentileza, meu amor! Preencha o saldo.")]
        public double Saldo { get; set; }
        public int Usuario_Id { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public ContaModel()
        {

        }

        public ContaModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public List<ContaModel> ListaConta()
        {
            List<ContaModel> lista = new List<ContaModel>();

            string sql = $"SELECT Id, Nome, Saldo, Usuario_Id FROM CONTA WHERE Usuario_Id={UsuarioModel.IdUsuarioLogado(HttpContextAccessor)}";
            DataTable dt = new DAL().RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ContaModel conta = new ContaModel();
                conta.Id = int.Parse(dt.Rows[i]["Id"].ToString());
                conta.Nome = dt.Rows[i]["Nome"].ToString();
                conta.Saldo = double.Parse(dt.Rows[i]["Saldo"].ToString());
                conta.Usuario_Id = int.Parse(dt.Rows[i]["Usuario_Id"].ToString());
                lista.Add(conta);
            }

            return lista;
        }

        public void Inserir()
        {
            string sql = $"INSERT INTO CONTA (Nome, Saldo, Usuario_Id) VALUES ('{Nome}',{Saldo}, {Usuario_Id})";
            new DAL().ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            string sql = $"DELETE FROM CONTA WHERE ID = {id}";
            new DAL().ExecutarComandoSQL(sql);
        }
    }
}
