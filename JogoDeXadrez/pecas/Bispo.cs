using tabuleiro;

namespace pecas
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tab, Cor cor) 
            : base(tab, cor)
        {
        }
        public override string ToString()
        {
            return "B";
        }

    }
}
