using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_Burguer.Modelos
{
    public class OrdensDeProducao
    {
       public OrdensDeProducao()
        {
            ordens = new List<Ordem>();

            ordensConcluidas = new List<Ordem>();
        }

        private List<Ordem> ordens;
        private List<Ordem> ordensConcluidas;
        public List<Ordem> OrdensConcluidas { get => ordensConcluidas; set => ordensConcluidas = value; }

        public List<Ordem> Ordens { get => ordens; set => ordens = value; }

        public void RegistrarNovaOrdemDeProducao(Ordem ordem)
        {
            Ordens.Add(ordem);
        }
        public List<Ordem> ListarOrdensDeProducao()
        {
            return Ordens;
        }
        public List<Ordem> ListarOrdensDeProducaoConcluidas()
        {
            return OrdensConcluidas;
        }
        public void ConcluirOrdemDeProducao(int id)
        {
            OrdensConcluidas.Add(Ordens[Ordens.Count - 1]);
            Ordens.RemoveAt(id - 1);
        }

    }
}
