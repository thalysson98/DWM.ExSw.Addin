using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows.Forms;
using DWM.ExSw.Addin.Properties;
using System.Windows.Media.Media3D;

namespace DWM.ExSw.Addin.Base
{
    internal class DataMaterial
    {
         public List<MaterialInfo> DataMaterial_load()
        {
            List<MaterialInfo> materials = new List<MaterialInfo>();
            string filePath ;
            filePath = Settings.Default.XMLMaterial;
            filePath = "C:\\Cardall\\ASSISTENTES\\configuracoesSW\\Materiais\\Cardall.sldmat";

            for (int i = 0; filePath.Length> i; i++)
            {
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show("O arquivo XML não foi encontrado.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                XDocument doc = XDocument.Load(filePath);
                if (doc != null)
                {
                    List<MaterialInfo> newMaterials = ExtractMaterialInfo(doc);
                    materials.AddRange(newMaterials);
                }

            }
            return materials;
        }
        public List<string[]> lista_material(DataMaterial banco)
        {
            List<string[]> val = new List<string[]>();
            foreach (MaterialInfo material in banco.DataMaterial_load())
            {
                ListViewItem item = new ListViewItem(material.Name);
                item.SubItems.Add(material.Description);
                item.SubItems.Add(material.Density);
                string[] var = new string[3];
                int i = 0;
                foreach (CustomProperty prop in material.CustomProperties)
                {
                    item.SubItems.Add(prop.Value);
                    switch (i)
                    {
                        case 0:
                            var[0] = prop.Value; break;
                        case 1:
                            var[1] = prop.Value; break;
                        case 2:
                            var[2] = prop.Value; break;
                        default: break;
                    }
                    i++;
                }
                val.Add(new string[] { material.Name, material.Description, material.Density, var[0], var[1], var[2] });
            }
            return val;
        }
        private List<MaterialInfo> ExtractMaterialInfo(XDocument doc)
        {
            List<MaterialInfo> materials = new List<MaterialInfo>();

            foreach (XElement materialElement in doc.Descendants("material"))
            {
                MaterialInfo material = new MaterialInfo
                {
                    Name = materialElement.Attribute("name")?.Value,
                    Description = materialElement.Attribute("description")?.Value,
                    Density = materialElement.Element("physicalproperties")?.Element("DENS")?.Attribute("value")?.Value
                    
                };

                // Extrair informações do elemento <custom>
                XElement customElement = materialElement.Element("custom");
                if (customElement != null)
                {
                    bool isElement = false;
                    foreach (XElement propElement in customElement.Elements("prop"))
                    {
                        string propName = propElement.Attribute("name")?.Value;
                        string propValue = propElement.Attribute("value")?.Value;
                        isElement = true;
                        // Adicione as informações do <prop> à lista de propriedades personalizadas
                        material.CustomProperties.Add(new CustomProperty
                        {
                            Name = propName,
                            Value = propValue
                        });
                    }
                    if (!isElement)
                    {
                        for(int i = 0; i < 3; i++)
                        {
                            material.CustomProperties.Add(new CustomProperty
                            {
                                Name = "",
                                Value = ""
                            });
                        }
                    }
                }

                materials.Add(material);
            }

            return materials;
        }
    }
    public class MaterialInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Density { get; set; }
        public List<CustomProperty> CustomProperties { get; set; } = new List<CustomProperty>();
    }
    public class CustomProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    
}
