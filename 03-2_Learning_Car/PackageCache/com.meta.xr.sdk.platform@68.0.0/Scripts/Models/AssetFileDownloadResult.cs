// This file was @generated with LibOVRPlatform/codegen/main. Do not modify it!

namespace Oculus.Platform.Models
{
  using System;
  using System.Collections;
  using Oculus.Platform.Models;
  using System.Collections.Generic;
  using UnityEngine;

  /// An AssetFileDownloadResult represents an asset that has been successfully
  /// downloaded. It contains the asset file ID and the file path of the
  /// downloaded asset.
  public class AssetFileDownloadResult
  {
    /// ID of the asset file
    public readonly UInt64 AssetId;
    /// File path of the asset file.
    public readonly string Filepath;


    public AssetFileDownloadResult(IntPtr o)
    {
      AssetId = CAPI.ovr_AssetFileDownloadResult_GetAssetId(o);
      Filepath = CAPI.ovr_AssetFileDownloadResult_GetFilepath(o);
    }
  }

}
