using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// struttura generale di un oggetto con cui puoi interagire
public interface InteractInterface
{
    public string InteractionPrompt {get;}
    public bool Interact(Interactor interactor);
    

}
