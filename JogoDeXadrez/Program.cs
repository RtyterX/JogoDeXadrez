using System;
using tabuleiro;
using pecas;

namespace JogoDeXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tab);

                    try
                    {
                        Console.WriteLine();
                        Console.WriteLine("Turno - " + partida.turno);
                        Console.WriteLine("Aguardando Jogada... ");
                        Console.WriteLine("Jogador: " + partida.jogadorAtual);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().convertePosicao();
                        partida.validarPosicaoDeOrigem(origem);

                        bool[,] posicaoPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicaoPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().convertePosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.Jogada(origem, destino);
                    }

                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
            }

            catch (TabuleiroException e)
            {
                 Console.WriteLine(e.Message);
            }
           
            Console.ReadLine();

        }
    }
}
