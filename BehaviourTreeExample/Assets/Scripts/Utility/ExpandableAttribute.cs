using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
/// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
/// area that allows for changing the values on the object without having to change editor.
/// </summary>
public class ExpandableAttribute : PropertyAttribute
{
	public ExpandableAttribute()
	{

	}
}


