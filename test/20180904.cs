﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// 此源代码由 xsd 自动生成, Version=4.6.1055.0。
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class TCondition
{

    private TConditionSubCondition[] subConditionField;

    private string isParentField;

    private string varNameField;

    private string conValueField;

    private string varCaptionField;

    private string conField;

    private string unitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("subCondition", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TConditionSubCondition[] subCondition
    {
        get
        {
            return this.subConditionField;
        }
        set
        {
            this.subConditionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string IsParent
    {
        get
        {
            return this.isParentField;
        }
        set
        {
            this.isParentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string VarName
    {
        get
        {
            return this.varNameField;
        }
        set
        {
            this.varNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ConValue
    {
        get
        {
            return this.conValueField;
        }
        set
        {
            this.conValueField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string VarCaption
    {
        get
        {
            return this.varCaptionField;
        }
        set
        {
            this.varCaptionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Con
    {
        get
        {
            return this.conField;
        }
        set
        {
            this.conField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Unit
    {
        get
        {
            return this.unitField;
        }
        set
        {
            this.unitField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TConditionSubCondition
{

    private string isParentField;

    private string varNameField;

    private string varCaptionField;

    private string conField;

    private string conValueField;

    private string unitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string IsParent
    {
        get
        {
            return this.isParentField;
        }
        set
        {
            this.isParentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string VarName
    {
        get
        {
            return this.varNameField;
        }
        set
        {
            this.varNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string VarCaption
    {
        get
        {
            return this.varCaptionField;
        }
        set
        {
            this.varCaptionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Con
    {
        get
        {
            return this.conField;
        }
        set
        {
            this.conField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ConValue
    {
        get
        {
            return this.conValueField;
        }
        set
        {
            this.conValueField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Unit
    {
        get
        {
            return this.unitField;
        }
        set
        {
            this.unitField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class TSchem
{

    private object[] itemsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TCondition", typeof(TCondition))]
    [System.Xml.Serialization.XmlElementAttribute("setCanlist", typeof(TSchemSetCanlist), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("setEthList", typeof(TSchemSetEthList), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("stepList", typeof(TSchemStepList), Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemSetCanlist
{

    private TSchemSetCanlistTSetCan[] tSetCanField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TSetCan", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TSchemSetCanlistTSetCan[] TSetCan
    {
        get
        {
            return this.tSetCanField;
        }
        set
        {
            this.tSetCanField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemSetCanlistTSetCan
{

    private string checkField;

    private string agreeMentFileField;

    private string bautField;

    private string aliasField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Check
    {
        get
        {
            return this.checkField;
        }
        set
        {
            this.checkField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string AgreeMentFile
    {
        get
        {
            return this.agreeMentFileField;
        }
        set
        {
            this.agreeMentFileField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Baut
    {
        get
        {
            return this.bautField;
        }
        set
        {
            this.bautField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Alias
    {
        get
        {
            return this.aliasField;
        }
        set
        {
            this.aliasField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemSetEthList
{

    private TSchemSetEthListTSetEth[] tSetEthField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TSetEth", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TSchemSetEthListTSetEth[] TSetEth
    {
        get
        {
            return this.tSetEthField;
        }
        set
        {
            this.tSetEthField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemSetEthListTSetEth
{

    private string checkField;

    private string ipField;

    private string portField;

    private string agreeMentFileField;

    private string aliasField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Check
    {
        get
        {
            return this.checkField;
        }
        set
        {
            this.checkField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string IP
    {
        get
        {
            return this.ipField;
        }
        set
        {
            this.ipField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Port
    {
        get
        {
            return this.portField;
        }
        set
        {
            this.portField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string AgreeMentFile
    {
        get
        {
            return this.agreeMentFileField;
        }
        set
        {
            this.agreeMentFileField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Alias
    {
        get
        {
            return this.aliasField;
        }
        set
        {
            this.aliasField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemStepList
{

    private TSchemStepListTStep[] tStepField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("TStep", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public TSchemStepListTStep[] TStep
    {
        get
        {
            return this.tStepField;
        }
        set
        {
            this.tStepField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemStepListTStep
{

    private TSchemStepListTStepCmdListTCMD[][] cmdListField;

    private string testidField;

    private string titleField;

    private string kindField;

    private string checkField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("TCMD", typeof(TSchemStepListTStepCmdListTCMD), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
    public TSchemStepListTStepCmdListTCMD[][] cmdList
    {
        get
        {
            return this.cmdListField;
        }
        set
        {
            this.cmdListField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string testid
    {
        get
        {
            return this.testidField;
        }
        set
        {
            this.testidField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string kind
    {
        get
        {
            return this.kindField;
        }
        set
        {
            this.kindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Check
    {
        get
        {
            return this.checkField;
        }
        set
        {
            this.checkField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class TSchemStepListTStepCmdListTCMD
{

    private string initlistField;

    private string setlistField;

    private TCondition[][] judgelistField;

    private TCondition[][] savelistField;

    private string kindField;

    private string cmdField;

    private string waitTimeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string initlist
    {
        get
        {
            return this.initlistField;
        }
        set
        {
            this.initlistField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string setlist
    {
        get
        {
            return this.setlistField;
        }
        set
        {
            this.setlistField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("TCondition", typeof(TCondition), IsNullable = false)]
    public TCondition[][] judgelist
    {
        get
        {
            return this.judgelistField;
        }
        set
        {
            this.judgelistField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlArrayItemAttribute("TCondition", typeof(TCondition), IsNullable = false)]
    public TCondition[][] savelist
    {
        get
        {
            return this.savelistField;
        }
        set
        {
            this.savelistField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Kind
    {
        get
        {
            return this.kindField;
        }
        set
        {
            this.kindField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Cmd
    {
        get
        {
            return this.cmdField;
        }
        set
        {
            this.cmdField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string WaitTime
    {
        get
        {
            return this.waitTimeField;
        }
        set
        {
            this.waitTimeField = value;
        }
    }
}
