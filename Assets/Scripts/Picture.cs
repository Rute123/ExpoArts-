﻿using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Picture
{
    public long id;
    [CanBeNull] public string author;
    public float x;
    public float y;
    public float z;
    public float rotationX;
    public float rotationY;
    public float rotationZ;
    public float rotationW;
    public string image;
    
    public Sprite Sprite
    {
        get
        {
            var decodedBytes = Convert.FromBase64String(image);
            var img = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            img.LoadImage(decodedBytes);
            return Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
        }
        set => image = Convert.ToBase64String(value.texture.EncodeToPNG());
    }
    
    public Picture(Vector3 position, Quaternion rotation)
    {
        x = position.x;
        y = position.y;
        z = position.z;
        rotationX = rotation.x;
        rotationY = rotation.y;
        rotationZ = rotation.z;
        rotationW = rotation.w;
    }
    
    public Picture(Vector3 position, Quaternion rotation, Sprite sprite) : this(position, rotation)
    {
        Sprite = sprite;
    }

    public Picture(Vector3 position, Quaternion rotation, Sprite sprite, long id) : this(position, rotation, sprite)
    {
        this.id = id;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(x, y, z);
    }

    public Quaternion GetRotation()
    {
        return new Quaternion(rotationX, rotationY, rotationZ, rotationW);
    }
}
