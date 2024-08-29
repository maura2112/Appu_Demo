using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    protected static LookAtCamera instance;
    public static LookAtCamera Instance { get => instance;  }

    [SerializeField]
    protected GameObject _cameraContainer;

    /**************************************************/

    private void Awake()
    {
        instance = this;
    }
    private void Update() {
        this.RotateAndLook();
    }
    
    public virtual void RotateAndLook()
    {
        this.gameObject.transform.rotation = _cameraContainer.transform.rotation;
    }

}