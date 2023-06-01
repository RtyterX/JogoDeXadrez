using System;
using tabuleiro;
using pecas;

namespace JogoDeXadrez
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            //Imprime as Linhas
            for (int l = 0; l < tab.linhas; l++)
            {
                // Imprime as Colunas  
                for (int c = 0; c < tab.colunas; c++)
                {
                    //Se a Peca for nula, Tracos para espaços vazios
                    if (tab.peca(l, c) == null)
                    {
                        Console.Write("- ");
                    }
                    else // Caso contrario Imprime a Peça
                    {
                        Console.Write(tab.peca(l, c) + " ");
                    }
                }
                // Sem o WriteLine fica tudo numa linha só, ele que cria as colunas
                Console.WriteLine();
            }
        }

    }
}
