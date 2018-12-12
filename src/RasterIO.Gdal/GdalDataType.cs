//   This file contains the C# port of some DataType functions from the
//   source file "gdal/gcore/gdal_misc.cpp", which are not available in the
//   C# bindings.


/******************************************************************************
 * Project:  GDAL Core
 * Purpose:  Free standing functions for GDAL.
 * Author:   Frank Warmerdam, warmerdam@pobox.com
 *
 ******************************************************************************
 * Copyright (c) 1999, Frank Warmerdam
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 ****************************************************************************/

using OSGeo.GDAL;
using System;

namespace Landis.RasterIO.Gdal
{
    /// <summary>
    /// Functions related to GDAL data types.
    /// </summary>
    public static class GdalDataType
    {
        // New function; not in original C++ file

        /// <summary>
        /// Convert system type code to a GDAL data type.
        /// </summary>
        public static DataType FromTypeCode(TypeCode typeCode)
        {
            switch(typeCode)
            {
                case TypeCode.Byte:
                    return DataType.GDT_Byte;

                case TypeCode.Int16:
                    return DataType.GDT_Int16;
                case TypeCode.UInt16:
                    return DataType.GDT_UInt16;

                case TypeCode.Int32:
                    return DataType.GDT_Int32;
                case TypeCode.UInt32:
                    return DataType.GDT_UInt32;

                case TypeCode.Single:
                    return DataType.GDT_Float32;
                case TypeCode.Double:
                    return DataType.GDT_Float64;

                default:
                    return DataType.GDT_Unknown;
            }
        }
 
        //---------------------------------------------------------------------

        /// <summary>
        /// Return the smallest data type that can fully express both input
        /// data types.
        /// </summary>
        /// <param name="eType1">
        /// </param>
        /// <param name="eType2">
        /// </param>
        /// <returns>
        /// a data type able to express eType1 and eType2.
        /// </returns>
        public static DataType Union(DataType eType1,
                                     DataType eType2)
        {
            bool bFloating, bComplex, bSigned;
            int nBits;
        
            bComplex = IsComplex(eType1) | IsComplex(eType2);
            
            switch( eType1 )
            {
              case DataType.GDT_Byte:
                nBits = 8;
                bSigned = false;
                bFloating = false;
                break;
                
              case DataType.GDT_Int16:
              case DataType.GDT_CInt16:
                nBits = 16;
                bSigned = true;
                bFloating = false;
                break;
                
              case DataType.GDT_UInt16:
                nBits = 16;
                bSigned = false;
                bFloating = false;
                break;
                
              case DataType.GDT_Int32:
              case DataType.GDT_CInt32:
                nBits = 32;
                bSigned = true;
                bFloating = false;
                break;
                
              case DataType.GDT_UInt32:
                nBits = 32;
                bSigned = false;
                bFloating = false;
                break;
        
              case DataType.GDT_Float32:
              case DataType.GDT_CFloat32:
                nBits = 32;
                bSigned = true;
                bFloating = true;
                break;
        
              case DataType.GDT_Float64:
              case DataType.GDT_CFloat64:
                nBits = 64;
                bSigned = true;
                bFloating = true;
                break;
        
              default:
                return DataType.GDT_Unknown;
            }
        
            switch( eType2 )
            {
              case DataType.GDT_Byte:
                break;
                
              case DataType.GDT_Int16:
              case DataType.GDT_CInt16:
                nBits = Math.Max(nBits,16);
                bSigned = true;
                break;
                
              case DataType.GDT_UInt16:
                nBits = Math.Max(nBits,16);
                break;
                
              case DataType.GDT_Int32:
              case DataType.GDT_CInt32:
                nBits = Math.Max(nBits,32);
                bSigned = true;
                break;
                
              case DataType.GDT_UInt32:
                nBits = Math.Max(nBits,32);
                break;
        
              case DataType.GDT_Float32:
              case DataType.GDT_CFloat32:
                nBits = Math.Max(nBits,32);
                bSigned = true;
                bFloating = true;
                break;
        
              case DataType.GDT_Float64:
              case DataType.GDT_CFloat64:
                nBits = Math.Max(nBits,64);
                bSigned = true;
                bFloating = true;
                break;
        
              default:
                return DataType.GDT_Unknown;
            }
        
            if( nBits == 8 )
                return DataType.GDT_Byte;
            else if( nBits == 16 && bComplex )
                return DataType.GDT_CInt16;
            else if( nBits == 16 && bSigned )
                return DataType.GDT_Int16;
            else if( nBits == 16 && !bSigned )
                return DataType.GDT_UInt16;
            else if( nBits == 32 && bFloating && bComplex )
                return DataType.GDT_CFloat32;
            else if( nBits == 32 && bFloating )
                return DataType.GDT_Float32;
            else if( nBits == 32 && bComplex )
                return DataType.GDT_CInt32;
            else if( nBits == 32 && bSigned )
                return DataType.GDT_Int32;
            else if( nBits == 32 && !bSigned )
                return DataType.GDT_UInt32;
            else if( nBits == 64 && bComplex )
                return DataType.GDT_CFloat64;
            else
                return DataType.GDT_Float64;
        }
 
        //---------------------------------------------------------------------

        /// <summary>
        /// Is data type complex?
        /// </summary>
        /// <returns>
        /// true if the passed type is complex (one of GDT_CInt16, GDT_CInt32,
        /// GDT_CFloat32 or GDT_CFloat64), that is it consists of a real and
        /// imaginary component.
        /// </returns>
        public static bool IsComplex(DataType eDataType)
        {
            switch( eDataType )
            {
              case DataType.GDT_CInt16:
              case DataType.GDT_CInt32:
              case DataType.GDT_CFloat32:
              case DataType.GDT_CFloat64:
                return true;
        
              default:
                return false;
            }
        }
    }
}
