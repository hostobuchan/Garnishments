/*
  Copyright 2006-2010 Stefano Chizzolini. http://www.pdfclown.org

  Contributors:
    * Stefano Chizzolini (original code developer, http://www.stefanochizzolini.it)

  This file should be part of the source code distribution of "PDF Clown library" (the
  Program): see the accompanying README files for more info.

  This Program is free software; you can redistribute it and/or modify it under the terms
  of the GNU Lesser General Public License as published by the Free Software Foundation;
  either version 3 of the License, or (at your option) any later version.

  This Program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY,
  either expressed or implied; without even the implied warranty of MERCHANTABILITY or
  FITNESS FOR A PARTICULAR PURPOSE. See the License for more details.

  You should have received a copy of the GNU Lesser General Public License along with this
  Program (see README files); if not, go to the GNU website (http://www.gnu.org/licenses/).

  Redistribution and use, with or without modification, are permitted provided that such
  redistributions retain the above copyright notice, license and disclaimer, along with
  this list of conditions.
*/

using bytes = org.pdfclown.bytes;
using org.pdfclown.documents;
using org.pdfclown.documents.contents.composition;
using org.pdfclown.documents.contents.objects;
using xObjects = org.pdfclown.documents.contents.xObjects;
using org.pdfclown.objects;
using org.pdfclown.util.io;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace org.pdfclown.documents.contents.entities
{
    /**
      <summary>JPEG image object [ISO 10918-1;JFIF:1.02].</summary>
    */
    public sealed class TiffImage
      : Image
    {
        protected internal System.IO.Stream ImageStream;
        #region dynamic
        #region constructors
        public TiffImage(
          System.IO.Stream stream
          )
            : base(stream)
        { Load(); }
        #endregion

        #region interface
        #region public
        public override ContentObject ToInlineObject(
          PrimitiveComposer composer
          )
        {
            return composer.Add(
              new InlineImage(
                new InlineImageHeader(
                  new List<PdfDirectObject>(
                    new PdfDirectObject[]
              {
                PdfName.W, new PdfInteger(Width),
                PdfName.H, new PdfInteger(Height),
                PdfName.CS, PdfName.N,
                PdfName.BPC, new PdfInteger(BitsPerComponent),
                PdfName.BS, new PdfInteger(BitsPerComponent),
                PdfName.F, PdfName.CCF
              }
                    )
                  ),
                new InlineImageBody(
                  new bytes::Buffer(Stream)
                  )
                )
              );
        }

        public override xObjects::XObject ToXObject(
          Document context
          )
        {
            return new xObjects::ImageXObject(
              context,
              new PdfStream(
                new PdfDictionary(
                  new PdfName[]
                    {
                      PdfName.Width,
                      PdfName.Height,
                      PdfName.Columns,
                      PdfName.BitsPerComponent,
                      PdfName.BitsPerSample,
                      PdfName.ColorSpace,
                      PdfName.Filter
                    },
                  new PdfDirectObject[]
                    {
                      new PdfInteger(Width),
                      new PdfInteger(Height),
                      new PdfInteger(Width),
                      new PdfInteger(BitsPerComponent),
                      new PdfInteger(BitsPerComponent),
                      PdfName.DeviceGray,
                      PdfName.CCITTFaxDecode
                    }
                  ),
                new bytes::Buffer(ImageStream)
                )
              );
        }
        #endregion

        #region private
        private void Load()
        {
            System.IO.Stream stream = Stream;

            TIFF TiffInfo = new TIFF(Stream);
            
            // Get the image bits per color component (sample precision)!
            BitsPerComponent = (int)TiffInfo.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.BitsPerSample).Offset;
            // Get the image size!
            Height = (int)TiffInfo.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.ImageLength).Offset;
            Width = (int)TiffInfo.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.ImageWidth).Offset;
            //int Bytes = (int)(uint)TiffInfo.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.StripByteCounts).GetValue(TiffInfo);

            //System.IO.Stream S = new System.IO.MemoryStream();
            //stream.Seek((long)TiffInfo.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.ImageWidth).Offset, SeekOrigin.Begin);
            //byte[] S2 = new byte[Bytes];
            //stream.Read(S2, 0, Bytes);
            //S.Write(S2, 0, Bytes);

            ImageStream = TiffInfo.GetImageStream();
        }
        #endregion
        #endregion
        #endregion
    }
}