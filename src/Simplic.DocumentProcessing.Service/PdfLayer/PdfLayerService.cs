using GdPicture14;
using System.IO;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;

namespace Simplic.DocumentProcessing.Service
{
    /// <summary>
    /// Pdf layer service
    /// </summary>
    public class PdfLayerService : IPdfLayerService
    {
        /// <summary>
        /// Add a layer to a pdf and return it
        /// </summary>
        /// <param name="pdf">Pdf to add the layer to</param>
        /// <param name="layer">Layer file as byte arrya</param>
        /// <param name="mode">Add mode (first page, each page, ...)</param>
        /// <returns>Pdf with layer</returns>
        public byte[] MergeLayer(byte[] pdf, byte[] layer, PdfLayerMode mode)
        {
            switch (mode)
            {
                case PdfLayerMode.AllPages:
                case PdfLayerMode.FirstPage:
                    return MergeLayerLayerSecond(pdf, layer, mode);

                case PdfLayerMode.FirstPageLayerFirst:
                case PdfLayerMode.AllPagesLayerFirst:
                    return MergeLayerLayerFirst(pdf, layer, mode);
            }

            throw new System.Exception($"Invalid PdfLayerMode: {mode}");
        }

        /// <summary>
        /// Add a layer to a pdf and return it
        /// </summary>
        /// <param name="pdf">Pdf to add the layer to</param>
        /// <param name="layer">Layer file as byte arrya</param>
        /// <param name="mode">Add mode (first page, each page, ...)</param>
        /// <returns>Pdf with layer</returns>
        private byte[] MergeLayerLayerFirst(byte[] pdf, byte[] layer, PdfLayerMode mode)
        {
            using (var templateStream = new MemoryStream(layer))
            {
                using (var templatePdf = GdPictureHelper.GetPDFInstance())
                {
                    templatePdf.LoadFromStream(templateStream);
                    templatePdf.SelectPage(1);

                    using (var sourceStream = new MemoryStream(pdf))
                    {
                        using (var sourcePdf = GdPictureHelper.GetPDFInstance())
                        {
                            using (var targetPdf = GdPictureHelper.GetPDFInstance())
                            {
                                targetPdf.NewPDF();
                                sourcePdf.LoadFromStream(sourceStream);

                                for (int i = 1; i <= sourcePdf.GetPageCount(); i++)
                                {
                                    if (sourcePdf.SelectPage(i) == GdPictureStatus.OK)
                                    {
                                        if (mode == PdfLayerMode.FirstPageLayerFirst)
                                        {
                                            if (i == 1)
                                            {
                                                targetPdf.ClonePage(templatePdf, 1);
                                                targetPdf.SelectPage(i);

                                                targetPdf.DrawPage(sourcePdf, i, 0, 0, templatePdf.GetPageWidth(), templatePdf.GetPageHeight());

                                                targetPdf.SelectPage(i);
                                            }
                                            else
                                            {
                                                targetPdf.ClonePage(sourcePdf, i);
                                                targetPdf.SelectPage(i);
                                            }
                                        }
                                        else if (mode == PdfLayerMode.AllPagesLayerFirst)
                                        {
                                            targetPdf.ClonePage(templatePdf, 1);
                                            targetPdf.SelectPage(i);

                                            targetPdf.DrawPage(sourcePdf, i, 0, 0, templatePdf.GetPageWidth(), templatePdf.GetPageHeight());
                                        }
                                    }
                                }

                                using (var targetStream = new MemoryStream())
                                {
                                    targetPdf.SaveToStream(targetStream);
                                    targetStream.Position = 0;
                                    return targetStream.ToArray();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a layer to a pdf and return it
        /// </summary>
        /// <param name="pdf">Pdf to add the layer to</param>
        /// <param name="layer">Layer file as byte arrya</param>
        /// <param name="mode">Add mode (first page, each page, ...)</param>
        /// <returns>Pdf with layer</returns>
        private byte[] MergeLayerLayerSecond(byte[] pdf, byte[] layer, PdfLayerMode mode)
        {
            using (var templateStream = new MemoryStream(layer))
            {
                using (var templatePdf = GdPictureHelper.GetPDFInstance())
                {
                    templatePdf.LoadFromStream(templateStream);
                    templatePdf.SelectPage(1);

                    using (var sourceStream = new MemoryStream(pdf))
                    {
                        using (var sourcePdf = GdPictureHelper.GetPDFInstance())
                        {
                            using (var targetPdf = GdPictureHelper.GetPDFInstance())
                            {
                                targetPdf.NewPDF();
                                sourcePdf.LoadFromStream(sourceStream);

                                for (int i = 1; i <= sourcePdf.GetPageCount(); i++)
                                {
                                    if (sourcePdf.SelectPage(i) == GdPictureStatus.OK)
                                    {
                                        targetPdf.ClonePage(sourcePdf, i);
                                        targetPdf.SelectPage(i);

                                        if (mode == PdfLayerMode.AllPages || (mode == PdfLayerMode.FirstPage && i == 1))
                                            targetPdf.DrawPage(templatePdf, 1, 0, 0, templatePdf.GetPageWidth(), templatePdf.GetPageHeight());
                                    }
                                }

                                using (var targetStream = new MemoryStream())
                                {
                                    targetPdf.SaveToStream(targetStream);
                                    targetStream.Position = 0;
                                    return targetStream.ToArray();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
