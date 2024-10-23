// See https://aka.ms/new-console-template for more information

public class ComputerPlayer : Player
{
    ICardChooser cardChooser;

     public ComputerPlayer(Deck deck) : base("Robotic", deck){
    }

}
