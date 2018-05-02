using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T> {

	public static T Instance => m_instance;
	private static T m_instance;

	public SingletonBehaviour(){
		m_instance = (T)this;
	}
}
