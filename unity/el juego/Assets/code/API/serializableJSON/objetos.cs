using UnityEngine;

public interface ISerializableJson{
    public string Serialize(){
        return JsonUtility.ToJson(this);
    }
}