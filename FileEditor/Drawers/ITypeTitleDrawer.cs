﻿using BlurFileFormats.XtFlask.Values;

namespace Editor.Drawers
{
    public interface ITypeTitleDrawer : ITypeDrawer
    {
        public void Draw(BlurFileFormats.XtFlask.XtDb xtDb, IXtValue xtValue);
    }
}