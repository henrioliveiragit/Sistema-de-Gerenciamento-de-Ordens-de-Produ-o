using System;
using System.Collections.Generic;
using System.Text;

namespace Fast_Burguer.Modelos
{
    public class Materiais
    {
       public Materiais()
        {
            materiais = new List<Material>();
        }

        private List<Material> materiais;

        public List<Material> MateriaisLista { get => materiais; set => materiais = value; }
        public void EntradaDeMateriais(Material material)
        {
            MateriaisLista.Add(material);
        }

        public List<Material> ListarMateriais()
        {
            return MateriaisLista;
        }

        public bool RemoverMaterial(string nome)
        {
            int c = 0;
            foreach (Material material in materiais)
            {
               if(material.Nome == nome)
                {
                    MateriaisLista[c].Quantidade = MateriaisLista[c].Quantidade - 1;
                }
            }
            return true;
        }

    }
}
