using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;
using SuperTiled2Unity;
using SuperTiled2Unity.Editor;

// Notes
// What we're tyring to do here is change the texture used by a tileset in realtime
// Unity doesn't (easily) allow this because tiles have sprites that may be cut up from textures and placed into sprite atlases
// Here's what we do to achieve our goal
// 1) Disable "Use Sprite Atlas for Tiles" in the tileset (.tsx file) that is imported into Unity.
// 2) Author a custom shader that samples from "summer" and "winter" textures.
// 3) Create a material that uses our custom shader with the summer and winter textures set.
// 4) Add a custom property (in Tiled) for the tile layer we want using this material
// 5) Import the Tiled map file (*.tmx file) using the custom importer defined in this file

[DisplayName("Swap Texture Importer")]
public class SwapTextureCustomTmxImporter : CustomTmxImporter
{
    public override void TmxAssetImported(TmxAssetImportedArgs args)
    {
        // Go over all tile layers in the exported map
        foreach (var layer in args.ImportedSuperMap.GetComponentsInChildren<SuperTileLayer>())
        {
            // Do any of the tile layers have a custom property on them telling us to use a specialized material?
            if (layer.gameObject.TryGetCustomPropertySafe("CustomMaterial", out CustomProperty property))
            {
                var materialName = property.GetValueAsString();
                var material = AssetDatabaseEx.LoadFirstAssetByFilter<Material>(materialName);

                if (material != null)
                {
                    // Get the tilemap renderer on the tile layer and assign the material
                    var renderer = layer.GetComponent<TilemapRenderer>();
                    renderer.material = material;
                }
                else
                {
                    Debug.LogError($"Material '{materialName}' not found.");
                }
            }
        }
    }
}
