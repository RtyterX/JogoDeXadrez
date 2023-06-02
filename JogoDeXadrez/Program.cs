﻿using System;
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
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Dama(tab, Cor.Preto), new Posicao(0, 3));
                tab.colocarPeca(new Torre(tab, Cor.Preto), new Posicao(1, 1));
                tab.colocarPeca(new Cavalo(tab, Cor.Preto), new Posicao(2, 7));
                tab.colocarPeca(new Rei(tab, Cor.Branco), new Posicao(3, 5));
                tab.colocarPeca(new Peao(tab, Cor.Branco), new Posicao(6,7));

                Tela.imprimirTabuleiro(tab);

            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }
}
