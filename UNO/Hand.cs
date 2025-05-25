
using System;
using System.Collections;
using System.Collections.Generic;

// KRAV 4:
//1: Iterator pattern

//2: Hand-klassen utgör den itererbara typen i vår kod genom att den implementerar IEnumerable<IUnoCard>, då det är objekt av typen hand som vi 
//   vill kunna utföra oändlig iteration över. Logiken för själva itereringen, alltså den itererande typen är klassen
//   InfiniteIterator och därmed separat från den itererbara typen Hand.

//3: Vi implementerade ett iterator pattern i form av 
//   oändlig iteration då vi ville att vår iterator automatiskt skulle börja om från början när den når 
//   slutet av en ändlig mängd (i vårt fall handen med kort). Att separera logiken för att bläddra 
//   mellan korten på handen från hand-objektet i sig gör även att vår kod blir enklare att underhålla samt förändra.

public class Hand : IEnumerable<IUnoCard>
{
    private List<IUnoCard> hand;
    int indexCurrentCard;
    public Hand()
    {
        this.hand = new List<IUnoCard>();
        indexCurrentCard = 0;
    }

    public IUnoCard ReturnCard()
    {
        return hand[0];
    }

    public void GenerateHand(Deck deck)
    {
        for (int i = 0; i < 7; i++)
            hand.Add(deck.DrawCard());
    }

    public void AddCardToHand(IUnoCard unoCard)
    {
        hand.Add(unoCard);
    }


    public void RemoveCard(IUnoCard card)
    {
        hand.Remove(card);
    }


    public void DrawCardToHand(Deck deck)
    {
        IUnoCard card = deck.DrawCard();
        AddCardToHand(card);

    }


 // KRAV 5
 // 1:LINQ

 // 2:LINQ används nedan för att sortera korten i handen på ett enkelt och effektivt sätt.
 //   Metoden sorterar korten först efter färg (CardColor) och sedan efter värde
 //   (CardValue) vilket är användbart för att på ett mer läsbart sätt presentera
 //   spelarens hand i konsolen. Det underlättar även för spelarens strategiska beslut
 //   och efterliknar hur en spelares hand ofta ser ut i verkligheten

 // 3: Vi nyttjar LINQ:s sorteringsoperationer OrderBy och ThenBy för att göra koden mer läsbar
 //    och kompakt jämfört med en manuell sorteringsalgoritm. Vi är medvetna om att den tillämpar immediate execution
 //    vilket kan vara problematiskt vid stor mängd data. I vårt fall kommer vi aldrig arbeta med stor mängd data eftersom
 //    antalet kort är begränsat. Det framgick heller inte i kravet att det vi måste använda LINQ med deferred execution 
 //    därför anser vi att vår LINQ metod är meningsfull.


    public void SortHand()
    {
        hand = hand.OrderBy(card => card.GetColor()).ThenBy(card => card.GetValue()).ToList();
    }


    public IUnoCard CurrentCard(int index)
    {
        return hand[index];
    }

    public int Count()
    {
        return hand.Count();
    }

    public bool checkEmpty()
    {
        if (hand.Count() == 0)
        {
            return true;
        }
        else return false;

    }

    public IEnumerator<IUnoCard> GetEnumerator()
    {
        return hand.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
