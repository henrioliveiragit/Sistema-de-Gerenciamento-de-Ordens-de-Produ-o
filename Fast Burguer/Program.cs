using System;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using Fast_Burguer.Modelos;

namespace Fast_Burguer
{
    internal class Program
    {
        static OrdensDeProducao ordensDeProducao = new OrdensDeProducao();
        static Materiais materiais = new Materiais();

        static void Main(string[] args)
        {
            string opcao;
            do
            {
                ListarOrdensDeProducao();
                Console.WriteLine(MostrarHeader());
                Console.WriteLine(MostrarMenu());
                opcao = LerOpcaoMenu();
                ProcessarOpcaoMenu(opcao);
            } while (opcao != "6");
        }
        static void ProcessarOpcaoMenu(string opcao)
        {
            switch(opcao)
            {
                case "1":
                    RegistrarNovaOrdemDeProducao();
                    break;
                case "2":
                    ConcluirOrdemDeProducao();
                    break;
                case "3":
                    EntradaNoEstoque();
                    break;
                case "4":
                    ListarMateriaisEmEstoque();
                    break;
                case "5":
                    RelatorioDeProducao();
                    break;
                default:
                    break;
            }    
        }
        static string SelecionarProdutoPorId(int id)
        {
            switch(id)
            {
                case 1:
                    return "Hambúrguer Picanha";
                case 2:
                    return "Hambúrguer Bacon";
                case 3:
                    return "Hambúrguer Egg";
                case 4:
                    return "Batata Frita";
                case 5:
                    return "Coca-Cola";
                case 6:
                    return "Guaraná";
                default:
                    return "Não Definido";
            }
        }
        static string SelecionarDataHoraPorId(int id)
        {
            DateTime dataHoraAtual = DateTime.Now;
            switch (id)
            {
                case 1:
                    return dataHoraAtual.AddMinutes(10).ToString("HH:mm"); 
                case 2:
                    return dataHoraAtual.AddMinutes(30).ToString("HH:mm");
                case 3:
                    return dataHoraAtual.AddMinutes(60).ToString("HH:mm"); 
                default:
                    return "Não Definido";
            }
        }
        static void ListarOrdensDeProducao()
        {
            //VARIÁVEIS
            string opcao = "";
            List<Ordem> ordens = ordensDeProducao.ListarOrdensDeProducao();

            //VERIFICANDO SE A LISTA NÃO ESTÁ VAZIA
            if (ordens.Count > 0)
            {
                int c = 0;
                Console.WriteLine("==============================");

                foreach (Ordem ordem in ordens)
                {
                    c++;
                    Console.WriteLine("(N#" + c + ")");
                    Console.WriteLine("Produto: " + ordem.Produto);
                    Console.WriteLine("Quantidade: " + ordem.Quantidade);
                    Console.WriteLine("Data/Hora: " + ordem.DataHora);
                    Console.WriteLine("==============================");
                }

            }
            else
            {
                Console.WriteLine("Lista vazia...");
            }

            
        }
        static void ListarMateriaisEmEstoque()
        {
            //VARIÁVEIS
            string opcao = "";
            List<Material> materiaisLista = materiais.ListarMateriais();

            //VERIFICANDO SE A LISTA NÃO ESTÁ VAZIA
            if (materiaisLista.Count > 0)
            {
                int c = 0;
                Console.WriteLine("==============================");

                foreach (Material material in materiaisLista)
                {
                    c++;
                    Console.WriteLine("(N#" + c + ")");
                    Console.WriteLine("Material: " + material.Nome);
                    Console.WriteLine("Quantidade: " + material.Quantidade);
                    Console.WriteLine("==============================");
                }

            }
            else
            {
                Console.WriteLine("Lista vazia...");
            }

            Console.WriteLine("Pressione Enter para Prosseguir...");
            Console.ReadLine();
            Console.Clear();
        }
        static void RelatorioDeProducao()
        {
            //VARIÁVEIS
            string opcao = "";
            List<Ordem> ordens = ordensDeProducao.ListarOrdensDeProducao();
            List<Ordem> ordensConcluidas = ordensDeProducao.ListarOrdensDeProducaoConcluidas();

            //VERIFICANDO SE AS LISTAS NÃO ESTÃO VAZIAS
            if (ordens.Count > 0 || ordensConcluidas.Count > 0)
            {
                Console.WriteLine("PENDENTES:");
                ListarOrdensDeProducao();
                Console.WriteLine("CONCLUÍDAS:");
                if(ordensConcluidas.Count > 0)
                {
                    Console.WriteLine("==============================");
                    foreach (Ordem ordem in ordensConcluidas)
                    {
                        Console.WriteLine("Produto: " + ordem.Produto);
                        Console.WriteLine("Quantidade: " + ordem.Quantidade);
                        Console.WriteLine("Data/Hora: " + ordem.DataHora);
                        Console.WriteLine("==============================");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhuma Ordem Concluída");
                }
                Console.WriteLine("Pressione Enter para Prosseguir...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Não há Ordens de Produção Pendentes ou Concluídas para Montar o Relatório");
                Console.WriteLine("Pressione Enter para Prosseguir...");
                Console.ReadLine();
                Console.Clear();
            }

        }
        static void EntradaNoEstoque()
        {
            //VARIÁVEIS
            string opcao = "";


       
  
            Console.WriteLine("Entrada Padrão de Teste(5un)");
            Console.ReadLine();
            Ordem ordem = new Ordem();

            //ENTRADA PADRAO DE TESTE
            Material materialPao = new Material();
            materialPao.Nome = "Pão";
            materialPao.Quantidade = 5;
            materiais.EntradaDeMateriais(materialPao);

            Material materialQueijo = new Material();
            materialQueijo.Nome = "Queijo";
            materialQueijo.Quantidade = 5;
            materiais.EntradaDeMateriais(materialQueijo);

            Material materialHamburguer = new Material();
            materialHamburguer.Nome = "Hambúrguer";
            materialHamburguer.Quantidade = 5;
            materiais.EntradaDeMateriais(materialHamburguer);

            Console.Clear();
               
            
        }
        static void ConcluirOrdemDeProducao()
        {
            //VARIÁVEIS
            string opcao = "";
            List<Ordem> ordens = ordensDeProducao.ListarOrdensDeProducao();

            //VERIFICANDO SE A LISTA NÃO ESTÁ VAZIA
            if (ordens.Count > 0)
            {
                do
                {
                    int opcaoInternaInt = -1;
                    Console.WriteLine("Qual ID Deseja Concluir?");
                    string opcaoInterna = Console.ReadLine();
                    if (Regex.IsMatch(opcaoInterna, @"\d"))
                    {
                        opcaoInternaInt = Int32.Parse(opcaoInterna);
                    }
                    if (opcaoInternaInt != -1 && opcaoInternaInt != 0)
                    {
                        if(opcaoInternaInt <= ordensDeProducao.ListarOrdensDeProducao().Count)
                        {
                            ordensDeProducao.ConcluirOrdemDeProducao(opcaoInternaInt);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Erro: A Lista de Ordens não Chega Nesse ID");
                            Console.ReadLine();
                        }
                        
                    }
                    Console.Clear();
                } while (opcao != "");
            }
        }
        static void RegistrarNovaOrdemDeProducao()
        {
            //VARIÁVEIS
            string opcao = "";
            string produto = "";
            int quantidade = 0;
            string dataHoraDeEntrega = "";

            //SELECIONAR PRODUTO
            do
            {
                int opcaoInternaInt = -1;
                Console.WriteLine("Registrar Nova Ordem de Produção");
                Console.WriteLine(MostrarProdutos());
                string opcaoInterna = Console.ReadLine();

                if (Regex.IsMatch(opcaoInterna, @"\d"))
                {
                    opcaoInternaInt = Int32.Parse(opcaoInterna);
                }                  
            
                if(opcaoInternaInt < 7 && opcaoInternaInt > 0)
                {
                    opcao = opcaoInterna.ToString();
                    produto = SelecionarProdutoPorId(opcaoInternaInt);
                    Console.Clear();

                   
                   
                }
                else 
                {
                    Console.Clear();
                }
            } while (opcao == "");
            opcao = "";

            //SELECIONAR QUANTIDADE
            do
            {
                int opcaoInternaInt = -1;
                Console.WriteLine("Registrar Nova Ordem de Produção");
                Console.WriteLine("Qual a Quantidade?");
                string opcaoInterna = Console.ReadLine();

                if (Regex.IsMatch(opcaoInterna, @"\d"))
                {
                    opcaoInternaInt = Int32.Parse(opcaoInterna);
                    opcao = opcaoInterna.ToString();
                }

                quantidade = opcaoInternaInt;
                    
                Console.Clear();
                
            } while (opcao == "");
            opcao = "";

            //SELECIONAR DATA/HORA
            do
            {
                int opcaoInternaInt = -1;
                Console.WriteLine("Registrar Nova Ordem de Produção");
                Console.WriteLine(MostrarDataHoraOpcoes());
                string opcaoInterna = Console.ReadLine();

                if (Regex.IsMatch(opcaoInterna, @"\d"))
                {
                    opcaoInternaInt = Int32.Parse(opcaoInterna);
                }

                if (opcaoInternaInt < 4 && opcaoInternaInt > 0)
                {
                    opcao = opcaoInterna.ToString();

                    dataHoraDeEntrega = SelecionarDataHoraPorId(opcaoInternaInt);
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }

            } while (opcao == "");

            
            var teste = materiais.ListarMateriais().Count;

            if (teste >= 3)
            {
                materiais.RemoverMaterial("Pão");
                materiais.RemoverMaterial("Queijo");
                materiais.RemoverMaterial("Hambúrguer");
                //ADICIONANDO A ORDEM À LISTA

                Ordem ordem = new Ordem();
                ordem.Produto = produto;
                ordem.Quantidade = quantidade;
                ordem.DataHora = dataHoraDeEntrega;

                ordensDeProducao.RegistrarNovaOrdemDeProducao(ordem);

                Console.WriteLine("Produto: " + produto);
                Console.WriteLine("Quantidade: " + quantidade + "x");
                Console.WriteLine("Data/Hora de Entrega: " + dataHoraDeEntrega);
                Console.WriteLine("Pressione Enter Para Prosseguir...");

                Console.ReadLine();

                Console.Clear();

            }
            else
            {
                Console.WriteLine("Não há materiais o suficiente.");
                Console.ReadLine();
                Console.Clear();
            }

            
        }

        static string MostrarHeader()
        {
            return "Fast Burguer v0.1";
        }
        static string MostrarMenu()
        {
            string menu = "Escolha uma opção:\n" +
                            "1 - Registrar Nova Ordem de Produção\n" +
                            "2 - Concluir Ordem de Produção\n" +
                            "3 - Registrar Entrada de Materiais\n" +
                            "4 - Listar Materiais em Estoque\n" +
                            "5 - Relatório de Produção\n" +
                            "6 - Sair do Programa\n";
            return menu;
        }
        static string LerOpcaoMenu()
        {
            string opcao;
            Console.Write("Opção desejada: ");
            opcao = Console.ReadLine();
            Console.Clear();
            return opcao;
        }
        static string MostrarProdutos()
        {
            string menu = "Escolha o Produto:\n" +
                            "1 - Hambúrguer Picanha\n" +
                            "2 - Hambúrguer Bacon\n" +
                            "3 - Hambúrguer Egg\n" +
                            "4 - Batata Frita\n" +
                            "5 - Coca-Cola\n" +
                            "6 - Guaraná\n";
            return menu;
        }
        static string MostrarDataHoraOpcoes()
        {
            string dataAtual = DateTime.Now.ToString("HH:mm");
            string menu = "Data/Hora Atual: " + dataAtual + "\n" +
                            "Escolha a Opção de Saída:\n" +
                            "1 - 10 Minutos\n" +
                            "2 - 30 Minutos\n" +
                            "3 - 1 Hora\n";
            return menu;
        }
        
        //public List<OrdemDeProducao> LerListaOrdensDeProducao()
        //{

        //}
        class OrdemDeProducao
        {
            public string Produto { get; set; }
            public int Quantidade { get; set; }
            public DateTime DataHora { get; set; }
        }
    }

    
}
