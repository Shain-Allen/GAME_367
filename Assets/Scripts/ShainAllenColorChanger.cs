using UnityEngine;

public class ShainAllenColorChanger : MonoBehaviour
{
    //Serialized Fields to allow assignment of materials in inspector while keeping variables private
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;
    [SerializeField] private Material material3;

    //Cached reference to objects render component
    private Renderer _objectRender;

    // Start is called before the first frame update
    private void Start()
    {
        //get reference to objects render and cache it in variable
        _objectRender = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _objectRender.material = material1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            _objectRender.material = material2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) _objectRender.material = material3;

        if (Input.anyKeyDown)
            //include the objects name and the name of the assigned material to keep things a bit more flexible
            Debug.Log($"{gameObject.name} has had its material changed to {_objectRender.material.name}");
    }
}