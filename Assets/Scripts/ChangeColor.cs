using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : Interactable
{
    private MeshRenderer baseMesh = null;
    private MaterialPropertyBlock block = null;
    private MaterialPropertyBlock offBlock = null;

    private bool isRed = false;

    // Runs when object is created
    public void Start()
    {
        baseMesh = GetComponent<MeshRenderer>();

        /* 
         * Using baseMesh.material.color = Color.red is bad,
         * It wastes memory because Unity just creates a new
         * mesh of that color and assigns it to the object, 
         * rather than just changing the color itself.
         */
        block = new MaterialPropertyBlock();
        block.SetColor("_Color", new Color(1.0f, 0.0f, 0.0f));

        offBlock = new MaterialPropertyBlock();
        offBlock.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f));
    }

    // Runs when player interacts with object
    public override void OnInteract()
    {
        // same is if red, make white, else make red
        baseMesh.SetPropertyBlock(isRed ? offBlock : block);
        // boolean toggle
        isRed = !isRed;
    }
}
