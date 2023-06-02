using tabuleiro;

namespace pecas
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

           // Converte para a Matriz
        public Posicao convertePosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString()
        {
            return ""
                + coluna
                + linha;
        }

    }
}
