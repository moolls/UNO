public class CardStrategy
{
    private Deck deck;
    private Hand nextPlayerHand;
    private PlayedCards playedCards;

    public CardStrategy(Deck deck, Hand nextPlayerHand, PlayedCards playedCards)
    {
        this.deck = deck;
        this.nextPlayerHand = nextPlayerHand;
        this.playedCards = playedCards;
    }

    public void ApplySpecialEffect(IUnoCard lastCard, ref bool skipTurn)
    {
        switch (lastCard.GetValue())
        {
            case CardValue.plusFour:
                PlusFour();
                break;
            case CardValue.plusTwo:
                PlusTwo();
                break;
            case CardValue.Block:
                Block();
                skipTurn = true;
                break;
        }
    }

    private void PlusFour()
    {
        Console.WriteLine("Special card +4 activated! Next player draws four cards.");
        for (int i = 0; i < 4; i++)
        {
            nextPlayerHand.AddCardToHand(deck.DrawCard());
        }
    }

    private void PlusTwo()
    {
        Console.WriteLine("Special card +2 activated! Next player draws two cards.");
        for (int i = 0; i < 2; i++)
        {
            nextPlayerHand.AddCardToHand(deck.DrawCard());
        }
    }

    private void Block()
    {
        Console.WriteLine("Block card activated! The next player's turn is skipped.");
    }
}