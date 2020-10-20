using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace EncodingConverter
{

    class PropertyLink<T>
    {
        public Action<T> Set;
        public Func<T> Get;
        public PropertyLink() { }
        public PropertyLink(Func<T> get, Action<T> set)
        {
            Set = set;
            Get = get;
        }
    }
    class EventLink
    {
        EventInfo _EventInfo;
        object _Target;

        #region ...ctor...
        public EventLink(object target, EventInfo eventInfo)
        {
            _Target = target;
            _EventInfo = eventInfo;
        }
        public EventLink(object target, string eventName)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            _Target = target;
            this._EventInfo = target.GetType().GetEvent(eventName);
        }
        #endregion

        public EventInfo EventInfo { get => _EventInfo; }
        public object EventRaiser { get => _Target; }
        public Delegate AddHandler(Action handler) { return _EventInfo.AddHandler(_Target, handler); }
        public Delegate _AddHandler<T1>(Action<T1> handler) { return _EventInfo._AddHandler(_Target, handler); }

        public void RemoveHandler(Delegate tocken) { _EventInfo.RemoveEventHandler(_Target, tocken); }
    }
    public static class EventLinkHelper
    {
        public static Delegate AddHandler(this EventInfo eventInfo, object eventRaiser, Action handler)
        {
            Delegate eventHandler = CreateEventHandler(eventInfo, handler);
            eventInfo.AddEventHandler(eventRaiser, eventHandler);

            return eventHandler;
        }
        public static Delegate _AddHandler<T1>(this EventInfo eventInfo, object eventRaiser, Action<T1> handler)
        {
            return null;
        }

        //void delegates with no parameters
        public static Delegate CreateEventHandler(this EventInfo evt, Action d)
        {
            var handlerType = evt.EventHandlerType;
            var eventParams = handlerType.GetMethod("Invoke").GetParameters();

            //lambda: (object x0, EventArgs x1) => d()
            var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x"));
            var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"));
            var lambda = Expression.Lambda(body, parameters.ToArray());
            return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
        }
        public static Delegate CreateHandler(this MethodInfo method, Action d)
        {
            var eventParams = method.GetParameters();

            //lambda: (object x0, EventArgs x1) => d()
            var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x"));
            var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"));
            var lambda = Expression.Lambda(body, parameters.ToArray());
            return lambda.Compile();// Delegate.CreateDelegate(method.DeclaringType, lambda.Compile(), "Invoke", false);
        }

        ////void delegate with one parameter
        //static public Delegate Create<T>(EventInfo evt, Action<T> d)
        //{
        //    var handlerType = evt.EventHandlerType;
        //    var eventParams = handlerType.GetMethod("Invoke").GetParameters();

        //    //lambda: (object x0, ExampleEventArgs x1) => d(x1.IntArg)
        //    var parameters = eventParams.Select(p => Expression.Parameter(p.ParameterType, "x")).ToArray();
        //    var arg = getArgExpression(parameters[1], typeof(T));
        //    var body = Expression.Call(Expression.Constant(d), d.GetType().GetMethod("Invoke"), arg);
        //    var lambda = Expression.Lambda(body, parameters);
        //    return Delegate.CreateDelegate(handlerType, lambda.Compile(), "Invoke", false);
        //}

        ////returns an expression that represents an argument to be passed to the delegate
        //static Expression getArgExpression(ParameterExpression eventArgs, Type handlerArgType)
        //{
        //    if (eventArgs.Type == typeof(ExampleEventArgs) && handlerArgType == typeof(int))
        //    {
        //        //"x1.IntArg"
        //        var memberInfo = eventArgs.Type.GetMember("IntArg")[0];
        //        return Expression.MakeMemberAccess(eventArgs, memberInfo);
        //    }

        //    throw new NotSupportedException(eventArgs + "->" + handlerArgType);
        //}
    }
    class OneWayUpdater<T> : IDisposable
    {
        public Func<T> SourceGetter;

        public Action<T> DestinationSetter;
        private EventLink _UpdateEvent;

        Delegate _EventProxy;//Holds the generated proxy method that links the handler to the event.

        public OneWayUpdater(Func<T> sourceGetter, Action<T> destinationSetter, EventLink updateEvent)
        {
            SourceGetter = sourceGetter;
            DestinationSetter = destinationSetter;
            this.UpdateEvent = updateEvent;
        }
        public EventLink UpdateEvent
        {
            get { return _UpdateEvent; }
            set
            {
                if (_UpdateEvent == value)
                {
                    return;
                }
                _UpdateEvent?.RemoveHandler(_EventProxy);
                _UpdateEvent = value;
                _EventProxy = _UpdateEvent?.AddHandler(this.Update);

            }
        }
        public virtual void Update() { DestinationSetter(SourceGetter()); }

        void IDisposable.Dispose()
        {
            _UpdateEvent?.RemoveHandler(_EventProxy);
            _UpdateEvent = null;
        }
    }

    class LockedOneWayUpdater<T> : OneWayUpdater<T>
    {
        public LockedOneWayUpdater(Func<T> sourceGetter, Action<T> destinationSetter, EventLink updateEvent)
            : base(sourceGetter, destinationSetter, updateEvent) { }

        public override void Update()
        {
            base.Update();
        }
    }
    /// <summary>
    /// A 2-way updater between 2 objects with an internal update-lock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class UpdateLock<T> : IDisposable
    {
        PropertyLink<T> _Obj1PropertyLink;
        EventLink _Obj1EventLink;


        PropertyLink<T> _Obj2PropertyLink;
        EventLink _Obj2EventLink;

        Delegate _Obj1EventProxy;
        Delegate _Obj2EventProxy;

        bool _Updating;
        #region ...ctor...
        public UpdateLock(PropertyLink<T> obj1PropertyLink
            , EventLink obj1EventLink
            , PropertyLink<T> obj2PropertyLink
            , EventLink obj2EventLink
            )
        {
            _Obj1PropertyLink = obj1PropertyLink;
            _Obj1EventLink = obj1EventLink;

            _Obj2PropertyLink = obj2PropertyLink;
            _Obj2EventLink = obj2EventLink;

            _Obj1EventProxy = _Obj1EventLink?.AddHandler(this.UpdateObj1To2);
            _Obj2EventProxy = _Obj2EventLink?.AddHandler(this.UpdateObj2To1);
        }
        //public UpdateLock(PropertyLink<T> obj1PropertyLink
        //    , Action<EventHandler> wireObj1ChangeEvent
        //    //, Action<EventHandler> unWireObj1ChangeEvent
        //    , PropertyLink<T> obj2PropertyLink
        //    , Action<EventHandler> wireObj2ChangeEvent
        //    //, Action<EventHandler> unWireObj2ChangeEvent
        //    )
        //{
        //    _Obj1PropertyLink = obj1PropertyLink;
        //    _WireObj1ChangeEvent = wireObj1ChangeEvent;
        //    //_UnWireObj1ChangeEvent = unWireObj1ChangeEvent;

        //    _Obj2PropertyLink = obj2PropertyLink;
        //    _WireObj2ChangeEvent = wireObj2ChangeEvent;
        //    //_UnWireObj2ChangeEvent = unWireObj2ChangeEvent;


        //    _WireObj1ChangeEvent?.Invoke(this.UpdateObj1To2);
        //    _WireObj2ChangeEvent?.Invoke(this.UpdateObj2To1);
        //}
        //public UpdateLock(PropertyLink<T> obj1PropertyLink
        //    , Action<Action> wireObj1ChangeEvent
        //    //, Action<EventHandler> unWireObj1ChangeEvent
        //    , PropertyLink<T> obj2PropertyLink
        //    , Action<Action> wireObj2ChangeEvent
        //    //, Action<EventHandler> unWireObj2ChangeEvent
        //    )
        //{
        //    _Obj1PropertyLink = obj1PropertyLink;
        //    //_UnWireObj1ChangeEvent = unWireObj1ChangeEvent;

        //    _Obj2PropertyLink = obj2PropertyLink;
        //    //_UnWireObj2ChangeEvent = unWireObj2ChangeEvent;


        //    wireObj1ChangeEvent?.Invoke(this.UpdateObj1To2);
        //    wireObj2ChangeEvent?.Invoke(this.UpdateObj2To1);

        //}
        #endregion

        void UpdateObj1To2(object sender, EventArgs e) { UpdateObj1To2(); }
        void UpdateObj2To1(object sender, EventArgs e) { UpdateObj2To1(); }


        public void UpdateObj1To2()
        {
            if (_Updating)
                return;

            _Updating = true;
            T newValue;
            newValue = _Obj1PropertyLink.Get();
            //if (!newValue.Equals(_Obj2PropertyLink.Get()))
            //{
            //    _Obj2PropertyLink.Set(newValue);
            //}
            _Obj2PropertyLink.Set(newValue);
            _Updating = false;
        }
        public void UpdateObj2To1()
        {
            if (_Updating)
                return;

            _Updating = true;
            T newValue;
            newValue = _Obj2PropertyLink.Get();
            //if (!newValue.Equals(_Obj1PropertyLink.Get()))
            //{
            //    _Obj1PropertyLink.Set(newValue);
            //}
            _Obj1PropertyLink.Set(newValue);
            _Updating = false;
        }

        void IDisposable.Dispose()
        {
            _Obj1EventLink?.RemoveHandler(_Obj1EventProxy);
            _Obj2EventLink?.RemoveHandler(_Obj2EventProxy);
        }
    }
}
