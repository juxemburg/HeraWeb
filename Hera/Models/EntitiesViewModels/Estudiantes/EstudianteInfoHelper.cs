using Entities.Usuarios;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Models.EntitiesViewModels.Estudiantes
{
    public static class EstudianteInfoHelper
    {
        public static List<SelectListItem>
            EstudianteInfo_UsoPcList
        { get; } = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.TodosLosDias,
                    Text = "Todos los días"
                },
                new SelectListItem()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.UnaVezSemana,
                    Text = "Una vez a la semana"
                },
                new SelectListItem()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.RaraVez,
                    Text = "Rara Vez"
                },
                new SelectListItem()
                {
                    Value = ""+(int)EstudianteInfo_UsoPc.CasiNunca,
                    Text = "Casi nunca"
                }
            };

        public static List<SelectListItem>
            EstudianteInfo_ActividadesList
        { get; }
            = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.RedesSociales,
                    Text= "Redes Sociales"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Videos,
                    Text= "Ver vídeos"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Peliculas_Series,
                    Text= "ver películas y/o series"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.BusquedaInformacion,
                    Text= "Buscar información"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.AprenderMas,
                    Text= "Aprender más"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.HacerTareas,
                    Text= "Hacer Tareas"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Jugar,
                    Text= "Jugar Videojuegos"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Comunicacion,
                    Text= "Comunicarme con familiares y amigos"
                },
                new SelectListItem()
                {
                    Value = ""+
                    (int)EstudianteInfo_ActividadesPc.Otros,
                    Text= "Otros"
                },
            };

        public static List<SelectListItem>
            EstudianteInfo_GeneroList
        { get; } = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value = ""+(int)Genero.Femenino,
                    Text = "Femenino"
                },
                new SelectListItem()
                {
                    Value = ""+(int)Genero.Masculino,
                    Text = "Masculino"
                }
            };

        public static List<SelectListItem>
            EstudianteInfo_MateriaFavoritaList
        { get; }
        = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Matematicas,
                Text = "Matemáticas/Geometría"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Historia,
                Text = "Historia"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Geografia,
                Text = "Geografía"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Espanol,
                Text = "Español"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Ingles,
                Text = "Inglés"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Artes,
                Text = "Artes"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Informatica,
                Text = "Informática"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.EdFisica,
                Text = "Educación Física"
            },
            new SelectListItem()
            {
                Value = ""+(int)EstudianteInfo_MateriaFavorita.Musica,
                Text = "Música"
            }
        };
    }
}
