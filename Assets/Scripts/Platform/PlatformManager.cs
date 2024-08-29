
using System.Collections.Generic;
using UnityEngine;


public class PlatformManager : MonoBehaviour
{
    protected static PlatformManager instance;
    public static PlatformManager Instance { get => instance; }

    [SerializeField] protected List<Platform> _platforms;
    [SerializeField] protected List<Platform> _platformPref;

    [SerializeField] protected bool _isGenerate;


    /**************************************************/

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        this.RePosition();
    }

    private void Update()
    {
            if (!this._isGenerate)
            {
                this._isGenerate = true;
                this.GenerateRandomPlatform();
            }
    }

    public virtual void RePosition()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        GameObject[] allPlatforms = GameObject.FindGameObjectsWithTag("Platform");

        for (int i = 0; i < allPlatforms.Length; i++)
        {
            GameObject platform = allPlatforms[i];
            BoxCollider collider = platform.GetComponentInChildren<BoxCollider>();
            collider.center = Vector3.zero;
            //convert pos vec into world space
            Vector3 colliderPos = collider.transform.TransformPoint(collider.center);
            Vector3 newColliderPos;
            //move platform collider depending on what side the camera is facing 
            Vector3 normalCam = Camera.main.transform.position.normalized;
            if (Mathf.Abs(Mathf.Round(normalCam.x)) == 1.0f)
            {
                newColliderPos = new Vector3(playerPosition.x, colliderPos.y, colliderPos.z);
            }
            else
            {
                newColliderPos = new Vector3(colliderPos.x, colliderPos.y, playerPosition.z);
            }
            //converts back into local space
            newColliderPos = collider.transform.InverseTransformPoint(newColliderPos);

            collider.center = newColliderPos;
        }
    }

    public virtual int RandomYRotation()
    {
        float randomRotation = 0;
        int randomIndex = Random.Range(0, 2);
        Debug.Log(randomIndex);
        switch (randomIndex)
        {
            case 0:
                randomRotation = 0;
                break;
            case 1:
                randomRotation = 90;
                break;
        }
        return (int)randomRotation;
    }

    public virtual void GenerateRandomPlatform()
    {


        Transform _playerPosition = PlayerController.Instance.gameObject.transform;
        for (int i = 0; i <= 4; i++)
        {
            float x = Random.Range(-6, 6);
            float y = Random.Range(1, 3);
            float z = Random.Range(-6, 6);
            Platform _randomPlatformPref;
            Platform _newPlatform;
            int randomPrefIndex = Random.Range(0, _platformPref.Count);
            _randomPlatformPref = _platformPref[randomPrefIndex];
            if (_platforms.Count > 0)
            {
                Transform _lastestPlatform = this._platforms[this._platforms.Count - 1].transform;
                _newPlatform = Instantiate(_randomPlatformPref, new Vector3(_lastestPlatform.position.x + x, _lastestPlatform.position.y + y, _lastestPlatform.position.z + z), Quaternion.Euler(0f, this.RandomYRotation(), 0f));
            }
            else
            {
                _newPlatform = Instantiate(_randomPlatformPref, new Vector3(x-2, y-1, z-2), Quaternion.Euler(0f, this.RandomYRotation(), 0f));
            }

            this._platforms.Add(_newPlatform);
        }
        foreach (var platform in this._platforms)
        {
            this.RePosition();
        }
    }

    public virtual List<Platform> GetPlatforms()
    {
        return this._platforms;
    }






}