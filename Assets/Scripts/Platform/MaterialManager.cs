using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    protected static MaterialManager instance;
    public static MaterialManager Instance { get =>  instance; }

    [SerializeField] protected GameObject[] _cube;


    private void Awake()
    {
        instance = this;
    }
    public virtual void TurnOnEmisson()
    {
        foreach (var cube in _cube)
        {
            cube.GetComponent<Material>().EnableKeyword("_EMISSION");
        }
        
    }
}
