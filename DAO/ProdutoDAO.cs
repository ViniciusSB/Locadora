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
    public class ProdutoDAO
    {
        private Context db = new Context();
        PedidoDAO pedidoDAO = new PedidoDAO();

        public void reduzirQuantidade(List<Produto> produtos)
        {
            foreach(Produto p in produtos)
            {
                p.quantidadeDisponivel -= 1;
                db.Produtoes.Add(p);
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }

        }


    }
}