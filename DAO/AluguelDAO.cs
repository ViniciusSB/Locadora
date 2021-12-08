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
    public class AluguelDAO
    {
        private Context db = new Context();
        PedidoDAO pedidoDAO = new PedidoDAO();

        public AluguelxPedidoxFuncxCliente buscarDados()
        {
            AluguelxPedidoxFuncxCliente apfc = new AluguelxPedidoxFuncxCliente();
            apfc.pedidos = pedidoDAO.listarPedidosPorUsuarioAluguel();
            return apfc;
        }

        public AluguelxPedidoxFuncxCliente calcularDados(int? idPedido)
        {
            AluguelxPedidoxFuncxCliente apfc = new AluguelxPedidoxFuncxCliente();
            apfc = pedidoDAO.buscarPedidosEspecificos(idPedido);
            return apfc;
        }
    }
}