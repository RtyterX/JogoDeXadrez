using tabuleiro;
using System.Collections.Generic;

namespace pecas
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool xeque { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual= Cor.Branco;
            terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retiraPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retiraPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retiraPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        public void Jogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("VOcê não pode se Colocar em Xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void MudaJogador()
        {
            if(jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Preto;
            }

            else
            {
                jogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Capturadas)
            {
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }

            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in Capturadas)
            {
                if (p.cor == cor)
                {
                    aux.Add(p);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca p in PecasEmJogo(cor))
            {
                bool[,] matriz = p.movimentosPossiveis();
                for (int l = 0; l < tab.linhas; l++)
                {
                    for (int c = 0; c < tab.colunas; c++)
                    {
                        if (matriz[l,c])
                        {
                            Posicao origem = p.posicao;
                            Posicao destino = new Posicao(l, c);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }                
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).convertePosicao());
            Pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branco));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branco)); 
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branco));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branco, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branco, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preto));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preto));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preto));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preto));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preto));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preto));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preto));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preto));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preto, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preto, this));
        }

    }
}
