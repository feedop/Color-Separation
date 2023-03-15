using Color_Separation.Separators;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Drawing.Imaging;
using System.Windows.Media;

namespace Color_Separation
{
    public class ColorSeparationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region bindable properties
        public Illuminant Illuminant
        {
            get { return ColorProfile.Illuminant; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }   
                ColorProfile.Illuminant = value;
                Update();
                OnPropertyChanged(nameof(WhiteX));
                OnPropertyChanged(nameof(WhiteY));
            }
        }
        public double WhiteX
        {
            get { return ColorProfile.Illuminant.X; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }  
                if (Illuminant.Description != "Custom Illuminant")
                {
                    customIlluminant.CustomFromIlluminant(Illuminant);
                    Illuminant = customIlluminant;
                    Illuminant.X = value;
                }  
                else
                {
                    Illuminant.X = value;
                    Update();
                }
                OnPropertyChanged(nameof(Illuminant));
            }
        }
        public double WhiteY
        {
            get { return ColorProfile.Illuminant.Y; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                if (Illuminant.Description != "Custom Illuminant")
                {
                    customIlluminant.CustomFromIlluminant(Illuminant);
                    Illuminant = customIlluminant;
                    Illuminant.Y = value;
                }
                else
                {
                    Illuminant.Y = value;
                    Update();
                }
                OnPropertyChanged(nameof(Illuminant));
            }
        }
        public double RedX
        {
            get { return ColorProfile.RedX; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.RedX = value;
                Update();
            }
        }
        public double RedY
        {
            get { return ColorProfile.RedY; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.RedY = value;
                Update();
            }
        }
        public double GreenX
        {
            get { return ColorProfile.GreenX; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.GreenX = value;
                Update();
            }
        }
        public double GreenY
        {
            get { return ColorProfile.GreenY; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.GreenY = value;
                Update();
            }
        }
        public double BlueX
        {
            get { return ColorProfile.BlueX; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.BlueX = value;
                Update();
            }
        }
        public double BlueY
        {
            get { return ColorProfile.BlueY; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.BlueY = value;
                Update();
            }
        }
        public double Gamma
        {
            get { return ColorProfile.Gamma; }
            set
            {
                if (ColorProfile.Description != "Custom color profile")
                {
                    customColorProfile.CustomFromProfile(colorProfile);
                    colorProfile = customColorProfile;
                    OnPropertyChanged(nameof(ColorProfile));
                }
                ColorProfile.Gamma = value;
                Update();
            }
        }
        #endregion

        // lists of records
        public List<Illuminant> Illuminants { get; init; }
        public List<ColorProfile> ColorProfiles { get; init; }
        private ColorProfile colorProfile;
        public ColorProfile ColorProfile
        {
            get { return colorProfile; }
            set
            {
                colorProfile = value;
                OnPropertyChanged(nameof(Illuminant));
                OnPropertyChanged(nameof(WhiteX));
                OnPropertyChanged(nameof(WhiteY));
                OnPropertyChanged(nameof(RedX));
                OnPropertyChanged(nameof(RedY));
                OnPropertyChanged(nameof(GreenX));
                OnPropertyChanged(nameof(GreenY));
                OnPropertyChanged(nameof(BlueX));
                OnPropertyChanged(nameof(BlueY));
                OnPropertyChanged(nameof(Gamma));
                Update();
            }
        }
        public List<Separator> Separators { get; init; }
        private Separator separator;
        public Separator Separator
        {
            get { return separator; }
            set { separator = value; Update(); }
        }

        private ColorProfile customColorProfile;
        private Illuminant customIlluminant;

        // bitmaps
        private WriteableBitmap? sourceImage;

        public WriteableBitmap? SourceImage
        {
            get { return sourceImage; }
            set
            {
                sourceImage = value;
                OnPropertyChanged(nameof(SourceImage));
                ResultImage1 = new WriteableBitmap(value);
                ResultImage2 = new WriteableBitmap(value);
                ResultImage3 = new WriteableBitmap(value);
                Update();
            }
        }

        public WriteableBitmap? ResultImage1 { get; set; }
        public WriteableBitmap? ResultImage2 { get; set; }
        public WriteableBitmap? ResultImage3 { get; set; }

        public ColorSeparationViewModel()
        {
            Illuminants = Illuminant.InitializeIlluminants();
            customIlluminant = Illuminants[Illuminants.Count - 1];

            ColorProfiles = ColorProfile.InitializeColorProfiles(Illuminants);
            colorProfile = ColorProfiles[0];
            customColorProfile = ColorProfiles[ColorProfiles.Count - 1];

            Separators = new List<Separator>();
            Separators.Add(new RGBSeparator());
            Separators.Add(new HSVSeparator());
            Separators.Add(new YCbCrSeparator());
            Separators.Add(new LabSeparator());
            separator = Separators[1];

        }
        /// <summary>
        /// Updates the result bitmaps
        /// </summary>
        private unsafe void Update()
        {
            if (SourceImage == null)
                return;
            Int32Rect rect = new Int32Rect(0, 0, SourceImage.PixelWidth, SourceImage.PixelHeight);
            try
            {
                SourceImage.Lock();
                ResultImage1!.Lock();
                ResultImage2!.Lock();
                ResultImage3!.Lock();
                int* result1BackBuffer = (int*)ResultImage1.BackBuffer;
                int* result2BackBuffer = (int*)ResultImage2.BackBuffer;
                int* result3BackBuffer = (int*)ResultImage3.BackBuffer;

                if (sourceImage!.BackBufferStride / SourceImage.PixelWidth == 4)
                {
                    int* sourceBackBuffer = (int*)SourceImage.BackBuffer;

                    //for (int i = 0; i < SourceImage.PixelWidth * SourceImage.PixelHeight; i++)
                    //{
                    //    (result1BackBuffer[i], result2BackBuffer[i], result3BackBuffer[i]) = Separator.Separate(sourceBackBuffer[i], ColorProfile);
                    //}

                    Parallel.For(0, SourceImage.PixelWidth * SourceImage.PixelHeight, i =>
                    {
                        (result1BackBuffer[i], result2BackBuffer[i], result3BackBuffer[i]) = Separator.Separate(sourceBackBuffer[i], ColorProfile);
                    });

                }
                else
                {
                    byte* sourceBackBuffer = (byte*)SourceImage.BackBuffer;

                    //for (int i = 0; i < SourceImage.PixelWidth * SourceImage.PixelHeight / 4; i++)
                    //{
                    //    (result1BackBuffer[i], result2BackBuffer[i], result3BackBuffer[i]) = Separator.Separate((int)sourceBackBuffer[i] * 4, ColorProfile);
                    //}

                    Parallel.For(0, SourceImage.PixelWidth * SourceImage.PixelHeight / 4, i =>
                    {
                        (result1BackBuffer[i], result2BackBuffer[i], result3BackBuffer[i]) = Separator.Separate(4 * sourceBackBuffer[i] , ColorProfile);
                    });
                }
            }
            finally
            {
                SourceImage.Unlock();
                ResultImage1?.AddDirtyRect(rect);
                ResultImage1?.Unlock();
                ResultImage2?.AddDirtyRect(rect);
                ResultImage2?.Unlock();
                ResultImage3?.AddDirtyRect(rect);
                ResultImage3?.Unlock();
                
            }

            OnPropertyChanged(nameof(ResultImage1));
            OnPropertyChanged(nameof(ResultImage2));
            OnPropertyChanged(nameof(ResultImage3));
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
