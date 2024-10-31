// See https://aka.ms/new-console-template for more information
public abstract class Player
{
    public string name {get; private set;}
    
    public Hand hand = new Hand();

    //KRAV 3:
    //1: Bridge pattern
    //2: Vi använder bridge pattern genom att abstrakta klassn Player har något av interfacet ICardChooser som implementerar olika strategier för val av kort.
    // Player har två konkreta klasser, HumanPlayer och ComputerPlayer och ICardChooser har tre konkreta klasser, InteractiveCardChooser, SmartCardChooser och StupidCardChooser.
    //3: Varför vi valde att skapa ett bridge pattern här är för att en HumanPlayer och och ComnputerPlayer kan representeras olika beroende på vilken
    // CardChooser de har. CompyerPlayer och HumanPlayer skiljer sig i hur de visar korten samt att HumanPlayer får hintar på vilket kort som bör spelas vilket inte
    // computerPlayer behöver. Denna implementation möjliggör att vidareutveckla och lägga t.ex IntelligentCardChooser utan att behöva ändra något
    // i Player klassen. Vi kan dels variera vilken CarcChooser användaren väljer men också variera i vilken Player som en CardChooser används. 

    public ICardChooser cardChooser;
    
    public Player(string name, Deck deck, ICardChooser cardChooser)
    {
      this.name = name;
      this.cardChooser = cardChooser;
      hand.GenerateHand(deck);
    }
   
      public abstract void ShowHand();

}