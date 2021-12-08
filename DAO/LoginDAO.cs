using SistemaLocadora.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaLocadora.DAO
{
    public class LoginDAO
    {
        private Context db = new Context();
        public Funcionario verificarLogin(String email, String senha)
        {
            List<Funcionario> lista = db.Funcionarios.ToList();
            foreach (Funcionario user in lista)
            {
                if (user.email.Equals(email) && user.senha.Equals(senha))
                {
                    return user;
                }
            }
            return new Funcionario();
        }
    }
}