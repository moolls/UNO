
using System;
using System.Collections;
using System.Collections.Generic;

public class Hand : IEnumerable<IUnoCard>
{
    private List<IUnoCard> hand;
    int indexCurrentCard;
    int drawCounter = 0;

    public Hand()
    {
        this.hand = new List<IUnoCard>();
        indexCurrentCard = 0;
    }

    // en metod som returnerar hand så vi slipper hämta humanplayer.hand exempelvis

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
        return hand.GetEnumerator(); // Returnerar en iterator för listan
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); // Krävs för den icke-generiska IEnumerable
    }

}




