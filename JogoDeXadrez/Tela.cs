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
                Console.Write(8 - l + " ");

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
                        imprimirPeca(tab.peca(l,c));
                        Console.Write(" ");
                    }
                }
                // Sem o WriteLine fica tudo numa linha só, ele que cria as colunas
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }

        }

    }
}
