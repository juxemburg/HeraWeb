using Entities.Notifications;
using Hera.Models.NotificationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hera.Services.NotificationServices.NotificationBuilders
{
    public static class NotificationBuilder
    {
        private static readonly Dictionary<NotificationType,
            Func<int, Dictionary<string, string>, Notification>>
            FactoryFunctions
            = new Dictionary<NotificationType,
                Func<int, Dictionary<string, string>, Notification>>()
            {
                [NotificationType.NotificationNuevaCalificacion]
                = (userId, values) =>
                {
                    var idCurso = Convert.ToInt32(values["IdCurso"]);
                    var key = $"NuevaCalificacion-{idCurso}";

                    return new Notification()
                    {
                        Key = key,
                        UsuarioId = userId,
                        Date = DateTime.Now,
                        Action = $"/Profesor/Curso/{idCurso}",
                        Message = $"{values["NombreEstudiante"]} " +
                        "ha realizado una nueva calificación en " +
                        $"el curso {values["NombreCurso"]}",
                        Unread = true,
                        Type = NotificationType.NotificationNuevaCalificacion
                    };

                },
                [NotificationType.NotificationNuevoEstudiante]
                = (userId, values) =>
                {
                    var idCurso = Convert.ToInt32(values["IdCurso"]);
                    var key = $"NuevoEstudiante-{idCurso}";

                    return new Notification()
                    {
                        Key = key,
                        UsuarioId = userId,
                        Date = DateTime.Now,
                        Action = $"/Profesor/Curso/{idCurso}",
                        Message = $"{values["NombreEstudiante"]} se ha matriculado en tu" +
                            $"curso {values["NombreCurso"]}!",
                        Unread = true,
                        Type = NotificationType.NotificationNuevoEstudiante
                    };

                },
                [NotificationType.NotificationDesafioCalificado]
                = (userId, values) => new Notification()
                {
                    UsuarioId = userId,
                    Date = DateTime.Now,
                    Action = $"/Desafios/Details/{values["IdDesafio"]}",
                    Message = "Han realizado una nueva " +
                              "calificación en " +
                              $"tu desafío {values["NombreDesafio"]}",
                    Unread = true,
                    Type = NotificationType.NotificationDesafioCalificado
                },
                [NotificationType.NotificationDesafioUsado]
                = (userId, values) => new Notification()
                {
                    UsuarioId = userId,
                    Date = DateTime.Now,
                    Action = $"/Desafios/Details/{values["IdDesafio"]}",
                    Message = $"tu desafío {values["NombreDesafio"]} " +
                              "ha aumentado su popularidad",
                    Unread = true,
                    Type = NotificationType.NotificationDesafioUsado
                },

                //Notificaciones Estudiante
                [NotificationType.NotificationNuevaRevision]
                = (userId, values) => new Notification
                {
                    UsuarioId = userId,
                    Date = DateTime.Now,
                    Action = $"/Desafios/Details/{values["IdDesafio"]}",
                    Message = "han calificado tu desafío" +
                              $" {values["NombreDesafio"]} " +
                              $"en el curso {values["NombreCurso"]}",
                    Unread = true,
                    Type = NotificationType.NotificationNuevaRevision
                },
                [NotificationType.NotificationMatriculaAnulada]
                = (userId, values) => new Notification
                {
                    UsuarioId = userId,
                    Date = DateTime.Now,
                    Action= "/Estudiante/Cursos",
                    Message = "¡Tu matricula de curso " +
                              $"{values["NombreCurso"]} " +
                              "ha sido eliminada!",
                    Unread =  true,
                    Type = NotificationType.NotificationMatriculaAnulada
                              
                }
            };

        private static readonly Dictionary<NotificationType,
            Func<List<Notification>, NotificationViewModel>>
            GroupFunctions =
            new Dictionary<NotificationType, Func<List<Notification>, NotificationViewModel>>()
            {
                [NotificationType.NotificationNuevaCalificacion] =
                (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"¡Tienes {data.Count} nuevas " +
                        $"calificaciones en tu curso!",
                    Count = data.Count,
                    Date = data.Min(e => e.Date)
                },
                [NotificationType.NotificationNuevoEstudiante] =
                (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"¡Tienes {data.Count} nuevos " +
                        $"estudiantes en tu curso!",
                    Count = data.Count,
                    Date = data.Min(e => e.Date)
                },
                [NotificationType.NotificationDesafioCalificado]
                = (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"Tienes {data.Count} nuevas calificaciones" +
                              "en tu desafío",
                    Count = data.Count,
                    Date = data.Min(d => d.Date)
                },
                [NotificationType.NotificationDesafioUsado]
                = (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"{data.Count} profesores están usando " +
                              "tu desafío",
                    Count = data.Count,
                    Date = data.Min(d => d.Date)
                }
            };

        private static NotificationViewModel NoNotifications
            => new NotificationViewModel()
            {
                Action = "#",
                Message = "No tienes notificaciones..."
            };

        public static Notification CreateNotification(NotificationType type,
            int userId, Dictionary<string, string> values)
        {
            return FactoryFunctions[type](userId, values);
        }

        public static IEnumerable<NotificationViewModel> ResumeNotifications(
            Dictionary<NotificationType, List<Notification>> notifications)
        {
            foreach (var type in notifications.Keys)
            {
                yield return
                    Get_resumedNotification(type, notifications[type]);
            }
        }

        private static NotificationViewModel Get_resumedNotification(
            NotificationType type, List<Notification> notifications)
        {
            if (notifications.Count <= 0)
                return NoNotifications;
            if (notifications.Count == 1)
                return new NotificationViewModel()
                {
                    Action = notifications[0].Action,
                    Message = notifications[0].Message
                };
            return GroupFunctions[type](notifications);
        }


    }


}
