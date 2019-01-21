using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FeaturesLogic;

namespace DesktopFacebook
{
    public class SharedPhotosUI : IFetch
    {
        private  readonly string         r_PhotosNotFound     = "Photos Not Found!";
        private  const    string         k_Filter             = "Bmp(*.BMP;)|*.BMP;| Jpg(*Jpg)|*.jpg";
        internal SharedPhotosLogic       SharedPhotosLogic      { get; }
        internal PhotosIterator      m_PhotosIterator     = null; 
       
        internal SharedPhotosUI()
        {
            SharedPhotosLogic    = new SharedPhotosLogic();
        }


        internal List<LazyPictureBox> ConvertPhotosToLazyPictureBoxes()
        {
            List<LazyPictureBox> list = new List<LazyPictureBox>();
            IEnumerator<Photo> itr = SharedPhotosLogic.GetPhotosIterator();
            m_PhotosIterator = itr as PhotosIterator;

            while (m_PhotosIterator.MoveNext())
            {
                LazyPictureBox pic = LazyPictureBox.ConvertPhotoToLazyPicBox(m_PhotosIterator.Current);
                list.Add(pic);
            }

            return list;
        }

        internal void LoadSharedPhotosToFlowLayoutPanel(FlowLayoutPanel i_Panel, List<LazyPictureBox> i_Pictures)
        {
            PhotosIterator itr = m_PhotosIterator as FeaturesLogic.PhotosIterator;

            if (m_PhotosIterator != null && itr.isEmpty())
            {
                string message = string.Format(@"No photos of You and {0} were found",
                    SharedPhotosLogic.Friend.Name);

                MessageBox.Show(message, r_PhotosNotFound,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {             
                foreach (LazyPictureBox pic in i_Pictures)
                {
                    i_Panel.Controls.Add(pic);
                }
            }
        }
        
        public void FetchInit(TabPage i_TabPageSharedPhotos)
        {
            foreach (Control control in i_TabPageSharedPhotos.Controls)
            {
                control.Visible = true;
            }

        }

        public void FetchReset(TabPage i_TabPageSharedPhotos)
        {

            foreach (Control control in i_TabPageSharedPhotos.Controls)
            {

                if (control is PictureBox)
                {
                    (control as PictureBox).Image = Properties.Resources.initial_image_picture;
                }

                control.Visible = false;
            }
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
    }
}
