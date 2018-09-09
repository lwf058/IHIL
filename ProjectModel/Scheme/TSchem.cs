using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Scheme
{
    public class TSchem
    {
        [XmlElement("setCanlist")]
        public SetCanlist setCanlist;
        [XmlElement("setEthList")]
        public setEthList SetEthList;
        [XmlElement("stepList")]
        public stepList StepList;
    }
    public class TSetCan
    {
        [XmlAttribute("Check")]
        public string Check;
        [XmlAttribute("AgreeMentFile")]
        public string AgreeMentFile;
        [XmlAttribute("Baut")]
        public string Baut;
        [XmlAttribute("Alias")]
        public string Alias;
    }
    public class SetCanlist
    {
        [XmlElement("TSetCan")]
        public List<TSetCan> TSetCans;
    }
    public class TSetEth
    {
        [XmlAttribute("Check")]
        public string Check;
        [XmlAttribute("IP")]
        public string IP;
        [XmlAttribute("Port")]
        public string Port;
        [XmlAttribute("AgreeMentFile")]
        public string AgreeMentFile;
        [XmlAttribute("Alias")]
        public string Alias;
    }
    public class setEthList
    {
        [XmlElement("TSetEth")]
        public List<TSetEth> TSetEths;
    }
    public class subCondition
    {
        [XmlAttribute("IsParent")]
        public string IsParent { get; set; }
        [XmlAttribute("VarName")]
        public string VarName { get; set; }
        [XmlAttribute("VarCaption")]
        public string VarCaption { get; set; }
        [XmlAttribute("Con")]
        public string Con { get; set; }
        [XmlAttribute("ConValue")]
        public string ConValue { get; set; }
        [XmlAttribute("Unit")]
        public string Unit { get; set; }
    }
    public class TCondition
    {
        [XmlAttribute("IsParent")]
        public string IsParent { get; set; }
        [XmlAttribute("VarName")]
        public string VarName { get; set; }
        [XmlAttribute("VarCaption")]
        public string VarCaption { get; set; }
        [XmlAttribute("Con")]
        public string Con { get; set; }
        [XmlAttribute("ConValue")]
        public string ConValue { get; set; }
        [XmlAttribute("Unit")]
        public string Unit { get; set; }
        [XmlElement("subCondition")]
        public List<subCondition> subConditions { get; set; }
    }
    public class judgelist
    {
        [XmlElement("TCondition")]
        public List<TCondition> tconditions { get; set; }
    }

    public class savelist
    {
        [XmlElement("TCondition")]
        public List<TCondition> TConditions;
    }
    public class initlist
    {
        [XmlElement("TCondition")]
        public List<TCondition> TConditions;
    }
    public class setlist
    {
        [XmlElement("TCondition")]
        public List<TCondition> TConditions;
    }
    public class TCMD
    {
        [XmlElement("initlist")]
        public initlist Initlist;
        [XmlElement("setlist")]
        public setlist Setlist;
        [XmlElement("judgelist")]
        public judgelist Judgelist;
        [XmlElement("savelist")]
        public savelist Savelist;
        [XmlAttribute("Kind")]
        public string Kind;
        [XmlAttribute("Cmd")]
        public string Cmd{ get; set; }
        [XmlAttribute("WaitTime")]
        public string WaitTime{ get; set; }
    }
    public class cmdList
    {
        [XmlElement("TCMD")]
        public List<TCMD> TCMDs;
    }
    public class TStep
    {
        [XmlAttribute("testid")]
        public string testid { get; set; }
        [XmlAttribute("title")]
        public string title{ get; set; }
        [XmlAttribute("kind")]
        public string kind;
        [XmlAttribute("Check")]
        public string Check;
        [XmlElement("cmdList")]
        public cmdList CmdList;
    }
    public class stepList
    {
        [XmlElement("TStep")]
        public List<TStep> TSteps;
    }
    public class test
    {
        public string testid { get; set; }
        public string title { get; set; }
    }
}
