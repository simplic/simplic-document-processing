using Simplic.Base;
using Simplic.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// ADR service
    /// </summary>
    public class ADRService : IADRService
    {
        #region [CreateConfiguration]
        /// <summary>
        /// Create adr Configuration
        /// </summary>
        /// <param name="configuration">List of configuration objects</param>
        /// <returns>Configuration as byte-array</returns>
        public byte[] CreateConfiguration(IList<ADRConfiguration> configurations)
        {
            byte[] result = null;

            var tempPathId = Guid.NewGuid();

            var path = $"{GlobalSettings.AppDataPath}\\ADR\\";
            var fullPath = $"{path}{tempPathId}.dat";

            DirectoryHelper.CreateDirectoryIfNotExists(path);

            using (var gdPictureImaging = GdPictureHelper.GetImagingInstance())
            {
                // Add images
                foreach (var configuration in configurations)
                {
                    int imageId = gdPictureImaging.CreateGdPictureImageFromByteArray(configuration.Blob);
                    if (imageId != 0)
                    {
                        int templateId = gdPictureImaging.ADRCreateTemplateEmpty();
                        gdPictureImaging.ADRSetTemplateTag(templateId, configuration.Tag);
                        gdPictureImaging.ADRAddGdPictureImageToTemplate(templateId, imageId);

                        gdPictureImaging.ReleaseGdPictureImage(imageId);
                    }
                }

                // Save file, get content and delete
                if (gdPictureImaging.ADRSaveTemplateConfig(fullPath))
                {
                    result = File.ReadAllBytes(fullPath);
                    File.Delete(fullPath);
                }

                return result;
            }
        }
        #endregion

        #region [Match]
        /// <summary>
        /// Match the blob
        /// </summary>
        /// <param name="configuration">ADR configuration</param>
        /// <param name="blob">Blob to match</param>
        /// <returns>Best match if exists, else null</returns>
        public ADRResult Match(byte[] configuration, byte[] blob)
        {
            ADRResult result = null;
            var tempPathId = Guid.NewGuid();
            var maxIdentityValue = 0.0;

            using (var gdPictureImaging = GdPictureHelper.GetImagingInstance())
            {
                var path = $"{GlobalSettings.AppDataPath}\\ADR\\";
                var fullPath = $"{path}{tempPathId}.dat";

                DirectoryHelper.CreateDirectoryIfNotExists(path);

                File.WriteAllBytes(fullPath, configuration);
                gdPictureImaging.ADRLoadTemplateConfig(fullPath);

                int imageId = gdPictureImaging.CreateGdPictureImageFromByteArray(blob);
                if (imageId != 0)
                {
                    int closerTemplateID = gdPictureImaging.ADRGetCloserTemplateForGdPictureImage(imageId);
                    if (closerTemplateID != 0)
                    {
                        var tag = gdPictureImaging.ADRGetTemplateTag(closerTemplateID);
                        var accuracy = Math.Round(gdPictureImaging.ADRGetLastConfidence(), 2);

                        if (accuracy > maxIdentityValue)
                        {
                            result = new ADRResult()
                            {
                                Tag = tag,
                                Accuracy = accuracy
                            };

                            maxIdentityValue = accuracy;
                        }
                    }

                    // Remove path
                    if (File.Exists(fullPath))
                        File.Delete(fullPath);
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {

        }
    }
}
