﻿#pragma checksum "..\..\..\WindowBike.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "99DB580AAAC85533EA61F3A57425BA31A1AC987C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BikeRepairShop.Admin.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BikeRepairShop.Admin.UI {
    
    
    /// <summary>
    /// WindowBike
    /// </summary>
    public partial class WindowBike : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CBCustomer;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBCustomer;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBId;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CBBikeType;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBPurchaseCost;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBDescription;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelBikeButton;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\WindowBike.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveBikeButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BikeRepairShop.Admin.UI;component/windowbike.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WindowBike.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\WindowBike.xaml"
            ((BikeRepairShop.Admin.UI.WindowBike)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CBCustomer = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.TBCustomer = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TBId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CBBikeType = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.TBPurchaseCost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TBDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.CancelBikeButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\WindowBike.xaml"
            this.CancelBikeButton.Click += new System.Windows.RoutedEventHandler(this.CancelBikeButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SaveBikeButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\WindowBike.xaml"
            this.SaveBikeButton.Click += new System.Windows.RoutedEventHandler(this.SaveBikeButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

