using DWM.ExSw.Addin.Base;
using DWM.ExSw.Addin.setup.info;
using SolidWorks.Interop.sldworks;

public class CardallValueService
{
    private readonly swSpecialComands _sw;
    private readonly ClientComandsCardall _cardall;
    private readonly Ppr _ppr;

    public CardallValueService(
        swSpecialComands sw,
        ClientComandsCardall cardall,
        Ppr ppr)
    {
        _sw = sw;
        _cardall = cardall;
        _ppr = ppr;
    }

    public CardallFormData Obter(ModelDoc2 model, bool comercial)
    {
        var data = new CardallFormData();
        string outVar;
        int err;

        // -------- Código / Revisão --------
        if (!comercial)
        {
            data.Codigo = _cardall.ValidandoCodigo(
                model,
                out err,
                false,
                _sw.sw_GetNameFile(model)
            );

            if (string.IsNullOrEmpty(data.Codigo))
                data.Codigo = model.GetTitle();

            data.Revisao = _sw.sw_GetCustomProperty(
                _ppr.revisao, model, "", out outVar);

            if (string.IsNullOrEmpty(data.Revisao))
                data.Revisao = "A";
        }

        // -------- Denominação --------
        data.Material = _sw.sw_GetCustomProperty(
            _ppr.material, model, "", out outVar);

        data.DenominacaoOriginal = _sw.sw_GetCustomProperty(
            _ppr.denominacao, model, "", out outVar);

        var textoProcessado =
            DenominacaoProcessor.Processar(model, data.DenominacaoOriginal);

        data.DenominacaoFinal =
            _cardall.GerarDenominacao(
                comercial,
                data.Codigo,
                data.Revisao,
                textoProcessado
            );

        // -------- Pessoas --------
        data.Projetista = _sw.sw_GetCustomProperty(_ppr.projetista, model, "", out outVar);
        data.ProjetistaData = _sw.sw_GetCustomProperty(_ppr.projetistaData, model, "", out outVar);

        data.Desenhista = _sw.sw_GetCustomProperty(_ppr.desenhista, model, "", out outVar);
        data.DesenhistaData = _sw.sw_GetCustomProperty(_ppr.desenhistaData, model, "", out outVar);

        data.Revisor = _sw.sw_GetCustomProperty(_ppr.revisor, model, "", out outVar);
        data.RevisorData = _sw.sw_GetCustomProperty(_ppr.revisorData, model, "", out outVar);

        // -------- Outros --------
        data.Tipo = _sw.sw_GetCustomProperty(_ppr.tipo, model, "", out outVar);
        data.SubGrupo = _sw.sw_GetCustomProperty(_ppr.subgrupo, model, "", out outVar);
        data.Unidade = _sw.sw_GetCustomProperty(_ppr.unidade, model, "", out outVar);
        data.PesoBruto = _sw.sw_GetCustomProperty(_ppr.pbruto, model, "", out outVar);

        return data;
    }
}
