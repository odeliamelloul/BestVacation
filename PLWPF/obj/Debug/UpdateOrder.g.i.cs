﻿#pragma checksum "..\..\UpdateOrder.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D5A63CCEBDBAB9B319E761E13534DD80B77B0C6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PLWPF;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace PLWPF {
    
    
    /// <summary>
    /// UpdateOrder
    /// </summary>
    public partial class UpdateOrder : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 57 "..\..\UpdateOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GoBack;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\UpdateOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BGR;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\UpdateOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListView_order;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\UpdateOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox NameHu;
        
        #line default
        #line hidden
        
        
        #line 230 "..\..\UpdateOrder.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Title;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PLWPF;component/updateorder.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UpdateOrder.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.GoBack = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\UpdateOrder.xaml"
            this.GoBack.Click += new System.Windows.RoutedEventHandler(this.BackButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BGR = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.ListView_order = ((System.Windows.Controls.ListView)(target));
            
            #line 70 "..\..\UpdateOrder.xaml"
            this.ListView_order.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListView_GR_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 70 "..\..\UpdateOrder.xaml"
            this.ListView_order.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.MouseDoubleClick_ListView_GR);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NameHu = ((System.Windows.Controls.ComboBox)(target));
            
            #line 115 "..\..\UpdateOrder.xaml"
            this.NameHu.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.NameHuCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Title = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

