﻿using ValueObjects.Images;

namespace ValueObject.Tests.Images;

internal class color_should
{
    [Test]
    public void allow_to_convert_from_Hsl_to_Rgb()
    {
        var colour24BitFromRgb = Color.FromRgb(
            Intensity.FromScalar(255),
            Intensity.FromScalar(0),
            Intensity.FromScalar(0));
        var colour24BitFromHsl = Color.FromHsl(
            Hue.FromScalar(0),
            Saturation.FromScalar(100),
            Lightness.FromScalar(50));

        colour24BitFromRgb.Should().Be(colour24BitFromHsl);
    }
}
