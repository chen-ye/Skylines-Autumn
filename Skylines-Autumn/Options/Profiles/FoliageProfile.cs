﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace DynamicFoliage.OptionsSpace.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

namespace DynamicFoliage.Profiles
{
}

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class FoliageProfileContainer {
    
    private FoliageProfileContainerProfiles[] itemsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Profiles", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FoliageProfileContainerProfiles[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class FoliageProfileContainerProfiles {
    
    private FoliageProfileContainerProfilesProfile[] profileField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Profile", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FoliageProfileContainerProfilesProfile[] Profile {
        get {
            return this.profileField;
        }
        set {
            this.profileField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class FoliageProfileContainerProfilesProfile {
    
    private FoliageProfileContainerProfilesProfileDefaultParameters[] defaultParametersField;
    
    private FoliageProfileContainerProfilesProfileStaticAnnualVerdance[] staticAnnualVerdanceField;
    
    private string profileNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("DefaultParameters", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FoliageProfileContainerProfilesProfileDefaultParameters[] DefaultParameters {
        get {
            return this.defaultParametersField;
        }
        set {
            this.defaultParametersField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("StaticAnnualVerdance", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FoliageProfileContainerProfilesProfileStaticAnnualVerdance[] StaticAnnualVerdance {
        get {
            return this.staticAnnualVerdanceField;
        }
        set {
            this.staticAnnualVerdanceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ProfileName {
        get {
            return this.profileNameField;
        }
        set {
            this.profileNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class FoliageProfileContainerProfilesProfileDefaultParameters {
    
    private string seaLevelField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string SeaLevel {
        get {
            return this.seaLevelField;
        }
        set {
            this.seaLevelField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class FoliageProfileContainerProfilesProfileStaticAnnualVerdance {
    
    private FoliageProfileContainerProfilesProfileStaticAnnualVerdanceMeasure[] measureField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Measure", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public FoliageProfileContainerProfilesProfileStaticAnnualVerdanceMeasure[] Measure {
        get {
            return this.measureField;
        }
        set {
            this.measureField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class FoliageProfileContainerProfilesProfileStaticAnnualVerdanceMeasure {
    
    private string dateField;
    
    private float verdanceField;
    
    private bool verdanceFieldSpecified;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string Date {
        get {
            return this.dateField;
        }
        set {
            this.dateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public float Verdance {
        get {
            return this.verdanceField;
        }
        set {
            this.verdanceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool VerdanceSpecified {
        get {
            return this.verdanceFieldSpecified;
        }
        set {
            this.verdanceFieldSpecified = value;
        }
    }
}
