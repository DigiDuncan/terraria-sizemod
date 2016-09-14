using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace sizemod
{
    public class sizemodPlayer : ModPlayer
    {
        public static float playerSize = 4.0f;
        private static List<Texture2D> WingsExtraTexOccupied = new List<Texture2D>{Main.itemFlameTexture[1866], Main.extraTexture[38], Main.FlameTexture[8], Main.glowMaskTexture[92], Main.glowMaskTexture[181], Main.glowMaskTexture[213], Main.glowMaskTexture[183]};
        Random rnd = new Random();
        
        public override void PreUpdate()
        {
            for (int k = 0; k < 255; k++)
            {
                int thisPlayerindex = Main.myPlayer;
                if (thisPlayerindex == k)
                {
                    Player thisPlayer = Main.player[k];
                    if (thisPlayer.active)
                    {
                        //playerSize = rnd.NextFloat() * 2.0f + 0.5f; // For testing purpose only

                        // Change player hitbox size
                        thisPlayer.position = thisPlayer.Bottom;
                        //thisPlayer.width = (int)(Player.defaultWidth * playerSize);
                        //thisPlayer.height = (int)(Player.defaultHeight * playerSize);
                        thisPlayer.Size = new Vector2(Player.defaultWidth * playerSize, Player.defaultHeight * playerSize);
                        thisPlayer.Bottom = thisPlayer.position;
                        
                        // Change item size
                        for (int l = 0; l < thisPlayer.inventory.Length; l++)
                        {
                            Item item = thisPlayer.inventory[l];
                            //item.scale *= GetSizeFromPrefix(item.prefix) * playerSize;
                        }
                        for (int l = 0; l < thisPlayer.miscEquips.Length; l++)
                        {
                            Item item = thisPlayer.miscEquips[l];
                            //item.scale = playerSize;
                        }
                        for (int l = 0; l < thisPlayer.armor.Length; l++)
                        {
                            Item item = thisPlayer.armor[l];
                            //item.scale = playerSize;
                        }

                        for (int l = 0; l < Main.npc.Length; l++)
                        {
                            NPC npc = Main.npc[l];
                            //npc.scale = playerSize;
                        }

                        // Change player max speed
                        thisPlayer.maxRunSpeed = player.maxRunSpeed * playerSize;
                    }
                }
            }
        }

        // Change player render size
        public static readonly PlayerLayer SizePlayerRender = new PlayerLayer("sizemod", "sizeBody", delegate (PlayerDrawInfo drawInfo)
        {
            List<DrawData> _playerDrawData = Main.playerDrawData;
            int count = _playerDrawData.Count;
            for (int i = 0; i < count; i++)
            {
                Player thisPlayer = Main.player[Main.myPlayer];
                var d = Main.playerDrawData[i];
                d.scale *= playerSize;
                Vector2 texOffset = new Vector2(d.sourceRect.Value.Width, d.sourceRect.Value.Height) - d.origin;
                texOffset.X -= d.sourceRect.Value.Width / 2;
                Vector2 thisPlayerPosOffset = (thisPlayer.Bottom - Main.screenPosition) - d.position;
                Vector2 totalOffset = thisPlayerPosOffset;
                if (d.texture == Main.wingsTexture[thisPlayer.wings]) totalOffset.Y = 0;
                for (int j = 0; j < WingsExtraTexOccupied.Count; j++)
                {
                    if (d.texture == WingsExtraTexOccupied[j]) totalOffset.Y = 0;
                }

                d.position -= totalOffset * (playerSize - 1f);
                d.position.Y -= 2f * (playerSize - 1f);
                Main.playerDrawData[i] = d;
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            SizePlayerRender.visible = true;
            layers.Add(SizePlayerRender);
        }

        // Function for getting item scale modifier from prefix ID. Taken from source code
        private static float GetSizeFromPrefix(int prefix)
        {
            if (prefix == 1)
            {
                return 1.12f;
            }
            else if (prefix == 2)
            {
                return 1.18f;
            }
            else if (prefix == 3)
            {
                return 1.05f;
            }
            else if (prefix == 5 || prefix == 81)
            {
                return 1.15f;
            }
            else if (prefix == 7)
            {
                return 0.82f;
            }
            else if (prefix == 8 || prefix == 10)
            {
                return 0.85f;
            }
            else if (prefix == 9 || prefix == 11)
            {
                return 0.9f;
            }
            else if (prefix == 4 || prefix == 6 || prefix == 12 || prefix == 13)
            {
                return 1.1f;
            }
            else
            {
                return 1.0f;
            }
        }
    }
}
