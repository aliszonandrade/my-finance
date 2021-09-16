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
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Preencha o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o e-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "E-mail informado é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha a senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Preencha a data de nascimento")]
        public DateTime Data_Nascimento { get; set; }

        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL='{Email}' AND SENHA='{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            if(dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = dt.Rows[0]["NOME"].ToString();
                    Data_Nascimento = DateTime.Parse(dt.Rows[0]["DATA_NASCIMENTO"].ToString());
                    return true;
                }
            }

            return false;
        }

        public static string IdUsuarioLogado(IHttpContextAccessor pContextAccessor)
        {
            return pContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
        }

        public void RegistrarUsuario()
        {
            string sql = $"INSERT INTO USUARIO(NOME, SENHA, EMAIL, DATA_NASCIMENTO) " +
                        $"VALUES ('{Nome}','{Senha}', '{Email}', '{Data_Nascimento.ToString("yyyy-MM-dd")}')";

            new DAL().ExecutarComandoSQL(sql);
        }

    }
}
