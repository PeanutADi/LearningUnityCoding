using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSDirector : System.Object {

    public ISceneController currentController;

    /* TA刚强调过设计模式的重要性所以赶紧百度了一波……
     * 这个类使用了单例模式，各位可以百度下单例模式
     * 这种写法只能应用于单线程，是线程不安全的，可能有多个线程同时调用set方法*/


    private static SSDirector director;

    //设为私有禁止他人创建实例，保证只有一个实例
    private SSDirector() { }

    public static SSDirector Director
    {
        get
        {
            if (director == null)
            {
                director = new SSDirector();
            }

            return director;
        }
        set
        {
            if (director == null)
            {
                director = new SSDirector();
            }
        }
    }

}
