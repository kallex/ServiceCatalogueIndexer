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
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlRootAttribute("ServiceModelAbstraction", Namespace="", IsNullable=false)]
public partial class ServiceModelAbstractionType {
    
    private ServiceModelType[] serviceModelField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ServiceModel")]
    public ServiceModelType[] ServiceModel {
        get {
            return this.serviceModelField;
        }
        set {
            this.serviceModelField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ServiceModelType {
    
    private ServiceType[] serviceField;
    
    private DataContractType[] dataContractField;
    
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
    [System.Xml.Serialization.XmlElementAttribute("DataContract")]
    public DataContractType[] DataContract {
        get {
            return this.dataContractField;
        }
        set {
            this.dataContractField = value;
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class ServiceType {
    
    private OperationType[] operationField;
    
    private string nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Operation")]
    public OperationType[] Operation {
        get {
            return this.operationField;
        }
        set {
            this.operationField = value;
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
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class OperationType {
    
    private SemanticDataType[] parameterField;
    
    private UsesOperationType[] usesOperationField;
    
    private SemanticDataType[] returnValueField;
    
    private string nameField;
    
    private string semanticTypeNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Parameter")]
    public SemanticDataType[] Parameter {
        get {
            return this.parameterField;
        }
        set {
            this.parameterField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("UsesOperation")]
    public UsesOperationType[] UsesOperation {
        get {
            return this.usesOperationField;
        }
        set {
            this.usesOperationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ReturnValue")]
    public SemanticDataType[] ReturnValue {
        get {
            return this.returnValueField;
        }
        set {
            this.returnValueField = value;
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
    public string semanticTypeName {
        get {
            return this.semanticTypeNameField;
        }
        set {
            this.semanticTypeNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class SemanticDataType {
    
    private string nameField;
    
    private string semanticTypeNameField;
    
    private string dataTypeField;
    
    private bool isArrayField;
    
    public SemanticDataType() {
        this.isArrayField = false;
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
    public string semanticTypeName {
        get {
            return this.semanticTypeNameField;
        }
        set {
            this.semanticTypeNameField = value;
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
    [System.ComponentModel.DefaultValueAttribute(false)]
    public bool isArray {
        get {
            return this.isArrayField;
        }
        set {
            this.isArrayField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class DataContractType {
    
    private SemanticDataType[] propertyField;
    
    private string nameField;
    
    private string semanticTypeNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Property")]
    public SemanticDataType[] Property {
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
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string semanticTypeName {
        get {
            return this.semanticTypeNameField;
        }
        set {
            this.semanticTypeNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
public partial class UsesOperationType {
    
    private string nameField;
    
    private string semanticTypeNameField;
    
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
    public string semanticTypeName {
        get {
            return this.semanticTypeNameField;
        }
        set {
            this.semanticTypeNameField = value;
        }
    }
}
