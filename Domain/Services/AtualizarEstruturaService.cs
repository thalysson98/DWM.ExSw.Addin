using SolidWorks.Interop.sldworks;
using System.Collections.Generic;
using System;
using System.Linq;
using DWM.ExSw.Addin.Base;

public class AtualizarEstruturaService
{
    private readonly ClientComandsCardall cardalcomands;
    private readonly swSpecialComands swComands;

    public AtualizarEstruturaService(
        ClientComandsCardall cardalcomands,
        swSpecialComands swComands)
    {
        this.cardalcomands = cardalcomands;
        this.swComands = swComands;
    }

    public List<EstruturaItem> AtualizarPart(
        ModelDoc2 model,
        bool comercial,
        bool atualizarListaCorte)
    {
        var resultado = new List<EstruturaItem>();

        if (model == null || comercial)
            return resultado;

        if (atualizarListaCorte)
        {
            swComands.SW_DeleteCutList(model);
            swComands.SW_UpdateCutlist(model);
        }

        var estruturaOBJ = cardalcomands.GetEstrutura(model);
        if (estruturaOBJ == null)
            return resultado;

        for (int i = 0; i < estruturaOBJ.GetLength(0); i++)
        {
            if (estruturaOBJ[i, 0] == null)
                continue;

            var arrayInterno = (object[])estruturaOBJ[i, 0];

            if ((string)arrayInterno[1] == "PESO ZERO")
                continue;

            resultado.Add(new EstruturaItem
            {
                Col0 = (string)arrayInterno[0],
                Col1 = (string)arrayInterno[1],
                Col2 = (string)arrayInterno[2],
                Col3 = (string)arrayInterno[3],
                Col4 = (string)arrayInterno[4],
                Col6 = (string)arrayInterno[6],
                Status = Convert.ToInt32(arrayInterno[5])
            });
        }

        return resultado;
    }

    public List<EstruturaGrupo> AtualizarAssembly(
        AssemblyDoc assembly)
    {
        var grupos = new List<EstruturaGrupo>();

        var componentes = swComands.GetCompModels(assembly);
        if (componentes == null)
            return grupos;

        foreach (Component2 comp in componentes)
        {
            if (comp == null)
                continue;

            var model = comp.GetModelDoc2() as ModelDoc2;
            if (model == null)
                continue;

            var grupo = new EstruturaGrupo
            {
                NomeGrupo = model.GetTitle()
            };

            var estruturaOBJ = cardalcomands.GetEstrutura(model);
            if (estruturaOBJ == null)
                continue;

            for (int i = 0; i < estruturaOBJ.GetLength(0); i++)
            {
                if (estruturaOBJ[i, 0] == null)
                    continue;

                var arrayInterno = (object[])estruturaOBJ[i, 0];

                if ((string)arrayInterno[1] == "PESO ZERO")
                    continue;

                grupo.Itens.Add(new EstruturaItem
                {
                    Col0 = (string)arrayInterno[0],
                    Col1 = (string)arrayInterno[1],
                    Col2 = (string)arrayInterno[2],
                    Col3 = (string)arrayInterno[3],
                    Col4 = (string)arrayInterno[4],
                    Col6 = (string)arrayInterno[6],
                    Status = Convert.ToInt32(arrayInterno[5])
                });
            }

            if (grupo.Itens.Any())
                grupos.Add(grupo);
        }

        return grupos;
    }
}

public class EstruturaItem
{
    public string Col0 { get; set; }
    public string Col1 { get; set; }
    public string Col2 { get; set; }
    public string Col3 { get; set; }
    public string Col4 { get; set; }
    public string Col6 { get; set; }

    public int Status { get; set; }
}
public class EstruturaGrupo
{
    public string NomeGrupo { get; set; }
    public List<EstruturaItem> Itens { get; set; }
}
