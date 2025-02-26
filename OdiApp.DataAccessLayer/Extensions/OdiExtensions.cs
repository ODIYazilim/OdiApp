using OdiApp.DTOs.SharedDTOs;
using OdiApp.EntityLayer.Base;
using System.Reflection;

namespace OdiApp.DataAccessLayer.Extensions
{
    public static class OdiExtensions
    {
        /// <summary>
        /// Derin kopya işlemi için extension method.
        /// </summary>
        /// <typeparam name="T">Kopyalanacak nesnenin tipi.</typeparam>
        /// <param name="orijinal">Kopyalanacak orijinal nesne.</param>
        /// <returns>Derin kopya oluşturulmuş nesne.</returns>
        public static Task<T> DeepCopy<T>(this T orijinal) where T : class, new()
        {
            if (orijinal == null) return null;

            // Yeni bir nesne oluştur
            T kopya = new T();

            // Tüm propertileri al
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(orijinal);

                    // Koleksiyonları kontrol et
                    if (value is IList<object> list)
                    {
                        // Yeni bir liste oluştur ve her bir eleman için derin kopya yap
                        var copyList = (IList<object>)Activator.CreateInstance(property.PropertyType);
                        foreach (var item in list)
                        {
                            copyList.Add(item.DeepCopy());
                        }

                        property.SetValue(kopya, copyList);
                    }
                    else if (value is ICloneable cloneable)
                    {
                        // ICloneable arayüzünü destekleyen nesneler için kopya oluştur
                        property.SetValue(kopya, cloneable.Clone());
                    }
                    else
                    {
                        // Diğer durumlar için basit atama
                        property.SetValue(kopya, value);
                    }
                }
            }

            return Task.FromResult(kopya);
        }

        /// <summary>
        /// Listeler için derin kopya işlemi.
        /// </summary>
        /// <typeparam name="T">Kopyalanacak listenin öğe tipi.</typeparam>
        /// <param name="orijinal">Kopyalanacak orijinal liste.</param>
        /// <returns>Derin kopya oluşturulmuş liste.</returns>
        public static async Task<List<T>> DeepCopy<T>(this List<T> orijinal) where T : class, new()
        {
            if (orijinal == null) return null;

            // Yeni bir liste oluştur
            var kopyaListesi = new List<T>();

            // Her öğe için derin kopya yap ve yeni listeye ekle
            foreach (var item in orijinal)
            {
                kopyaListesi.Add(await item.DeepCopy());
            }

            return kopyaListesi;
        }

        public static void SetAuditFields(this BaseAuditModel model, OdiUser user, DateTime? tarih = null)
        {
            var currentDate = tarih ?? DateTime.Now;

            if (model.EklenmeTarihi == null)
            {
                model.EklenmeTarihi = currentDate;
                model.Ekleyen = user.AdSoyad;
                model.EkleyenId = user.Id;
            }

            model.GuncellenmeTarihi = currentDate;
            model.Guncelleyen = user.AdSoyad;
            model.GuncelleyenId = user.Id;
        }
    }
}