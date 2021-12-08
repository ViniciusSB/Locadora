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
    public class PedidoDAO
    {
        private Context db = new Context();

        public AluguelxPedidoxFuncxCliente buscarPedidosEspecificos(int? idPedido)
        {
            Pedido pedido = db.Pedidoes.Find(idPedido);
            int vezesAlugadas = db.Pedidoes.Find(pedido.id).vezesAlugadas;
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezesAlugadas", new SqlParameter("@idCliente", pedido.idCliente), new SqlParameter("@vezesAlugadas", vezesAlugadas)).ToList();
            AluguelxPedidoxFuncxCliente apfc = new AluguelxPedidoxFuncxCliente();
            foreach (Pedido p in pedidos)
            {
                if (apfc.pedidos == null)
                {
                    apfc.pedidos = new List<Pedido>();
                }
                apfc.pedidos.Add(p);
                Produto prod = db.Produtoes.Find(p.idProduto);
                
                apfc.valorTotal += prod.valor;
                if (apfc.produtos == null)
                {
                    apfc.produtos = new List<Produto>();
                }
                apfc.produtos.Add(prod);
            }
            Cliente cliente = db.Clientes.Find(pedido.idCliente);
            apfc.cliente = cliente;
            DateTime hoje = DateTime.Now;
            DateTime entrega = hoje.AddDays(apfc.pedidos[0].quantidadeDias);
            apfc.dataLimiteEntrega = entrega.ToString().Substring(0,10);
            return apfc; 
        }

        public List<Pedido> listarPedidosPorUsuarioPedido()
        {
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes order by vezesAlugadas").ToList();
            List<int> idsClientes = new List<int>();


            foreach(Pedido p in pedidos)
            {
                if (!idsClientes.Contains(p.idCliente))
                {
                    idsClientes.Add(p.idCliente);
                }
            }
            List<int> idPedidoRemover = new List<int>();
            List<Pedido> copia = pedidos;
            foreach(int n in idsClientes)
            {
                List<Pedido> pedido = new List<Pedido>();
                foreach (Pedido p in copia)
                {
                    if (p.idCliente == n)
                    {
                        pedido.Add(p);
                    }
                }

                foreach(Pedido i in pedido)
                {
                    List<Pedido> pe = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezes", new SqlParameter("@idCliente", i.idCliente), new SqlParameter("@vezes", i.vezesAlugadas)).ToList();
                    if (pe.Count() > 1)
                    {
                        for(int j=0; j<pe.Count() -1; j++)
                        {
                            if (pe[j].id == i.id)
                            {
                                var itemToRemove = pedidos.Single(r => r.id == i.id);
                                pedidos.Remove(itemToRemove);
                            }
                        }
                    }
                }

            }
            //pedidos = db.Pedidoes.ToList();
            return pedidos;
        }

        public int buscarQuantidadePedidosById(int idPedido)
        {
            Pedido pedido = db.Pedidoes.Find(idPedido);
            List<Pedido> pe = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezes", new SqlParameter("@idCliente", pedido.idCliente), new SqlParameter("@vezes", pedido.vezesAlugadas)).ToList();
            return pe.Count();
        }

        public List<Pedido> listarPedidosPorUsuarioAluguel()
        {
            //Verifica se já existe Aluguel ativo com os pedidos 
            List<Aluguel> aluguels = db.Aluguels.ToList();
            List<Pedido> pedidosFeitos = new List<Pedido>();
            if (aluguels != null)
            {
                foreach (Aluguel al in aluguels)
                {
                    AluguelxPedidoxFuncxCliente apfc = buscarPedidosEspecificos(al.idPedido);
                    foreach (Pedido ped in apfc.pedidos)
                    {
                        pedidosFeitos.Add(ped);
                    }
                }
            }

            List<Pedido> pedidos = new List<Pedido>();
            pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes order by vezesAlugadas").ToList();
            List<int> idsClientes = new List<int>();

            //Limpando os pedidos que ja foram feitos da lista
            foreach (Pedido p in pedidosFeitos)
            {
                var itemToRemove = pedidos.Single(r => r.id == p.id);
                pedidos.Remove(itemToRemove);
            }


            foreach (Pedido p in pedidos)
            {
                if (!idsClientes.Contains(p.idCliente))
                {
                    idsClientes.Add(p.idCliente);
                }
            }
            List<int> idPedidoRemover = new List<int>();
            List<Pedido> copia = pedidos;
            foreach (int n in idsClientes)
            {
                List<Pedido> pedido = new List<Pedido>();
                foreach (Pedido p in copia)
                {
                    if (p.idCliente == n)
                    {
                        pedido.Add(p);
                    }
                }

                foreach (Pedido i in pedido)
                {
                    List<Pedido> pe = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezes", new SqlParameter("@idCliente", i.idCliente), new SqlParameter("@vezes", i.vezesAlugadas)).ToList();
                    if (pe.Count() > 1)
                    {
                        for (int j = 0; j < pe.Count() - 1; j++)
                        {
                            if (pe[j].id == i.id)
                            {
                                var itemToRemove = pedidos.Single(r => r.id == i.id);
                                pedidos.Remove(itemToRemove);
                            }
                        }
                    }
                }

            }
            //pedidos = db.Pedidoes.ToList();
            return pedidos;
        }

        public void salvarPedido(int idCliente, Pedido pedido, int[] idsFilmes, int[] idsSeries, int[] idsJogos)
        {
            List<Pedido> p = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @id order by vezesAlugadas desc", new SqlParameter("@id", idCliente)).ToList();
            Pedido ped = new Pedido();
            if (p.Count() > 0)
            {
                ped.vezesAlugadas = p[0].vezesAlugadas +1;
            }
            else
            {
                ped.vezesAlugadas = 1;
            }
            ped.quantidadeDias = pedido.quantidadeDias;
            if (idsFilmes != null)
            {
                foreach (int idFilme in idsFilmes)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idFilme;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
            if (idsSeries != null)
            {
                foreach (int idSerie in idsSeries)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idSerie;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
            if (idsJogos != null)
            {
                foreach (int idJogo in idsJogos)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idJogo;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
        }

        public void editarPedido(int idCliente, Pedido pedido, int[] idsFilmes, int[] idsSeries, int[] idsJogos)
        {
            int vezesAlugadas = db.Pedidoes.Find(pedido.id).vezesAlugadas;
            List<Pedido> pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezes", new SqlParameter("@idCliente", idCliente), new SqlParameter("@vezes", vezesAlugadas)).ToList();
            foreach(Pedido pedi in pedidos)
            {
                pedi.vezesAlugadas = pedido.vezesAlugadas;
                db.Pedidoes.Remove(pedi);
                db.SaveChanges();
            }

            List<Pedido> p = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @id order by vezesAlugadas desc", new SqlParameter("@id", idCliente)).ToList();
            Pedido ped = new Pedido();
            if (p.Count() > 0)
            {
                ped.vezesAlugadas = p[0].vezesAlugadas +1;
            }
            else
            {
                ped.vezesAlugadas += 1;
            }
            ped.quantidadeDias = pedido.quantidadeDias;
            if (idsFilmes != null) {
                foreach (int idFilme in idsFilmes)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idFilme;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
            if (idsSeries != null)
            {
                foreach (int idSerie in idsSeries)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idSerie;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
            if (idsJogos != null)
            {
                foreach (int idJogo in idsJogos)
                {
                    ped.idCliente = idCliente;
                    ped.idProduto = idJogo;
                    db.Pedidoes.Add(ped);
                    db.SaveChanges();
                }
            }
        }

        public List<int> buscarSeriesPedidos(int? id)
        {
            Pedido ped = db.Pedidoes.Find(id);
            List<Pedido> pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezesAlugadas", new SqlParameter("@idCliente", ped.idCliente), new SqlParameter("@vezesAlugadas", ped.vezesAlugadas)).ToList();
            List<int> series = new List<int>();
            foreach(Pedido pedi in pedidos)
            {       
                Produto serie = db.Produtoes.Find(pedi.idProduto);
                if (serie != null)
                {
                    series.Add(serie.id);
                }
            }
            return series;
        }

        public List<int> buscarJogosPedidos(int? id)
        {
            Pedido ped = db.Pedidoes.Find(id);
            List<Pedido> pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezesAlugadas", new SqlParameter("@idCliente", ped.idCliente), new SqlParameter("@vezesAlugadas", ped.vezesAlugadas)).ToList();
            List<int> jogos = new List<int>();
            foreach (Pedido pedi in pedidos)
            {
                Produto jogo = db.Produtoes.Find(pedi.idProduto);
                if (jogo != null)
                {
                    jogos.Add(jogo.id);
                }
            }
            return jogos;
        }

        public List<int> buscarFilmesPedidos(int? id)
        {
            Pedido ped = db.Pedidoes.Find(id);
            List<Pedido> pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezesAlugadas", new SqlParameter("@idCliente", ped.idCliente), new SqlParameter("@vezesAlugadas", ped.vezesAlugadas)).ToList();
            List<int> filmes = new List<int>();
            foreach (Pedido pedi in pedidos)
            {
                Produto filme = db.Produtoes.Find(pedi.idProduto);
                if (filme != null)
                {
                    filmes.Add(filme.id);
                }
            }
            return filmes;
        }

        public void deletarPedidos(int idCliente, Pedido pedido)
        {
            int vezesAlugadas = db.Pedidoes.Find(pedido.id).vezesAlugadas;
            List<Pedido> pedidos = db.Pedidoes.SqlQuery("Select * from dbo.Pedidoes where idCliente = @idCliente and vezesAlugadas = @vezes", new SqlParameter("@idCliente", idCliente), new SqlParameter("@vezes", vezesAlugadas)).ToList();
            foreach (Pedido pedi in pedidos)
            {
                pedi.vezesAlugadas = pedido.vezesAlugadas;
                db.Pedidoes.Remove(pedi);
                db.SaveChanges();
            }
        }
    }
}