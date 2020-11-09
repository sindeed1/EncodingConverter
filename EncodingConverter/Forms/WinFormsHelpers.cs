using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq.Expressions;

namespace EncodingConverter.Forms
{
    static class WinFormsHelpers
    {
        static EventInfo _ControlTextChangedEventInfo = typeof(Control).GetEvent(nameof(Control.TextChanged));

        public static Binding Bind(this Control control, string propertyNameOfControl, object dataSource, string propertyOfDataSource)
        {
            Binding binding = new Binding(propertyNameOfControl, dataSource, propertyOfDataSource, true, DataSourceUpdateMode.OnPropertyChanged);
            control.DataBindings.Add(binding);
            return binding;
        }
        public static UpdateLock<T> Bind<T>(PropertyLink<T> property1, EventLink obj1ChangeEvent, PropertyLink<T> property2, EventLink obj2ChangeEvent)
        {
            UpdateLock<T> binding = new UpdateLock<T>(property1, obj1ChangeEvent, property2, obj2ChangeEvent);
            return binding;
        }
        public static UpdateLock<String> BindText(this Control textBox, PropertyLink<String> property2, EventLink obj2ChangeEvent)
        {
            PropertyLink<String> property1 = new PropertyLink<String>(() => textBox.Text, x => textBox.Text = x);
            EventLink textChangeEventLinks = new EventLink(textBox, _ControlTextChangedEventInfo);

            UpdateLock<String> binding = new UpdateLock<String>(property1, textChangeEventLinks, property2, obj2ChangeEvent);
            return binding;
        }

        public static OneWayUpdater<String> BindTextAsDestination(this Control textBox, PropertyLink<String> property2, EventLink obj2ChangeEvent)
        {
            OneWayUpdater<String> binding = new OneWayUpdater<String>(property2.Get, x => textBox.Text = x, obj2ChangeEvent);
            return binding;
        }
        public static OneWayUpdater<String> BindTextAsDestination(this Control textBox, Func<string> sourceGetter, EventLink obj2ChangeEvent)
        {
            OneWayUpdater<String> binding = new OneWayUpdater<String>(sourceGetter, x => textBox.Text = x, obj2ChangeEvent);
            return binding;
        }
        public static OneWayUpdater<String> BindTextAsSource(this Control textBox, PropertyLink<String> destinationPropertyLink)
        {
            EventLink textChangeEventLinks = new EventLink(textBox, _ControlTextChangedEventInfo);
            OneWayUpdater<String> binding = new OneWayUpdater<String>(() => textBox.Text, destinationPropertyLink.Set, textChangeEventLinks);
            return binding;
        }
        public static OneWayUpdater<String> BindTextAsSource(this Control textBox, Action<String> destinationSetter)
        {
            EventLink textChangeEventLinks = new EventLink(textBox, _ControlTextChangedEventInfo);
            OneWayUpdater<String> binding = new OneWayUpdater<String>(() => textBox.Text, destinationSetter, textChangeEventLinks);
            return binding;
        }

        public static OneWayUpdater<String> BindTextAsDestination(this ToolStripItem textBox, Func<string> sourceGetter, EventLink obj2ChangeEvent)
        {
            OneWayUpdater<String> binding = new OneWayUpdater<String>(sourceGetter, x => textBox.Text = x, obj2ChangeEvent);
            return binding;
        }
        public static OneWayUpdater<String> BindTextAsSource(this ToolStripItem textBox, Action<String> destinationSetter)
        {
            EventLink textChangeEventLinks = new EventLink(textBox, _ControlTextChangedEventInfo);
            OneWayUpdater<String> binding = new OneWayUpdater<String>(() => textBox.Text, destinationSetter, textChangeEventLinks);
            return binding;
        }

    }//WinFormsHelpers

}
