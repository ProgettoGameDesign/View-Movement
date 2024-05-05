using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _interactRange = 1; // raggio di interazione
    [SerializeField] private LayerMask _interactableMask; // tutti gli oggetti con cui puoi interagire hanno lo stesso layer

    private Collider[] _colliders = new Collider[2]; // può avere un massimo di 2 oggetti interagibili alla volta

    private void Update()
    {
        if (Physics.OverlapSphereNonAlloc(_interactPoint.position, _interactRange, _colliders, _interactableMask) != 0)
        {
            //Debug.Log("Questo oggetto è interagibile");
            var interactable = _colliders[0].GetComponent<InteractInterface>(); // acquisisco l'interfaccia dell'oggetto
            if (interactable != null && Input.GetKeyDown(KeyCode.E)) interactable.Interact(this); // richiamo il metodo Interact

        }

    } 
}
