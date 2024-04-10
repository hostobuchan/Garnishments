using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms
{
    /// <summary>
    /// (eraghi)
    /// Custom CheckedListBox with binding facilities (Value property)
    /// </summary>
    [ToolboxBitmap(typeof(CheckedListBox))]
    public class CheckedListBoxDS : CheckedListBox
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CheckedListBoxDS()
        {
            this.CheckOnClick = true;
        }



        /// <summary>
        ///    Gets or sets the property to display for this CustomControls.CheckedListBox.
        ///
        /// Returns:
        ///     A System.String specifying the name of an object property that is contained
        ///     in the collection specified by the CustomControls.CheckedListBox.DataSource
        ///     property. The default is an empty string ("").
        /// </summary>
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        public new string DisplayMember
        {
            get
            {
                return base.DisplayMember;
            }
            set
            {
                base.DisplayMember = value;

            }
        }

        /// <summary>
        ///    Gets or sets the property to get the values for this CustomControls.CheckedListBox.
        ///
        /// Returns:
        ///     A System.String specifying the name of an object property that is contained
        ///     in the collection specified by the CustomControls.CheckedListBox.DataSource
        ///     property. The default is an empty string ("").
        /// </summary>
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Browsable(true)]
        public new string ValueMember
        {
            get
            {
                return base.ValueMember;
            }
            set
            {
                base.ValueMember = value;

            }
        }


        /// <summary>
        /// Gets or sets the data source for this CustomControls.CheckedListBox.
        /// Returns:
        ///    An object that implements the System.Collections.IList or System.ComponentModel.IListSource
        ///    interfaces, such as a System.Data.DataSet or an System.Array. The default
        ///    is null.
        ///
        ///Exceptions:
        ///  System.ArgumentException:
        ///    The assigned value does not implement the System.Collections.IList or System.ComponentModel.IListSource
        ///    interfaces.
        /// </summary>
        [DefaultValue("")]
        [AttributeProvider(typeof(IListSource))]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;

            }
        }

    }
}
