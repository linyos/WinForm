﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_Chart
{
    public partial class Form3 : Form
    {



        public Form3()
        {
            InitializeComponent();
        }
        Person p = new Person();
        private void button1_Click(object sender, EventArgs e)
        {
            p.Name = "Admin";
            p.Age = 15;
            p.Email = "asdjasojdisa";

            propertyGrid1.SelectedObject = p;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = new MyClass();
            
        }
    }

 
    [TypeConverter(typeof(optionEnumConvertor))]
    enum MyEnum
    {
        OPTION1,
        OPTION2,
    }
    class optionEnumConvertor : EnumConverter
    {
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            switch (value.ToString())
            {
                case "選項1":
                    return MyEnum.OPTION1;
                case "選項2":
                    return MyEnum.OPTION2;
                default:
                    return null;
            }
        }
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            switch ((MyEnum)value)
            {
                case MyEnum.OPTION1:
                    return "選項1";
                case MyEnum.OPTION2:
                    return "選項2";
                default:
                    return null;
            }

        }
        public optionEnumConvertor(Type type) : base(type)
        {
        }
    }

    class MyClass
    {
        [Description("The input value range is 16-19")] //offered the description which would showed on the bar below
        [Category("Catagory A")]            // offered the folded categoring method
        [DisplayName("Integer Parameter")]  // offered the customised display name
   

        [ReadOnly(true)]
        public int Property1
        {
            get { return __innerVariableInteger; }
            set {
                if (value > 10 && value < 20)
                    __innerVariableInteger = value;
            }
        }
        [Browsable(true)]  //wont be showed on PropertyGrid
        [EditorAttribute(typeof(stringUIEditor), typeof(UITypeEditor))]
        public string Property2
        {
            get
            {
                return __innerVariableString;
            }
            set
            {
                __innerVariableString = value;
            }
        }
        [DisplayName("想要選什麼？")] // much more human-readable?
        [TypeConverter(typeof(optionEnumConvertor))]
        public MyEnum Property3 { get; set; }

        protected string __innerVariableString = "default";
        protected int __innerVariableInteger = 15;

    }


    class stringUIEditor :UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Form __form = new Form();       // allocating a form , used to show-up editable facility 
            TextBox __tb = new TextBox();
            __tb.Text = "Please Input What You Want";
            __tb.Dock = DockStyle.Fill;

            __form.Controls.Add(__tb);
            __form.AutoSize = true;
            __form.Text = "Raised by EditValue()";
            __form.ShowDialog(); // show-up the form 
            return __tb.Text; //return the text value the user just input.
        }
    }


    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}




   



    

    

