using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] protected bool _isPlayerOn;
    protected MeshRenderer _meshRenderer;

    private void Start()
    {
       _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public virtual void TurnOnEmission()
    {
        _meshRenderer.material.EnableKeyword("_EMISSION");
        Invoke("TurnOffEmission", 10f);
    }

    public virtual void TurnOffEmission()
    {
        _meshRenderer.material.DisableKeyword("_EMISSION");
    }

    //public virtual IEnumerator WaitingToTurnOnEmission() {
    //    this.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
    //    yield return 1f;
        
    //}

}
