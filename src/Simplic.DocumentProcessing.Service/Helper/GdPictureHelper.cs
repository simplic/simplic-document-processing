using GdPicture14;
using Simplic.Base;
using Simplic.ComponentLicense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    internal static class GdPictureHelper
    {
        private static bool licenseRegistered;
        
        private static void RegisterLicense()
        {
            if (!licenseRegistered)
            {
                var service = CommonServiceLocator.ServiceLocator.Current.GetInstance<IComponentLicenseService>();
                var key = service.Get("gdpicture14");

                if (key == null)
                    throw new Exception("Component license not found: `gdpicutre14`");

                licenseRegistered = true;
                var licenseManager = new LicenseManager();
                licenseManager.RegisterKEY(key.License);
            }
        }

        internal static GdPictureImaging GetImagingInstance()
        {
            RegisterLicense();
            return new GdPictureImaging();
        }

        internal static GdPicturePDF GetPDFInstance()
        {
            RegisterLicense();
            return new GdPicturePDF();
        }

        internal static GdPictureOCR GetOCRInstance()
        {
            RegisterLicense();
            return new GdPictureOCR();            
        }

        public static string OCRDirectory
        {
            get
            {
                return $"{GlobalSettings.StudioPath}\\OCR";
            }
        }
    }
}
