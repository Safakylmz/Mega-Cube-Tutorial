using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    Cube cube;
    private void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();

        //check if contacted with other cube
        if(otherCube != null && cube.cubeID > otherCube.cubeID)
        {
            if (cube.cubeNumber == otherCube.cubeNumber) //if both cubes have same number.
            {
                Vector3 contactPoint = collision.contacts[0].point;


                //check if cubes number less than max number in CubeSpawner;
                if (otherCube.cubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.cubeNumber * 2, contactPoint + Vector3.up * 1.6f); //spawn new cube.
                    float pushForce = 2.5f;
                    newCube.cubeRigidbody.AddForce(new Vector3(0, 0.3f, 1f) * pushForce, ForceMode.Impulse); //push the new cube up and forward.

                    // add some torque
                    float randomValue = Random.Range(-20f, 20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.cubeRigidbody.AddTorque(randomDirection);
                }


                //the explosion should effect surrounded cubes too
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;

                foreach (Collider coll in surroundedCubes)
                {
                    if (coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                }

                FX.instance.PlayCubeExplosionFX(contactPoint, cube.cubeColor); //FX.

                // Destroy two cubes.
                CubeSpawner.Instance.DestroyCube(cube);
                CubeSpawner.Instance.DestroyCube(otherCube);
            }
        }
    }
}
