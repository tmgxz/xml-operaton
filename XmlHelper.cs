using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace ConsoleApp_XMLOperation
{
    public class XmlHelper
    {
        /// <summary>
        /// Xml文档
        /// 可以调用LoadXml方法，从xml字符串加载XmlDocument文档
        /// </summary>
        public XmlDocument XmlDoc {get;set;}
        /// <summary>
        /// 文档存储路径
        /// </summary>
        private string filePath = string.Empty;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">文档存储路径</param>
        public XmlHelper(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("参数不能为空或null", "filePath");
            }

            this.XmlDoc = new XmlDocument();
            this.filePath = filePath;
            this.XmlDoc.Load(this.filePath);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inStream">文件流</param>
        public XmlHelper(Stream inStream)
        {
            this.XmlDoc = new XmlDocument();
            if (inStream==null)
            {
                throw new ArgumentException("参数不能为null", "inStream");
            }
            this.XmlDoc.Load(inStream);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txtReader">字符串读取器</param>
        public XmlHelper(TextReader txtReader)
        {
            this.XmlDoc = new XmlDocument();
            if (txtReader == null)
            {
                throw new ArgumentException("参数不能为null", "txtReader");
            }
            this.XmlDoc.Load(txtReader);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txtReader">xml读取器</param>
        public XmlHelper(XmlReader reader)
        {
            this.XmlDoc = new XmlDocument();
            if (reader == null)
            {
                throw new ArgumentException("参数不能为null", "reader");
            }
            this.XmlDoc.Load(reader);
        }
        #endregion

        #region 加载XmlDocument
        /// <summary>
        /// 从指定字符串加载文档
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <returns></returns>
        public XmlDocument LoadXml(string xml)
        {
            XmlDocument xmlDocTemp = new XmlDocument();
            xmlDocTemp.LoadXml(xml);
            return xmlDocTemp;
        }

        #endregion

        #region 查询
        /// <summary>
        /// 得到xml节点
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="xmlnsManager"></param>
        /// <returns></returns>
        public XmlNode GetSingleNode(string xpath, XmlNamespaceManager xmlnsManager = null)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }
            return XmlDoc.SelectSingleNode(xpath, xmlnsManager);
        }
        /// <summary>
        /// 获得xml节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xpath"></param>
        /// <param name="xmlnsManager"></param>
        /// <returns></returns>
        public XmlNode GetSingleNode(XmlNode node, string xpath, XmlNamespaceManager xmlnsManager = null)
        {
            if (node == null)
            {
                throw new ArgumentException("参数不能为null", "rootNode");
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }
            
            return xmlnsManager == null ? node.SelectSingleNode(xpath) : node.SelectSingleNode(xpath, xmlnsManager);
        }

        /// <summary>
        /// 得到NodeList
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xpath"></param>
        /// <param name="xmlnsManager"></param>
        /// <returns></returns>
        public XmlNodeList GetNodeList(XmlNode node, string xpath, XmlNamespaceManager xmlnsManager = null)
        {
            if (node == null)
            {
                throw new ArgumentException("参数不能为null", "rootNode");
            }

            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            return node.SelectNodes(xpath, xmlnsManager);

        }

        /// <summary>
        /// 获得xml节点内容
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="xpath"></param>
        /// <param name="xmlnsManager"></param>
        /// <returns></returns>
        public string GetXmlNodeText(XmlNode rootNode, string xpath, XmlNamespaceManager xmlnsManager = null)
        {
            string ret = string.Empty;
            XmlNode childNode = GetSingleNode(rootNode, xpath, xmlnsManager);
            if (childNode != null)
            {
                ret = childNode.InnerText;
            }

            return ret;
        }

        /// <summary>
        /// 获得给定Xpath指定的节点属性值
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="attributeName"></param>
        /// <param name="nsmgr"></param>
        /// <returns></returns>
        public string GetAttributeValue(string xpath, string attributeName, XmlNamespaceManager nsmgr=null)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            XmlElement  element = XmlDoc.DocumentElement;
            XmlNode node = element.SelectSingleNode(xpath, nsmgr);
            if (node == null)
            {
                return null;
            }

            XmlAttribute attribute = node.Attributes[attributeName];
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }

        /// <summary>
        /// 获得给定Xpath指定的节点属性值
        /// </summary>
        /// <param name="rootNode">根节点</param>
        /// <param name="xpath">相对于根节点的路径</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="nsmgr"></param>
        /// <returns></returns>
        public string GetAttributeValue(XmlNode rootNode, string xpath, string attributeName, XmlNamespaceManager nsmgr=null)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            XmlNode node = rootNode.SelectSingleNode(xpath, nsmgr);
            if (node == null)
            {
                return null;
            }

            XmlAttribute attribute = node.Attributes[attributeName];
            if (attribute == null)
            {
                return null;
            }
            return attribute.Value;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加根节点的子节点
        /// </summary>
        /// <param name="xmlNode"></param>
        public void AppendNode(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentException("参数不能为null", "xmlNode");
            }

            //导入节点
            XmlNode node = XmlDoc.ImportNode(xmlNode, true);
            XmlElement element = XmlDoc.DocumentElement;
            
            //将节点插入到根节点下
            element.AppendChild(node);
        }

        /// <summary>
        /// 添加节点为指定节点的子节点
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="newNodeName"></param>
        /// <param name="innerText"></param>
        public void AddNodeText(string xpath, string newNodeName, string innerText)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            //新建节点
            XmlElement xelement = XmlDoc.CreateElement(newNodeName);
            xelement.InnerText = innerText;
            //添加到指定节点的子节点列表
            XmlDoc.SelectSingleNode(xpath).AppendChild(xelement);
            using (XmlTextWriter writer = new XmlTextWriter(this.filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                XmlDoc.WriteContentTo(writer);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改某节点内容
        /// </summary>
        /// <param name="xpath"></param>
        /// <param name="newInnerText"></param>
        public void ModifyNodeText(string xpath, string newInnerText)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }
            //找到节点
            XmlNode node = XmlDoc.SelectSingleNode(xpath);
            if (node == null)
            {
                return;
            }
            node.InnerText = newInnerText;
            using (XmlTextWriter writer = new XmlTextWriter(this.filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                XmlDoc.WriteContentTo(writer);
            }
        }
        /// <summary>
        /// 修改某节点内容
        /// </summary>
        /// <param name="nodeModified">被修改节点</param>
        /// <param name="newInnerText"></param>
        public void ModifyNodeText(XmlNode nodeModified, string newInnerText)
        {
            if (nodeModified==null)
            {
                throw new ArgumentException("参数不能为null", "nodeModified");
            }

            nodeModified.InnerText = newInnerText;
            using (XmlTextWriter writer = new XmlTextWriter(this.filePath, null))
            {
                writer.Formatting = Formatting.Indented;
                XmlDoc.WriteContentTo(writer);
            }
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xpath"></param>
        public void RemoveNode(string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            //获取要删除的节点
            XmlNode node = XmlDoc.SelectSingleNode(xpath);
            if (node == null)
            {
                return;
            }
            XmlElement element = XmlDoc.DocumentElement;
            //删除节点
            element.RemoveChild(node);

            XmlDoc.Save(this.filePath);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="rootNode">指定的根节点</param>
        /// <param name="xpath">相对于根节点的路径</param>
        public void RemoveNode(XmlNode rootNode,string xpath)
        {
            if (string.IsNullOrWhiteSpace(xpath))
            {
                throw new ArgumentException("参数不能为空或null", "xpath");
            }

            //获取要删除的节点
            XmlNode node = rootNode.SelectSingleNode(xpath);
            if (node == null)
            {
                return;
            }
            XmlElement element = (XmlElement)rootNode;
            //删除节点
            element.RemoveChild(node);

            XmlDoc.Save(this.filePath);
        }
        #endregion


        #region XML文档与字符串互转
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="path">文档路径</param>
        /// <returns></returns>
        public static string ToString(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.InnerXml;
        }
        /// <summary>
        /// 转换为XmlDocument对象
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static XmlDocument ToXmlDocument(string xmlString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlString);
            return doc;
        }
        #endregion

        #region 序列化与反序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Serializer<T>(T entity) 
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                //序列化写入stream
                xml.Serialize(stream, entity);
                //上一步操作流，完成时流中当前位置为写入数据的长度，所以要置0
                stream.Position = 0;
                //utf8编码防止中文乱码
                using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(stream);
            }
        }  
        #endregion

        #region 用XSLT转换XML文档
        /// <summary>
        /// 转换XML文档
        /// 保存到指定路径
        /// </summary>
        /// <param name="xmlUri"></param>
        /// <param name="xslUri"></param>
        /// <param name="newXmlUri"></param>
        /// <param name="enableDebug"></param>
        public static void Transform(string xmlUri, string xslUri, string newXmlUri,bool enableDebug = false)
        {
            XslCompiledTransform xslTrans = new XslCompiledTransform(enableDebug);
            xslTrans.Load(xslUri);
            xslTrans.Transform(xmlUri, newXmlUri);
        }
        /// <summary>
        /// 转换XML文档
        /// </summary>
        /// <param name="xmlUri">xml文档路径</param>
        /// <param name="xslUri">xslt文档路径</param>
        /// <param name="xsltArguments"></param>
        /// <param name="enableDebug"></param>
        /// <returns>
        /// xml字符串
        /// </returns>
        public static string Transform(string xmlUri, string xslUri, XsltArgumentList xsltArguments, bool enableDebug = false)
        {
            using (StringWriter sw = new StringWriter())
            {
                XslCompiledTransform xslTrans = new XslCompiledTransform(enableDebug);
                xslTrans.Load(xslUri);
                xslTrans.Transform(xmlUri, xsltArguments, sw);
                return sw.ToString();
            }
        }
        /// <summary>
        /// 转换XML文档
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xslUri"></param>
        /// <param name="xsltArguments"></param>
        /// <param name="enableDebug"></param>
        /// <returns>
        ///  xml字符串
        /// </returns>
        public static string Transform(XmlDocument xmlDoc, string xslUri, XsltArgumentList xsltArguments, bool enableDebug = false)
        {
            using(StringWriter sw = new StringWriter())
            {
                XslCompiledTransform xslTrans = new XslCompiledTransform(enableDebug);
                xslTrans.Load(xslUri);
                xslTrans.Transform(xmlDoc.CreateNavigator(), xsltArguments, sw);
                return sw.ToString();
            }
        }
        /// <summary>
        /// 转换XML文档
        /// </summary>
        /// <param name="xmlUri"></param>
        /// <param name="xslReader"></param>
        /// <param name="xsltArguments"></param>
        /// <param name="enableDebug"></param>
        /// <returns>
        /// xml字符串
        /// </returns>
        public static string Transform(string xmlUri, XmlReader xslReader, XsltArgumentList xsltArguments, bool enableDebug = false)
        {
            using (StringWriter sw = new StringWriter())
            {
                XslCompiledTransform xslTrans = new XslCompiledTransform(enableDebug);
                xslTrans.Load(xslReader);
                xslTrans.Transform(xmlUri, xsltArguments, sw);
                return sw.ToString();
            }
        }

        #endregion 
    }
}
