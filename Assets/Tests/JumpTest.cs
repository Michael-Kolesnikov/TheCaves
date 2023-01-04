using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JumpTest
{
    [Test]
    public void JumpTestSimplePasses()
    {
        int a = 1;
        a += a;
        Assert.AreEqual(a, 1);
    }
}
