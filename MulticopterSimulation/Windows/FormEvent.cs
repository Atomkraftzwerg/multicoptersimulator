using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MulticopterSimulation.Windows
{
    /// <summary>
    /// A class providing general Form Operations
    /// </summary>
    public class FormEvent
    {
        private Form _theForm;

        /// <summary>
        /// The form on which the event will be called
        /// </summary>
        public Form Form
        {
            get { return _theForm; }
            set { _theForm = value; }
        }

        /// <summary>
        /// Constructor providing the form
        /// </summary>
        /// <param name="form">The form on which the event will be called</param>
        public FormEvent(Form form)
        {
            _theForm = form;
        }
    }

    /// <summary>
    /// A class providing the load form event
    /// </summary>
    public class OnLoad : FormEvent
    {
        public OnLoad(Form form)
            : base(form)
        {
        }
    }

    /// <summary>
    /// A class providing the close form event
    /// </summary>
    public class OnClosed : FormEvent
    {
        public OnClosed(Form form)
            : base(form)
        {
        }
    }
}
