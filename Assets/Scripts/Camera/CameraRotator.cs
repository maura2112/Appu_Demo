using UnityEngine;
using DG.Tweening;

public class CameraRotator : MonoBehaviour
{
    protected static CameraRotator instance;
    public static CameraRotator Instance { get => instance; }

    [SerializeField] protected float Duration = 1f;

    [SerializeField] protected bool _onRotate = false;

    /**************************************************/

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        this.CheckKeyboardInputs();
    }

    #region CheckKeyboardInputs

    private void CheckKeyboardInputs()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.Rotate(RotateDirection.Left);

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            this.Rotate(RotateDirection.Right);

        }
    }

    #endregion

    #region Rotate

    public virtual void Rotate(RotateDirection direction)
    {
        if (this._onRotate)
        {
            return;
        }

        this._onRotate = true;
        PlayerController.Instance.RePosition();
        PlayerController.Instance.Disable();

        Vector3 target = this.gameObject.transform.rotation.eulerAngles;

        switch (direction)
        {
            case RotateDirection.Left:
                target = new Vector3(0, target.y + 90, 0);
                break;
            case RotateDirection.Right:
                target = new Vector3(0, target.y - 90, 0);
                break;
        }

        this.gameObject.transform.DORotate(target, this.Duration)
            .OnComplete(() => this.OnRotateComplete());
    }

    public virtual void OnRotateComplete()
    {
        PlatformManager.Instance.RePosition();
        PlayerController.Instance.Enable();
        this._onRotate = false;
    }


    #endregion

}