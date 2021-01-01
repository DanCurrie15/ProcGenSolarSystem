using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemGenerator : MonoBehaviour
{
    public int numPlanets;

    public GameObject sun;
    public GameObject celestialBody;

    public Transform solarSystem;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newSun = Instantiate(sun);
        newSun.transform.parent = solarSystem;
        
        for (int i = 1; i <= numPlanets; i++)
        {
            GameObject newPlanet = Instantiate(celestialBody);
            newPlanet.transform.parent = solarSystem;
            newPlanet.transform.position = new Vector3(RandomPosOrNeg() * 80f * i, RandomPosOrNeg() * 80f * i, 0f);
            newPlanet.GetComponent<CelestialBody>().radius = Random.Range(5f, 10f);
            newPlanet.GetComponent<CelestialBody>().surfaceGravity = 5f;
            newPlanet.GetComponent<CelestialBody>().initialVelocity = GetOrbitVelocity(newPlanet.GetComponent<CelestialBody>().mass, newPlanet, newSun);
        }
    }

    private int RandomPosOrNeg()
    {
        return Random.Range(0, 2) * 2 - 1;
    }

    private Vector3 GetOrbitVelocity(float mass, GameObject satellite, GameObject toOrbit)
    {
        float velocity = Mathf.Sqrt(Universe.gravitationalConstant * satellite.GetComponent<CelestialBody>().mass / Vector3.Distance(satellite.transform.position, toOrbit.transform.position));
        Vector3 radialVector = (toOrbit.transform.position + satellite.transform.position).normalized;
        return new Vector3(radialVector.y * velocity, -radialVector.x * velocity, radialVector.z *velocity);
    }
}
