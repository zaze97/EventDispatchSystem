using System;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter
{
    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();

    #region 事件派发添加监听事件
    /// <summary>
    /// 添加监听的方法（无参）
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    public static void AddListener(EventType eventType, CallBack callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
    }
    /// <summary>
    /// 添加监听的方法（一个参数）
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T1,T2>(EventType eventType, CallBack<T1,T2> callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1,T2>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T1, T2,T3>(EventType eventType, CallBack<T1, T2, T3> callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T1, T2, T3,T4>(EventType eventType, CallBack<T1, T2, T3, T4> callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3, T4>)m_EventTable[eventType] + callBack;
    }
    public static void AddListener<T1, T2, T3, T4,T5>(EventType eventType, CallBack<T1, T2, T3, T4, T5> callBack)
    {
        ListenerAdding(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3, T4, T5>)m_EventTable[eventType] + callBack;
    }
    /// <summary>
    /// 公用方法
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    private static void ListenerAdding(EventType eventType, Delegate callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }

        Delegate @delegate = m_EventTable[eventType];

        if (@delegate != null && @delegate.GetType() != callBack.GetType())//如果@delegate不等于空同时两个类型参数不同
        {
            throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1},要添加的委托类型为{2}", eventType, @delegate.GetType(), callBack.GetType()));
        }
    }
    #endregion
    #region 事件派发移除监听事件
    /// <summary>
    /// 移除监听的方法（无参）
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="callBack"></param>
    public static void RemoveListenter(EventType eventType, CallBack callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);
    }
    public static void RemoveListenter<T>(EventType eventType, CallBack<T> callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);

    }
    public static void RemoveListenter<T1,T2>(EventType eventType, CallBack<T1, T2> callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2>)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);

    }
    public static void RemoveListenter<T1, T2,T3>(EventType eventType, CallBack<T1, T2, T3> callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3>)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);

    }
    public static void RemoveListenter<T1, T2, T3,T4>(EventType eventType, CallBack<T1, T2, T3, T4> callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3, T4>)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);

    }
    public static void RemoveListenter<T1, T2, T3, T4,T5>(EventType eventType, CallBack<T1, T2, T3, T4, T5> callBack)
    {
        ListenterRemoveing(eventType, callBack);
        m_EventTable[eventType] = (CallBack<T1, T2, T3, T4, T5>)m_EventTable[eventType] - callBack;
        ListenterRemoveed(eventType);

    }
    private static void ListenterRemoveing(EventType eventType, Delegate callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate @delegate = m_EventTable[eventType];
            if (@delegate == null)
            {
                throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托类型", eventType));
            }
            else if (@delegate.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除同类型的委托，当前委托类型为{1},要移除的委托类型为{2}", eventType, @delegate.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除监听错误：没有事件码[0]", eventType));
        }
    }
    private static void ListenterRemoveed(EventType eventType)
    {
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }

    #endregion
    #region 事件派发开始监听事件
    /// <summary>
    /// 事件开始
    /// </summary>
    /// <param name="eventType"></param>
    public static void Broadcast(EventType eventType)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate)) //TryGetValue: 获取与指定的键相关联的值
        {
            CallBack callBack = @delegate as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T>(EventType eventType, T arg)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate))
        {
            CallBack<T> callBack = @delegate as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件[0]对应的委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T1,T2>(EventType eventType, T1 arg1,T2 arg2)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate))
        {
            CallBack<T1, T2> callBack = @delegate as CallBack<T1, T2>;
            if (callBack != null)
            {
                callBack(arg1,arg2);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T1, T2,T3>(EventType eventType, T1 arg1,T2 arg2,T3 arg3)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate))
        {
            CallBack<T1, T2, T3> callBack = @delegate as CallBack<T1, T2, T3>;
            if (callBack != null)
            {
                callBack(arg1,arg2,arg3);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T1, T2, T3,T4>(EventType eventType, T1 arg1,T2 arg2,T3 arg3,T4 arg4)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate))
        {
            CallBack<T1, T2, T3, T4> callBack = @delegate as CallBack<T1, T2, T3, T4>;
            if (callBack != null)
            {
                callBack(arg1,arg2,arg3,arg4);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    public static void Broadcast<T1, T2, T3, T4,T5>(EventType eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4,T5 arg5)
    {
        Delegate @delegate;
        if (m_EventTable.TryGetValue(eventType, out @delegate))
        {
            CallBack<T1, T2, T3, T4, T5> callBack = @delegate as CallBack<T1, T2, T3, T4, T5>;
            if (callBack != null)
            {
                callBack(arg1,arg2,arg3,arg4,arg5);
            }
            else
            {
                throw new Exception(string.Format("广播事件错误:事件{0}对应的委托具有不同的类型", eventType));
            }
        }
    }
    #endregion
}