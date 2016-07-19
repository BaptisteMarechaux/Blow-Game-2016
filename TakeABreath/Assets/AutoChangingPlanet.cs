using UnityEngine;
using System.Collections;

public class AutoChangingPlanet : MonoBehaviour {
    public SimpleShpereGeneration sphereGenerator;

    public bool canChange = true;
    // Use this for initialization
    void Start () {
        StartCoroutine("PlanetChange");
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.A))
        {
            canChange = !canChange;
        }
	} 

    IEnumerator PlanetChange()
    {   
        while(!canChange)
        {
            sphereGenerator.offset.x += 1;
            sphereGenerator.OnValidate();
            yield return new WaitForFixedUpdate();
        }
        yield return null;
       
    }
}
