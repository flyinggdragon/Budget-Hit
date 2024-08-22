using System.Collections.Generic;

public class ElementalReaction {
    public Element currentElement { get; private set; }
    public Element reactingWith { get; private set; }
    public Reaction reaction { get; private set; }
    //public int damage { get; set; }
    
    public ElementalReaction(Element current, Element reacting) {
        currentElement = current;
        reactingWith = reacting;

        reaction = DetermineReaction(currentElement, reactingWith);
    }

    private Reaction DetermineReaction(Element currentElement, Element reactingWith) {
        if (reactionMap.TryGetValue((currentElement, reactingWith), out Reaction foundReaction)) {
            return foundReaction;
        } else if (reactionMap.TryGetValue((reactingWith, currentElement), out Reaction reverseReaction)) {
            return reverseReaction;
        }

        return Reaction.None;
    }

    private Dictionary<(Element, Element), Reaction> reactionMap = new Dictionary<(Element, Element), Reaction> {
        {(Element.Pyro, Element.Dendro), Reaction.Burn},
        {(Element.Pyro, Element.Cryo), Reaction.Melt},
        {(Element.Pyro, Element.Hydro), Reaction.Vaporize},
        {(Element.Pyro, Element.Electro), Reaction.Explode},
        {(Element.Pyro, Element.Scotos), Reaction.Darken},
        {(Element.Dendro, Element.Pyro), Reaction.Burn},
        {(Element.Dendro, Element.Electro), Reaction.Activate},
        {(Element.Dendro, Element.Hydro), Reaction.Bloom},
        {(Element.Dendro, Element.Scotos), Reaction.Darken},
        {(Element.Cryo, Element.Pyro), Reaction.Melt},
        {(Element.Cryo, Element.Electro), Reaction.Concentrate},
        {(Element.Cryo, Element.Hydro), Reaction.Freeze},
        {(Element.Cryo, Element.Scotos), Reaction.Darken},
        {(Element.Electro, Element.Pyro), Reaction.Explode},
        {(Element.Electro, Element.Dendro), Reaction.Activate},
        {(Element.Electro, Element.Hydro), Reaction.Electrocute},
        {(Element.Electro, Element.Cryo), Reaction.Concentrate},
        {(Element.Electro, Element.Scotos), Reaction.Darken},
        {(Element.Hydro, Element.Pyro), Reaction.Vaporize},
        {(Element.Hydro, Element.Dendro), Reaction.Bloom},
        {(Element.Hydro, Element.Electro), Reaction.Electrocute},
        {(Element.Hydro, Element.Cryo), Reaction.Freeze},
        {(Element.Hydro, Element.Scotos), Reaction.Darken},
        {(Element.Scotos, Element.Pyro), Reaction.Darken},
        {(Element.Scotos, Element.Dendro), Reaction.Darken},
        {(Element.Scotos, Element.Cryo), Reaction.Darken},
        {(Element.Scotos, Element.Electro), Reaction.Darken},
        {(Element.Scotos, Element.Hydro), Reaction.Darken}
    };
}