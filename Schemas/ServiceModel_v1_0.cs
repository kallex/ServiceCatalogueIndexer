﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("ServiceModel", Namespace="", IsNullable=false)]
public partial class ServiceModelType {
    
    private ServicesType[] servicesField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Services")]
    public ServicesType[] Services {
        get {
            return this.servicesField;
        }
        set {
            this.servicesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ServicesType {
    
    private ServiceType[] serviceField;
    
    private CompositeTypeType[] compositeTypeField;
    
    private string contractNamespaceNameField;
    
    private string clientNamespaceNameField;
    
    private string serverNamespaceNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Service")]
    public ServiceType[] Service {
        get {
            return this.serviceField;
        }
        set {
            this.serviceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CompositeType")]
    public CompositeTypeType[] CompositeType {
        get {
            return this.compositeTypeField;
        }
        set {
            this.compositeTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string contractNamespaceName {
        get {
            return this.contractNamespaceNameField;
        }
        set {
            this.contractNamespaceNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string clientNamespaceName {
        get {
            return this.clientNamespaceNameField;
        }
        set {
            this.clientNamespaceNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string serverNamespaceName {
        get {
            return this.serverNamespaceNameField;
        }
        set {
            this.serverNamespaceNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ServiceType {
    
    private MethodType[] methodField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Method")]
    public MethodType[] Method {
        get {
            return this.methodField;
        }
        set {
            this.methodField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class MethodType {
    
    private ParameterType[] parameterField;
    
    private string nameField;
    
    private string returnValueTypeField;
    
    private string semanticNameField;
    
    public MethodType() {
        this.returnValueTypeField = "\"\"";
        this.semanticNameField = "";
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Parameter")]
    public ParameterType[] Parameter {
        get {
            return this.parameterField;
        }
        set {
            this.parameterField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute("\"\"")]
    public string returnValueType {
        get {
            return this.returnValueTypeField;
        }
        set {
            this.returnValueTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    [System.ComponentModel.DefaultValueAttribute("")]
    public string semanticName {
        get {
            return this.semanticNameField;
        }
        set {
            this.semanticNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ParameterType {
    
    private string nameField;
    
    private string dataTypeField;
    
    private string semanticNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string dataType {
        get {
            return this.dataTypeField;
        }
        set {
            this.dataTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string semanticName {
        get {
            return this.semanticNameField;
        }
        set {
            this.semanticNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class PropertyType {
    
    private string nameField;
    
    private string dataTypeField;
    
    private string initializationExpressionField;
    
    private string semanticNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string dataType {
        get {
            return this.dataTypeField;
        }
        set {
            this.dataTypeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string initializationExpression {
        get {
            return this.initializationExpressionField;
        }
        set {
            this.initializationExpressionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string semanticName {
        get {
            return this.semanticNameField;
        }
        set {
            this.semanticNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class CompositeTypeType {
    
    private PropertyType[] propertyField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Property")]
    public PropertyType[] Property {
        get {
            return this.propertyField;
        }
        set {
            this.propertyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }
}
