using DWM.ExSw.Addin.DataSRV;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DWM.ExSw.Addin.Base
{
    class ValidateProperty
    {
        #region Variaveis

        // Usar nomes mais descritivos e convenções C# (camelCase para campos privados se não for usar _prefixo)
        private readonly SwSpecialCommands _swCommands = new SwSpecialCommands(); // Renomeado e readonly se não for reatribuído
        private readonly StringCorrection _stringManager = new StringCorrection(); // Renomeado
        private cardallData _cardallDatabase; // Renomeado
        private List<string[]> _xmlMaterials; // Renomeado

        // 'config' não é usado nesta classe. Se for usado em outro lugar ou pretendido,
        // deve ser inicializado ou removido.
        // string[] config;

        #endregion

        public void LoadData(cardallData data, List<string[]> dataXml)
        {
            _cardallDatabase = data;
            _xmlMaterials = dataXml;
        }

        public bool IsCommercialPart(ModelDoc2 model) // Nome mais descritivo e em PascalCase
        {
            if (model == null) return false; // Adicionar verificação de nulo

            // 'varOut' não é usado, pode ser descartado se sw_GetCustomProperty permitir
            // ou usar um nome de descarte como '_' se a assinatura do método exigir um out.
            string propertyValue = _swCommands.sw_GetCustomProperty("Soldagem", model, "", out _);
            return propertyValue == "Sim";
        }

        #region Codigo

        // A lógica deste método é complexa e os códigos de erro (err) podem ser difíceis de gerenciar.
        // Considerar refatorar para maior clareza ou usar um enum para os códigos de erro.
        public string ValidateAndGetCode(ModelDoc2 model, string inputText, out int errorCode) // Nome e parâmetros mais claros
        {
            // Padrões Regex (considerar torná-los const static readonly se não mudarem)
            // Adicionado '$' ao final de PadraoComercial para correspondência exata se o '$' for literal.
            // Se o '$' não for literal, o padrão original @"^\d{6}$" está correto.
            const string StandardCodePattern = @"^\d{3}\.\d{3}\.\d{4}$";
            const string FictitiousCodePattern = @"^F\d{2}\.\d{3}\.\d{4}$";
            const string CommercialCodePattern = @"^\d{6}\$$"; // Ex: "123456$" - o '$' é um caractere literal

            errorCode = 0; // 0: OK, 1: Novo arquivo/código do título, 2: Input diferente da prop/código inválido, 3: Input diferente do nome do arquivo
            string currentCodeProperty = GetCodeProperty(model); // Renomeado GetCodigo para GetCodeProperty
            string finalCode = currentCodeProperty;

            if (model == null)
            {
                errorCode = -1; // Código de erro para modelo inválido
                return string.Empty;
            }

            string modelTitle = System.IO.Path.GetFileNameWithoutExtension(model.GetPathName());
            if (string.IsNullOrEmpty(modelTitle))
            {
                modelTitle = model.GetTitle(); // Caso o arquivo não esteja salvo
            }


            if (!string.IsNullOrEmpty(currentCodeProperty)) // Existe a propriedade "NumeroDesenho"
            {
                if (!string.IsNullOrEmpty(inputText) && inputText != currentCodeProperty)
                {
                    finalCode = inputText;
                    // Se inputText é diferente da propriedade, ele se torna o código,
                    // mas ainda precisa ser validado contra os padrões e o nome do arquivo.
                    // O 'err = 2' original aqui parecia indicar um tipo de aviso/mudança.
                }
                // Se inputText está vazio ou igual à propriedade, finalCode permanece currentCodeProperty.
            }
            else if (string.IsNullOrEmpty(inputText)) // Propriedade "NumeroDesenho" não existe E inputText está vazio (novo arquivo?)
            {
                finalCode = modelTitle;
                errorCode = 1; // Indica que o código foi pego do título do modelo
            }
            else // Propriedade "NumeroDesenho" não existe, MAS inputText tem valor
            {
                finalCode = inputText;
            }

            // Validação crítica: o código final DEVE corresponder ao nome do arquivo (sem extensão)
            if (finalCode != modelTitle)
            {
                errorCode = 3; // Erro crítico: código não corresponde ao nome do arquivo
                // Neste ponto, a exportação/salvamento seria bloqueada, então talvez retornar string.Empty ou lançar exceção.
                return finalCode; // Ou string.Empty para indicar falha grave
            }

            // Validação do formato do código final
            if (Regex.IsMatch(finalCode, StandardCodePattern) ||
                Regex.IsMatch(finalCode, FictitiousCodePattern) ||
                Regex.IsMatch(finalCode, CommercialCodePattern))
            {
                // Se o código já era '1' (novo arquivo), e o formato é válido, mantém '1'.
                // Se era '0' e o formato é válido, mantém '0'.
                // Se houve mudança (finalCode veio de inputText e era diferente da prop),
                // e o formato é válido, talvez um código de 'aviso' seja necessário se 'err=2' era para isso.
                // Se errorCode já é 3 (nome não bate), essa validação de formato não deveria "resetar" para 0.
                if (errorCode != 3) // Só altera se não for o erro crítico de nome de arquivo
                {
                    // Se finalCode veio de inputText e era diferente da propriedade, e o formato é válido,
                    // podemos manter um código de erro/status específico se necessário.
                    // A lógica original de 'err' era um pouco confusa aqui.
                    // Se errorCode era 0 e o código é válido, continua 0.
                    // Se errorCode era 1 e o código é válido, continua 1.
                }
            }
            else // Formato inválido
            {
                if (errorCode != 3) // Não sobrescreve o erro 3
                {
                    errorCode = 2; // Formato de código inválido
                }
            }

            return finalCode;
        }

        #endregion

        #region Revisão

        public void ValidateRevision(ModelDoc2 model, string inputTextRevision, out int errorCode) // Nome mais claro
        {
            if (model == null)
            {
                errorCode = -1; // Modelo inválido
                return;
            }

            errorCode = 0; // OK
            string currentRevision = GetRevisionProperty(model); // Renomeado GetRevisao

            if (string.IsNullOrEmpty(currentRevision))
            {
                errorCode = 1; // Propriedade de revisão não encontrada/vazia
            }
            else if (inputTextRevision != currentRevision)
            {
                errorCode = 2; // Revisão do input diferente da propriedade
            }
        }

        #endregion

        #region Denominação

        public string ValidateAndFormatDescription(ModelDoc2 model, string descriptionInput) // Nome mais claro
        {
            if (model == null) return descriptionInput; // Ou string.Empty

            // Limpeza inicial da string de entrada
            string cleanedDescription = descriptionInput.Replace("=", ":").Replace("&", "").Replace("\r\n", "");

            StringBuilder resultBuilder = new StringBuilder();
            StringBuilder dimensionNameBuilder = new StringBuilder();
            bool insideQuotes = false;

            foreach (char c in cleanedDescription)
            {
                if (c == '"')
                {
                    if (insideQuotes) // Fim de uma dimensão entre aspas
                    {
                        resultBuilder.Append(GetDimensionValue(dimensionNameBuilder.ToString(), model));
                        dimensionNameBuilder.Clear();
                    }
                    insideQuotes = !insideQuotes;
                }
                else
                {
                    if (insideQuotes)
                    {
                        dimensionNameBuilder.Append(c);
                    }
                    else
                    {
                        resultBuilder.Append(c);
                    }
                }
            }

            // Caso a string termine com uma dimensão entre aspas não fechada (ou malformada)
            if (insideQuotes && dimensionNameBuilder.Length > 0)
            {
                // Decide como tratar: retornar como está, ou tentar resolver, ou sinalizar erro.
                // A lógica original adicionava "''" + nome da dimensão.
                // Aqui, vamos apenas adicionar o que foi coletado, precedido por aspas para indicar que não foi resolvido.
                resultBuilder.Append("\"").Append(dimensionNameBuilder.ToString());
            }

            return resultBuilder.ToString();
        }

        public string GetDimensionValue(string dimensionName, ModelDoc2 model) // Renomeado
        {
            if (model == null || string.IsNullOrEmpty(dimensionName))
            {
                return $"'{dimensionName}'"; // Retorna o nome entre aspas simples se não puder resolver
            }

            Dimension swDimension = (Dimension)model.Parameter(dimensionName);

            if (swDimension != null)
            {
                // Usar GetValue3 para obter o valor na configuração ativa e nas unidades do documento.
                // O segundo parâmetro de GetValue3 é a configuração (null ou "" para a ativa).
                // O terceiro é um array de nomes de configuração se quiser valores de múltiplas configs.
                object valueObj = swDimension.GetValue3((int)swInConfigurationOpts_e.swThisConfiguration, null);
                if (valueObj is double[] values && values.Length > 0)
                {
                    double value = values[0]; // O valor é retornado em metros para dimensões de comprimento
                                              // Precisaria converter para as unidades do documento se necessário,
                                              // ou usar GetSystemValue3 se quiser o valor como está no sistema.

                    // Formatação: "0.##" remove zeros finais desnecessários.
                    // Usar CultureInfo.InvariantCulture para consistência.
                    string formattedValue = value.ToString("0.##", CultureInfo.InvariantCulture);
                    return formattedValue;
                }
                return $"'{dimensionName}'"; // Não conseguiu obter o valor
            }
            else
            {
                return $"'{dimensionName}'"; // Dimensão não encontrada
            }
        }

        #endregion

        #region Obter Propriedades (Getters de Propriedades)

        public string GetCodeProperty(ModelDoc2 model) // Renomeado
        {
            if (model == null) return string.Empty;
            return _swCommands.sw_GetCustomProperty("NumeroDesenho", model, "", out _);
        }

        public string GetDescriptionProperty(ModelDoc2 model) // Renomeado GetDenominacao
        {
            // Originalmente buscava "Material", mas o nome do método era GetDenominacao.
            // Se a intenção é "Denominação", mudar a string da propriedade.
            // Se for "Material", o nome do método deveria ser GetMaterialProperty.
            // Assumindo que "Denominação" é a propriedade correta para este método.
            if (model == null) return string.Empty;
            return _swCommands.sw_GetCustomProperty("Denominação", model, "", out _); // Ou "Material"
        }

        public string GetRevisionProperty(ModelDoc2 model) // Renomeado
        {
            if (model == null) return string.Empty;
            return _swCommands.sw_GetCustomProperty("Revisão", model, "", out _);
        }

        #endregion

        #region Estrutura Lista de Corte

        // Retornar uma lista de um objeto mais fortemente tipado seria melhor que object[,]
        public class CutListItemData
        {
            public string[] Properties { get; set; } // QUANTITY, MATERIAL, COMPRIMENTO, Massa, Peso Bruto, Erro, Info
            public Feature Feature { get; set; }
        }

        public List<CutListItemData> GetCutListStructure(ModelDoc2 model) // Nome e tipo de retorno alterados
        {
            if (model == null) return new List<CutListItemData>();

            object vCutLists = _swCommands.GetCutLists(model);
            var resultList = new List<CutListItemData>();

            if (vCutLists is Array cutListsArray && cutListsArray.Length > 0)
            {
                foreach (Feature swCutListFeat in cutListsArray)
                {
                    if (swCutListFeat == null) continue;

                    CustomPropertyManager custPrpMgr = swCutListFeat.CustomPropertyManager;
                    BodyFolder swBodyFolder = swCutListFeat.GetSpecificFeature2() as BodyFolder;

                    if (custPrpMgr == null || swBodyFolder == null) continue;

                    string[] itemProperties = new string[7]; // QUANTITY, MATERIAL, COMPRIMENTO, Massa, Peso Bruto, CódigoErro, InfoAdicional

                    // Obter propriedades no nível da lista de corte
                    custPrpMgr.Get2("MATERIAL", out _, out itemProperties[1]); // MATERIAL
                    custPrpMgr.Get2("QUANTITY", out _, out itemProperties[0]); // QUANTITY
                    custPrpMgr.Get2("COMPRIMENTO", out _, out itemProperties[2]); // COMPRIMENTO
                    custPrpMgr.Get2("Massa", out _, out itemProperties[3]);    // Massa
                    custPrpMgr.Get2("Peso Bruto", out _, out itemProperties[4]); // Peso Bruto

                    // Processamento de cada corpo DENTRO da lista de corte
                    // A lógica original parecia processar todos os corpos mas só usar o resultado do último
                    // para 'err' e 'Val2[6]' (info).
                    // Se a intenção é agregar ou processar individualmente, a lógica precisa mudar.
                    // Assumindo por agora que as propriedades principais são do CutListFeat,
                    // e o processamento do corpo é para validação/cálculo específico.

                    int overallErrorCode = 0;
                    string aggregatedInfo = string.Empty;
                    bool firstBody = true;

                    object[] bodies = swBodyFolder.GetBodies() as object[];
                    if (bodies != null)
                    {
                        foreach (object bodyObj in bodies)
                        {
                            Body2 swBody = bodyObj as Body2;
                            if (swBody == null) continue;

                            string materialName = itemProperties[1]; // Material da lista de corte
                            string bodyProcessingInfo;
                            int bodyErrorCode;

                            if (materialName != "SAE 1020") // Hardcoded?
                            {
                                bool materialIsValid = IsMaterialValid(materialName, out string unit1, out string unit2, out string weightPerUnit);
                                if (materialIsValid)
                                {
                                    bodyErrorCode = ValidateBodyProperties(custPrpMgr, swBody.IsSheetMetal(), unit1, unit2, weightPerUnit, out bodyProcessingInfo, true);
                                }
                                else
                                {
                                    bodyErrorCode = 3; // Código de erro para material inválido
                                    bodyProcessingInfo = "<ERRO#MaterialInválido>";
                                }
                            }
                            else
                            {
                                bodyErrorCode = 3; // Código de erro para SAE 1020 (conforme original)
                                bodyProcessingInfo = "<info#002>"; // Mensagem original para SAE 1020
                            }

                            if (bodyErrorCode > overallErrorCode) overallErrorCode = bodyErrorCode; // Pega o maior erro
                            if (!firstBody) aggregatedInfo += "; ";
                            aggregatedInfo += $"Corpo: {swBody.Name} ({bodyProcessingInfo})";
                            firstBody = false;
                        }
                    }

                    itemProperties[5] = overallErrorCode.ToString(); // Código de Erro agregado
                    itemProperties[6] = aggregatedInfo; // Informação agregada dos corpos

                    // Substituir vírgulas por pontos para consistência (se necessário para processamento posterior)
                    // Usar CultureInfo.InvariantCulture para parsing/formatting é geralmente mais robusto.
                    itemProperties[2] = itemProperties[2]?.Replace(",", "."); // COMPRIMENTO
                    itemProperties[3] = itemProperties[3]?.Replace(",", "."); // Massa
                    itemProperties[4] = itemProperties[4]?.Replace(",", "."); // Peso Bruto

                    resultList.Add(new CutListItemData { Properties = itemProperties, Feature = swCutListFeat });
                }
            }
            return resultList;
        }

        private int ValidateBodyProperties(CustomPropertyManager cutlistCustPropMgr, bool isSheetMetal, string unit1, string unit2, string weightFromTable, out string info, bool materialIsInTable) // Renomeado
        {
            // Este método é muito complexo. Seria ideal quebrá-lo em partes menores.
            // A lógica de cálculo do "Peso Bruto" e "COMPRIMENTO" parece ser o foco principal.

            info = "";
            int errorCode = 0; // 0 = OK, 1 = Aviso/Modificado, 3 = Erro Crítico

            if (cutlistCustPropMgr == null)
            {
                info = "<ERRO#PropMgrNulo>";
                return 3;
            }

            string currentCalculatedWeight = "";
            _swCommands.sw_GetAllvaluesProperty("Peso Bruto", cutlistCustPropMgr, out _, out currentCalculatedWeight);

            if (materialIsInTable)
            {
                if (isSheetMetal)
                {
                    #region Lógica para Chapa Metálica
                    _swCommands.sw_GetAllvaluesProperty("Largura da Caixa delimitadora", cutlistCustPropMgr, out string widthDisplay, out string widthValue);
                    _swCommands.sw_GetAllvaluesProperty("Comprimento da Caixa delimitadora", cutlistCustPropMgr, out string lengthDisplay, out string lengthValue);
                    _swCommands.sw_GetAllvaluesProperty("Espessura da Chapa metálica", cutlistCustPropMgr, out string thicknessDisplay, out string thicknessValue);
                    _swCommands.sw_GetAllvaluesProperty("Tolerância da dobra", cutlistCustPropMgr, out string bendAllowanceDisplay, out string bendAllowanceValue);
                    _swCommands.sw_GetAllvaluesProperty("Raio de dobra", cutlistCustPropMgr, out string bendRadiusDisplay, out string bendRadiusValue);

                    info = $"#{thicknessValue} K{bendAllowanceValue} R{bendRadiusValue}"; // Formato original da info

                    if (!IsPropertyEdited(cutlistCustPropMgr)) // Verifica se o valor é editado
                    {
                        if (unit1 == "KG" && unit2 == "M2")
                        {
                            if (double.TryParse(widthValue.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dWidth) &&
                                double.TryParse(lengthValue.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dLength) &&
                                double.TryParse(weightFromTable.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dWeightTable))
                            {
                                double widthMeters = dWidth / 1000.0;
                                double lengthMeters = dLength / 1000.0;
                                double calculatedGrossWeight = widthMeters * lengthMeters * dWeightTable;

                                _swCommands.sw_DeleteProperty(cutlistCustPropMgr, "Peso Bruto");
                                _swCommands.SW_AddProperty(cutlistCustPropMgr, "Peso Bruto", calculatedGrossWeight.ToString("0.00", CultureInfo.InvariantCulture));

                                if (currentCalculatedWeight != calculatedGrossWeight.ToString("0.00", CultureInfo.InvariantCulture)) errorCode = 1;

                                if (widthMeters > 1.2 || lengthMeters > 6.0 || widthMeters > 6.0 || lengthMeters > 1.2)
                                {
                                    errorCode = 1; // Ou 3 se for um erro crítico
                                    info = "<info#001_MedidaForaPadrao>";
                                }

                                _swCommands.sw_DeleteProperty(cutlistCustPropMgr, "COMPRIMENTO");
                                _swCommands.SW_AddProperty(cutlistCustPropMgr, "COMPRIMENTO", $"{widthDisplay} X {lengthDisplay}");
                            }
                            else
                            {
                                errorCode = 3; info = "<ERRO#ParseChapaKG_M2>";
                            }
                        }
                        else if (unit1 == "M" || unit2 == "M") // Unidade linear para chapa? Parece inconsistente.
                        {
                            info = "<ERRO#104_UnidadeIncompativelChapa>";
                            errorCode = 3;
                        }
                        else // Outras unidades para chapa (não KG/M2)
                        {
                            _swCommands.sw_DeleteProperty(cutlistCustPropMgr, "COMPRIMENTO");
                            _swCommands.sw_DeleteProperty(cutlistCustPropMgr, "Peso Bruto");
                            _swCommands.SW_AddProperty(cutlistCustPropMgr, "COMPRIMENTO", $"{widthDisplay} X {lengthDisplay}");
                            _swCommands.SW_AddProperty(cutlistCustPropMgr, "Peso Bruto", "0.00");
                            // Não define 'err = 1; info = "<info#003>";' a menos que haja uma razão específica
                        }
                    }
                    else // Propriedade "EDITADO" = "SIM"
                    {
                        // A lógica original para COMPRIMENTO quando é chapa e editado:
                        // Verifica se o formato do COMPRIMENTO bate com M ou M2.
                        // Isso parece estranho, pois se é editado, o valor já estaria lá.
                        // A intenção aqui é validar o formato do campo "COMPRIMENTO" editado?
                        _swCommands.sw_GetAllvaluesProperty("COMPRIMENTO", cutlistCustPropMgr, out _, out string currentComprimentoValue);
                        const string m2Pattern = @"^\d+(\.\d+)?\s*X\s*\d+(\.\d+)?$"; // Permite espaço opcional ao redor do X
                        const string mPattern = @"^\d+(\.\d+)?$";

                        bool formatOk = false;
                        if (unit1 == "M" || unit2 == "M") formatOk = Regex.IsMatch(currentComprimentoValue, mPattern);
                        else if (unit1 == "M2" || unit2 == "M2") formatOk = Regex.IsMatch(currentComprimentoValue, m2Pattern);
                        else formatOk = true; // Se não for M ou M2, assume que o formato é aceitável ou não validável aqui

                        if (!formatOk)
                        {
                            // A lógica original adicionava "0 X 0" e retornava erro.
                            // _swCommands.SW_AddProperty(cutlistCustPropMgr, "COMPRIMENTO", "0 X 0"); // Cuidado ao sobrescrever valor editado
                            info = "<ERRO#104_FormatoEditadoInvalido>";
                            errorCode = 3;
                        }
                        else
                        {
                            errorCode = 1; // Indica que foi editado, mas o formato está OK (ou não checado)
                            info = "<info#003_Editado>";
                        }
                    }
                    #endregion
                }
                else // Não é Chapa Metálica (Perfis, etc.)
                {
                    #region Lógica para Não Chapa Metálica
                    if (unit1 == "KG" && unit2 == "M") // Ex: Perfil vendido por KG/M
                    {
                        _swCommands.sw_GetAllvaluesProperty("COMPRIMENTO", cutlistCustPropMgr, out _, out string lengthValue); // Valor numérico do comprimento
                        // currentCalculatedWeight já foi lido no início do método.

                        if (!string.IsNullOrEmpty(lengthValue) &&
                            double.TryParse(lengthValue.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dLength) &&
                            double.TryParse(weightFromTable.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out double dWeightTable))
                        {
                            double lengthMeters = dLength / 1000.0; // Assumindo que COMPRIMENTO está em mm
                            double calculatedGrossWeight = lengthMeters * dWeightTable;

                            if (!IsPropertyEdited(cutlistCustPropMgr)) // Só atualiza se não for editado
                            {
                                _swCommands.sw_DeleteProperty(cutlistCustPropMgr, "Peso Bruto");
                                _swCommands.SW_AddProperty(cutlistCustPropMgr, "Peso Bruto", calculatedGrossWeight.ToString("0.00", CultureInfo.InvariantCulture));
                                if (currentCalculatedWeight != calculatedGrossWeight.ToString("0.00", CultureInfo.InvariantCulture)) errorCode = 1;
                            }


                            if (lengthMeters > 6.0) // Exemplo de validação de comprimento
                            {
                                if (errorCode == 0) errorCode = 1; // Se não houve outro erro, marca como aviso
                                info = "<info#002_ComprimentoPerfilExcedido>";
                            }
                            if (string.IsNullOrEmpty(weightFromTable) || dWeightTable == 0) // Peso da tabela zerado/inválido
                            {
                                errorCode = 3;
                                info = "<ERRO#103_PesoTabelaInvalido>";
                            }
                        }
                        else
                        {
                            errorCode = 3; info = "<ERRO#ParsePerfilKG_M>";
                        }
                    }
                    else if (unit1 == "PC") // Peça comprada
                    {
                        errorCode = 0; // Geralmente não há cálculo de peso bruto para PC, a menos que seja regra específica
                        info = "<Info#PeçaComprada>";
                    }
                    else // Outras combinações de unidades para não-chapa
                    {
                        _swCommands.sw_GetAllvaluesProperty("Denominação", cutlistCustPropMgr, out _, out string denominationValue);
                        if (string.IsNullOrEmpty(denominationValue))
                        {
                            errorCode = 3;
                            info = "<ERRO#101_DenominacaoVazia>";
                        }
                        // Outras validações podem ser necessárias aqui
                    }
                    #endregion
                }
            }
            else // Material não está na tabela (XML_MATERIAIS)
            {
                _swCommands.sw_GetAllvaluesProperty("Denominação", cutlistCustPropMgr, out _, out string denominationValue);
                if (string.IsNullOrEmpty(denominationValue)) // Verifica se é multicorpo (originalmente)
                {
                    // Significa que não é multicorpo e não está no XML
                    errorCode = 3;
                    info = "<ERRO#102_MaterialNaoEmTabela_SemDenominacao>";
                }
                // Se tem denominação mas não está na tabela, pode ser um erro ou um item a ser cadastrado.
                // A lógica original não tratava este caso explicitamente para 'err'.
            }

            // A linha 'custPrpMgr.LinkProperty("Massa", true);' pode causar problemas se chamada repetidamente
            // ou se a propriedade já estiver vinculada. Geralmente, isso é feito uma vez.
            // Se a intenção é garantir que a propriedade de massa do SW esteja atualizada e usada,
            // pode ser melhor ler a massa diretamente do Body2.MassProperties2.
            // ModelDoc2 partModel = swBody.GetModelDoc2(); // Se precisar do modelo da peça do corpo
            // IMassProperty mp = partModel.Extension.CreateMassProperty();
            // mp.UserAssignedMass = ... ou mp.Density = ...
            // double mass = mp.Mass;

            return errorCode;
        }

        private bool IsPropertyEdited(CustomPropertyManager custPrpMgr) // Renomeado
        {
            if (custPrpMgr == null) return false;
            _swCommands.sw_GetAllvaluesProperty("EDITADO", custPrpMgr, out _, out string editStatus);
            return editStatus.Trim().ToUpperInvariant() == "SIM";
        }

        private bool IsMaterialValid(string propertyMaterial, out string unit1, out string unit2, out string weight) // Renomeado
        {
            unit1 = string.Empty;
            unit2 = string.Empty;
            weight = string.Empty;

            if (string.IsNullOrEmpty(propertyMaterial) || _cardallDatabase == null || _xmlMaterials == null)
            {
                return false;
            }

            // A função ValTipoCod original usava Substring em loop, o que é problemático com Regex ^$.
            // Assumindo que ValTipoCod agora valida a string inteira.
            int materialType = DetermineMaterialType(propertyMaterial, out string extractedCodeOrMaterial); // Nova função auxiliar

            if (materialType == 0) // Código Padrão ou Fictício (do banco de dados)
            {
                if (string.IsNullOrEmpty(extractedCodeOrMaterial)) return false;

                string codePart = extractedCodeOrMaterial.Split(' ')[0];
                string descriptionPart = string.Join(" ", extractedCodeOrMaterial.Split(' ').Skip(1));

                string dbDescriptionFull = _cardallDatabase.GetCodigoDesc(codePart); // Assume que retorna "CODIGO DESC"
                if (string.IsNullOrEmpty(dbDescriptionFull)) return false;

                string dbCodePart = dbDescriptionFull.Split(' ')[0];
                string dbDescriptionPart = string.Join(" ", dbDescriptionFull.Split(' ').Skip(1));

                return codePart == dbCodePart && descriptionPart == dbDescriptionPart;
            }
            else if (materialType == 1) // Código Comercial (do XML e depois banco para unidades)
            {
                if (string.IsNullOrEmpty(extractedCodeOrMaterial)) return false;

                // Encontra no XML_MATERIAIS. var[0]=Material, var[1]=?, var[2]=?, var[3]=UN1_XML, var[4]=UN2_XML, var[5]=PESO_XML
                string[] xmlEntry = _xmlMaterials.FirstOrDefault(m => m != null && m.Length > 0 && m[0].StartsWith(extractedCodeOrMaterial));

                if (xmlEntry != null && xmlEntry.Length >= 6)
                {
                    // Encontra no banco de dados para obter as unidades de medida esperadas para este material comercial
                    for (int i = 0; i < _cardallDatabase.comercial_DATA.Count; i++)
                    {
                        if (extractedCodeOrMaterial == _cardallDatabase.comercial_DATA[i])
                        {
                            string expectedUnitFromDb = _cardallDatabase.um_DATA?[i] ?? ""; // Unidade do banco

                            string xmlUnit1 = xmlEntry[3];
                            string xmlUnit2 = xmlEntry[4];
                            string xmlWeight = xmlEntry[5];

                            // Lógica de validação de unidade:
                            // Se o XML especifica unidades, elas devem corresponder à unidade do banco.
                            // Se o XML não especifica unidades, assume a unidade do banco.
                            bool unitsMatchOrAreAdopted = false;
                            if (!string.IsNullOrEmpty(xmlUnit1) && !string.IsNullOrEmpty(xmlUnit2)) // XML tem ambas as unidades
                            {
                                // A lógica original era 'if (var[3] == banco.um_DATA[i]) { intable = true; } if (var[4] == banco.um_DATA[i]) { intable = true; }'
                                // Isso significava que QUALQUER uma das unidades do XML batendo com a do banco era OK.
                                // Isso parece permitir inconsistências. Ex: XML M/KG, Banco M -> OK. XML M2/KG, Banco M -> OK.
                                // Refinando: Se a unidade do banco é M, o XML deve ser M (para UN1 ou UN2, dependendo do tipo de material).
                                // Se a unidade do banco é M2, o XML deve ser M2.
                                // Se a unidade do banco é PC, o XML pode ter PC ou vazio.
                                if (xmlUnit1 == expectedUnitFromDb || xmlUnit2 == expectedUnitFromDb || string.IsNullOrEmpty(expectedUnitFromDb))
                                {
                                    unitsMatchOrAreAdopted = true;
                                }
                            }
                            else if (string.IsNullOrEmpty(xmlUnit1) && string.IsNullOrEmpty(xmlUnit2)) // XML não tem unidades
                            {
                                unitsMatchOrAreAdopted = true; // Adota a unidade do banco
                                xmlUnit1 = expectedUnitFromDb; // Define para uso posterior
                                // xmlUnit2 pode permanecer vazio ou ser igual a xmlUnit1 dependendo da regra
                            }
                            else // XML tem apenas uma unidade definida, ou uma combinação estranha
                            {
                                // Se xmlUnit1 é a unidade do banco, ou xmlUnit2 é a unidade do banco
                                if (xmlUnit1 == expectedUnitFromDb || xmlUnit2 == expectedUnitFromDb)
                                {
                                    unitsMatchOrAreAdopted = true;
                                }
                            }


                            if (unitsMatchOrAreAdopted)
                            {
                                unit1 = xmlUnit1; // Ou a unidade adotada do banco se xmlUnit1 era vazio
                                unit2 = xmlUnit2; // Ou a unidade adotada do banco se xmlUnit2 era vazio
                                weight = xmlWeight;
                                return true;
                            }
                            else
                            {
                                // A mensagem original era mostrada se as unidades não batiam E o XML não tinha unidades vazias.
                                // MessageBox.Show($"Material: {propertyMaterial}\nUnidade XML: ({xmlUnit1}, {xmlUnit2})\nUnidade Banco: {expectedUnitFromDb}\nAs unidades de medida não são compatíveis ou não encontradas.", "Validação de Material", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false; // Unidades não compatíveis
                            }
                        }
                    }
                    return false; // Material comercial não encontrado no banco de dados (comercial_DATA)
                }
                else // Material comercial não encontrado no XML ou entrada XML malformada
                {
                    // A lógica original aqui iterava comercial_DATA e retornava true se achasse o material,
                    // mesmo sem entrada no XML. Isso parece permitir materiais sem dados de peso/unidade do XML.
                    for (int i = 0; i < _cardallDatabase.comercial_DATA.Count; i++)
                    {
                        if (extractedCodeOrMaterial == _cardallDatabase.comercial_DATA[i])
                        {
                            unit1 = _cardallDatabase.um_DATA?[i] ?? "";
                            unit2 = _cardallDatabase.um_DATA?[i] ?? ""; // Assume mesma unidade se UN2 não especificada
                            weight = "0"; // Peso padrão se não encontrado no XML
                            return true; // Encontrado no banco, mas não no XML (ou XML inválido)
                        }
                    }
                    return false;
                }
            }
            return false; // Tipo de material desconhecido
        }

        // Método auxiliar para determinar o tipo de material baseado nos padrões Regex.
        // Retorna: 0 para Padrão/Fictício, 1 para Comercial, 3 para Desconhecido/Inválido.
        // out string matchedPatternText: o texto que correspondeu ao padrão.
        public int DetermineMaterialType(string inputText, out string matchedPatternText)
        {
            // Padrões devem ser para a string inteira.
            const string StandardCodePattern = @"^(\d{3}\.\d{3}\.\d{4})"; // Captura o código
            const string FictitiousCodePattern = @"^(F\d{2}\.\d{3}\.\d{4})"; // Captura o código
            const string CommercialCodePattern = @"^(\d{6})\$?($|\s)"; // Captura código comercial de 6 dígitos, com $ opcional no final, seguido por fim de string ou espaço

            // Verifica o padrão comercial primeiro, pois pode ser um prefixo de outros.
            Match commercialMatch = Regex.Match(inputText, CommercialCodePattern);
            if (commercialMatch.Success)
            {
                matchedPatternText = commercialMatch.Groups[1].Value; // O código de 6 dígitos
                // Se o padrão comercial também pode ser seguido por '$', ajuste o regex e a captura.
                // Ex: @"^(\d{6})(\$)?($|\s)" e então verificar commercialMatch.Groups[2].Success para o '$'.
                // Por agora, o padrão original @"^\d{6}$" (sem o $ como parte do código) é usado para identificar o tipo.
                // O padrão atualizado acima captura os 6 dígitos.
                // Se a intenção do ValTipoCod original era pegar o código de 6 dígitos e retornar 1,
                // e o resto da string seria a descrição, então este 'matchedPatternText' é só o código.
                // A lógica em IsMaterialValid precisaria então do resto da string.
                // Para simplificar, se for comercial, talvez 'extractedCodeOrMaterial' deva ser o inputText inteiro.
                // A lógica original de ValTipoCod com Substring é difícil de replicar fielmente sem ambiguidade.

                // Se a intenção é que o código comercial seja APENAS os 6 dígitos e o resto é descrição:
                // matchedPatternText = commercialMatch.Groups[1].Value;
                // return 1;

                // Se a intenção é que o código comercial seja toda a string se ela corresponder ao formato comercial:
                // E o padrão regex for para a string inteira, como @"^\d{6}\$$"
                if (Regex.IsMatch(inputText, @"^\d{6}\$$"))
                { // Ex: 123456$
                    matchedPatternText = inputText;
                    return 1;
                }
                if (Regex.IsMatch(inputText, @"^\d{6}$"))
                { // Ex: 123456 (sem cifrão, se isso for comercial também)
                    matchedPatternText = inputText;
                    return 1;
                }
            }

            Match standardMatch = Regex.Match(inputText, StandardCodePattern);
            if (standardMatch.Success)
            {
                // Se o código padrão for encontrado, e ele ocupar toda a string ou for seguido de espaço.
                if (standardMatch.Value == inputText || inputText.StartsWith(standardMatch.Value + " "))
                {
                    matchedPatternText = inputText; // Ou standardMatch.Groups[1].Value se quiser só o código
                    return 0;
                }
            }

            Match fictitiousMatch = Regex.Match(inputText, FictitiousCodePattern);
            if (fictitiousMatch.Success)
            {
                if (fictitiousMatch.Value == inputText || inputText.StartsWith(fictitiousMatch.Value + " "))
                {
                    matchedPatternText = inputText; // Ou fictitiousMatch.Groups[1].Value
                    return 0;
                }
            }

            // Fallback se nenhum padrão específico corresponder ao início da string
            // A lógica original de ValTipoCod retornava 3 se nenhum dos padrões batesse no loop.
            // E o OutString era o último substring.
            // Aqui, se nenhum padrão bate, retornamos 3 e o texto original.
            matchedPatternText = inputText;
            return 3; // Tipo desconhecido ou formato não correspondente
        }


        #endregion
    }
    // Classe auxiliar (exemplo, coloque em seu próprio arquivo/namespace)
    public class SwSpecialCommands
    {
        public string sw_GetCustomProperty(string propertyName, ModelDoc2 model, string configuration, out string resolvedValue)
        {
            resolvedValue = "";
            if (model == null) return "";
            return model.Extension.CustomPropertyManager[configuration].Get(propertyName);
        }

        public void sw_GetAllvaluesProperty(string propertyName, CustomPropertyManager custPropMgr, out string valOut, out string resValOut)
        {
            valOut = "";
            resValOut = "";
            if (custPropMgr == null) return;
            custPropMgr.Get3(propertyName, false, out valOut, out resValOut);
        }

        public void sw_DeleteProperty(CustomPropertyManager custPropMgr, string propertyName)
        {
            if (custPropMgr == null) return;
            custPropMgr.Delete(propertyName);
        }

        public void SW_AddProperty(CustomPropertyManager custPropMgr, string propertyName, string value)
        {
            if (custPropMgr == null) return;
            custPropMgr.Add2(propertyName, (int)swCustomInfoType_e.swCustomInfoText, value);
            custPropMgr.Set(propertyName, value); // Garante que o valor resolvido seja o mesmo
        }
        public object GetCutLists(ModelDoc2 model)
        {
            // Implementação de exemplo para obter listas de corte
            if (model == null || model.GetType() != (int)swDocumentTypes_e.swDocPART) return null; // Listas de corte são de peças

            FeatureManager featMgr = model.FeatureManager;
            Feature swFeat = (Feature)model.FirstFeature();
            List<Feature> cutListFeatures = new List<Feature>();

            while (swFeat != null)
            {
                if (swFeat.GetTypeName2() == "WeldMemberFeat") // Ou "CutListFolder" se estiver procurando a pasta
                {
                    // Este é um membro de soldagem, a pasta de lista de corte é um sub-feature ou pai.
                    // A API para obter a "CutListFeat" exata pode variar.
                    // Se GetCutLists na sua implementação original retorna as pastas de lista de corte:
                    if (swFeat.GetTypeName2() == "CutListFolder" || swFeat.Name.ToUpperInvariant().Contains("LISTA DE CORTE") || swFeat.Name.ToUpperInvariant().Contains("CUT-LIST"))
                    {
                        cutListFeatures.Add(swFeat);
                    }
                }
                if (swFeat.GetTypeName2() == "CutListFolder") // Nome exato do tipo da feature da pasta de lista de corte
                {
                    cutListFeatures.Add(swFeat);
                }
                swFeat = (Feature)swFeat.GetNextFeature();
            }
            return cutListFeatures.ToArray();
        }
    }

    // Classe auxiliar (exemplo)
    public class StringCorrection
    {
        // Implemente métodos de correção de string aqui, se necessário
    }
}
