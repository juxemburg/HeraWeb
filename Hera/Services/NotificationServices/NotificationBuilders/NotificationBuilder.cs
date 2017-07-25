using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hera.Services.NotificationServices.NotificationBuilders
{
    public static class NotificationBuilder
    {
        private static Dictionary<NotificationType,
            Func<int, Dictionary<string, string>, Notification>>
            factoryFunctions 
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
                        Message = $"tu curso {values["NombreCurso"]} tiene" +
                            $"1 calificación pendiente",
                        Unread = true
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
                            $"curso ${values["NombreCurso"]}!",
                        Unread = true
                    };

                }
            };

        public static Notification CreateNotification(NotificationType type,
            int userId, Dictionary<string, string> values)
        {
            return factoryFunctions[type](userId, values);
        }
    }

    public enum NotificationType
    {
        Notification_NuevaCalificacion,
        Notification_NuevoEstudiante
    }
}
