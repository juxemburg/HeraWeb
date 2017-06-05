using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Desafios
{
    public enum Desafio_Abstraccion
    {
        Ninguno,
        NoBloquesNoUsados,
        CreacionBloquesPropios,
        UsoDeClones
    }

    public enum Desafio_PensamientoAlgoritmico
    {
        Ninguno,
        UsoSecuencias
    }

    public enum Desafio_DescomposicionProblemas
    {
        Ninguno,
        DosHilosPorSprite,
        MultiplesEventosPorSprite
    }

    public enum Desafio_Paralelismo
    {
        Ninguno,
        DosHilosBanderaVerde,
        EnvioYRecepcionMensajes,
        DosProgramasEnOtrosEventos
    }

    public enum Desafio_ControlFlujo
    {
        Ninguno,
        FlujoSimple,
        FlujoComplejo,
        FlujoAnidado
    }

    public enum Desafio_Interaccion
    {
        Ninguno,
        InputBasico,
        UsoVariables,
        SensoresSprite
    }

    public enum Desafio_Representacion
    {
        Ninguno,
        CreacionVariables,
        UsoCompartidoVariables,
        UsoListas
    }

    public enum Desafio_Analisis
    {
        Ninguno,
        OperadoresBasicos,
        OperadoresComplejos,
        OperadoresAnidados
    }
}
