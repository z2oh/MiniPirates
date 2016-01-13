using MiniPirates.Engine.Objects;
using MiniPirates.Engine.Objects.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.GUI
{
    public class GUIObject : GameObject
    {
        GUIRenderer guiRenderer;

        public override void Initialize()
        {
            base.Initialize();
            guiRenderer = AddNewComponent<GUIRenderer>();
        }
    }
}
