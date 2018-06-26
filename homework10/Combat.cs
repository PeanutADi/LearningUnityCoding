using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {

	public const int maxHealth = 100;
	public bool destroyOnDeath;
    public string type = "";

    void Update()
    {
        if(this.type == "enemy")
        {
            float r = Random.Range(-5.0f, 3.0f);
            float r2 = Random.Range(-5.0f, 3.0f);
            if (this.transform.position.x < 8.0f && this.transform.position.z < 20.0f
                && this.transform.position.x > -8.0f && this.transform.position.z > -20.0f) this.transform.position += new Vector3(Time.deltaTime * r, 0, Time.deltaTime * r2);
            if (this.transform.position.x >= 8.0f) this.transform.position += new Vector3(-Time.deltaTime * 2, 0, Time.deltaTime * r2);
            if (this.transform.position.x <= -8f) this.transform.position += new Vector3(Time.deltaTime * 2, 0, Time.deltaTime * r2);
            if (this.transform.position.z <= -20f) this.transform.position += new Vector3(Time.deltaTime * r, 0, Time.deltaTime * 2);
            if (this.transform.position.z >= 20f) this.transform.position += new Vector3(Time.deltaTime * r, 0, Time.deltaTime * -2);

        }
    }



    [SyncVar]
	public int health = maxHealth;

	public void TakeDamage(int amount)
	{
        Debug.Log(destroyOnDeath);
		if (!isServer)
			return;
		
		health -= amount;
		Debug.Log("health value = " + health.ToString());
		if (health <= 0)
		{
			if (destroyOnDeath)
			{
				print("Destory");
				Destroy(gameObject);
			}
			else
			{
				health = maxHealth;

				// called on the server, will be invoked on the clients
				RpcRespawn();
			}
		}
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// move back to zero location
			transform.position = Vector3.zero;
		}
	}
}
