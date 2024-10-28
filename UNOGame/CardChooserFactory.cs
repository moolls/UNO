public class CardChooserFactory
{


    //CardChooserFactory tar emot en interactive cardChooser (tilldelar till människa och sedan en ComputerCardChooserFactory??
    //ComputerCardChooserFactory retunrerar vilken strategy som anvädnaren väljer att runtime?
    public ICardChooser CreateCardChooser(string choice, Deck deck, PlayedCards playedCards)
    {
        return choice switch
        {
            "1" => new StupidCardChooser(deck, playedCards), 
            "2" => new SmartCardChooser(deck, playedCards),
            _ => throw new ArgumentException("Invalid choice")
        };
    }

}