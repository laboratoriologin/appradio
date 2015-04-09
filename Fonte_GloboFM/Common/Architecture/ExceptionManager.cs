using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Common.Architecture
{
    /// <summary>
    /// Classe de exceção
    /// </summary>
    public class ExceptionManager
    {
        /// <summary>
        /// Tipo de exceptions
        /// </summary>
        public enum TipoMensagem
        {
            /// <summary>
            /// Mensagem Padrao
            /// </summary>
            Padrao,

            /// <summary>
            /// Mensagem Conexao
            /// </summary>
            Conexao,

            /// <summary>
            /// Mensagem ErroAtualizacao
            /// </summary>
            ErroAtualizacao
        }
    }
}
