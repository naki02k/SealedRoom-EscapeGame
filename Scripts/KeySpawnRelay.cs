using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawnRelay : MonoBehaviour
{
    public CodeChecker codeChecker;
    public MemoryPuzzleManager memoryPuzzleManager;
    public void RelaySpawnKey()
    {
        GameObject key = null;
        if (codeChecker != null)
        {
            key = codeChecker.GetSpawnKey();
        }
        else if (memoryPuzzleManager != null)
        {
            key = memoryPuzzleManager.GetSpawnMemo();
        }
        else
        {
            Debug.LogWarning("CodeChecker Ç‡ MemoryPuzzleManager Ç‡ñ¢ê›íËÇ≈Ç∑");
            return;
        }

        if (key != null)
        {
            Outline outline = key.GetComponent<Outline>();
            if(outline != null) outline.enabled = true;
            InteractableItem item = key.GetComponent<InteractableItem>();
            if (item != null) item.enabled = true;
        }
    }
    
}
