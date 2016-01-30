# KWorks
My personal Unity Framework. It is directly the result of previous projects, being iterated on with every project.

It realies heavily on the CSharpMessenger provided in [the Unity Wiki](http://wiki.unity3d.com/index.php?title=CSharpMessenger_Extended) for class communication. 

## Managers

The prebuilt managers have some functionality ready to use, out of the box.
Each of them should be included in their own prefab, in the Resources folder. The GameManager prefab should be in the `Resources/Prefabs/Managers/` root, and the other prefabs should be in `Resources/Prefabs/Managers/Other/`. These managers will be instantiates automatically at startup, and will not be destroyed with scene changes.

In future versions, I plan to include the prefabs for the managers in the .unitypackage release.

### GameManager

### InputManager

### DialogManager

## Messaging

To allow classes to communicate amongst them without knowing about each other, the gramework uses the Messenger described above. The messages are prefixed by `[<Class Name>]`, so the messages `GameManager.cs` sends will be prefixed by `[GAME] <message>`.

## MonoBehaviourBase

## Wrappers

A collection of idioms wrap the Unity classes, to provide a nicer interface for the most common operations, which are not directly supported by the engine. These classes will continue to grow as needs arise from different projects, but I prefer to follow the [YAGNI](https://en.wikipedia.org/wiki/You_aren't_gonna_need_it) style to only add the most critical functions.
They integrate seamlessly in the workflow, appearing as a normal operation of the class:

```C#
Transform m_tr;

void Update() {
  // ... some code
  m_tr.SetX(4.2f);
  // ... some other code
}
```

