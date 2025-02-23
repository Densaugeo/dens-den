/*
 * To get Intellisense working:
 * - Installed VSCode
 * - Set Unity's script editor to VSCode in Edit > Preferences > External Tools
 * - VSCode must be opened by selecting a script in Unity and clicking "open" in
 *   the inspector
 * - Installed C# and Unity extensions from VSCode prompt
 * - Installed .NET 9.0 SDK (current latest)
 * - Installed .NET 4.7.1 developer pack
 * - Assembly-CSharp.csproj, Assembly-CSharp-Editor.csproj, and
 *   .vscode/settings.json must be presernt. If they are missing, they can be
 *   created in Unity > Edit > Preferences > Regenerate project files (I left
 *   all of the associated checkboxes empty)
 * - Rebooted Windows
 */

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class MirrorButton : UdonSharpBehaviour {
    public GameObject mirror;

    public override void Interact() {
        mirror.SetActive(!mirror.activeSelf);
        base.Interact();
    }
}
