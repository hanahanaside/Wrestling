using System;

public class IMobileIconParams
{
    public IMobileIconParams ()
    {
        iconNumber = 4;
        iconViewLayoutWidth = -1;
        iconTitleEnable = true;
        iconTitleFontColor = "#FFFFFF";      
        iconTitleShadowEnable = true;
        iconTitleShadowColor = "#000000";
        iconTitleShadowDx = -1;
        iconTitleShadowDy = -1;
    }

    public int iconNumber{ get; set; }
    public int iconViewLayoutWidth{ get; set; }
    public bool iconTitleEnable{ get; set; }
    public string iconTitleFontColor{ get; set; }
    public bool iconTitleShadowEnable{ get; set; }
    public string iconTitleShadowColor{ get; set; }
    public int iconTitleShadowDx{ get; set; }
    public int iconTitleShadowDy{ get; set; }
}