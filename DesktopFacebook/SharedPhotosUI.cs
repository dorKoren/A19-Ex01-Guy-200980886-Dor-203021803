using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;

namespace DesktopFacebook
{
    internal class SharedPhotosUI : IFetch
    {
        #region Class Members
        private readonly string      r_PhotosNotFound = "Photos Not Found!";
        private const string         k_Filter = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
        internal List<LazyPictureBox> SharedLazyPictureBox      { get;      }
        internal SharedPhotosLogic    SharedPhotosLogic { get; set; }
        #endregion Class Members

        #region Class members


        #region constructor
        internal SharedPhotosUI()
        {
            SharedPhotosLogic    = new SharedPhotosLogic();
            SharedLazyPictureBox = new List<LazyPictureBox>();
        }
        #endregion constructor


        internal void LoadSharedPhotosToFlowLayoutPanel(FlowLayoutPanel i_Panel, Button i_Button)
        {
            if (SharedPhotosLogic.SharedPhotosList.Count <= 0)
            {
                string message = string.Format(@"No photos of You and {0} were found",
                    SharedPhotosLogic.Friend.Name);

                MessageBox.Show(message, r_PhotosNotFound,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Photo> sharedPhotos = SharedPhotosLogic.SharedPhotosList;          

                convertPhotosToLazyPicBox(sharedPhotos);

                i_Button.Enabled = true;

                foreach (LazyPictureBox pic in SharedLazyPictureBox)
                {
                    i_Panel.Controls.Add(pic);
                }
            }
        }

        private void convertPhotosToLazyPicBox(List<Photo> i_Photos)
        {
            foreach (Photo photo in i_Photos)
            {
                LazyPictureBox lazyPicBox = new LazyPictureBox();

                lazyPicBox.Click += lazyPicBox_Click;
                lazyPicBox.Load(photo.PictureNormalURL);
                SharedLazyPictureBox.Add(lazyPicBox);
            }
        }

        private void lazyPicBox_Click(object sender, EventArgs e)
        {
            LazyPictureBox pic = sender as LazyPictureBox;

            if (pic != null)
            {
                // Select current image
                if (!pic.WasSelected)
                {
                    selectPic(pic);
                }
                // Deselect current image
                else
                {
                    deSelectPic(pic);
                }
            }
        }


        private void deSelectPic(LazyPictureBox i_Picture)
        {
            i_Picture.BackColor = Color.Red;
            i_Picture.WasSelected = false;
            SharedPhotosLogic.TotalSelectedSharedPictures--;
        }

        private void selectPic(LazyPictureBox i_Picture)
        {
            i_Picture.BackColor = Color.Transparent;
            i_Picture.WasSelected = true;
            SharedPhotosLogic.TotalSelectedSharedPictures++;
        }


        internal static void DownLoadPhotos(List<LazyPictureBox> i_SharedPicBox)   
        {
            foreach (LazyPictureBox lazyPicBox in i_SharedPicBox)
            {
                if (lazyPicBox.WasSelected)
                {
                    downLoadPhoto(lazyPicBox);
                }
            }
        }


        #endregion internal Static Methods

        #region  private static methods   
        private static void downLoadPhoto(LazyPictureBox i_LazyPicBox)
        {

            SaveFileDialog file = new SaveFileDialog
            {
                Filter = k_Filter,
                FileName = "shared Photos",
                AddExtension = true
            };

            using (FileStream stream = (FileStream)file.OpenFile())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    switch (Path.GetExtension(file.FileName).ToUpper())
                    {
                        case ".BMP":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;

                        case ".JPG":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;

                        case ".PNG":
                            i_LazyPicBox.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                }
            }
        }

        public void FetchInit(Form i_Form)
        {
            foreach (Control control in i_Form.Controls)
            {
                control.Visible = true;
            }

        }

        public void FetchReset(Form i_Form )
        {

            foreach (Control control in i_Form.Controls)
            {

                if (control is PictureBox)
                {
                    (control as PictureBox).Image = Properties.Resources.initial_image_picture;
                }

                control.Visible = false;
            }
        }

        #endregion  Private Static Methods
    }
}
