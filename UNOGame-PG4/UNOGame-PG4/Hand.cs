
using System;
using System.Collections;
using System.Collections.Generic;

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

    //KRAV 5:
    //1: LINQ
    //2: Vi använder oss av LINQ för att förenkla algoritmen för att filtrera ut ett visst kort som ska tas bort från handen när man spelar ett kort.
    //3: Vi använder det oss av LINQ för att minska och förenkla koden som krävs för att göra denna operation. 
    public void RemoveCard(IUnoCard card)
    {
        hand = hand.Where(x => x != card).ToList();
    }


    public void DrawCardToHand(Deck deck)
    {
        IUnoCard card = deck.DrawCard();
        AddCardToHand(card);
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
    if(hand.Count() == 0)
    {
        return true;
    } else return false;
    
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




