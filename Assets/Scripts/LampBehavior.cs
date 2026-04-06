using System.Collections.Generic;
using UnityEngine;

public class LampBehavior : MonoBehaviour
{
    private Dictionary<Material, float> startAlphas = new Dictionary<Material, float>();
    private Renderer[] renderers;
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            if (!startAlphas.ContainsKey(rend.material) && rend.material.HasProperty("_Color"))
            {
                startAlphas[rend.material] = rend.material.color.a;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        SetAlpha(0.5f);
    }
    void OnTriggerExit(Collider other)
    {
        RestoreAlpha();
    }
    void SetAlpha(float alpha)
    {
        foreach (Renderer rend in renderers)
        {
            if (rend.material.HasProperty("_Color"))
            {
                Color color = rend.material.color;
                color.a = alpha;
                rend.material.color = color;
            }
        }
    }
    void RestoreAlpha()
    {
        foreach (Renderer rend in renderers)
        {
            if (rend.material.HasProperty("_Color") && startAlphas.ContainsKey(rend.material))
            {
                Color color = rend.material.color;
                color.a = startAlphas[rend.material];
                rend.material.color = color;
            }
        }
    }
}
