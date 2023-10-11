using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using Dummiesman;

/** Change size of mesh by changing size of "box"'s box collider.
*/

public class OldMeshDownloader : MonoBehaviour
{
    private readonly string APIkey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJwdWJsaWNfaWQiOiI0NjdjMWExOS0xOWU4LTRlOTItYjc2ZC00ZmVjYWIwMWJjMjIifQ.WfCD9CvA7DAFUEobFlAmCHZX41jddclXDJgiBxCpqko";
    string url = "http://mesh.neuvue.io/download?source=graphene://https://minnie.microns-daf.com/segmentation/table/minnie3_v1&secrets={%22token%22:%20%227b140f3bff407e8f7a4f47a6966afa49%22}";
    public string seg_id;
    public bool loaded = false;
    public Material glowEffect;

    void Start()
    {
        Debug.Log(seg_id);
        StartCoroutine(testStart());
    }

    IEnumerator testStart() {
      yield return StartCoroutine("downloadMesh");
      if (loaded) {
          transformMesh(seg_id);
          UnityWebRequest readInfo = UnityWebRequest.Get(url);
          loaded = false;
      } else {
          Debug.Log("naur");
      }
    }

    IEnumerator downloadMesh() {
      url += "&seg_ids=" + seg_id;
      string meshDownloadPath = Path.Combine(Application.temporaryCachePath, seg_id + ".obj");
      UnityWebRequest DownloadRequest = UnityWebRequest.Get(url);
      DownloadRequest.SetRequestHeader("x-access-tokens", APIkey);
      DownloadRequest.downloadHandler = new DownloadHandlerFile(meshDownloadPath);

      yield return DownloadRequest.SendWebRequest();

      if (DownloadRequest.result != UnityWebRequest.Result.Success)
      {
          Debug.LogError(DownloadRequest.error);
      }
      else
      {
          GameObject loadedMesh = new OBJLoader().Load(meshDownloadPath);
          loaded = true;
      }
  }

    private void transformMesh(string id) {
    GameObject mesh = GameObject.Find(id);
    GameObject actual = GameObject.Find("default");
    GameObject dimensions = GameObject.Find("box");
    GameObject sphere = GameObject.Find("Sphere");
    actual.transform.parent = null;
    actual.AddComponent<Rigidbody>(); // adds a rigidbody to the mesh
    actual.AddComponent<BoxCollider>(); //adds a boxcollider to the mesh.
    Rigidbody rg = actual.GetComponent<Rigidbody>();
    rg.useGravity = false;

    Vector3 com = rg.centerOfMass;
    actual.transform.position -= new Vector3(com.x, com.y, com.z);
    actual.transform.parent = gameObject.transform;

    Vector3 meshSize = actual.GetComponent<BoxCollider>().bounds.size;
    Vector3 dimSize = dimensions.GetComponent<BoxCollider>().bounds.size;
    gameObject.transform.localScale = new Vector3(dimSize.x / meshSize.x, dimSize.y / meshSize.y, dimSize.z / meshSize.z);
    rg.constraints = RigidbodyConstraints.FreezeAll;
    actual.GetComponent<Renderer>().material = glowEffect;
    Destroy(dimensions.GetComponent<BoxCollider>());
    Destroy(actual.GetComponent<BoxCollider>());
    loaded = true;
  }
}