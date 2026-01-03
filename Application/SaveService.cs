using DWM.ExSw.Addin.Base;
using DWM.ExSw.Addin.setup.info;
using SolidWorks.Interop.sldworks;
using System;

public class SaveService
{
    private readonly swSpecialComands _swComands;
    private readonly Ppr _ppr;

    public SaveService(swSpecialComands swComands, Ppr ppr)
    {
        _swComands = swComands;
        _ppr = ppr;
    }

    public void Save(ModelDoc2 model, CardallFormData data)
    {
        if (model == null) return;

        string[] configs = _swComands.sw_GetConfigurations(model);
        Array.Resize(ref configs, configs.Length + 1);
        configs[1] = ""; // configuração padrão

        foreach (string cfg in configs)
        {
            var swCustProp = model.Extension.CustomPropertyManager[cfg];

            DeleteProperties(swCustProp);
            AddProperties(swCustProp, data);
        }
    }

    private void DeleteProperties(CustomPropertyManager swCustProp)
    {
        _swComands.sw_DeleteProperty(swCustProp, _ppr.codigo);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.revisao);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.denominacao);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.descricao);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.material);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.projetista);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.projetistaData);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.desenhista);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.desenhistaData);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.revisor);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.revisorData);
        _swComands.sw_DeleteProperty(swCustProp, _ppr.pbruto);
        _swComands.sw_DeleteProperty(swCustProp, "Soldagem");
    }

    private void AddProperties(CustomPropertyManager swCustProp, CardallFormData d)
    {
        _swComands.SW_AddProperty(
            swCustProp,
            "Soldagem",
            d.Comercial ? "Sim" : "Não"
        );

        _swComands.SW_AddProperty(swCustProp, _ppr.codigo, d.Codigo);
        _swComands.SW_AddProperty(swCustProp, _ppr.revisao, d.Revisao);
        _swComands.SW_AddProperty(swCustProp, _ppr.denominacao, d.DenominacaoFinal);
        _swComands.SW_AddProperty(swCustProp, _ppr.descricao, d.DenominacaoOriginal);
        _swComands.SW_AddProperty(swCustProp, _ppr.material, d.Material);

        _swComands.SW_AddProperty(swCustProp, _ppr.projetista, d.Projetista);
        _swComands.SW_AddProperty(swCustProp, _ppr.projetistaData, d.ProjetistaData);

        _swComands.SW_AddProperty(swCustProp, _ppr.desenhista, d.Desenhista);
        _swComands.SW_AddProperty(swCustProp, _ppr.desenhistaData, d.DesenhistaData);

        _swComands.SW_AddProperty(swCustProp, _ppr.revisor, d.Revisor);
        _swComands.SW_AddProperty(swCustProp, _ppr.revisorData, d.RevisorData);

        _swComands.SW_AddProperty(swCustProp, _ppr.pbruto, d.PesoBruto);
    }
}
