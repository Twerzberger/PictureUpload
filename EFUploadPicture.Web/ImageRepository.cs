using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFUploadPicture.Web
{
    public class ImageRepository
    {
        private string _connectionString;

        public ImageRepository(string connectionStirng)
        {
            _connectionString = connectionStirng;
        }

        public IEnumerable<Image> GetImages()
        {
            using (var context = new ImageContext(_connectionString))
            {
                return context.Pictures.ToList();
            }
        }

        public void AddImage(Image image)
        {
            using (var context = new ImageContext(_connectionString))
            {
                context.Pictures.Add(image);
                context.SaveChanges();
            }
        }

        public Image GetImageById(int id)
        {
            using (var context = new ImageContext(_connectionString))
            {
                return context.Pictures.FirstOrDefault(p => p.Id == id);
            }
        }
    }
}
