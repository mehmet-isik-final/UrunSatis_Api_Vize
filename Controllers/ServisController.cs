using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UrunSatis.Models;
using UrunSatis.View_Model;
namespace UrunSatis.Controllers
{
    public class ServisController : ApiController
    {
        DBURUNEntities db = new DBURUNEntities();
        SonucModel sonuc = new SonucModel();


        #region Ürün

        
        [HttpGet]
        [Route("api/urunlistele")]
        public List<UrunModel> UrunListele()
        {
            List<UrunModel> liste = db.Urun.Select(x => new UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunBilgi = x.urunBilgi,
                urunFiyat = x.urunFiyat,
                urunKat = x.urunKat,
                urunYorum = x.urunYorum,
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunModel UrunById(string urunId)
        {
            UrunModel kayit = db.Urun.Where(s => s.urunId == urunId).Select(x => new
            UrunModel()
            {
                urunId = x.urunId,
                urunAdi = x.urunAdi,
                urunBilgi = x.urunBilgi,
                urunFiyat = x.urunFiyat,
                urunKat = x.urunKat,
                urunYorum = x.urunYorum,
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunModel model)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == model.urunId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı !";
                return sonuc;
            }
            kayit.urunId = model.urunId;
            kayit.urunAdi = model.urunAdi;
            kayit.urunFiyat = model.urunFiyat;
            kayit.urunBilgi = model.urunBilgi;
            kayit.urunKat = model.urunKat;
            kayit.urunYorum = model.urunYorum;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Düzenlendi .";
            return sonuc;
            
        }



        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunModel model)
        {
            if (db.Urun.Count(s => s.urunAdi == model.urunAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Mevcut !";
                return sonuc;

            }

            Urun yeni = new Urun();
            yeni.urunId = Guid.NewGuid().ToString();
            yeni.urunAdi = model.urunAdi;
            yeni.urunBilgi = model.urunBilgi;
            yeni.urunFiyat = model.urunFiyat;
            yeni.urunKat = model.urunKat;
            yeni.urunYorum = model.urunYorum;
            db.Urun.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Urun Eklendi";
            return sonuc;

        }
        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SonucModel UrunSil(string urunId)
        {
            Urun kayit = db.Urun.Where(s => s.urunId == urunId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı !";
                return sonuc;
                
            }
            if (db.Kayit.Count(s => s.kayitUrunId == urunId) > 0)
                {
                    sonuc.islem = false;
                    sonuc.mesaj = "Ürün Kategoriye Kayıtlıdır Silinemez ! ";
                    return sonuc;

                }
            db.Urun.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi !";
            return sonuc;
        }
        #endregion

        #region Kategori

        [HttpGet]
        [Route("api/kategorilistele")]
        public List<KategoriModel> KategoriListele()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi, 
            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public KategoriModel KategoriById(string katId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.katId == katId).Select(x => new
            KategoriModel()
            {
                katId = x.katId,
                katAdi = x.katAdi,
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == model.katId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı !";
                return sonuc;
            }
            kayit.katId = model.katId;
            kayit.katAdi = model.katAdi;        
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi .";
            return sonuc;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.katAdi == model.katAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Mevcut !";
                return sonuc;

            }

            Kategori yeni = new Kategori();
            yeni.katId = Guid.NewGuid().ToString();
            yeni.katAdi = model.katAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;

        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(string katId)
        {
            Kategori kayit = db.Kategori.Where(s => s.katId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı !";
                return sonuc;
            }
            if (db.Kayit.Count(s => s.kayitKategoriId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategoride Ürün Kayıtlıdır Silinemez ! ";
                return sonuc;

            }

            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi !";
            return sonuc;
        }
        #endregion

        #region Kullanici
        [HttpGet]
        [Route("api/kullanicilistele")]
        public List<KullaniciModel> KullaniciListele()
        {
            List<KullaniciModel> liste = db.Kullanici.Select(x => new KullaniciModel()
            {
                userId = x.userId,
                userAdi= x.userAdi,
                userMail = x.userMail,
                userSifre = x.userSifre,
                userAdmin = x.userAdmin

            }).ToList();
            return liste;
        }

        [HttpGet]
        [Route("api/kullanicibyid/{userId}")]
        public KullaniciModel KullaniciById(string userId)
        {
            KullaniciModel kayit = db.Kullanici.Where(s => s.userId == userId).Select(x => new
            KullaniciModel()
            {
                userId = x.userId,
                userAdi = x.userAdi,
                userMail = x.userMail,
                userSifre = x.userSifre,
                userAdmin = x.userAdmin,
            }).FirstOrDefault();
            return kayit;
        }

        [HttpPut]
        [Route("api/kullaniciduzenle")]
        public SonucModel KullaniciDuzenle(KullaniciModel model)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.userId == model.userId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Bulunamadı !";
                return sonuc;
            }
            kayit.userId = model.userId;
            kayit.userAdi= model.userAdi;
            kayit.userMail = model.userMail;
            kayit.userSifre = model.userSifre;
            kayit.userAdmin = model.userAdmin;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Düzenlendi .";
            return sonuc;
        }

        [HttpPost]
        [Route("api/kullaniciekle")]
        public SonucModel KullaniciEkle(KullaniciModel model)
        {
            if (db.Kullanici.Count(s => s.userId == model.userId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanıcı Mevcut !";
                return sonuc;

            }

            Kullanici yeni = new Kullanici();
            yeni.userId = Guid.NewGuid().ToString();
            yeni.userAdi = model.userAdi;
            yeni.userMail = model.userMail;
            yeni.userSifre = model.userSifre;
            yeni.userAdmin = model.userAdmin;
            db.Kullanici.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Eklendi";
            return sonuc;

        }

        [HttpDelete]
        [Route("api/kullanicisil/{userId}")]
        public SonucModel KullaniciSil(string userId)
        {
            Kullanici kayit = db.Kullanici.Where(s => s.userId == userId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kullanici Bulunamadı !";
                return sonuc;
            }
            db.Kullanici.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kullanıcı Silindi !";
            return sonuc;
        }


        #endregion

        #region Kayit
        [HttpGet]
        [Route("api/urunkategoriliste/{urunId}")]
        public List<KayitModel> UrunKategoriListe(string urunId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitUrunId == urunId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitUrunId = x.kayitUrunId,
                kayitKategoriId = x.kayitKategoriId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.urunBilgi = UrunById(kayit.kayitUrunId);
                kayit.kategoriBilgi = KategoriById(kayit.kayitKategoriId);
            }
            return liste;
        }


        [HttpGet]
        [Route("api/kategoriurunliste/{katId}")]
        public List<KayitModel> KategoriArabaListe(string katId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKategoriId == katId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitUrunId = x.kayitUrunId,
                kayitKategoriId = x.kayitKategoriId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.urunBilgi = UrunById(kayit.kayitUrunId);
                kayit.kategoriBilgi = KategoriById(kayit.kayitKategoriId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(s => s.kayitUrunId == model.kayitUrunId && s.kayitKategoriId == model.kayitKategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Zaten Kaydedilmiş !";

            }

            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitUrunId = model.kayitUrunId;
            yeni.kayitKategoriId = model.kayitKategoriId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün kaydı Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı !";
                return sonuc;

            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Silindi !";
            return sonuc;
        }
        #endregion
    }
}
