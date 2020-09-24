using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace EncodingConverter.Forms
{
    static class WinFormsHelpers
    {
        public static Binding Bind(this Control control, string propertyNameOfControl, object dataSource, string propertyOfDataSource)
        {
            Binding binding = new Binding(propertyNameOfControl, dataSource, propertyOfDataSource, true, DataSourceUpdateMode.OnPropertyChanged);
            control.DataBindings.Add(binding);
            return binding;
        }
        public static UpdateLock<T> Bind<T>(PropertyLink<T> property1, Action<EventHandler> wireObj1ChangeEvent, PropertyLink<T> property2, Action<EventHandler> wireObj2ChangeEvent)
        {
            UpdateLock<T> binding = new UpdateLock<T>(property1, wireObj1ChangeEvent, property2, wireObj2ChangeEvent);
            return binding;
        }
        public static UpdateLock<String> BindText(this Control textBox, PropertyLink<String> property2, Action<EventHandler> wireObj2ChangeEvent)
        {
            PropertyLink<String> property1 = new PropertyLink<String>(() => textBox.Text, x => textBox.Text = x);

            UpdateLock<String> binding = new UpdateLock<String>(property1, x => textBox.TextChanged += x, property2, wireObj2ChangeEvent);
            return binding;
        }

    }
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
    class EventLink<T>
    {
        Action<T> Wire;
        Action<T> Unwire;

    }
    class OneWayUpdater<T>
    {
        public Func<T> SourceGetter;

        public Action<T> DestinationSetter;
        private EventInfo _UpdateEvent;

        Action _Update;// = this.Update;

        public OneWayUpdater()
        {
            _Update = this.Update;
        }
        public EventInfo UpdateEvent
        {
            get { return _UpdateEvent; }
            set 
            {
                if (_UpdateEvent == value)
                {
                    return;
                }
                if (_UpdateEvent != null)
                {
                    _UpdateEvent.RemoveEventHandler(null, _Update);
                }
                _UpdateEvent = value;
                if (_UpdateEvent != null)
                {
                    _UpdateEvent.AddEventHandler(null, _Update);
                }

            }
        }


        public void Update() { DestinationSetter(SourceGetter()); }

        void Wire()
        {

        }
    }

    class TestClass
    {
        public Control control1;

        void test()
        {
            OneWayUpdater<string> oneWayUpdater = new OneWayUpdater<string>();

            oneWayUpdater.SourceGetter = () => this.MyProperty;
            oneWayUpdater.DestinationSetter = x => this.Dest = x;

            EventInfo eventInfo;
            control1.TextChanged += (s, e) => oneWayUpdater.Update();
            //            eventInfo.AddEventHandler(,)
        }

        private void Control1_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string myVar;

        public string MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private string myDest;

        public string Dest
        {
            get { return myDest; }
            set { myDest = value; }
        }


    }
    /// <summary>
    /// A 2-way updater between 2 objects with an internal update-lock.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class UpdateLock<T>
    {
        PropertyLink<T> _Obj1PropertyLink;
        Action<EventHandler> _WireObj1ChangeEvent;
        Action<EventHandler> _UnWireObj1ChangeEvent;


        PropertyLink<T> _Obj2PropertyLink;
        Action<EventHandler> _WireObj2ChangeEvent;
        Action<EventHandler> _UnWireObj2ChangeEvent;

        bool _Updating;

        public UpdateLock(PropertyLink<T> obj1PropertyLink
            , Action<EventHandler> wireObj1ChangeEvent
            //, Action<EventHandler> unWireObj1ChangeEvent
            , PropertyLink<T> obj2PropertyLink
            , Action<EventHandler> wireObj2ChangeEvent
            //, Action<EventHandler> unWireObj2ChangeEvent
            )
        {
            _Obj1PropertyLink = obj1PropertyLink;
            _WireObj1ChangeEvent = wireObj1ChangeEvent;
            //_UnWireObj1ChangeEvent = unWireObj1ChangeEvent;

            _Obj2PropertyLink = obj2PropertyLink;
            _WireObj2ChangeEvent = wireObj2ChangeEvent;
            //_UnWireObj2ChangeEvent = unWireObj2ChangeEvent;


            _WireObj1ChangeEvent?.Invoke(this.Obj1Changed);
            _WireObj2ChangeEvent?.Invoke(this.Obj2Changed);
        }


        void Obj1Changed(T newValue)
        {
            if (_Updating)
                return;
            _Updating = true;
            _Obj2PropertyLink.Set(newValue);
            //_Obj2PropertyLink(newValue);
            _Updating = false;
        }
        void Obj2Changed(T newValue)
        {
            if (_Updating)
                return;
            _Updating = true;
            _Obj1PropertyLink.Set(newValue);
            //_Obj1PropertyLink(newValue);
            _Updating = false;
        }

        void Obj1Changed(object sender, EventArgs e) { UpdateObj1To2(); }
        void Obj2Changed(object sender, EventArgs e) { UpdateObj2To1(); }


        public void UpdateObj1To2()
        {
            if (_Updating)
                return;

            _Updating = true;
            T value;
            value = _Obj1PropertyLink.Get();
            _Obj2PropertyLink.Set(value);
            _Updating = false;
        }
        public void UpdateObj2To1()
        {
            if (_Updating)
                return;

            _Updating = true;
            T value;
            value = _Obj2PropertyLink.Get();
            _Obj1PropertyLink.Set(value);
            _Updating = false;
        }

    }

}
