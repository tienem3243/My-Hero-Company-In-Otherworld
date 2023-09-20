using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    private GameObject m_gameObject;
    private Transform m_transform;
    private SpriteRenderer m_renderer;
    private Rigidbody m_rigidbody;
    private Rigidbody2D m_rigidbody2D;
    private Collider2D m_collider2D;
    private Renderer _renderer;

    public new GameObject gameObject
    {
        get
        {
            if (m_gameObject == null)
            {
                m_gameObject = base.gameObject;
            }
            return m_gameObject;
        }
    }

    public new Transform transform
    {
        get
        {
            if (m_transform == null)
            {
                m_transform = GetComponent<Transform>();
            }
            return m_transform;
        }
    }

    public new Renderer renderer
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
            }
            return _renderer;
        }
    }


    public new Rigidbody rigidbody
    {
        get
        {
            if (m_rigidbody == null)
            {
                m_rigidbody = GetComponent<Rigidbody>();
            }
            return m_rigidbody;
        }
    }

    public new Rigidbody2D rigidbody2D
    {
        get
        {
            if (m_rigidbody2D == null)
            {
                m_rigidbody2D = GetComponent<Rigidbody2D>();
            }
            return m_rigidbody2D;
        }
    }
       public new Collider2D collider2D
    {
        get
        {
            if (m_collider2D == null)
            {
                m_collider2D = GetComponent<Collider2D>();
            }
            return m_collider2D;
        }
    }

}
