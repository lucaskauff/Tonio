using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tonio
{
    [System.Serializable]
    public class Dialogue
    {
        public DialogueParts[] allDialogueParts;
    }

    [System.Serializable]
    public class DialogueParts
    {
        public string speaker;

        [TextArea(3, 10)]
        public string sentence;
    }
}