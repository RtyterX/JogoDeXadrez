using tabuleiro;

namespace pecas
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor)
            : base(tab, cor)
        {
        }
        public override string ToString()
        {
            return "P";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] matriz = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);



            return matriz;
        }

    }
}