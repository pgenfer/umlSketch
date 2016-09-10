using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuml.Notification;

namespace Yuml.Command
{
    public class MethodSingleCommandContext : SingleCommandContextBase
    {
        public MethodSingleCommandContext(
            Method method,
            IMethodNameValidationService validateName,
            MethodNotificationService notificationService)
        {
            Rename = new RenameMethodCommand(
                method,
                validateName,
                notificationService);
        }
    }
}
