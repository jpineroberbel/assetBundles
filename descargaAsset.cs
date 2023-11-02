using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class descargaAsset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string prefabBundleUrl="https://drive.google.com/uc?export=download&id=142fjGS6nFkKRo0Nnv6AmGEHvOmDKRlit";
        
        StartCoroutine(FetchGameObjectFromServer(prefabBundleUrl,"prueba",1419886407,new Hash128()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FetchGameObjectFromServer(string url,string manifestFileName,uint crcR,Hash128 hashR)
        {
         
            //Get from generated manifest file of assetbundle.
            uint crcNumber = crcR;
            //Get from generated manifest file of assetbundle.
            Hash128 hashCode = hashR;
             UnityWebRequest webrequest =
                UnityWebRequestAssetBundle.GetAssetBundle( url); //, new CachedAssetBundle(manifestFileName, hashCode), crcNumber);
    
           
            webrequest.SendWebRequest();
    
            while (!webrequest.isDone)
            {
              Debug.Log(webrequest.downloadProgress);  
              
            }
        
            AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(webrequest);
           yield return assetBundle;
           if (assetBundle == null)
                yield break;
       
      
            //Gets name of all the assets in that assetBundle.
            string[] allAssetNames = assetBundle.GetAllAssetNames();
             Debug.Log(allAssetNames.Length +"objects inside prefab bundle");
            foreach (string gameObjectsName in allAssetNames)
            {
                string gameObjectName = Path.GetFileNameWithoutExtension(gameObjectsName).ToString();
                GameObject objectFound = assetBundle.LoadAsset(gameObjectName) as GameObject;
                Instantiate(objectFound);
            }
            assetBundle.Unload(false);
            yield return null;
        }
}
