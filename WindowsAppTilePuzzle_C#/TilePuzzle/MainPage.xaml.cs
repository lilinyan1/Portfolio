//Authors: Amshar Basheer and Linyan (Becky) Li
//Project Name: TilePuzzle
//File Name: MainPage.xaml.cs
//Date: 2014-12-09
//Description: Contains the code for the MainPage of the Tile Puzzle game.  The methods that are called to handle different events are found here and include:
//  loading/saving state, GetPhotoButton, Start button, tile movement, and pause/resume game.

/// reference: http://msdn.microsoft.com/en-ca/library/windows/apps/jj676794.aspx 

///         notification tile won't update
///         swith start to pause after resume
///         save current image at start(once only) load it in load state
using TilePuzzle.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
//tile notification
using Windows.UI.Notifications;
using NotificationsExtensions.TileContent;
//using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Media.Capture;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.Storage.Streams;


//camera
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using Windows.Storage.Pickers;

//using System.Drawing;
//using System.Web;
//using System.Windows.Media.Imaging;
// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace TilePuzzle
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // defult variables
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private string mruToken = null;

        // timer variables
        DispatcherTimer dispatcherTimer;
        int timesTicked = 0;
        string timeStr;

        //some status variables / flags to keep track of current state
        bool gameOn = false;
        bool gamePaused = false;
        bool havePicture = false;

        bool isImageSaved = false;

        //make 2D array of objects representing different spots on the board to keep track of status of board
        public static Spot [,] spots = new Spot [4,4];

        RenderTargetBitmap renderTargetBitmap;
        const string tileImageFile = "tileImg.png";
        const string currentImageFile = "currentImg.png";

        //camera
        private WriteableBitmap _writeableBitmap;
        private ShareOperation _shareOperation;
        

        //declare moveMatrix 2D array of structs declared as [15,16] so will number [0-14,0-15] 
        //the first index represents tileToMove (15 possible tiles) and second index represents destSpot (16 possible spots).
        //initializing whole matrix with declaration
        static MoveValues[,] moveMatrix = new MoveValues[15,16] 
        {
            {new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(300,0), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100), new MoveValues(300,100),
            new MoveValues(0,200), new MoveValues(100,200), new MoveValues(200,200), new MoveValues(300,200), new MoveValues(0,300), new MoveValues(100,300), new MoveValues(200,300), new MoveValues(300,300)},
            
            {new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100),
            new MoveValues(-100,200), new MoveValues(0,200), new MoveValues(100,200), new MoveValues(200,200), new MoveValues(-100,300), new MoveValues(0,300), new MoveValues(100,300), new MoveValues(200,300)},

            {new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100),
            new MoveValues(-200,200), new MoveValues(-100,200), new MoveValues(0,200), new MoveValues(100,200), new MoveValues(-200,300), new MoveValues(-100,300), new MoveValues(0,300), new MoveValues(100,300)},

            {new MoveValues(-300,0), new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(-300,100), new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100),
            new MoveValues(-300,200), new MoveValues(-200,200), new MoveValues(-100,200), new MoveValues(0,200), new MoveValues(-300,300), new MoveValues(-200,300), new MoveValues(-100,300), new MoveValues(0,300)},

            {new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100), new MoveValues(300,-100), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(300,0),
            new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100), new MoveValues(300,100), new MoveValues(0,200), new MoveValues(100,200), new MoveValues(200,200), new MoveValues(300,200)},

            {new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0),
            new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100), new MoveValues(-100,200), new MoveValues(0,200), new MoveValues(100,200), new MoveValues(200,200)},

            {new MoveValues(-200,-100), new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0),
            new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(-200,200), new MoveValues(-100,200), new MoveValues(0,200), new MoveValues(100,200)},

            {new MoveValues(-300,-100), new MoveValues(-200,-100), new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(-300,0), new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0),
            new MoveValues(-300,100), new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(-300,200), new MoveValues(-200,200), new MoveValues(-100,200), new MoveValues(0,200)},

            {new MoveValues(0,-200), new MoveValues(100,-200), new MoveValues(200,-200), new MoveValues(300,-200), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100), new MoveValues(300,-100),
            new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(300,0), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100), new MoveValues(300,100)},

            {new MoveValues(-100,-200), new MoveValues(0,-200), new MoveValues(100,-200), new MoveValues(200,-200), new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100),
            new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100), new MoveValues(200,100)},

            {new MoveValues(-200,-200), new MoveValues(-100,-200), new MoveValues(0,-200), new MoveValues(100,-200), new MoveValues(-200,-100), new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100),
            new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100), new MoveValues(100,100)},

            {new MoveValues(-300,-200), new MoveValues(-200,-200), new MoveValues(-100,-200), new MoveValues(0,-200), new MoveValues(-300,-100), new MoveValues(-200,-100), new MoveValues(-100,-100), new MoveValues(0,-100),
            new MoveValues(-300,0), new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(-300,100), new MoveValues(-200,100), new MoveValues(-100,100), new MoveValues(0,100)},

            {new MoveValues(0,-300), new MoveValues(100,-300), new MoveValues(200,-300), new MoveValues(300,-300), new MoveValues(0,-200), new MoveValues(100,-200), new MoveValues(200,-200), new MoveValues(300,-200),
            new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100), new MoveValues(300,-100), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0), new MoveValues(300,0)},

            {new MoveValues(-100,-300), new MoveValues(0,-300), new MoveValues(100,-300), new MoveValues(200,-300), new MoveValues(-100,-200), new MoveValues(0,-200), new MoveValues(100,-200), new MoveValues(200,-200),
            new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(200,-100), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0), new MoveValues(200,0)},

            {new MoveValues(-200,-300), new MoveValues(-100,-300), new MoveValues(0,-300), new MoveValues(100,-300), new MoveValues(-200,-200), new MoveValues(-100,-200), new MoveValues(0,-200), new MoveValues(100,-200),
            new MoveValues(-200,-100), new MoveValues(-100,-100), new MoveValues(0,-100), new MoveValues(100,-100), new MoveValues(-200,0), new MoveValues(-100,0), new MoveValues(0,0), new MoveValues(100,0)}
              
        };
               

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // Restore values stored in session state.
            if (e.PageState != null && e.PageState.ContainsKey("timer"))
            {
                timer.Text = e.PageState["timer"].ToString();
                timesTicked = int.Parse(e.PageState["timesTicked"].ToString());

                gameOn = (bool)e.PageState["gameOn"];
                if (gameOn)
                {
                    startButton.Visibility = Visibility.Visible;
                    DispatcherTimerSetup();
                    dispatcherTimer.Start();
                }
                gamePaused = (bool)e.PageState["gamePaused"];
                havePicture = (bool)e.PageState["havePicture"];

                displayImage.Source = new BitmapImage(new Uri(@"ms-appdata:///local/" + currentImageFile));
             
                if (havePicture)
                {
                    startButton.Visibility = Visibility.Visible;
                    pauseButton.Visibility = Visibility.Visible;
                }
                displayImage.Source = new BitmapImage(new Uri(@"ms-appdata:///local/" + currentImageFile));

            }

            //restore picture if one was selected using GetPhotoButton
            if (e.PageState != null && e.PageState.ContainsKey("mruToken"))
            {
                object value = null;
                if (e.PageState.TryGetValue("mruToken", out value))
                {
                    if (value != null)
                    {
                        mruToken = value.ToString();

                        // Open the file via the token that you stored when adding this file into the MRU list.
                        Windows.Storage.StorageFile file =
                            await Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.GetFileAsync(mruToken);

                        if (file != null)
                        {
                            // Open a stream for the selected file.
                            Windows.Storage.Streams.IRandomAccessStream fileStream =
                                await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                            // Set the image source to a bitmap.
                            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                                new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                            //convert size from actual image size to standardized size of our board of 400x400, which will makes tiles of 100x100
                            bitmapImage.DecodePixelWidth = 400;
                            bitmapImage.DecodePixelHeight = 400;

                            bitmapImage.SetSource(fileStream);
                            displayImage.Source = bitmapImage;

                            // Set the data context for the page.
                            this.DataContext = file;                            

                        }
                    }
                }
            }            

            //if start button had been clicked before then restore tiles and related game data
            if (e.PageState != null && e.PageState.ContainsKey("gameOn"))
            {
                if (gameOn || gamePaused)
                {
                    //cut image into tiles
                    tile1.Source = displayImage.Source;
                    RectangleGeometry r = new RectangleGeometry();
                    r.Rect = new Rect(0, 0, 100, 100);
                    tile1.Clip = r;

                    tile2.Source = displayImage.Source;
                    RectangleGeometry r2 = new RectangleGeometry();
                    r2.Rect = new Rect(100, 0, 100, 100);
                    tile2.Clip = r2;

                    tile3.Source = displayImage.Source;
                    RectangleGeometry r3 = new RectangleGeometry();
                    r3.Rect = new Rect(200, 0, 100, 100);
                    tile3.Clip = r3;

                    tile4.Source = displayImage.Source;
                    RectangleGeometry r4 = new RectangleGeometry();
                    r4.Rect = new Rect(300, 0, 100, 100);
                    tile4.Clip = r4;

                    tile5.Source = displayImage.Source;
                    RectangleGeometry r5 = new RectangleGeometry();
                    r5.Rect = new Rect(0, 100, 100, 100);
                    tile5.Clip = r5;

                    tile6.Source = displayImage.Source;
                    RectangleGeometry r6 = new RectangleGeometry();
                    r6.Rect = new Rect(100, 100, 100, 100);
                    tile6.Clip = r6;

                    tile7.Source = displayImage.Source;
                    RectangleGeometry r7 = new RectangleGeometry();
                    r7.Rect = new Rect(200, 100, 100, 100);
                    tile7.Clip = r7;

                    tile8.Source = displayImage.Source;
                    RectangleGeometry r8 = new RectangleGeometry();
                    r8.Rect = new Rect(300, 100, 100, 100);
                    tile8.Clip = r8;

                    tile9.Source = displayImage.Source;
                    RectangleGeometry r9 = new RectangleGeometry();
                    r9.Rect = new Rect(0, 200, 100, 100);
                    tile9.Clip = r9;

                    tile10.Source = displayImage.Source;
                    RectangleGeometry r10 = new RectangleGeometry();
                    r10.Rect = new Rect(100, 200, 100, 100);
                    tile10.Clip = r10;

                    tile11.Source = displayImage.Source;
                    RectangleGeometry r11 = new RectangleGeometry();
                    r11.Rect = new Rect(200, 200, 100, 100);
                    tile11.Clip = r11;

                    tile12.Source = displayImage.Source;
                    RectangleGeometry r12 = new RectangleGeometry();
                    r12.Rect = new Rect(300, 200, 100, 100);
                    tile12.Clip = r12;

                    tile13.Source = displayImage.Source;
                    RectangleGeometry r13 = new RectangleGeometry();
                    r13.Rect = new Rect(0, 300, 100, 100);
                    tile13.Clip = r13;

                    tile14.Source = displayImage.Source;
                    RectangleGeometry r14 = new RectangleGeometry();
                    r14.Rect = new Rect(100, 300, 100, 100);
                    tile14.Clip = r14;

                    tile15.Source = displayImage.Source;
                    RectangleGeometry r15 = new RectangleGeometry();
                    r15.Rect = new Rect(200, 300, 100, 100);
                    tile15.Clip = r15;

                    //initialize 2D array
                    spots = GameLogic.InitArray<Spot>(4, 4);

                    // Restore values stored in app data for TileNum's in spots 2D array.
                    Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                    if (roamingSettings.Values.ContainsKey("spot16TileNum"))
                    {
                        spots[0, 0].TileNum = (int)roamingSettings.Values["spot1TileNum"];
                        spots[0, 1].TileNum = (int)roamingSettings.Values["spot2TileNum"];
                        spots[0, 2].TileNum = (int)roamingSettings.Values["spot3TileNum"];
                        spots[0, 3].TileNum = (int)roamingSettings.Values["spot4TileNum"];
                        spots[1, 0].TileNum = (int)roamingSettings.Values["spot5TileNum"];
                        spots[1, 1].TileNum = (int)roamingSettings.Values["spot6TileNum"];
                        spots[1, 2].TileNum = (int)roamingSettings.Values["spot7TileNum"];
                        spots[1, 3].TileNum = (int)roamingSettings.Values["spot8TileNum"];
                        spots[2, 0].TileNum = (int)roamingSettings.Values["spot9TileNum"];
                        spots[2, 1].TileNum = (int)roamingSettings.Values["spot10TileNum"];
                        spots[2, 2].TileNum = (int)roamingSettings.Values["spot11TileNum"];
                        spots[2, 3].TileNum = (int)roamingSettings.Values["spot12TileNum"];
                        spots[3, 0].TileNum = (int)roamingSettings.Values["spot13TileNum"];
                        spots[3, 1].TileNum = (int)roamingSettings.Values["spot14TileNum"];
                        spots[3, 2].TileNum = (int)roamingSettings.Values["spot15TileNum"];
                        spots[3, 3].TileNum = (int)roamingSettings.Values["spot16TileNum"];
                        
                    }

                    int counter = 1; //used in the following loop to set spot numbers 1 to 16

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            spots[i, j].SpotNum = counter;

                            //check which tile number we are dealing with so we know which actual tile image to move to the current spot
                            if (spots[i, j].TileNum == 1)
                            {
                                tile1.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 2)
                            {
                                tile2.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 3)
                            {
                                tile3.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 4)
                            {
                                tile4.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 5)
                            {
                                tile5.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 6)
                            {
                                tile6.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 7)
                            {
                                tile7.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 8)
                            {
                                tile8.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 9)
                            {
                                tile9.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 10)
                            {
                                tile10.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 11)
                            {
                                tile11.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 12)
                            {
                                tile12.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 13)
                            {
                                tile13.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 14)
                            {
                                tile14.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 15)
                            {
                                tile15.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }

                            ++counter;
                        }
                    }
                    //now that TileNum's and SpotNum's are updated in spots 2D array -- just need to update CanMove's using the following method call
                    GameLogic.UpdateCanMoveSpots();
                }
            }

            
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            //save data for timer and status variables
            e.PageState["timer"] = timer.Text;            
            e.PageState["timesTicked"] = timesTicked;
            e.PageState["gameOn"] = gameOn;
            e.PageState["gamePaused"] = gamePaused;
            e.PageState["havePicture"] = havePicture;
            //await SaveElement(displayImage, currentImageFile);
            //save state data for image selected using GetPhotoButton
            if (!string.IsNullOrEmpty(mruToken))
            {
                e.PageState["mruToken"] = mruToken;
            }
                        
        }


        //Method Name: SaveElement
        //Parameters: UIElement saveElement, string desiredName
        //Return: void
        //Description: save xaml element as image under the app data folder
        private async Task SaveElement(UIElement saveElement, string desiredName)
        {

            //installation folder is read-only, http://msdn.microsoft.com/en-us/library/windows/apps/hh967755.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1
            // StorageFolder localFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            var renderTargetBitmap = new RenderTargetBitmap();

            await renderTargetBitmap.RenderAsync(saveElement);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            try
            {
                var file = await localFolder.CreateFileAsync(desiredName, CreationCollisionOption.ReplaceExisting);

                using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Ignore,
                        (uint)renderTargetBitmap.PixelWidth,
                        (uint)renderTargetBitmap.PixelHeight, 96d, 96d,
                        pixelBuffer.ToArray());

                    await encoder.FlushAsync();
                }
                
            }
            catch (Exception ex)
            {

            }
            //if (desiredName == "tileImg.png" )
            //{
            //    SendTileNotification();
            //}
        }    
    

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        //Method Name: GetPhotoButton_Click
        //Parameters: object sender, RoutedEventArgs e
        //Return: void
        //Description: event handler for GetPhotoButton click allows user to pick a photo to use for the game and resizes it to a standard 400x400
        private async void GetPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            startButton.Visibility = Visibility.Visible;
            //the following code was borrowed from the Hello World exercise
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            // file is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                // Set the image source to the selected bitmap.
                Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                    new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                //convert size from actual image size to standardized size of our board of 400x400, which will makes tiles of 100x100
                bitmapImage.DecodePixelWidth = 400;
                bitmapImage.DecodePixelHeight = 400;

                //set displayImage.Source to use this now 400x400 image
                await bitmapImage.SetSourceAsync(fileStream);
                displayImage.Source = bitmapImage;

                startButton.Visibility = Visibility.Visible;

                this.DataContext = file;

                // Add picked file to MostRecentlyUsedList.
                mruToken = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(file);

                havePicture = true;

                dispatcherTimer.Stop();
                gameOn = false;
                gamePaused = true;
                timesTicked = 0;
                startButton.Content = "Start";
                pauseButton.Content = "Pause";
                pauseButton.Visibility = Visibility.Collapsed;

                
            }

        }


        //Method Name: pageRoot_Loaded
        //Parameters: object sender, RoutedEventArgs e
        //Return: void
        //Description: load timer
        private void pageRoot_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimerSetup();
            
        }

        //Method Name: DispatcherTimerSetup
        //Parameters: none
        //Return: void
        //Description: used to setup timer
        public void DispatcherTimerSetup()
        {
            if (dispatcherTimer == null) //only setup timer if not already setup
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += dispatcherTimer_Tick; // fires tick event for every tick
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);   //(hour,minute,second)
                //IsEnabled defaults to false
            }
        }

        //Method Name: dispatcherTimer_Tick
        //Parameters: object sender, object e
        //Return: void
        //Description: handles tick event for each tick of timer, which includes calculating and displaying time elapsed
        void dispatcherTimer_Tick(object sender, object e)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;

            //DateTimeOffset time = DateTimeOffset.Now;

            timesTicked++;
            hour = timesTicked / 3600 % 60;
            minute = timesTicked / 60 % 60;
            second = timesTicked % 60;


            timeStr = "Time: " + hour.ToString() + " : " + minute.ToString() + " : " + second.ToString();
            timer.Text = timeStr;
            TileUpdate();
            //SendTileNotification();
            SendLiveTileUpdate();
        }

        //Method Name: GameStart
        //Parameters: object sender, RoutedEventArgs e
        //Return: void
        //Description: handles event of clicking start button. If havePicture then cuts picture into tiles, scrambles tiles (and makes sure solvable configuration),
        //  saves state related data, updates flags, and starts timer. Also initializes and sets values for spots 2D array used to keep track of game state.
        private void GameStart(object sender, RoutedEventArgs e)
        {
            if (havePicture)
            {
                pauseButton.Visibility = Visibility.Visible;
                startButton.Content = "Restart";
                //make sure win msg isn't currently displayed
                winMsg.Text = "";

                //set each tile to have same source as displayImage, then make a rectangle of size 100x100 used to clip appropriate location of image 
                //to make respective tile. Repeat process for all 15 tiles.            

                tile1.Source = displayImage.Source;
                RectangleGeometry r = new RectangleGeometry();
                r.Rect = new Rect(0, 0, 100, 100);
                tile1.Clip = r;

                tile2.Source = displayImage.Source;
                RectangleGeometry r2 = new RectangleGeometry();
                r2.Rect = new Rect(100, 0, 100, 100);
                tile2.Clip = r2;

                tile3.Source = displayImage.Source;
                RectangleGeometry r3 = new RectangleGeometry();
                r3.Rect = new Rect(200, 0, 100, 100);
                tile3.Clip = r3;

                tile4.Source = displayImage.Source;
                RectangleGeometry r4 = new RectangleGeometry();
                r4.Rect = new Rect(300, 0, 100, 100);
                tile4.Clip = r4;

                tile5.Source = displayImage.Source;
                RectangleGeometry r5 = new RectangleGeometry();
                r5.Rect = new Rect(0, 100, 100, 100);
                tile5.Clip = r5;

                tile6.Source = displayImage.Source;
                RectangleGeometry r6 = new RectangleGeometry();
                r6.Rect = new Rect(100, 100, 100, 100);
                tile6.Clip = r6;

                tile7.Source = displayImage.Source;
                RectangleGeometry r7 = new RectangleGeometry();
                r7.Rect = new Rect(200, 100, 100, 100);
                tile7.Clip = r7;

                tile8.Source = displayImage.Source;
                RectangleGeometry r8 = new RectangleGeometry();
                r8.Rect = new Rect(300, 100, 100, 100);
                tile8.Clip = r8;

                tile9.Source = displayImage.Source;
                RectangleGeometry r9 = new RectangleGeometry();
                r9.Rect = new Rect(0, 200, 100, 100);
                tile9.Clip = r9;

                tile10.Source = displayImage.Source;
                RectangleGeometry r10 = new RectangleGeometry();
                r10.Rect = new Rect(100, 200, 100, 100);
                tile10.Clip = r10;

                tile11.Source = displayImage.Source;
                RectangleGeometry r11 = new RectangleGeometry();
                r11.Rect = new Rect(200, 200, 100, 100);
                tile11.Clip = r11;

                tile12.Source = displayImage.Source;
                RectangleGeometry r12 = new RectangleGeometry();
                r12.Rect = new Rect(300, 200, 100, 100);
                tile12.Clip = r12;

                tile13.Source = displayImage.Source;
                RectangleGeometry r13 = new RectangleGeometry();
                r13.Rect = new Rect(0, 300, 100, 100);
                tile13.Clip = r13;

                tile14.Source = displayImage.Source;
                RectangleGeometry r14 = new RectangleGeometry();
                r14.Rect = new Rect(100, 300, 100, 100);
                tile14.Clip = r14;

                tile15.Source = displayImage.Source;
                RectangleGeometry r15 = new RectangleGeometry();
                r15.Rect = new Rect(200, 300, 100, 100);
                tile15.Clip = r15;

                //initialize 2D array
                spots = GameLogic.InitArray<Spot>(4, 4);

                /* Array of Spot objects to represent game board
                 *  [0,0]  [0,1]  [0,2]  [0,3] 
                 *  [1,0]  [1,1]  [1,2]  [1,3] 
                 *  [2,0]  [2,1]  [2,2]  [2,3] 
                 *  [3,0]  [3,1]  [3,2]  [3,3] 
                 */

                bool solvable = false;

                do
                {
                    
                    //the following block of code used to randomize the set of tile numbers 
                    //was borrowed from http://stackoverflow.com/questions/5864921/how-can-i-randomize-numbers-in-an-array
                    int[] numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
                    List<int> randomized = new List<int>();
                    List<int> original = new List<int>(numbers);
                    Random rand = new Random();
                    while (original.Count > 0)
                    {
                        int index = rand.Next(original.Count);
                        randomized.Add(original[index]);
                        original.RemoveAt(index);
                    }


                    int counter = 1; //used in loop to set spot numbers 1 to 16
                    int indexCounter = 0; //used to go through randomized list index by index

                    //Now going through scramble process and recording initial scrambled state of board
                    //loop through 2D array of spots and scramble tiles (keeping record of what tiles are in what spots and actually moving the tiles to their spots)
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            spots[i, j].SpotNum = counter;
                            spots[i, j].TileNum = randomized[indexCounter];

                            //check which tile number we are dealing with so we know which actual tile image to move to the current spot
                            if (spots[i, j].TileNum == 1)
                            {
                                tile1.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 2)
                            {
                                tile2.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 3)
                            {
                                tile3.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 4)
                            {
                                tile4.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 5)
                            {
                                tile5.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 6)
                            {
                                tile6.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 7)
                            {
                                tile7.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 8)
                            {
                                tile8.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 9)
                            {
                                tile9.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 10)
                            {
                                tile10.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 11)
                            {
                                tile11.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 12)
                            {
                                tile12.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 13)
                            {
                                tile13.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 14)
                            {
                                tile14.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }
                            else if (spots[i, j].TileNum == 15)
                            {
                                tile15.Margin = new Thickness(moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveX, moveMatrix[spots[i, j].TileNum - 1, spots[i, j].SpotNum - 1].GetMoveY, 0, 0);
                            }

                            ++counter;
                            ++indexCounter;
                        }
                    }
                    //call method to check if solvable
                    solvable = GameLogic.IsSolvable();
                }
                while (!solvable); //loop again to scramble again if was an unsolvable configuration

                //now that proper scramble done can save state related data in case need to restore after suspension+termination
                Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                roamingSettings.Values["spot1TileNum"] = spots[0, 0].TileNum;
                roamingSettings.Values["spot2TileNum"] = spots[0, 1].TileNum;
                roamingSettings.Values["spot3TileNum"] = spots[0, 2].TileNum;
                roamingSettings.Values["spot4TileNum"] = spots[0, 3].TileNum;
                roamingSettings.Values["spot5TileNum"] = spots[1, 0].TileNum;
                roamingSettings.Values["spot6TileNum"] = spots[1, 1].TileNum;
                roamingSettings.Values["spot7TileNum"] = spots[1, 2].TileNum;
                roamingSettings.Values["spot8TileNum"] = spots[1, 3].TileNum;
                roamingSettings.Values["spot9TileNum"] = spots[2, 0].TileNum;
                roamingSettings.Values["spot10TileNum"] = spots[2, 1].TileNum;
                roamingSettings.Values["spot11TileNum"] = spots[2, 2].TileNum;
                roamingSettings.Values["spot12TileNum"] = spots[2, 3].TileNum;
                roamingSettings.Values["spot13TileNum"] = spots[3, 0].TileNum;
                roamingSettings.Values["spot14TileNum"] = spots[3, 1].TileNum;
                roamingSettings.Values["spot15TileNum"] = spots[3, 2].TileNum;
                roamingSettings.Values["spot16TileNum"] = spots[3, 3].TileNum;

                //update canMove in spots
                GameLogic.UpdateCanMoveSpots();

                //set gameOn flag to true and start timer
                gameOn = true;
                gamePaused = false;
                timesTicked = 0;
                dispatcherTimer.Start();
                //IsEnabled should now be true after calling start              
                
            }
        }

        //Method Name: Tile_Tapped
        //Parameters: object sender: used to extract object as Image type that represents image that was tapped
        //              TappedRoutedEventArgs e
        //Return: void
        //Description: event handler for tap of any tile. If gameOn and tile is movable then will move tile, update spots and save state data, and also
        //  checks if solved and if solved will stop game and timer and display win msg
        private void Tile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image tile = (Image)sender; //this line allows us to extract the object as an Image type (represents the image that was tapped) which we can use to execute the move with .Margin = new Thickness(...)
            bool movable = false;
            int whereEmpty = 0;
            int tileLocIndex1 = 0;
            int tileLocIndex2 = 0;
            int emptyLocIndex1 = 0;
            int emptyLocIndex2 = 0;
            int selectedTile = 0;
            //these next two variables will be used as indexes (after appending to them) for saving state data further down
            String saveSpot1 = "spot";
            String saveSpot2 = "spot";


            if (gameOn) //only if gameOn is true will we actually check if a tile should move
            {
                //determine what number of tile we are dealing with by checking which image was tapped to call this event handler method
                if (e.OriginalSource == tile1)
                {
                    selectedTile = 1;
                }
                else if (e.OriginalSource == tile2)
                {
                    selectedTile = 2;
                }
                else if (e.OriginalSource == tile3)
                {
                    selectedTile = 3;
                }
                else if (e.OriginalSource == tile4)
                {
                    selectedTile = 4;
                }
                else if (e.OriginalSource == tile5)
                {
                    selectedTile = 5;
                }
                else if (e.OriginalSource == tile6)
                {
                    selectedTile = 6;
                }
                else if (e.OriginalSource == tile7)
                {
                    selectedTile = 7;
                }
                else if (e.OriginalSource == tile8)
                {
                    selectedTile = 8;
                }
                else if (e.OriginalSource == tile9)
                {
                    selectedTile = 9;
                }
                else if (e.OriginalSource == tile10)
                {
                    selectedTile = 10;
                }
                else if (e.OriginalSource == tile11)
                {
                    selectedTile = 11;
                }
                else if (e.OriginalSource == tile12)
                {
                    selectedTile = 12;
                }
                else if (e.OriginalSource == tile13)
                {
                    selectedTile = 13;
                }
                else if (e.OriginalSource == tile14)
                {
                    selectedTile = 14;
                }
                else if (e.OriginalSource == tile15)
                {
                    selectedTile = 15;
                }

                //loop through 2D array of spots and determine which spot our tile is at, whether it is movable, which spot is empty, and also append to the two
                //  index strings which will be used for updating save state related data
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (spots[i, j].TileNum == selectedTile)
                        {
                            movable = spots[i, j].CanMove;
                            saveSpot1 += spots[i, j].SpotNum.ToString() + "TileNum";
                            tileLocIndex1 = i;
                            tileLocIndex2 = j;
                        }
                        if (spots[i, j].TileNum == 16)
                        {
                            whereEmpty = spots[i, j].SpotNum;
                            saveSpot2 += spots[i, j].SpotNum.ToString() + "TileNum";
                            emptyLocIndex1 = i;
                            emptyLocIndex2 = j;
                        }
                    }
                }

                //if our tile was movable then can move it to empty spot
                if (movable)
                {
                    //move tile using moveMatrix to get x and y values. First index in moveMatrix is selectedTile-1 (b/c start at 0) and second is whereEmpty-1 (b/c start at 0)
                    tile.Margin = new Thickness(moveMatrix[selectedTile - 1, whereEmpty - 1].GetMoveX, moveMatrix[selectedTile - 1, whereEmpty - 1].GetMoveY, 0, 0);

                    //update spots to keep status of board up to date
                    //since tile number at the two spots have changed need to update just those two spots

                    //first set spot that had our tile to be empty (tileNum of 16)
                    spots[tileLocIndex1, tileLocIndex2].TileNum = 16;

                    //then set spot that was empty to be our tile number
                    spots[emptyLocIndex1, emptyLocIndex2].TileNum = selectedTile;

                    //update saves of app data so can restore if program suspends and terminates
                    Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
                    roamingSettings.Values[saveSpot1] = spots[tileLocIndex1, tileLocIndex2].TileNum;
                    roamingSettings.Values[saveSpot2] = spots[emptyLocIndex1, emptyLocIndex2].TileNum;
                    
                    //need to call method that will update canMove in spots
                    GameLogic.UpdateCanMoveSpots();

                    //need to check if solved
                    if (GameLogic.CheckIfSolved())
                    {
                        //display win msg, set gameOn flag to false, and stop timer
                        winMsg.Text = "You Win!";
                        gameOn = false;
                        dispatcherTimer.Stop();                        
                    }
                }
            }
        }
        
        //Method Name: PauseGame
        //Parameters: object sender, TappedRoutedEventArgs e
        //Return: void
        //Description: event handler for pause button. If gameOn and timer on then will stop timer, set gameOn to false, and gamePaused to true.
       private void PauseGame(object sender, TappedRoutedEventArgs e)
       {
            if (dispatcherTimer.IsEnabled == true && gameOn == true)
            {
                dispatcherTimer.Stop();
                gameOn = false;
                gamePaused = true;
                pauseButton.Content = "Start";
            }
            else if (gamePaused)
            {
                dispatcherTimer.Start();
                gameOn = true;
                gamePaused = false;
                pauseButton.Content = "Pause";
            }

       }

       //Method Name: ResumeGame
       //Parameters: object sender, TappedRoutedEventArgs e
       //Return: void
       //Description: event handler for resume button. If game paused then will start timer, set gameOn to true, and gamePaused to false.
       private void ResumeGame(object sender, TappedRoutedEventArgs e)
       {
           if (gamePaused)
           {
               dispatcherTimer.Start();
               gameOn = true;
               gamePaused = false;
           }
       }


       //Method Name: SendTileNotification
       //Parameters: none
       //Return: void
       //Description: TODO: send update to notification tile
       private void SendTileNotification()
       {

           ITileSquare150x150Image tileContent = TileContentFactory.CreateTileSquare150x150Image();

           //tileContent.TextCaptionWrap.Text = "Tile Puzzle Game Status";
           // must clear the current tile before update it
 
           //TileUpdateManager.CreateTileUpdaterForApplication().Clear();
           tileContent.Image.Src = "ms-appdata:///local/" + tileImageFile;
           tileContent.Image.Alt = "Tile Puzzle Game Status";
           TileUpdateManager.CreateTileUpdaterForApplication().Update(tileContent.CreateNotification());

       }

       private void SendLiveTileUpdate()
       {
           XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText01);

           XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
           tileTextAttributes[0].InnerText = "TilePuzzle";   // this will grab the latest review text

           XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");
           ((XmlElement)tileImageAttributes[0]).SetAttribute("src", "ms-appdata:///local/" + tileImageFile);
           ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", " status");

           TileNotification tileNotification = new TileNotification(tileXml);
           tileNotification.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(1);
           TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
       }

       //Method Name: LoadBitmap
       //Parameters: IRandomAccessStream stream
       //Return: void
       //Description: load captured image to the image box in specific size
       private void LoadBitmap(IRandomAccessStream stream)
       {
           Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
               new Windows.UI.Xaml.Media.Imaging.BitmapImage();

           //convert size from actual image size to standardized size of our board of 400x400, which will makes tiles of 100x100
           bitmapImage.DecodePixelWidth = 400;
           bitmapImage.DecodePixelHeight = 400;

           bitmapImage.SetSource(stream);
           displayImage.Source = bitmapImage;

       }


       //Method Name: CheckAndClearShareOperation
       //Parameters: none
       //Return: void
       //Description: shared operation
       private void CheckAndClearShareOperation()
       
       {
           if (_shareOperation != null)
           {
               _shareOperation.ReportCompleted();
               _shareOperation = null;
           }
       }

private async void TileUpdate()
{

           await SaveElement(GameCanvas, tileImageFile);
           await SaveElement(displayImage, currentImageFile);
           //SendTileNotification();
}

       //Method Name: SaveButton_Click_1
       //Parameters: object sender, RoutedEventArgs e
       //Return: void
       //Description: save camera captured image to file
       public async void SaveButton_Click_1(object sender, RoutedEventArgs e)
       {
           if (_writeableBitmap != null)
           {
               var picker = new FileSavePicker
               {
                   SuggestedStartLocation = PickerLocationId.PicturesLibrary
               };
               picker.FileTypeChoices.Add("Image", new List<string>() { ".png" });
               picker.DefaultFileExtension = ".png";
               picker.SuggestedFileName = "photo";
               var savedFile = await picker.PickSaveFileAsync();

               try
               {

                   // file is null if user cancels the file picker.
                   if (savedFile != null)
                   {
                       // Open a stream for the selected file.
                       Windows.Storage.Streams.IRandomAccessStream fileStream =
                           await savedFile.OpenAsync(Windows.Storage.FileAccessMode.Read);

                       // Set the image source to the selected bitmap.
                       Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                           new Windows.UI.Xaml.Media.Imaging.BitmapImage();

                       //convert size from actual image size to standardized size of our board of 400x400, which will makes tiles of 100x100
                       bitmapImage.DecodePixelWidth = 400;
                       bitmapImage.DecodePixelHeight = 400;

                       //set displayImage.Source to use this now 400x400 image
                       await bitmapImage.SetSourceAsync(fileStream);
                       displayImage.Source = bitmapImage;
                       startButton.Visibility = Visibility.Visible;
                       this.DataContext = savedFile;

                       // Add picked file to MostRecentlyUsedList.
                       mruToken = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList.Add(savedFile);

                       havePicture = true;
                       
                   }
               }
               catch (Exception ex)
               {
                   var s = ex.ToString();
               }
               finally
               {
                   CheckAndClearShareOperation();
               }
           }
       }


       //Method Name: GetCameraButton_Click
       //Parameters: object sender, TappedRoutedEventArgs e
       //Return: void
       //Description: use camera captured image for the game, reset button status
       private async void GetCameraButton_Click(object sender, TappedRoutedEventArgs e)
       {
           CheckAndClearShareOperation();
           var camera = new CameraCaptureUI();
           var result = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
           if (result != null)
           {
               LoadBitmap(await result.OpenAsync(FileAccessMode.Read));
               startButton.Visibility = Visibility.Visible;

               havePicture = true;
               dispatcherTimer.Stop();
               gameOn = false;
               gamePaused = true;
               timesTicked = 0;
               startButton.Content = "Start";
               pauseButton.Content = "Pause";
               pauseButton.Visibility = Visibility.Collapsed;
           }



       }


       //Method Name: NumberPuzzle_Click
       //Parameters: object sender, TappedRoutedEventArgs e
       //Return: void
       //Description: switch to number puzzle
       private void NumberPuzzle_Click(object sender, TappedRoutedEventArgs e)
       {
           Image img = new Image();
           BitmapImage bitmapImage = new BitmapImage();
           bitmapImage.DecodePixelWidth = 400;
           bitmapImage.DecodePixelHeight = 400;
           Uri uri = new Uri("ms-appx:///Assets/NumberPuzzle1.jpg");
           bitmapImage.UriSource = uri;
           displayImage.Source = bitmapImage;

           havePicture = true;
           dispatcherTimer.Stop();
           gameOn = false;
           gamePaused = true;
           timesTicked = 0;
           startButton.Content = "Start";
           pauseButton.Content = "Pause";
           startButton.Visibility = Visibility.Visible;
           pauseButton.Visibility = Visibility.Collapsed;
       }

                       
    }
}
