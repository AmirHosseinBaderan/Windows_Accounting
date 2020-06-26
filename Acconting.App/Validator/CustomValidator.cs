using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ValidationComponents
{
    public partial class CustomValidator : BaseValidator
    {
        public bool IsValidated { get; set; }
        public CustomValidator()
        {
            InitializeComponent();
        }

        public CustomValidator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public event EventHandler OnValidate;
        

        protected override bool EvaluateIsValid()
        {
            OnValidate(this, EventArgs.Empty);
            return IsValidated;
        }
    }
}
