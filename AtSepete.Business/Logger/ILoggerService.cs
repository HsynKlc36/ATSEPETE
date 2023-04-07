using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtSepete.Business.Logger
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);

        //        Trace(iz) - Uygulamanın tüm izlerini içerir ve en ayrıntılı loglama seviyesidir.
        //Debug(hata ayıklama) - Hata ayıklama amaçlı loglamaları içerir.
        //Info(bilgi) - Uygulamanın önemli bilgilerini içerir.
        //Warn(uyarı) - Uygulamada oluşabilecek potansiyel sorunları içerir.
        //Error(hata) - Uygulamanın hata mesajlarını içerir.
        //Fatal(ölümcül hata) - Uygulamanın kurtarılamayan hatalarını içerir ve en yüksek loglama seviyesidir.

    }
}
