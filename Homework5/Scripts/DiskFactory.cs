using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour {

    Color[] color = { Color.red, Color.black, Color.yellow, Color.white };

    public GameObject prefab;

    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

    private int setColor()
    {
        int i = UnityEngine.Random.Range(0, 4);
        while (i < 0 || i >= 4)
        {
            i = UnityEngine.Random.Range(0, 4);
        }
        return i;
    }

    private void Awake()
    {
        prefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("disk"), Vector3.zero, Quaternion.identity);
        prefab.SetActive(false);
    }

    public GameObject GetDisk(int round)
    {
        GameObject disk = null;
        if (free.Count > 0)
        {
            disk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            disk = GameObject.Instantiate<GameObject>(prefab);
            disk.AddComponent<DiskData>();
        }

        if (round == 0)
        {
            disk.GetComponent<DiskData>().speed = 2f;
            float dx = UnityEngine.Random.Range(-1f, 1f);
            disk.GetComponent<DiskData>().direction = new Vector3(dx, 1, 0);

            int index = setColor();
            disk.GetComponent<Renderer>().material.color = color[ index ] ;
            disk.GetComponent<DiskData>().score += index;
        }else if (round == 1)
        {
            disk.GetComponent<DiskData>().speed = 4f;
            float dx = UnityEngine.Random.Range(-1f, 1f);
            disk.GetComponent<DiskData>().direction = new Vector3(dx, 1, 0);

            int index = setColor();
            disk.GetComponent<Renderer>().material.color = color[index];
            disk.GetComponent<DiskData>().score += (index*2);
        }else if(round == 2)
        {
            disk.GetComponent<DiskData>().speed = 6f;
            float dx = UnityEngine.Random.Range(-1f, 1f);
            disk.GetComponent<DiskData>().direction = new Vector3(dx, 1, 0);

            int index = setColor();
            disk.GetComponent<Renderer>().material.color = color[index];
            disk.GetComponent<DiskData>().score += (index*3);
        }

        used.Add(disk.GetComponent<DiskData>());
        disk.SetActive(true);

        return disk;

    }

    public void FreeDisk(GameObject disk)
    {
        DiskData tmp = null;
        for(int i = 0; i < used.Count; i++)
        {
            if (disk.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                tmp = used[i];
            }
        }
        if (tmp != null)
        {
            tmp.gameObject.SetActive(false);
            used.Remove(tmp);
            free.Add(tmp);
        }
    }

}
