using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/DataEventChannelSO")]
public class DataEventChannelSO : ScriptableObject
{
    private event System.Action<object> DataEventHandler;
  
    public void RaiseEvent(object data)
    {
        DataEventHandler?.Invoke(data);
    }
    
    public void RegisterListener(System.Action<object> listener)
    {
        DataEventHandler += listener;
    }
    
    public void UnregisterListener(System.Action<object> listener)
    {
        DataEventHandler -= listener;
    }
}