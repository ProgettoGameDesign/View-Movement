using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _interactRange = 1; // raggio di interazione
    [SerializeField] private LayerMask _interactableMask; // tutti gli oggetti con cui puoi interagire hanno lo stesso layer

    private Collider[] _colliders = new Collider[2]; // può avere un massimo di 2 oggetti interagibili alla volta

    private Transform[] allTransforms;

    private void Awake()
    {
        allTransforms = Resources.FindObjectsOfTypeAll<Transform>(); // lista di tutti i gameObject con una transform

    }

    
    private void Update()
    {
        if (Physics.OverlapSphereNonAlloc(_interactPoint.position, _interactRange, _colliders, _interactableMask) != 0)
        {
            //Debug.Log("Questo oggetto è interagibile");
            var interactable = _colliders[0].GetComponent<InteractInterface>(); // acquisisco l'interfaccia dell'oggetto
            string _tagToSearch = interactable.InteractionPrompt;
            if (interactable != null && Input.GetKeyDown(KeyCode.E)) {
                interactable.Interact(this); // richiamo il metodo Interact
                if (_tagToSearch != "Button") {
                    Debug.Log("ha un panel associato");
                    PanelActivation(_tagToSearch);
                    }
            }
           
        }

    } 
    private void PanelActivation(string _tagToSearch)
    { 
        // il problema di questo script è che posso attivare/disattivare il panel 
        // solo in prossimità dell'oggetto interagibile
        // vorrei poterlo attivare e disattivare una volta sola
        // o addirittura disattivarlo dopo un tot di tempo
        foreach (Transform t in allTransforms)
        {
            if (t.CompareTag(_tagToSearch))
            {
                t.gameObject.SetActive(!t.gameObject.activeSelf);
            }
        }
    }
}
    
