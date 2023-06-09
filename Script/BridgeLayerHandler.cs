using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeLayerHandler : MonoBehaviour
{
    List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

    List<Collider2D> overpassColliderList = new List<Collider2D>();
    List<Collider2D> underpassColliderList = new List<Collider2D>();

    Collider2D userCollider;

    bool isOnBridge = false;
    void Awake()
    {
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.sortingLayerName == "Default")
                defaultLayerSpriteRenderers.Add(spriteRenderer);
        }

        foreach (GameObject overpassColliderGameObject in GameObject.FindGameObjectsWithTag("overpassCollider"))
        {
            overpassColliderList.Add(overpassColliderGameObject.GetComponent<Collider2D>());
        }

        foreach (GameObject underpassColliderGameObject in GameObject.FindGameObjectsWithTag("underpassCollider"))
        {
            underpassColliderList.Add(underpassColliderGameObject.GetComponent<Collider2D>());
        }

        userCollider = GetComponentInChildren<Collider2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateSortingAndCollisionLayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateSortingAndCollisionLayers()
    {
        if (isOnBridge)
        {
            SetSortingLayer("OnBridge");
        }
        else
        {
            SetSortingLayer("Default");
        }

        SetCollisionWithOverPass();

    }

    void SetCollisionWithOverPass()
    {
        foreach (Collider2D collider2D in overpassColliderList)
        {
            Physics2D.IgnoreCollision(userCollider, collider2D, !isOnBridge);
        }

        foreach (Collider2D collider2D in underpassColliderList)
        {
            if (isOnBridge)
                Physics2D.IgnoreCollision(userCollider, collider2D, true);
            else Physics2D.IgnoreCollision(userCollider, collider2D, false);
        }

    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }
    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("underpassTrigger"))
        {
            isOnBridge = false;

            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("overpassTrigger"))
        {
            isOnBridge = true;

            UpdateSortingAndCollisionLayers();
        }    
    }

}
