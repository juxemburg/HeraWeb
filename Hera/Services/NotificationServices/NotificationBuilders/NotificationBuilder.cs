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
                [NotificationType.Notification_NuevaCalificacion]
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
                        Type = NotificationType.Notification_NuevaCalificacion
                    };

                },
                [NotificationType.Notification_NuevoEstudiante]
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
                        Type = NotificationType.Notification_NuevoEstudiante
                    };

                },
                [NotificationType.Notification_DesafioCalificado]
                = (userId, values) =>
                {
                    var idDesafio = Convert.ToInt32(values["IdDesafio"]);
                    return new Notification()
                    {
                        UsuarioId = userId,
                        Date = DateTime.Now,
                        Action = $"/Desafios/Details/{idDesafio}",
                        Message = "Han realizado una nueva " +
                                  "calificación en " +
                                  $"tu desafío {values["NombreDesafio"]}",
                        Unread = true,
                        Type = NotificationType.Notification_DesafioCalificado
                    };
                }
            };

        private static readonly Dictionary<NotificationType,
            Func<List<Notification>, NotificationViewModel>>
            GroupFunctions =
            new Dictionary<NotificationType, Func<List<Notification>, NotificationViewModel>>()
            {
                [NotificationType.Notification_NuevaCalificacion] =
                (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"¡Tienes {data.Count} nuevas " +
                        $"calificaciones en tu curso!",
                    Count = data.Count,
                    Date = data.Min(e => e.Date)
                },
                [NotificationType.Notification_NuevoEstudiante] =
                (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"¡Tienes {data.Count} nuevos " +
                        $"estudiantes en tu curso!",
                    Count = data.Count,
                    Date = data.Min(e => e.Date)
                },
                [NotificationType.Notification_DesafioCalificado]
                = (data) => new NotificationViewModel()
                {
                    Action = data.First().Action,
                    Message = $"Tienes {data.Count} nuevas calificaciones" +
                              "en tu desafío",
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
