public class GameHistoryEntry
{
public string Player1 {get; set;}
public string Player2 {get; set;}
public string Winner {get; set;}
public string GameDuration {get; set;}

public GameHistoryEntry(string player1, string player2, string winner, string gameDuration)
    {
        Player1 = player1;
        Player2 = player2;
        Winner = winner;
        GameDuration = gameDuration;

    }

}