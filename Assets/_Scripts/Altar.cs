﻿using System;
using UnityEngine;
using UnityEngine.VFX;

public class Altar : MonoBehaviour
{
    public Pickup[] sacrifice;
    [SerializeField] private AltarPiece[] pieces;

    [SerializeField] private MeshRenderer altarRenderer;
    [SerializeField] private Material activatedMaterial;

    [SerializeField] private VisualEffect VFX;
    [SerializeField] private string VFX_StartEvent;

    [SerializeField] private bool onlyActivateOnce = true;

    private int VFX_StartEventHash;
    
    [NonSerialized]
    public bool activated = false;

    void Awake()
    {
        if(altarRenderer == null)
            altarRenderer = GetComponentInChildren<MeshRenderer>();

        if(VFX_StartEvent != "")
            VFX_StartEventHash = Shader.PropertyToID(VFX_StartEvent);
    }

    public virtual void Activate()
    {
        if (!CanBeActivated()) return;

        Debug.Log($"Activating altar: {name}");

        activated = true;

        if(altarRenderer != null && activatedMaterial != null)
            altarRenderer.material = activatedMaterial;

        if(VFX != null)
            VFX.SendEvent(VFX_StartEventHash);

        foreach (var piece in pieces)
        {
            piece.Activate();
        }
    }

    public void ChangeMaterial()
    {
        altarRenderer.material = activatedMaterial;
    }
    
    public bool CanBeActivated()
    {
        if (!onlyActivateOnce) return true;
        return !activated;
    }
}
